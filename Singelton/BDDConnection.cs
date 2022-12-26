using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBricolage.Singelton
{
    public  class BDDConnection
    {


        private string connString = "SERVER= localhost ; DATABASE = mrbricolage ; UID= root ; PASSWORD =;";

        //object connection 
        private static MySqlConnection conn;


        //singleton 
        private volatile static BDDConnection single;

        /**
	    * private constructor  
	    */
        private BDDConnection()
        {
            try
            {

                conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connection to database opened !");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem  de connexion a votre base de données");
                Console.WriteLine(ex.Message);
            }

        }//end constructor 







        /**
         * method connect to our DB  
         * @return Connection
         */
        public static MySqlConnection getInstance()
        {
            if (single == null)
            {
                single = new BDDConnection();
            }

            return conn;
        }//end getInstance




    }//end class 
}//end namespace 
