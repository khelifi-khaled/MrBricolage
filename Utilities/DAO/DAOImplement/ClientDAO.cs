using MrBricolage.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public override Client find(int idClient)
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
                }




                reader.Close();



            }
            catch(Exception ex)
            {
                MessageBox.Show("problem de récupération de client num" + idClient , "infos");

                Console.WriteLine(ex.Message);
            
            }//end trycatch 
            
            
            
            return clientToFind;

        }//end Find





        public override ObservableCollection<Client> findAll()
        {
            ObservableCollection < Client > clients = new ObservableCollection<Client>();
            try
            {

                string sql = string.Format("SELECT ID_client , is_company, name_client , f_name_client , Email_client , Adresse_client FROM {0}  ;","client");

                //Execuction of my sql query 
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();



            }
            catch(Exception ex) { 

                Console.WriteLine (ex.Message);
                MessageBox.Show("problem de récupération de client num", "infos");

            }//end trycatch 

            return null;
        }




        public override bool create(Client client)
        {
            return false;
        }



        public override bool delete(Client client)
        {
            return false;
        }


        public override  bool update(Client client)
        {
            return false;
        }




    }//end class 
}//end namespace 
