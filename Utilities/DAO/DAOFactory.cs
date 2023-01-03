using MrBricolage.Models;
using MrBricolage.Singelton;
using MrBricolage.Utilities.DAO.DAOImplement;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBricolage.Utilities.DAO
{



    public  class DAOFactory
    {


         protected static MySqlConnection conn = BDDConnection.getInstance();




        /// <summary>
        /// an anstance of DAO type Employee
        /// </summary>
        public static DAO<Employee> GetEmployeeDAO
        {
            get { return new EmployeeDAO(conn); }
        }




        /// <summary>
        /// an anstance of DAO type Client 
        /// </summary>
        public static DAO<Client> GetClientDAO
        {
            get { return new ClientDAO(conn); }
        }

        /// <summary>
        /// an anstance of DAO type Article 
        /// </summary>
        public static DAO<Article> GetArticleDAO
        {
            get { return new ArticleDAO(conn); }
        }

        /// <summary>
        /// an anstance of DAO type Facture 
        /// </summary>
        public static DAO<Facture> GetFactureDAO
        {
            get { return new FactureDAO(conn); }
        }




    }//end class 
}//end namespace 
