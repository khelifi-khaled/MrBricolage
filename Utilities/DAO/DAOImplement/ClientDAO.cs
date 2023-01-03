using MrBricolage.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace MrBricolage.Utilities.DAO.DAOImplement
{
    public  class ClientDAO : DAO<Client>
    {

        public ClientDAO(MySqlConnection conn) : base(conn)
        {

        }


        // test  ok 
        public override Client find(int idClient )
        {
            Client clientToFind= null;

            try
            {

                string sql ="SELECT ID_client , is_company, name_client , f_name_client , Email_client , Adresse_client FROM client WHERE ID_client = @id ;";


                
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", idClient);

                //Execuction of my sql query 
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //if my cursor can move to the next row, so the reader.Read() will sent true, and my obj exist on my DB 
                    clientToFind = new Client(reader.GetInt32("ID_client"),reader.GetBoolean("is_company"), reader.GetString("name_client"),reader.GetString("f_name_client"),reader.GetString("Email_client"),reader.GetString("Adresse_client"));
                }
                else
                {
                    //else there's no rows in the reader, so the client don't exist on my DB 
                    MessageBox.Show("le client num " + idClient + "n'existe pas dans votre DB !");

                }//end if 




                reader.Close();



            }
            catch(Exception ex)
            {
                MessageBox.Show("problem de récupération de client num" + idClient , "infos");

                Console.WriteLine(ex.Message);
            
            }//end trycatch 
            
            
            
            return clientToFind;

        }//end Find




        //no test 
        public override ObservableCollection<Client> findAll()
        {
            ObservableCollection < Client > clients = new ObservableCollection<Client>();

            try
            {

                string sql = "SELECT ID_client , is_company, name_client , f_name_client , Email_client , Adresse_client FROM client WHERE is_active  ;";

                
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                

                //Execuction of my sql query 
                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Client c = new Client(reader.GetInt32("ID_client"), reader.GetBoolean("is_company"), reader.GetString("name_client"), reader.GetString("f_name_client"), reader.GetString("Email_client"), reader.GetString("Adresse_client"));
                    clients.Add(c);

                }//end while loop 

                reader.Close();
            }
            catch(Exception ex) { 

                Console.WriteLine (ex.Message);
                MessageBox.Show("problem de récupération de client num", "infos");

            }//end trycatch 

            return clients;
        }



        // test ok 
        public override bool create(Client client)
        {
            bool flag  = true;


            if (client != null)
            {

                try
                {
                    string sql = "SELECT name_Client , f_name_client , is_active FROM client WHERE name_Client = @name AND f_name_client = @f_name ; ";

                   
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name",client.Name);
                    cmd.Parameters.AddWithValue("@f_name", client.F_name);

                    //Execuction of my sql query 
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        //if the client existe with the same name and same f_name so i check, if he is active or not 
                        if (reader.GetBoolean("is_active"))
                        {
                            MessageBox.Show("Le client " + client.Name + " "+ client.F_name +  " existe deja dans votre DB","infos");
                            flag= false;
                            reader.Close();
                        }
                        else
                        {
                            //in this case, the client with the same name and same f_name exist in our db, he is just inactive, i will propose to my user if he wan to activate it or no
                            if (MessageBox.Show("voulez vous activé le client " + client.Name + " "+ client.F_name + " ?", "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                flag = false;
                                reader.Close();
                                sql = "UPDATE client SET is_active = @bool WHERE  name_client = @name and f_name_client = @f_name ;";
                                 
                                MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                                cmd2.Parameters.AddWithValue("@bool", true);
                                cmd2.Parameters.AddWithValue("@name", client.Name);
                                cmd2.Parameters.AddWithValue("@f_name", client.F_name);

                                //Execuction of my sql query
                                cmd2.ExecuteNonQuery();


                                MessageBox.Show("le client " + client.Name + " " + client.F_name + " est active maintenant ! ", "infos");
                                
                            }
                            else
                            {
                                // the user don't want update this client, in this case flag is false and i do nothing 
                                flag = false;
                                reader.Close();

                            }//end if 

                        }//end if 
                    }
                    else
                    {
                        reader.Close();
                        sql = "INSERT INTO client (is_Company , name_client , f_name_client , Email_client , Adresse_client , is_active) VALUES (@company, @name , @f_name , @email , @adresse , @bool) ; ";

                        
                        MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                        cmd2.Parameters.AddWithValue("@company",client.IsCompany);
                        cmd2.Parameters.AddWithValue("@name", client.Name);
                        cmd2.Parameters.AddWithValue("@f_name", client.F_name);
                        cmd2.Parameters.AddWithValue("@email", client.Email);
                        cmd2.Parameters.AddWithValue("@adresse", client.Adresse);
                        cmd2.Parameters.AddWithValue("@bool",true);

                        //Execuction of my sql query 
                        cmd2.ExecuteNonQuery();

                    }//end if 


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Problem d'insertion de  " + client.Name + " " + client.F_name, "infos");
                    flag = false;
                }//end trycatch 
            }else
            {
                flag = false;
                MessageBox.Show("Le client est null !", "infos");

            }

            return flag;

        }// end create


        // test ok 
        public override bool delete(Client client)
        {
            bool flag = true;

            if (client != null)
            {
                string sql = "SELECT client_num , is_active FROM facture inner join client on client_num = ID_client WHERE client_num = @id ;";

                
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", client.Id);

                //Execuction of my sql query
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())//if the client existe in facture
                {
                    

                    if (reader.GetBoolean("is_active"))//in this block i will check if the client is active or not 
                    {
                        
                        if (MessageBox.Show("vous ne pouvez pas supprimer le client num " + client.Id + " car il existe dans des factures, voulez vous le randre inactive ?", "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            reader.Close();
                            sql = "UPDATE client SET is_active = @bool WHERE  ID_client = @id ; ";


                            MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                            cmd2.Parameters.AddWithValue("@bool", false);
                            cmd2.Parameters.AddWithValue("@id", client.Id);

                            //Execuction of my sql query
                            cmd2.ExecuteNonQuery();

                            MessageBox.Show("Le client num " + client.Id + " n'est pas supprimé de votre DB, mais il est inactive, donc vous ne pouvez plus  l'insere dans une facture", "infos");
                        }
                        else
                        {
                            // the user don't want update this client, in this case flag is false and i do nothing 
                            flag = false;
                            reader.Close();
                        }//end if 

                    }else // Here the client existe in  facture, and he is inactive 
                    {
                        flag = false;
                        MessageBox.Show("Le client num " + client.Id + " n'est pas supprimé de votre DB, et il est inactive.", "infos");
                        reader.Close();

                    }//end if 
                   

                }
                else // here our client don't existe in my factures 
                {
                    reader.Close();
                    sql = "DELETE FROM client WHERE id_client = @id ; ";

                    
                    MySqlCommand cmd3 = new MySqlCommand(sql, conn);
                    cmd3.Parameters.AddWithValue("@id", client.Id);


                    //Execuction of my sql query
                    cmd3.ExecuteNonQuery();
                }//end if 
            }
            else
            {
                flag = false;
                MessageBox.Show("Le client est null !", "infos");
            }//end if 


            return flag;

        }//end delete 






        //test ok 

        public override  bool update(Client client)
        {
            bool flag = true;
            if (client != null)
            {
                try
                {
                    string sql = "UPDATE client SET is_Company = @company , name_client = @name , f_name_client = @f_name , Email_client = @email , Adresse_client = @adresse WHERE ID_client = @id ";

                    
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@company",client.IsCompany);
                    cmd.Parameters.AddWithValue("@name", client.Name);
                    cmd.Parameters.AddWithValue("@email", client.Email);
                    cmd.Parameters.AddWithValue("@adresse", client.Adresse);
                    cmd.Parameters.AddWithValue("@id", client.Id);


                    if (client.IsCompany)
                    {
                        cmd.Parameters.AddWithValue("@f_name", "     ");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@f_name", client.F_name);
                    }
                    //Execuction of my sql query 
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Problem de mis à jour  de  " + client.Name + " " + client.F_name, "infos");
                    flag = false;
                }//end trycatch 
            }
            else
            {
                flag = false;
                MessageBox.Show("le client est null ! ", "infos");
            }//end if 



            return flag;

        }//end update 




    }//end class 
}//end namespace 
