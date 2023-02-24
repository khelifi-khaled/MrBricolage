using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using MrBricolage.Views;
using MrBricolage.Utilities.DAO.DAOImplement;

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
                    //ArticleToAdd = DAOFactory.Get<ArticleDAO>().find(val);

                }
                else
                {
                    MessageBox.Show("Format d'ID incorrect !!");
                }//end if 
            }//end if 

        }//end Getarticle


        public void AddArticleBTN(AddArticleToFactureWindow window)
        {
            if (ArticleToAdd != null)
            {
                if (DAOFactory.GetArticleDAO.GetArticleStatus(ArticleToAdd))
                {
                    if(int.TryParse(window.Quantity_art.Text, out int val))
                    {
                        if ( val <= 0  )
                        {

                            MessageBox.Show("La valeur de la quantité d'article  ("+ ArticleToAdd.Name + ") doit être strictement supérieure à 0 ! ", "infos");

                        }else if (ArticleToAdd.Quantity < val)
                        {
                            MessageBox.Show("La quantité en stock est insuffisante", "infos");
                        }else
                        {
                            ArticleToAdd.Quantity = val;
                            FactureToModifie.AddArticle(ArticleToAdd);
                            FactureToModifie.Date = DateTime.Now;

                            if(DAOFactory.GetFactureDAO.Add_article_facture(FactureToModifie, ArticleToAdd))
                            {
                                ModifieFactureWindow win = new ModifieFactureWindow(FactureToModifie);
                                win.Show();
                                window.Close();
                            }//ned if 
                        }//end if 

                    }else
                    {
                        MessageBox.Show("Format de quantité d'article souhaité incorrect ! ", "infos");
                    }//end if 

                }else
                {
                    MessageBox.Show("L'article N° " + ArticleToAdd.Id + " est inactif !", "infos");
                } //end if 

            }else
            {
                MessageBox.Show("Vous ne pouvez pas insérer un article vide à la facture N° " + FactureToModifie.Id, "infos");
            }//end if 

        }//end AddArticleBTN





        public void Exit(AddArticleToFactureWindow win)
        {
            ModifieFactureWindow window = new ModifieFactureWindow(FactureToModifie);
            window.Show();
            win.Close();
        }//end exit






        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged




    }//end class 
}//end namespace 
