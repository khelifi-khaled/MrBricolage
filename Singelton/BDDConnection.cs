using MySql.Data.MySqlClient;
using System;

namespace MrBricolage.Singelton
{
    public  class BDDConnection
    {


        private string connString = "SERVER= localhost ; DATABASE = mrbricolage ; UID= root ; PASSWORD =;";

        //object connection 
        private  MySqlConnection _conn;





        //singleton 
        private volatile static BDDConnection _instance;



        public static BDDConnection Instance
        { 
            get 
            {
                //Est-ce que l'_instance est !=null?
                _instance = _instance ?? new BDDConnection();
                //est-ce que ma connection est toujours en vie (alive)
                if(_instance.Conn.State== System.Data.ConnectionState.Broken)
                {
                    try
                    {
                        _instance.Conn.Close();
                        _instance.Conn.Open();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                return _instance;
            }//end get  
        }//end Instance



        public MySqlConnection Conn { get => _conn; set => _conn = value; }






        /// <summary>
        /// private constructor  
        /// </summary>
        private BDDConnection()
        {
            try
            {
                Conn = new MySqlConnection(connString);
                Conn.Open();
                Console.WriteLine("Connection to database opened !");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem  de connexion a votre base de données");
                Console.WriteLine(ex.Message);
            }//end trycatch 

            

        }//end constructor 

    }//end class 
}//end namespace 
