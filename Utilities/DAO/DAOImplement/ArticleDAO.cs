using MrBricolage.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MrBricolage.Utilities.DAO.DAOImplement
{
    public  class ArticleDAO : DAO<Article>
    {


        public ArticleDAO(MySqlConnection conn) : base(conn)
        {

        }







        // no test 
        public override bool create(Article article)
        {
            bool flag = true;

            if (article != null)
            {
                try
                {
                    string sql = string.Format("SELECT name_art , is_active  FROM {0} WHERE name_art = '{1}' ; ", "article", article.Name);

                    //Execuction of my sql query 
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        //if the article existe in our db , i check if the article is active or not 
                        if (reader.GetBoolean("is_active"))
                        {
                            flag = false;
                            MessageBox.Show("l'article existe dans votre DB, et il est bien active ", "infos");
                            reader.Close();
                        }else
                        {
                            // if the article existe in our DB and he is just inactive 
                            if (MessageBox.Show("voulez vous activé l'article " + article.Name + " ?", "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                flag = false;
                                reader.Close();
                                sql = string.Format("UPDATE article SET is_active = '{0}' WHERE  art_name = '{1}' ; ", "1", article.Name);
                                //Execuction of my sql query 
                                MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                                MySqlDataReader reader2 = cmd2.ExecuteReader();
                                reader2.Close();
                            }
                            else
                            {
                                // the user don't want update this article, in this case flag is false and i do nothing 
                                flag = false;

                            }//end if 


                        }//end if 
                    }
                    else
                    {
                        reader.Close();
                        sql = string.Format("INSERT INTO article (name_art , price_art , is_active) VALUES ( '{0}' , {1} , '1') ; ", article.Name, article.Price);

                        //Execuction of my sql query 
                        MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                        MySqlDataReader reader2 = cmd2.ExecuteReader();
                        reader2.Close();
                    }//end if 

                }
                catch (Exception ex)
                {
                    flag = false;
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Problem d'insertion de  " + article.Name, "infos");
                    
                   
                }//end trycatch
            }else
            {
                flag = false;
                MessageBox.Show("L'article est null !", "infos");
            }//end if 
           
            return flag;

        }//end create








        //no test 
        public override bool delete(Article article )
        {
            bool flag = true;
            if (article != null)
            {
                string sql = string.Format("SELECT id_art FROM list_of_art WHERE id_art = {0} ;", article.Id);

                //Execuction of my sql query
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) 
                {
                    //if the article existe in facture
                    if (MessageBox.Show("vous ne pouvez pas supprimer l'article num " + article.Id + " car il existe dans des factures, voulez vous le randre inactive ?","infos",MessageBoxButton.YesNo,MessageBoxImage.Question)==MessageBoxResult.Yes)
                    {
                        reader.Close();
                        sql = string.Format("UPDATE article SET is_active = '{0}' WHERE  id_art = {1} ; ", "0",article.Id) ;
                        //Execuction of my sql query
                        MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                        MySqlDataReader reader2 = cmd2.ExecuteReader();
                        reader2.Close();
                        MessageBox.Show("L'article num "+ article.Id + " n'est pas supprimé de votre DB, mais il est inactive, donc vous ne pouvez plus  l'insere dans une facture", "infos");
                    }
                    else
                    {
                        // the user don't want update this article, in this case flag is false and i do nothing 
                        flag = false;

                    }//end if 

                }
                else // here our article don't existe in my factures 
                {
                    reader.Close();
                    sql = string.Format("DELETE FROM article WHERE id_art = {0} ; ", article.Id);

                    //Execuction of my sql query
                    MySqlCommand cmd3 = new MySqlCommand(sql, conn);
                    MySqlDataReader reader3 = cmd3.ExecuteReader();
                    reader3.Close();
                }//end if 
            }
            else
            {
                flag = false;
                MessageBox.Show("L'article est null !", "infos");
            }//end if 

            return flag;
        }//end delete




        // no test 
        public override Article find(int id)
        {
            Article article = null;
            try
            {
                string sql = string.Format("SELECT ID_art , name_art , price_art FROM article WHERE ID_art = {0} ; ", id);

                //Execuction of my sql query
                MySqlCommand cmd = new MySqlCommand(sql, conn);
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



            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                
            }//end trycatch 

            return article;
        }//end find




        //no test 
        public override ObservableCollection<Article> findAll()
        {
            ObservableCollection < Article > articles = new ObservableCollection<Article>();

            try
            {
                string sql = string.Format("SELECT ID_art , name_art , price_art FROM {0} WHERE is_active ; ", "article");

                //Execuction of my sql query
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    Article art = new Article(reader.GetInt32("ID_art"),reader.GetString("name_art"),reader.GetDouble("price_art"));
                    articles.Add(art);

                }//end while loop 

                reader.Close();
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("problem de récupération des Articles");
            }//end trycatch 

            return articles;
        }//end findAll 







        //no test 
        public override bool update(Article articleToUpdate)
        {
            bool flag= true;

            if (articleToUpdate != null)
            {
                try
                {
                    string sql = string.Format("UPDATE article SET  name_art = '{0}' , price_art = {1} WHERE ID_art = {2}",  articleToUpdate.Name, articleToUpdate.Price, articleToUpdate.Id);

                    //Execuction of my sql query
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    reader.Close();

                }
                catch(Exception ex )
                {
                    flag = false;
                    Console.WriteLine(ex.Message);
                }//end trycatch 
            }
            else
            {
                flag = false;
                MessageBox.Show("L'article est null ! ", "infos");
            }//end if 
            return flag;

        }//end update





    }//end class 
}//end namespace 
