using MrBricolage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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






















        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged




    }//end class 
}//end namespace 
