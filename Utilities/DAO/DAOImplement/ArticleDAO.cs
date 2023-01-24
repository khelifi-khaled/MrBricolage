using MrBricolage.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;

namespace MrBricolage.Utilities.DAO.DAOImplement
{
    public  class ArticleDAO : DAO<Article>
    {


        public ArticleDAO(MySqlConnection conn) : base(conn)
        {

        }









        /// <summary>
        /// create an article in our DB
        /// </summary>
        /// <param name="article">the art that we want to insert in our DB</param>
        /// <returns>true if the article hase been created in our DB, false if not </returns>
        public override bool create(Article article)
        {
            bool flag = true;

            MySqlTransaction mySqlTransaction = conn.BeginTransaction();

            try 
            {
                string sql = "INSERT INTO article (name_art , price_art , is_active , current_quantity) VALUES (@name , @price , @bool ,@current_quantity) ; ";

                MySqlCommand cmd2 = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd2.Parameters.AddWithValue("@name", article.Name);
                cmd2.Parameters.AddWithValue("@price", article.Price);
                cmd2.Parameters.AddWithValue("@bool", true);
                cmd2.Parameters.AddWithValue("@current_quantity", article.Quantity);


                //Execution of my sql query 
                cmd2.ExecuteNonQuery();

                mySqlTransaction.Commit();
            }
            catch
            {
                flag = false;
                MessageBox.Show("problème sur create !", "infos");
                mySqlTransaction.Rollback();
            }//end try catch 

         
           
            return flag;

        }//end create










        /// <summary>
        /// update our article status in our DB
        /// </summary>
        /// <param name="article"> the article to update </param>
        /// <param name="status"> the status that we want to set to our article on DB</param>
        /// <returns>true if article status has been updated, false if not </returns>
        public bool UpdateArticleStatus(Article article , bool status)
        {
            bool flag = false;

            MySqlTransaction mySqlTransaction = conn.BeginTransaction();

            try
            {
                string sql = "UPDATE article SET is_active = @bool WHERE  name_art = @name ; ";

                MySqlCommand cmd2 = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd2.Parameters.AddWithValue("@bool", status);
                cmd2.Parameters.AddWithValue("@name", article.Name);

                //Execution of my sql query
                cmd2.ExecuteNonQuery();

                flag = true;

                mySqlTransaction.Commit();

            }
            catch
            {
                MessageBox.Show("problème sur UpdateArticleStatus !", "infos");
                mySqlTransaction.Rollback();
            }

            return flag;

        }//end UpdateArticleStatus
















        /// <summary>
        /// get the actual article status from DB 
        /// </summary>
        /// <param name="article"></param>
        /// <returns>art status located in DB (true, false)</returns>
        public bool GetArticleStatus(Article article)
        {
            bool flag = false;

            MySqlTransaction mySqlTransaction = conn.BeginTransaction();

            try
            {
                string sql = "SELECT is_active  FROM article WHERE name_art = @name ;";


                MySqlCommand cmd = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd.Parameters.AddWithValue("@name", article.Name);



                //Execution of my sql query 
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    flag = reader.GetBoolean("is_active");

                }//end if 

                

                reader.Close();

                mySqlTransaction.Commit();
            }
            catch
            {
                mySqlTransaction.Rollback();
                MessageBox.Show("problème sur GetArticleStatus !", "infos");
            }

            return flag;
        }//end CheckArticleStatus

        













        /// <summary>
        /// check if article.Name exist in our DB or not 
        /// </summary>
        /// <param name="article"> the article that we want to check </param>
        /// <returns>true if the article exist in our BD, false if not </returns>
        public bool CheckExistedArticle(Article article)
        {
            bool flag = false;

            MySqlTransaction mySqlTransaction = conn.BeginTransaction();

            try
            {
                string sql = "SELECT name_art , is_active  FROM article WHERE name_art = @name ;";


                MySqlCommand cmd = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd.Parameters.AddWithValue("@name", article.Name);



                //Execution of my sql query 
                MySqlDataReader reader = cmd.ExecuteReader();

                flag = reader.Read();


                
                reader.Close();

                mySqlTransaction.Commit();
            }
            catch
            {
                mySqlTransaction.Rollback();
                MessageBox.Show("problème sur checkExestedArticle !", "infos");
            }

            return flag;
        }//end checkExestedArticle














        /// <summary>
        /// check if the article exist in List_of_art 
        /// </summary>
        /// <param name="article">the article that we want to check </param>
        /// <returns>true if article exist, false if not </returns>

        public bool CheckExistedArticleInFacture(Article article)
        {
            bool flag = false;

            MySqlTransaction mySqlTransaction = conn.BeginTransaction();

            try
            {
                string sql = "SELECT id_art from list_of_art WHERE  id_art = @id ; ";

                MySqlCommand cmd = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd.Parameters.AddWithValue("@id", article.Id);

                //Execution of my sql query
                MySqlDataReader reader = cmd.ExecuteReader();

                flag = reader.Read();

                

                reader.Close();

                mySqlTransaction.Commit();


            }
            catch
            {
                mySqlTransaction.Rollback();
                MessageBox.Show("problème sur CheckExistedArticleInFacture !", "infos");

            }//end trycatch
            return flag;
        }//end CheckExistedArticleInFacture














        


        /// <summary>
        /// delete the article from our DB
        /// </summary>
        /// <param name="article">the article that we want to delete </param>
        /// <returns>true if the art has been deleted, false if not </returns>
        public override bool delete(Article article )
        {
            bool flag = true;

            MySqlTransaction mySqlTransaction = conn.BeginTransaction();

            try
            {
                string sql = "DELETE FROM article WHERE id_art = @id ; ";


                MySqlCommand cmd = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd.Parameters.AddWithValue("@id", article.Id);

                //Execution of my sql query
                cmd.ExecuteNonQuery();


                mySqlTransaction.Commit();

            }
            catch
            {
                mySqlTransaction.Rollback();
                MessageBox.Show("problème sur delete !", "infos");
                flag = false;
            }

            return flag;
        }//end delete


















        /// <summary>
        /// find an article from our DB
        /// </summary>
        /// <param name="id"> article id that we are looking for </param>
        /// <returns>the article if found, null if not </returns>
        public override Article find(int id)
        {
            Article article = null;

            MySqlTransaction mySqlTransaction = conn.BeginTransaction();

            try
            {
                string sql = "SELECT ID_art , name_art , price_art ,current_quantity  FROM article WHERE ID_art = @id ; ";

                
                MySqlCommand cmd = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd.Parameters.AddWithValue("@id", id);

                //Execuction of my sql query
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //if the art with the same id existe in our DB
                    article = new Article(reader.GetInt32("ID_art") , reader.GetString("name_art"),reader.GetDouble("price_art"),reader.GetInt32("current_quantity"));

                }else
                {
                    //if the art don't existe with the same id 
                    MessageBox.Show("l'article num " + id + " n'existe pas dans votre DB", "infos");

                }//end if 

               
                reader.Close();

                mySqlTransaction.Commit();
            }
            catch(Exception e)
            {
                mySqlTransaction.Rollback();
                Console.WriteLine(e.Message);
                
            }//end trycatch 

            return article;
        }//end find





       





















        /// <summary>
        /// find all existed articles in our db, with active status  
        /// </summary>
        /// <returns>Observable Collection of type article contains all article in our DB</returns>
        public override ObservableCollection<Article> findAll()
        {
            ObservableCollection < Article > articles = new ObservableCollection<Article>();

            MySqlTransaction mySqlTransaction = conn.BeginTransaction();

            try
            {
                string sql = "SELECT ID_art , name_art , price_art , current_quantity FROM article WHERE is_active ; ";

                
                MySqlCommand cmd = new MySqlCommand(sql, conn, mySqlTransaction);

                //Execuction of my sql query
                MySqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    Article art = new Article(reader.GetInt32("ID_art"),reader.GetString("name_art"),reader.GetDouble("price_art"),reader.GetInt32("current_quantity"));
                    articles.Add(art);

                }//end while loop 

              

                reader.Close();

                mySqlTransaction.Commit();

            }
            catch
            {
                mySqlTransaction.Rollback();
                Console.WriteLine("problème de récupération des Articles");
            }//end trycatch 

            return articles;
        }//end findAll 



















        /// <summary>
        /// update the article infos in our DB
        /// </summary>
        /// <param name="articleToUpdate">the article that we want to update </param>
        /// <returns>true if articleToUpdate has been updated, false if not  </returns>
        public override bool update(Article articleToUpdate)
        {
            bool flag= true;

            MySqlTransaction mySqlTransaction = conn.BeginTransaction();

            try
            {

                string sql = "UPDATE article SET  name_art = @name , price_art = @price , current_quantity = @quantity WHERE ID_art = @id";

                   
                MySqlCommand cmd = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd.Parameters.AddWithValue("@name" , articleToUpdate.Name);
                cmd.Parameters.AddWithValue("@price", articleToUpdate.Price);
                cmd.Parameters.AddWithValue("@id", articleToUpdate.Id);
                cmd.Parameters.AddWithValue("@quantity", articleToUpdate.Quantity);

                //Execution of my sql query
                cmd.ExecuteNonQuery();

                mySqlTransaction.Commit();
            }
            catch
            {
                mySqlTransaction.Rollback();
                flag = false;
                MessageBox.Show("problème sur update");
            }//end trycatch 
           
           
           return flag;

        }//end update





    }//end class 
}//end namespace 
