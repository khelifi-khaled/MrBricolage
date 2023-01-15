using MrBricolage.Models;
using MySql.Data.MySqlClient;
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

                //Execution of my sql query 
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //if my cursor can move to the next row, so the reader.Read() will sent true, and my obj exist on my DB 
                    clientToFind = new Client(reader.GetInt32("ID_client"),reader.GetBoolean("is_company"), reader.GetString("name_client"),reader.GetString("f_name_client"),reader.GetString("Email_client"),reader.GetString("Adresse_client"));
                }
                else
                {
                    //else there's no rows in the reader, so the client don't exist on my DB 
                    MessageBox.Show("le client num " + idClient +  "   n'existe pas dans votre DB !");

                }//end if 

                reader.Close();
            }
            catch
            {
                MessageBox.Show("problème de récupération de client num" + idClient , "infos");


            }//end trycatch 
            
            
            
            return clientToFind;

        }//end Find
















        /// <summary>
        /// find all Active Clients existed in our DB 
        /// </summary>
        /// <returns>Observable Collection of type Client contains all Clients</returns>
        public override ObservableCollection<Client> findAll()
        {
            ObservableCollection < Client > clients = new ObservableCollection<Client>();

            try
            {

                string sql = "SELECT ID_client , is_company, name_client , f_name_client , Email_client , Adresse_client FROM client WHERE is_active  ;";

                
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                

                //Execution of my sql query 
                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Client c = new Client(reader.GetInt32("ID_client"), reader.GetBoolean("is_company"), reader.GetString("name_client"), reader.GetString("f_name_client"), reader.GetString("Email_client"), reader.GetString("Adresse_client"));
                    clients.Add(c);

                }//end while loop 

                reader.Close();
            }
            catch
            { 
                MessageBox.Show("problème de récupération des clients  ", "infos");

            }//end trycatch 

            return clients;
        }














        /// <summary>
        /// insert the client in our DB 
        /// </summary>
        /// <param name="client"> the client tha we want to insert </param>
        /// <returns>true if the client has been inserted, false if not</returns>
        public override bool create(Client client)
        {
            bool flag  = true;
            try
            {
                string sql = "INSERT INTO client (is_Company , name_client , f_name_client , Email_client , Adresse_client , is_active) VALUES (@company, @name , @f_name , @email , @adresse , @bool) ; ";


                MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                cmd2.Parameters.AddWithValue("@company", client.IsCompany);
                cmd2.Parameters.AddWithValue("@name", client.Name);
                cmd2.Parameters.AddWithValue("@f_name", client.F_name);
                cmd2.Parameters.AddWithValue("@email", client.Email);
                cmd2.Parameters.AddWithValue("@adresse", client.Adresse);
                cmd2.Parameters.AddWithValue("@bool", true);

                //Execution of my sql query 
                cmd2.ExecuteNonQuery();
            }
            catch 
            {
                flag = false;
                MessageBox.Show("problème de  create " + client.Name + " " + client.F_name, "infos");

            }//end trycatch 

            return flag;

        }// end create
















        /// <summary>
        /// update client status in our DB
        /// </summary>
        /// <param name="client"> the client that we want to update</param>
        /// <param name="status">the status that we want to insert in DB</param>
        /// <returns>true if the status has been updated, false if not </returns>        
        public bool UpdateClientStatus(Client client,bool status)
        {
            bool flag = true;
            try
            {
                string sql = "UPDATE client SET is_active = @bool WHERE  name_client = @name and f_name_client = @f_name ;";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@bool", status);
                cmd.Parameters.AddWithValue("@name", client.Name);
                cmd.Parameters.AddWithValue("@f_name", client.F_name);

                //Execution of my sql query
                cmd.ExecuteNonQuery();
            }
            catch
            {
                flag = false;
                MessageBox.Show("problème de  UpdateClientStatusToActive " + client.Name + " " + client.F_name, "infos");
            }

            return flag;

        }//end UpdateClientStatusToActive


       

















        /// <summary>
        /// Check the client status in our DB
        /// </summary>
        /// <param name="client"> the client that we want to check </param>
        /// <returns>Client status from our DB</returns>
        public  bool CheckClientStatus(Client client)
        {
            bool flag = false;

            try
            {
                string sql = "SELECT  is_active FROM client WHERE  name_client = @name and f_name_client = @f_name ; ";


                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@name", client.Name);
                cmd.Parameters.AddWithValue("@f_name", client.F_name);

                //Execution of my sql query 
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    flag = reader.GetBoolean("is_active");

                }//end if                

                reader.Close();

            }
            catch 
            {
                
                MessageBox.Show("problème de  CheckClientStatus " + client.Name + " " + client.F_name, "infos");
                flag = false;
            }//end trycatch 

            return flag;

        }//end CheckClientStatus



















        /// <summary>
        /// Check if the Client exist in our DB with the same name and same f_name 
        /// </summary>
        /// <param name="client">the client the we want to check </param>
        /// <returns>true if the method find any existed client in our DB, false if not </returns>        
        public bool CheckExistedClientFullName(Client client)
        {
            bool flag;

            try
            {
                string sql = "SELECT name_Client , f_name_client  FROM client WHERE name_Client = @name AND f_name_client = @f_name ; ";


                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", client.Name);
                cmd.Parameters.AddWithValue("@f_name", client.F_name);

                //Execution of my sql query 
                MySqlDataReader reader = cmd.ExecuteReader();

                flag = (reader.Read());

                reader.Close();
            }
            catch 
            {
                MessageBox.Show("problème de  CheckExistedClientFullName " + client.Name + " " + client.F_name, "infos");
                flag = false;
            }//end trycatch 


            return flag;

        }//end checkExistedClient





















        /// <summary>
        /// delete client from our DB
        /// </summary>
        /// <param name="client">the client that we want to delete</param>
        /// <returns>true if the client has been deleted, false if not</returns>        
        public override bool delete(Client client)
        {
            bool flag = true;

            try
            {
                string sql = "DELETE FROM client WHERE id_client = @id ; ";


                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", client.Id);


                //Execution of my sql query
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("problème sur delete ! ");
                flag = false;
            }

            return flag;

        }//end delete 

















        /// <summary>
        /// check if the client exist in facture 
        /// </summary>
        /// <param name="client">the client that we want to check</param>
        /// <returns>true if the client exist in facture , false </returns>
        public bool CheckExistedClientInFacture(Client client)
        {
            bool flag;
            try
            {
                string sql = "SELECT client_num FROM facture WHERE client_num = @id ;";


                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", client.Id);

                //Execution of my sql query
                MySqlDataReader reader = cmd.ExecuteReader();

                flag = reader.Read();

                reader.Close();
            }
            catch
            {
                MessageBox.Show("problème sur CheckExistedClientInFacture !", "infos");
                flag = false;
            }




            return flag;


        }//end CheckExistedClientInFacture

















        /// <summary>
        /// update client infos in our DB
        /// </summary>
        /// <param name="client">the client that we want to update</param>
        /// <returns>true if the client has been updated, false if not</returns>

        public override  bool update(Client client)
        {
            bool flag = true;

            try
            {
                string sql = "UPDATE client SET is_Company = @company , name_client = @name , f_name_client = @f_name , Email_client = @email , Adresse_client = @adresse WHERE ID_client = @id ";


                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@company", client.IsCompany);
                cmd.Parameters.AddWithValue("@name", client.Name);
                cmd.Parameters.AddWithValue("@email", client.Email);
                cmd.Parameters.AddWithValue("@adresse", client.Adresse);
                cmd.Parameters.AddWithValue("@id", client.Id);
                cmd.Parameters.AddWithValue("@f_name", client.F_name);

                //Execution of my sql query 
                cmd.ExecuteNonQuery();

            }
            catch
            {
                MessageBox.Show("problème de mis à jour  de  " + client.Name + " " + client.F_name, "infos");
                flag = false;
            }//end trycatch  

            return flag;

        }//end update 




    }//end class 
}//end namespace 
