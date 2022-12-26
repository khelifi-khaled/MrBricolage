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


        public static DAO<Employee> GetEmployeeDAO
        {
            get { return new EmployeeDAO(conn); }
        }


        public static DAO<Client> GetDAOClient
        {
            get { return new ClientDAO(conn); }
        }




    }//end class 
}//end namespace 
