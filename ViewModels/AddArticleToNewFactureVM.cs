using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace MrBricolage.ViewModels
{
    public  class AddArticleToNewFactureVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private Article _article;

        private Facture _thisFacture; 


        public AddArticleToNewFactureVM(Facture facture )
        {
            ThisFacture = facture;
           
        }



        public Facture ThisFacture
        {
            get => _thisFacture;
            set
            {
                _thisFacture = value;
                OnPropertyChanged(nameof(ThisFacture));
            }
        }

        




        public Article ArticleToAdd
        {
            get => _article;
            set
            {
                _article = value;
                OnPropertyChanged(nameof(ArticleToAdd));
            }
        }





        public void Exit(AddArticleToNewFactureWindow win)
        {
            AddFactureWindow window = new AddFactureWindow(ThisFacture);
            window.Show();
            win.Close();
        }//end exit








        public void AddArticleBTN(AddArticleToNewFactureWindow window)
        {
            if (ArticleToAdd != null)
            {
                if (DAOFactory.GetArticleDAO.GetArticleStatus(ArticleToAdd))
                {
                    if (int.TryParse(window.Quantity_art.Text, out int val))
                    {
                        if (val <= 0)
                        {

                            MessageBox.Show("La valeure de la Quantité d'article  (" + ArticleToAdd.Name + ") doit etre strictement supérieur a 0 ! ", "infos");

                        }
                        else if (ArticleToAdd.Quantity < val)
                        {
                            MessageBox.Show("La quantité en stock est insuffisante", "infos");
                        }
                        else
                        {
                            ArticleToAdd.Quantity = val;
                            ThisFacture.AddArticle(ArticleToAdd);
                            OnPropertyChanged(nameof(ThisFacture));

                            AddFactureWindow win = new AddFactureWindow(ThisFacture);
                            win.Show();
                            window.Close();
                            

                        }//end if 

                    }
                    else
                    {
                        MessageBox.Show("Format de quantité d'article souhaité est incorrect ! ", "infos");
                    }//end if 

                }
                else
                {
                    MessageBox.Show("L'article N° " + ArticleToAdd.Id + " est inactive !", "infos");
                } //end if 

            }
            else
            {
                MessageBox.Show("Vour ne pouvez pas inserer un article vide a la facture N° " + ThisFacture.Id, "infos");
            }//end if 

        }//end AddArticleBTN



        
        
        
        
        public void Getarticle(AddArticleToNewFactureWindow win, KeyEventArgs e)
        
        {

            if (e.Key == Key.Enter)
            {
                if (int.TryParse(win.Article_Number.Text, out int val))
                {
                    ArticleToAdd = DAOFactory.GetArticleDAO.find(val);

                    if (ArticleToAdd != null)
                    {
                        ArticleToAdd = ThisFacture.UpdateArtQuatity(ArticleToAdd);
                        OnPropertyChanged(nameof(ArticleToAdd));
                    }
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


    }//end 
}
