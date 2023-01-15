using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System.ComponentModel;
using System.Windows;

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

            if (SelectedArticle != null)
            {
                if (DAOFactory.GetArticleDAO.CheckExistedArticleInFacture(SelectedArticle))//if the article exist in facture
                {
                    if (DAOFactory.GetArticleDAO.GetArticleStatus(SelectedArticle)) //if the article exist in facture and he is active
                    {
                        if (MessageBox.Show("vous ne pouvez pas supprimer l'article num " + SelectedArticle.Id + " car il existe dans des factures, voulez vous le randre inactive ?", "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            DAOFactory.GetArticleDAO.UpdateArticleStatus(SelectedArticle, false);
                            MessageBox.Show("L'article num " + SelectedArticle.Id + " n'est pas supprimé de votre DB, mais il est inactive, donc vous ne pouvez plus  l'insere dans une facture", "infos");
                            ManagementArtWindow articles = new ManagementArtWindow();
                            articles.Show();
                            win.Close();
                        }//end if 
                    }
                    else // Here the article exist in facture, and he is inactive 
                    {
                        MessageBox.Show("vous ne pouvez pas supprimer l'article num " + SelectedArticle.Id + " car il existe dans des facture, il est bien inactive.", "infos");
                        ManagementArtWindow articles = new ManagementArtWindow();
                        articles.Show();
                        win.Close();
                    }//end if 
                }else
                {
                    // here our article don't exist in my factures 
                    if (DAOFactory.GetArticleDAO.delete(SelectedArticle))
                    {
                        ManagementArtWindow articles = new ManagementArtWindow();
                        articles.Show();
                        win.Close();
                    }

                }//end if 

            }
            else
            {
                MessageBox.Show("L'article est null !", "infos");

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
