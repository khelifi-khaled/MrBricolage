using System;
using System.Collections.ObjectModel;


namespace MrBricolage.Models
{
    public  class Facture
    {

        private int _id;
        private DateTime _date;
        private Client _client;
        private Employee _employee;
        private double _totalAmount ;
        private ObservableCollection<Article> _articles;


        public Facture()
        {
            this._articles = new ObservableCollection<Article>();
        }

        public Facture(DateTime date , int id = 0, Client client = null, Employee
            employee = null, double totalAmount = 0.0)
        {
            this._id = id;
            this._date = date;
            this._client = client;
            this._employee = employee;
            this._totalAmount = totalAmount;
            this._articles = new ObservableCollection<Article>();
        }//end constructor 



        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        public DateTime Date
        {
            get { return this._date; }
            set { this._date = value; }
        }

        public Client Client
        {
            get { return this._client;}
            set { this._client = value; }
        }

        public Employee Employee
        {
            get { return this._employee; }
            set { this._employee = value; }
        }



        public ObservableCollection<Article> Articles
        {
            get { return this._articles;}
            set { this._articles = value;}

        }


        public double  TotalAmount
        {
            get { return this._totalAmount; }
            set { this._totalAmount = value; }
        } 


    }//end class 
}//end namespace 
