


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





        /// <summary>
        /// employee id
        /// </summary>
        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        /// <summary>
        /// employee name 
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }


        /// <summary>
        /// employee last name (family name )
        /// </summary>
        public string F_Name
        {
            get { return this._f_name; }
            set { this._f_name = value; }
        }



        /// <summary>
        /// emloyee Login
        /// </summary>
        public string Login
        {
            get { return this._login; }
            set { this._login = value; }
        }



        /// <summary>
        /// employee password
        /// </summary>
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
