using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System.ComponentModel;


namespace MrBricolage.ViewModels
{
    public  class ModifieArtVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;



        public ModifieArtVM(Article art) 
        {
            SelectedArticle = art;
        }



        public Article SelectedArticle { get;set; }





        public void Exit (ModifieArtWindow win)
        {
            ManagementArtWindow articles = new ManagementArtWindow();
            articles.Show();
            win.Close();
        }//end Exit_BTN



        public void Delete (ModifieArtWindow win)
        {
            if (DAOFactory.GetArticleDAO.delete(SelectedArticle))
            {
                ManagementArtWindow articles = new ManagementArtWindow();
                articles.Show();
                win.Close();

            }//end if 

        }//end Delete





        public void Save (ModifieArtWindow win)
        {
            if (DAOFactory.GetArticleDAO.update(SelectedArticle))
            {
                ManagementArtWindow articles = new ManagementArtWindow();
                articles.Show();
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
}//ennd namespace 
