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

        /// <summary>
        /// an empty constructor, by default date of this facture = Datetime.Now and new articles 
        /// </summary>
        public Facture()
        {
            this._date = DateTime.Now;
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


        /// <summary>
        /// facture id
        /// </summary>
        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }



        /// <summary>
        /// facture date 
        /// </summary>
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



        /// <summary>
        /// employee of this fature 
        /// </summary>
        public Employee Employee
        {
            get { return this._employee; }
            set { this._employee = value; }
        }


        /// <summary>
        /// our Articles liste of this facture 
        /// </summary>
        public ObservableCollection<Article> Articles
        {
            get { return this._articles;}
            set { this._articles = value;}

        }




        /// <summary>
        /// total of this facture
        /// </summary>
        public double  TotalAmount
        {
            get { return this._totalAmount; }
            set { this._totalAmount = value; }
        }



        /// <summary>
        /// mithod to delete an article from this.Articles  
        /// </summary>
        /// <param name="id"> id of article to delete </param>
        public void Delete_art_facture(int id)
        {
            for (int i=0; i<this.Articles.Count;i++)
            {
                if (this.Articles[i].Id ==  id )
                {
                    this.Articles.Remove(this.Articles[i]);
                }//end if 
            }//end 

            this.TotalAmount = CalculTotalAmount();

        }//end Delete_art_facture




        /// <summary>
        /// void function to add an article to this.Articles, Modifie this.TotalAmount 
        /// </summary>
        /// <param name="artToAdd"></param>
        public void AddArticle(Article artToAdd)
        {
            if (Check_existed_art(artToAdd))
            {
                foreach(Article art in this.Articles)
                {
                    if (art.Id == artToAdd.Id)
                    {
                        art.Quantity += artToAdd.Quantity;

                    }//end if 
                }//end foreach loop 

            }else
            {
                this.Articles.Add(artToAdd);
            }//end if 

            this.TotalAmount = CalculTotalAmount();

        }//AddArticle



        /// <summary>
        /// Method to calcul total amount 
        /// </summary>
        /// <returns>if the client is a company, it will return total without VAT , if not total + VAT</returns>

        public double CalculTotalAmount()
        {
            double total = 0.0;
           foreach(Article article in this.Articles)
            {
                total += (this.Client.IsCompany) ? (article.Price * article.Quantity) : (article.Price * article.Quantity * 1.21);
            }
           return Math.Round(total,2);

        }//end CalculTotalAmount

    

        /// <summary>
        /// Check if the article exist in this.Articles
        /// </summary>
        /// <param name="art"> article that we want to check</param>
        /// <returns>true is exist, false if not </returns>
        public bool Check_existed_art(Article art)
        {
            bool flag = false;

            foreach(Article article in this.Articles)
            {
                if (article.Id== art.Id)
                {
                    flag = true;
                }//end if 
            }//end foreach loop 


            return flag;
        }//end Check_existed_art



        /// <summary>
        /// function to update Article quantity came from DB,
        /// </summary>
        /// <param name="artFromDB">art came from DB </param>
        /// <returns>artFromDB with the Correcte Quantity</returns>
        public Article UpdateArtQuatity(Article artFromDB)
        {
            foreach (Article article in this.Articles)
            {
                if (article.Id == artFromDB.Id)
                {
                    artFromDB.Quantity -= article.Quantity;
                }
            }

            return artFromDB;

        }//end UpdateArtQuatity


    }//end class 
}//end namespace 
