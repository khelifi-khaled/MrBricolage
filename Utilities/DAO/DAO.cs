using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;


namespace MrBricolage.Utilities.DAO
{
    public abstract class DAO <T>
    {
        //our connection to DB
        protected MySqlConnection conn;



        public DAO(MySqlConnection conn)
        {
            this.conn = conn;
        }



        public abstract T find (int id) ;





        public abstract ObservableCollection<T> findAll();




        public abstract bool create(T e); 



        public abstract bool delete(T obj);    


        public abstract bool update(T obj);



    }//end class 
}//end namespace 
