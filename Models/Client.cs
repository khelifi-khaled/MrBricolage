using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBricolage.Models
{
    public  class Client
    {

        private int _id;
        private bool _is_company;
        private string _name;
        private string _f_name;
        private string _email;
        private string _adresse;




        public Client() { }


        public Client (int id=0,bool is_compny = false , string name="", string f_name="",string email="", string adresse = "")
        {
            this._id = id;
            this._is_company = is_compny;
            this._name= name;
            this._f_name = f_name;
            this._email= email;
            this._adresse= adresse;
        }//end constructor




        /// <summary>
        /// client Id 
        /// </summary>
        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        /// <summary>
        /// boo, if false => facture total price *= 1.21 , if ture => facture  total price =  facture  total price
        /// </summary>
        public bool IsCompany
        {
            get { return this._is_company; }
            set { this._is_company = value; }
        }

        /// <summary>
        /// client name
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }


        /// <summary>
        /// client last name (family name )
        /// </summary>
        public string F_name
        {
            get { return this._f_name; }
            set
            {
                this._f_name = value;
            }
        }



        /// <summary>
        /// client email 
        /// </summary>
        public string Email
        {
            get { return this._email;}
            set
            {
                this._email = value;
            }
        }

        /// <summary>
        /// client add 
        /// </summary>
        public string Adresse
        {
            get { return this._adresse;}
            set 
            {
                this._adresse = value;
            }
        }








    }//end class 
}//end namespace 
