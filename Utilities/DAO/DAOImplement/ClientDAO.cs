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
                    clientToFind = new Client(reader.GetInt32("ID_client"),reader.GetBoolean("is_company"), reader.GetString("name_client"),reader.GetString("f_name_client"),reader.GetString("Email_client"),reader.GetString("Adresse_client"));
                }
                else
                {
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




        //test ok 
        public override ObservableCollection<Client> findAll()
        {
            ObservableCollection < Client > clients = new ObservableCollection<Client>();

            try
            {

                string sql = string.Format("SELECT ID_client , is_company, name_client , f_name_client , Email_client , Adresse_client FROM {0}  ;","client");

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
                    string sql = string.Format("SELECT name_Client , f_name_client FROM {0} WHERE name_Client = '{1}' AND f_name_client = '{2}' ; ", "client", client.Name, client.F_name);

                    //Execuction of my sql query 
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        MessageBox.Show("Le client " + client.Name + " " + client.F_name + " existe deja dans votre DB !", "infos");
                        flag = false;
                        reader.Close();
                    }
                    else
                    {
                        reader.Close();
                        sql = string.Format("INSERT INTO client (is_Company , name_client , f_name_client , Email_client , Adresse_client) VALUES ({0} , '{1}' , '{2}' , '{3}', '{4}') ; ", client.IsCompany, client.Name, client.F_name, client.Email, client.Adresse);

                        //Execuction of my sql query 
                        MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                        MySqlDataReader reader2 = cmd2.ExecuteReader();

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
                try
                {
                    string sql = string.Format("DELETE FROM list_of_art WHERE id_facture IN (SELECT id_f FROM facture WHERE Client_num = {0} ; ); FELETE FROM facture WHERE Client_num = {0} ; DELETE FROM client WHERE ID_client = {0} ;", client.Id);

                    //Execuction of my sql query 
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Problem de suppression  de  " + client.Name + " " + client.F_name, "infos");
                    flag = false;
                }//end try catch 

            }else
            {
                flag = false;
                MessageBox.Show("le client est null ! ", "infos");
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
