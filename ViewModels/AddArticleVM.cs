using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System.ComponentModel;
using System.Windows;

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

            if(ArticleToAdd != null)
            {
                if(DAOFactory.GetArticleDAO.CheckExistedArticle(ArticleToAdd))
                {
                    //if the article existe in our db , i check if the article is active or not
                    if (DAOFactory.GetArticleDAO.GetArticleStatus(ArticleToAdd))
                    {
                        MessageBox.Show("L'article existe dans votre DB et il est bien actif ", "infos");
                    }else
                    {
                        // if the article exist in our DB and he is just inactive 
                        if (MessageBox.Show("L'article avec le nom " + ArticleToAdd.Name + " existe déjà dans votre DB, voulez-vous l'activer ?", "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            DAOFactory.GetArticleDAO.UpdateArticleStatus(ArticleToAdd, true);
                            MessageBox.Show("L'article "+ ArticleToAdd.Name + " est bien activé maintenant sur votre DB ", "infos");
                            ManagementArtWindow window = new ManagementArtWindow();
                            window.Show();
                            win.Close();

                        }//end if 
                    }//end if 
                }else
                {
                    //the article don't exist in our DB, so i will try to create it 
                    if (DAOFactory.GetArticleDAO.create(ArticleToAdd))
                    {
                        ManagementArtWindow window = new ManagementArtWindow();
                        window.Show();
                        win.Close();
                    }//end if 
                }

            }
            else
            {
                MessageBox.Show("L'article est null");
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
