using MrBricolage.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                    string sql = string.Format("SELECT name_art FROM {0} WHERE name_art = '{1}' ; ", "article", article.Name);

                    //Execuction of my sql query 
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        MessageBox.Show("L'article " + article.Name + " existe deja dans votre DB !", "infos");
                        flag = false;
                        reader.Close();
                    }
                    else
                    {
                        reader.Close();
                        sql = string.Format("INSERT INTO article (name_art , price_art) VALUES ( '{0}' , {1}) ; ", article.Name, article.Price);

                        //Execuction of my sql query 
                        MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                        MySqlDataReader reader2 = cmd2.ExecuteReader();
                    }//end if 

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Problem d'insertion de  " + article.Name, "infos");
                    flag = false;
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







            return flag;
        }//end delete

        public override Article find(int id)
        {
            throw new NotImplementedException();
        }//end find

        public override ObservableCollection<Article> findAll()
        {
            throw new NotImplementedException();
        }//end findAll 

        public override bool update(Article obj)
        {
            throw new NotImplementedException();
        }//end update
    }//end class 
}//end namespace 
