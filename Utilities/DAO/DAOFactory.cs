using MrBricolage.Singelton;
using MrBricolage.Utilities.DAO.DAOImplement;
using MySql.Data.MySqlClient;
using System.Linq;

namespace MrBricolage.Utilities.DAO
{



    public  class DAOFactory
    {


         protected static MySqlConnection conn = BDDConnection.Instance.Conn;




        /// <summary>
        /// an anstance of DAO type Employee
        /// </summary>
        public static EmployeeDAO GetEmployeeDAO
        {
            get { return new EmployeeDAO(conn); }
        }



        //public static T Get<T>()
        //    where T : class
        //{
        //    return typeof(T).GetConstructors().First()?.Invoke(new object[] { conn }) as T;
        //}




        /// <summary>
        /// an anstance of DAO type Client 
        /// </summary>
        public static ClientDAO GetClientDAO
        {
            get { return new ClientDAO(conn); }
        }

        /// <summary>
        /// an anstance of DAO type Article 
        /// </summary>
        public static ArticleDAO GetArticleDAO
        {
            get { return new ArticleDAO(conn); }
        }

        /// <summary>
        /// an anstance of DAO type Facture 
        /// </summary>
        public static FactureDAO GetFactureDAO
        {
            get { return new FactureDAO(conn); }
        }




    }//end class 
}//end namespace 
