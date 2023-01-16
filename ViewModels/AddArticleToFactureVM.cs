using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using MrBricolage.Views;

namespace MrBricolage.ViewModels
{
    public class AddArticleToFactureVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private Article _article;

        public AddArticleToFactureVM(Facture facture)
        {
            FactureToModifie = facture;
        }




        public Facture FactureToModifie { get; set; }



        public Article ArticleToAdd
        {
            get => _article;
            set
            {
                _article = value;
                OnPropertyChanged(nameof(ArticleToAdd));
            }
        }






        public void Getarticle(AddArticleToFactureWindow win, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                if (int.TryParse(win.Article_Number.Text, out int val))
                {
                    ArticleToAdd = DAOFactory.GetArticleDAO.find(val);
                }
                else
                {
                    MessageBox.Show("Format d'Id est incorrecte !!");
                }//end if 
            }//end if 

        }//end Getarticle















        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged




    }//end class 
}//end namespace 
