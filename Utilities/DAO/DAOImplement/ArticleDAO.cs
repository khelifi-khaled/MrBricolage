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







        //  test  ok 
        public override bool create(Article article)
        {
            bool flag = true;

            if (article != null)
            {
                try
                {
                    string sql ="SELECT name_art , is_active  FROM article WHERE name_art = @name ;";

                    
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", article.Name);



                    //Execuction of my sql query 
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
                                sql = "UPDATE article SET is_active = @bool WHERE  name_art = @name ; ";
                                 
                                MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                                cmd2.Parameters.AddWithValue("@bool", true);
                                cmd2.Parameters.AddWithValue("@name", article.Name);

                                //Execuction of my sql query
                                cmd2.ExecuteNonQuery();
                               
                            }
                            else
                            {
                                //the user don't want update this article, in this case flag is false and i do nothing 
                                flag = false;
                                reader.Close();

                            }//end if 


                        }//end if 
                    }
                    else
                    {
                        reader.Close();
                        sql = "INSERT INTO article (name_art , price_art , is_active) VALUES (@name , @price , @bool ) ; ";

                        
                        MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                        cmd2.Parameters.AddWithValue("@name",article.Name) ;
                        cmd2.Parameters.AddWithValue("@price", article.Price);
                        cmd2.Parameters.AddWithValue("@bool",true);


                        //Execuction of my sql query 
                        cmd2.ExecuteNonQuery();
                       
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








        // test ok 
        public override bool delete(Article article )
        {
            bool flag = true;
            if (article != null)
            {
                string sql = "SELECT a.ID_art , a.is_active FROM list_of_art l INNER join article a on l.id_art = a.ID_art WHERE a.id_art = @id ;";

                
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id",article.Id);


                //Execuction of my sql query
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) //if the article existe in facture
                {
                    if (reader.GetBoolean(1))//if the article existe in facture and he is active 
                    {
                        if (MessageBox.Show("vous ne pouvez pas supprimer l'article num " + article.Id + " car il existe dans des factures, voulez vous le randre inactive ?", "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            reader.Close();
                            sql = "UPDATE article SET is_active = @bool WHERE  id_art = @id ; ";

                            MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                            cmd2.Parameters.AddWithValue("@bool", false);
                            cmd2.Parameters.AddWithValue("@id", article.Id);

                            //Execuction of my sql query
                            cmd2.ExecuteNonQuery();

                            MessageBox.Show("L'article num " + article.Id + " n'est pas supprimé de votre DB, mais il est inactive, donc vous ne pouvez plus  l'insere dans une facture", "infos");
                        }
                        else
                        {
                            // the user don't want update this article, in this case flag is false and i do nothing 
                            flag = false;
                            reader.Close();

                        }//end if 

                    }
                    else // Here the article existe in facture, and he is inactive 
                    {
                        MessageBox.Show("L'article num " + article.Id + " n'est pas supprimé de votre DB, et il est inactive.", "infos");
                        reader.Close();
                        flag = false;
                    }
                   

                }
                else // here our article don't existe in my factures 
                {
                    reader.Close();
                    sql = "DELETE FROM article WHERE id_art = @id ; ";

                    
                    MySqlCommand cmd3 = new MySqlCommand(sql, conn);
                    cmd3.Parameters.AddWithValue("@id",article.Id);

                    //Execuction of my sql query
                    cmd3.ExecuteNonQuery();
                    
                }//end if 
            }
            else
            {
                flag = false;
                MessageBox.Show("L'article est null !", "infos");
            }//end if 

            return flag;
        }//end delete




        //  test  ok 
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




        // test ok 
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
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("problem de récupération des Articles");
            }//end trycatch 

            return articles;
        }//end findAll 







        // test ok !!!
        public override bool update(Article articleToUpdate)
        {
            bool flag= true;

            if (articleToUpdate != null)
            {
                try
                {



                    string sql = "UPDATE article SET  name_art = @name , price_art = @price WHERE ID_art = @id";

                   
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name" , articleToUpdate.Name);
                    cmd.Parameters.AddWithValue("@price", articleToUpdate.Price);
                    cmd.Parameters.AddWithValue("@id", articleToUpdate.Id);

                    //Execuction of my sql query
                    cmd.ExecuteNonQuery();



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
