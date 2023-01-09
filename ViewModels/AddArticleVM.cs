using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBricolage.ViewModels
{
    public class AddArticleVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;



        public AddArticleVM()
        {
            ArticleToAdd = new Article();
        }





        public Article ArticleToAdd { get ; set ; }






        public void Exit (AddArticleWindow win)
        {
            ManagementArtWindow window = new ManagementArtWindow();
            window.Show();
            win.Close();


        }//end Exit


        public void Save(AddArticleWindow win)
        {

            if(DAOFactory.GetArticleDAO.create(ArticleToAdd))
            {
                ManagementArtWindow window = new ManagementArtWindow();
                window.Show();
                win.Close();

            }//end if 

        }//end save



        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged


    }//end class 
}//end namespace 
