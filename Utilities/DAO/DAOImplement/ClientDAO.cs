using MrBricolage.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MrBricolage.Utilities.DAO.DAOImplement
{
    public  class ClientDAO : DAO<Client>
    {

        public ClientDAO(MySqlConnection conn) : base(conn)
        {

        }


        //test ok 
        public override Client find(int idClient )
        {
            Client clientToFind= null;

            try
            {

                string sql = string.Format("SELECT ID_client , is_company, name_client , f_name_client , Email_client , Adresse_client FROM {0} WHERE ID_client = {1} ;", "client", idClient);


                //Execuction of my sql query 
                MySqlCommand cmd = new MySqlCommand(sql, conn);
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

                string sql = string.Format("SELECT ID_client , is_company, name_client , f_name_client , Email_client , Adresse_client FROM {0} WHERE is_active ;","client");

                //Execuction of my sql query 
                MySqlCommand cmd = new MySqlCommand(sql, conn);
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



        //no test 
        public override bool create(Client client)
        {
            bool flag  = true;


            if (client != null)
            {

                try
                {
                    string sql = string.Format("SELECT name_Client , f_name_client , is_active FROM {0} WHERE name_Client = '{1}' AND f_name_client = '{2}' ; ", "client", client.Name, client.F_name);

                    //Execuction of my sql query 
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        //if the client existe with the same name and same f_name so i check, if he is active or not 
                        if (reader.GetBoolean("is_active"))
                        {
                            MessageBox.Show("Le client " + client.Name + " "+ client.F_name +  " existe deja dans votre DB","infos");
                            flag= false;
                        }
                        else
                        {
                            //in this case, the client with the same name and same f_name exist in our db, he is just inactive, i will propose to my user if he wan to activate it or no
                            if (MessageBox.Show("voulez vous activé le client " + client.Name + " "+ client.F_name + " ?", "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                flag = false;
                                reader.Close();
                                sql = string.Format("UPDATE client SET is_active = '{0}' WHERE  name_client = '{1}' and f_name_client = '{2}' ; ", "1", client.Name , client.F_name);
                                //Execuction of my sql query 
                                MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                                MySqlDataReader reader2 = cmd2.ExecuteReader();


                                MessageBox.Show("le client " + client.Name + " " + client.F_name + " est active maintenant ! ", "infos");
                                reader2.Close();
                            }
                            else
                            {
                                // the user don't want update this client, in this case flag is false and i do nothing 
                                flag = false;

                            }//end if 

                        }//end if 
                    }
                    else
                    {
                        reader.Close();
                        sql = string.Format("INSERT INTO client (is_Company , name_client , f_name_client , Email_client , Adresse_client) VALUES ({0} , '{1}' , '{2}' , '{3}', '{4}','{5}') ; ", client.IsCompany, client.Name, client.F_name, client.Email, client.Adresse,"1");

                        //Execuction of my sql query 
                        MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                        MySqlDataReader reader2 = cmd2.ExecuteReader();


                        reader2.Close();

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


        //no test 
        public override bool delete(Client client)
        {
            bool flag = true;

            if (client != null)
            {
                string sql = string.Format("SELECT id_client FROM facture WHERE id_client = {0} ;", client.Id);

                //Execuction of my sql query
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //if the client existe in facture
                    if (MessageBox.Show("vous ne pouvez pas supprimer le client num " + client.Id + " car il existe dans des factures, voulez vous le randre inactive ?", "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        reader.Close();
                        sql = string.Format("UPDATE client SET is_active = '{0}' WHERE  ID_client = {1} ; ", "0", client.Id);

                        //Execuction of my sql query
                        MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                        MySqlDataReader reader2 = cmd2.ExecuteReader();
                        reader2.Close();
                        MessageBox.Show("Le client num " + client.Id + " n'est pas supprimé de votre DB, mais il est inactive, donc vous ne pouvez plus  l'insere dans une facture", "infos");
                    }
                    else
                    {
                        // the user don't want update this article, in this case flag is false and i do nothing 
                        flag = false;

                    }//end if 

                }
                else // here our client don't existe in my factures 
                {
                    reader.Close();
                    sql = string.Format("DELETE FROM client WHERE id_client = {0} ; ", client.Id);

                    //Execuction of my sql query
                    MySqlCommand cmd3 = new MySqlCommand(sql, conn);
                    MySqlDataReader reader3 = cmd3.ExecuteReader();
                    reader3.Close();
                }//end if 
            }
            else
            {
                flag = false;
                MessageBox.Show("Le client est null !", "infos");
            }//end if 


            return flag;

        }//end delete 

        //no test 
        public override  bool update(Client client)
        {
            bool flag = true;
            if (client != null)
            {
                try
                {
                    string sql = string.Format("UPDATE client SET is_Company = {0} , name_client = '{1}' , f_name_client = '{2}' , Email_client = '{3}', Adresse_client = '{4}' WHERE ID_client = {4} ", client.IsCompany, client.Name, client.F_name, client.Email, client.Adresse, client.Id);

                    //Execuction of my sql query 
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

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
