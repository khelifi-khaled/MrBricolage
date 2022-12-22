using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBricolage.Models
{
    public  class Employee
    {
        private int _id;
        private string _name;
        private string _f_name;
        private string _login;
        private string _password;


        public Employee() { }

        public Employee(int id=0, string name="", string f_name="", string login = "",string password = "")
        { 
            this._id = id;
            this._name= name;
            this._f_name = f_name;
            this._login= login;
            this._password= password;

        }//end constructor






        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public string F_Name
        {
            get { return this._f_name; }
            set { this._f_name = value; }
        }


        public string Login
        {
            get { return this._login; }
            set { this._login = value; }
        }


        public string Password
        {
            get { return this._password; }
            set
            {
                this._password = value;
            }
        }

    }//end class 
}//end namespace
