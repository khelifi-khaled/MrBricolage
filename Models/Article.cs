﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MrBricolage.Models
{
    public  class Article
    {


        private int _id;
        private string _name;
        private double _price;


        public Article() { }


        public Article (int id=0, string name="", double price=0.0)
        {
            this._id = id;
            this._name = name;
            this._price = price;
        }


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

        public double Price
        {
            get { return this._price; }
            set { this._price = value; }
        }



    }//end class 
}//end namespace 