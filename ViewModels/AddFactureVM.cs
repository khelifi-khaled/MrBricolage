using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

namespace MrBricolage.ViewModels
{
    public class AddFactureVM : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;

        private Facture _factureToCreate;

        public AddFactureVM(Facture facture)
        {
            FactureToCreate = facture;
        }





        public Facture FactureToCreate
        {
            get => _factureToCreate;
            set
            {
                _factureToCreate = value;
                OnPropertyChanged(nameof(FactureToCreate));
            }
        }






        public Article SelectedArticleToDelete { get; set; }





        public void Exit(AddFactureWindow win)
        {
            ManagementFactureWindow window = new ManagementFactureWindow();
            window.Show();
            win.Close();

        }//end Exit

        public void AddArticle(AddFactureWindow win)
        {
            if (FactureToCreate.Client == null)
            {
                MessageBox.Show("Vous devez choisir un client avant de commencer à choisir des articles ! ");
            }
            else if (FactureToCreate.Employee == null)
            {
                MessageBox.Show("Vous devez choisir un employé avant de commencer à choisir des articles ! ");
            }
            else
            {

                AddArticleToNewFactureWindow addArticle = new AddArticleToNewFactureWindow(FactureToCreate);
                addArticle.Show();
                win.Close();
            }
        }


        public void DataGrid_MouseClick()
        {
            if (SelectedArticleToDelete != null)
            {
                FactureToCreate.Delete_art_facture(SelectedArticleToDelete.Id);
                OnPropertyChanged(nameof(FactureToCreate));
            }
        }//end DataGrid_MouseClick

        public void GetClient(AddFactureWindow win, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (int.TryParse(win.Client_Id.Text, out int val))
                {
                    FactureToCreate.Client = DAOFactory.GetClientDAO.find(val);
                    OnPropertyChanged(nameof(FactureToCreate));
                }
                else
                {
                    MessageBox.Show("Format d'ID incorrect !!");
                }//end if 
            }//end if 
        }


        public void GetEmployee(AddFactureWindow win, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (int.TryParse(win.Employee_Id.Text, out int val))
                {
                    FactureToCreate.Employee = DAOFactory.GetEmployeeDAO.find(val);
                    OnPropertyChanged(nameof(FactureToCreate));
                }
                else
                {
                    MessageBox.Show("Format d'ID incorrect !!");
                }//end if 
            }//end if 


        }//end GetEmployee


        public void AddFacture(AddFactureWindow win)
        {
            if (FactureToCreate.Client==null || FactureToCreate.Employee ==null || FactureToCreate.Articles.Count == 0)
            {
                MessageBox.Show("Vous devez remplir tous les infos SVP !");
            }
            else
            {
                if (DAOFactory.GetFactureDAO.create(FactureToCreate))
                {
                    ManagementFactureWindow window = new ManagementFactureWindow();
                    window.Show();
                    win.Close();
                }
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
