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

                            MessageBox.Show("La valeure de la Quantité d'article  ("+ ArticleToAdd.Name + ") doit etre strictement supérieur a 0 ! ", "infos");

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
                        MessageBox.Show("Format de quantité d'article souhaité est incorrect ! ", "infos");
                    }//end if 

                }else
                {
                    MessageBox.Show("L'article N° " + ArticleToAdd.Id + " est inactive !", "infos");
                } //end if 

            }else
            {
                MessageBox.Show("Vour ne pouvez pas inserer un article vide a la facture N° " + FactureToModifie.Id, "infos");
            }//end if 

        }//end AddArticleBTN





        






        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged




    }//end class 
}//end namespace 
