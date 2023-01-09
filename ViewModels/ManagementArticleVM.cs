using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBricolage.ViewModels
{
    public  class ManagementArticleVM : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;


        public  ManagementArticleVM ()
        {
            Articles = DAOFactory.GetArticleDAO.findAll();
            SelectedArticle = new Article();

        }


        public ObservableCollection< Article > Articles { get; set; }


        public Article SelectedArticle { get; set; }




        public void DataGrid_Click(ManagementArtWindow win)
        {
            ModifieArtWindow window = new ModifieArtWindow(SelectedArticle);
            window.Show();
            win.Close();
        }//end DataGrid_Click




        public  void Add_art (ManagementArtWindow win)
        {
            AddArticleWindow addArticle = new AddArticleWindow();
            addArticle.Show();
            win.Close();
        }//end Add_art


        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged


    }//end class 
}//end namespace 
