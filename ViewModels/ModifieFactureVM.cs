using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System;
using System.ComponentModel;
using System.Windows;

namespace MrBricolage.ViewModels
{
    public  class ModifieFactureVM : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;

        private Facture _facture;

        public ModifieFactureVM(Facture facture )
        {
            SelectedFacture = facture;
        }



        public Facture SelectedFacture
        {
            get => _facture;
            set
            {
                _facture = value;
                OnPropertyChanged(nameof(SelectedFacture));
            }
        }


        public Article SelectedArticleToDelete { get; set; }
        

        public void Exit (ModifieFactureWindow thisWin)
        {
            ManagementFactureWindow window = new ManagementFactureWindow();
            window.Show();
            thisWin.Close();
        }


        public void Delete (ModifieFactureWindow thisWin)
        {

            if (MessageBox.Show("êtes vous sûr de vouloir supprimer la facture N° " + SelectedFacture.Id + " ?", "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (DAOFactory.GetFactureDAO.delete(SelectedFacture))
                {
                    ManagementFactureWindow window = new ManagementFactureWindow();
                    window.Show();
                    thisWin.Close();
                }//end if 
            }
            

        }//end Delete


        public void Chenge_Client (ModifieFactureWindow thisWin)
        {
            ChangeClientInFactureWindow win = new ChangeClientInFactureWindow(SelectedFacture);
            win.Show();
            thisWin.Close();
        }//end chenge-client


        public void ChangeEmpt (ModifieFactureWindow thisWin)
        {
            ChangeEmployeeInFactureWindow win = new ChangeEmployeeInFactureWindow(SelectedFacture);
            win.Show();
            thisWin.Close();

        }//end ChengeEmpt



        public void AddArticle(ModifieFactureWindow window)
        {

            AddArticleToFactureWindow add = new AddArticleToFactureWindow(SelectedFacture);
            add.Show();
            window.Close();
        }//end AddArticle



        public void DataGrid_MouseClick(ModifieFactureWindow window)
        {
            
            if(SelectedArticleToDelete!= null)

            {
                int id = SelectedArticleToDelete.Id; 
                int quantity = SelectedArticleToDelete.Quantity;

                if (MessageBox.Show("Les modifications vont etre faite sur votre DB,  etes-vous  sur de suprimé l'article N° " + SelectedArticleToDelete.Id + " de la facture N° " + SelectedFacture.Id, "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    SelectedFacture.Delete_art_facture(id);
                    SelectedFacture.Date = DateTime.Now;

                    if (DAOFactory.GetFactureDAO.Delete_art_facture(SelectedFacture, id, quantity))
                    {

                        ModifieFactureWindow win = new ModifieFactureWindow(SelectedFacture);
                        win.Show();
                        window.Close();

                    }//end if 
                }//end if 
            }
            else
            {
                MessageBox.Show("L'aricle est null !! ");

            }//end if 

        }//end DataGrid_MouseClick

        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged


    }//end class 
}//end namespace 
