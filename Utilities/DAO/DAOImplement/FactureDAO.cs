using MrBricolage.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;

namespace MrBricolage.Utilities.DAO.DAOImplement
{
    public  class FactureDAO : DAO<Facture>
    {

        public FactureDAO(MySqlConnection conn) : base(conn)
        {

        }



        /// <summary>
        /// insert the invoice  in our DB
        /// </summary>
        /// <param name="factureToCreate"> invoice the we want to insert </param>
        /// <returns>true if the invoice has been inserted, false if not </returns>
        public override bool create(Facture factureToCreate)
        {
            bool flag = true;
            MySqlTransaction mySqlTransaction = conn.BeginTransaction();
            try
            {

               

                string sql = "INSERT INTO facture (date_f , Client_num , emp_num , totalPrice) VALUES (@date,@idClient,@idEmp,@totelPrice)";

                MySqlCommand cmd = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd.Parameters.AddWithValue("@date", factureToCreate.Date);
                cmd.Parameters.AddWithValue("@idClient", factureToCreate.Client.Id);
                cmd.Parameters.AddWithValue("@idEmp", factureToCreate.Employee.Id);
                cmd.Parameters.AddWithValue("@totelPrice", factureToCreate.TotalAmount);


                //Execuction of my sql query 
                cmd.ExecuteNonQuery();

                

                foreach (Article art in factureToCreate.Articles)
                {
                    sql = "INSERT INTO list_of_art (id_art , id_Facture , quantity ) VALUES (@id_art , @id_facture , @quantity) ;";
                    MySqlCommand cmd2 = new MySqlCommand(sql, conn,mySqlTransaction);
                    cmd2.Parameters.AddWithValue("@id_art", art.Id);
                    cmd2.Parameters.AddWithValue("@id_facture", factureToCreate.Id);
                    cmd2.Parameters.AddWithValue("@quantity", art.Quantity);

                    //Execuction of my sql query 
                    cmd2.ExecuteNonQuery();

                }//end foreach loop 

                mySqlTransaction.Commit();

            }
            catch (Exception ex)
            {
                flag = false;
                Console.WriteLine(ex.Message);
                MessageBox.Show("Problem de creation de la Facture !");
                mySqlTransaction.Rollback();

            }//end try catch 


            return flag;
        }//end create





        /// <summary>
        /// get all Factures existed in our DB
        /// </summary>
        /// <returns>Observable Collection of type Facture </returns>        
        public override ObservableCollection<Facture> findAll()
        {
            ObservableCollection<Facture> factures = new ObservableCollection<Facture>();
            MySqlTransaction mySqlTransaction = conn.BeginTransaction();
            try
            {
                string sql = "SELECT  date_f , ID_f, Client_num , emp_num , totalPrice  , Is_Company , name_client , f_name_client , Email_client , Adresse_client , emp_name , emp_f_name , Login , _password FROM facture join employee ON id_emp = emp_num join client ON ID_client = client_num;" +
                    "SELECT a.id_art , l.id_facture , l.quantity as q , a.name_art as name , a.price_art as price from list_of_art l join article a  ON  l.id_art = a.id_art ;";

                MySqlCommand cmd = new MySqlCommand(sql, conn, mySqlTransaction);


                //Execuction of my sql query 
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Facture facture = new Facture(reader.GetDateTime("date_f"), reader.GetInt32("ID_f"), GetSQLClient(reader), GetSQLEmployee(reader), reader.GetDouble("totalPrice"));
                    factures.Add(facture);


                }//end while loop 

                if (reader.NextResult())
                {
                    GetFactureArt(reader , factures);
                }//end if 

                

                reader.Close();

                mySqlTransaction.Commit();
            }
            catch(Exception ex)
            {
                mySqlTransaction.Rollback();
                Console.WriteLine(ex.Message);
            }//end trycatch 




            return factures;

        }//end findAll




        private Client GetSQLClient (MySqlDataReader reader)
        {
            return new Client(reader.GetInt32("Client_num"), reader.GetBoolean("Is_Company"), reader.GetString("name_client"), reader.GetString("f_name_client"), reader.GetString("Email_client"), reader.GetString("Adresse_client"));
        }



        private Employee GetSQLEmployee(MySqlDataReader reader)
        {
            return new Employee(reader.GetInt32("emp_num"),reader.GetString("emp_name"),reader.GetString("emp_f_name"),reader.GetString("Login"),reader.GetString("_password"));
        }




        private void GetFactureArt(MySqlDataReader reader, ObservableCollection<Facture> factures)
        {
            while (reader.Read())
            {
                //find my art 
                Article art = new Article(reader.GetInt32("id_art"), reader.GetString("name"), reader.GetDouble("price"), reader.GetInt32("q"));
                foreach (Facture f in factures)
                {
                    //injection of my art in factures 
                    if (reader.GetInt32("id_facture") == f.Id)
                    {
                        f.Articles.Add(art);
                    }//end if 

                }//end foreach loop 

            }//end while loop 

           
        }//end GetFactureArt

        public override bool delete(Facture obj)
        {
            throw new NotImplementedException();
        }

        public override Facture find(int id)
        {
            throw new NotImplementedException();
        }



        public override bool update(Facture obj)
        {
            throw new NotImplementedException();
        }



        
    }//end class 
}//end namespace 
