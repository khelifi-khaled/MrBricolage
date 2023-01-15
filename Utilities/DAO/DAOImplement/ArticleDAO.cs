﻿using MrBricolage.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
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

            try 
            {
                string sql = "INSERT INTO article (name_art , price_art , is_active) VALUES (@name , @price , @bool ) ; ";

                MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                cmd2.Parameters.AddWithValue("@name", article.Name);
                cmd2.Parameters.AddWithValue("@price", article.Price);
                cmd2.Parameters.AddWithValue("@bool", true);


                //Execution of my sql query 
                cmd2.ExecuteNonQuery();

            }
            catch
            {
                flag = false;
                MessageBox.Show("problème sur create !", "infos");
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

            try
            {
                string sql = "UPDATE article SET is_active = @bool WHERE  name_art = @name ; ";

                MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                cmd2.Parameters.AddWithValue("@bool", status);
                cmd2.Parameters.AddWithValue("@name", article.Name);

                //Execution of my sql query
                cmd2.ExecuteNonQuery();

                flag = true;

            }
            catch
            {
                MessageBox.Show("problème sur UpdateArticleStatus !", "infos");
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

            try
            {
                string sql = "SELECT is_active  FROM article WHERE name_art = @name ;";


                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", article.Name);



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

            try
            {
                string sql = "SELECT name_art , is_active  FROM article WHERE name_art = @name ;";


                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", article.Name);



                //Execution of my sql query 
                MySqlDataReader reader = cmd.ExecuteReader();

                flag = reader.Read();

                reader.Close();
            }
            catch
            {
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

            try
            {
                string sql = "SELECT id_art from list_of_art WHERE  id_art = @id ; ";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", article.Id);

                //Execution of my sql query
                MySqlDataReader reader = cmd.ExecuteReader();

                flag = reader.Read();

                reader.Close();


            }
            catch
            {
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

            try
            {
                string sql = "DELETE FROM article WHERE id_art = @id ; ";


                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", article.Id);

                //Execution of my sql query
                cmd.ExecuteNonQuery();


            }
            catch
            {
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
            try
            {
                string sql = "SELECT ID_art , name_art , price_art FROM article WHERE ID_art = @id ; ";

                
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                //Execuction of my sql query
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //if the art with the same id existe in our DB
                    article = new Article(reader.GetInt32("ID_art") , reader.GetString("name_art"),reader.GetDouble("price_art"));

                }else
                {
                    //if the art don't existe with the same id 
                    MessageBox.Show("l'article num " + id + " n'existe pas dans votre DB", "infos");

                }//end if 


                reader.Close();
            }
            catch(Exception e)
            {
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

            try
            {
                string sql = "SELECT ID_art , name_art , price_art FROM article WHERE is_active ; ";

                
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                //Execuction of my sql query
                MySqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    Article art = new Article(reader.GetInt32("ID_art"),reader.GetString("name_art"),reader.GetDouble("price_art"));
                    articles.Add(art);

                }//end while loop 

                reader.Close();
                
            }catch
            {
                
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

            
            try
            {

                string sql = "UPDATE article SET  name_art = @name , price_art = @price WHERE ID_art = @id";

                   
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name" , articleToUpdate.Name);
                cmd.Parameters.AddWithValue("@price", articleToUpdate.Price);
                cmd.Parameters.AddWithValue("@id", articleToUpdate.Id);

                //Execution of my sql query
                cmd.ExecuteNonQuery();


            }
            catch
            {
                flag = false;
                MessageBox.Show("problème sur update");
            }//end trycatch 
           
           
           return flag;

        }//end update





    }//end class 
}//end namespace 
