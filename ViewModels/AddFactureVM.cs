using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace MrBricolage.ViewModels
{
    public  class AddFactureVM :  INotifyPropertyChanged 
    {


        public event PropertyChangedEventHandler PropertyChanged;

        private Facture _factureToCreate;
        private Employee _employeeFacture;

        public AddFactureVM(Facture facture)
        {
            FactureToCreate = facture;
        }


        


        public Facture FactureToCreate 
        {
            get => _factureToCreate;
            set
            {
                _factureToCreate= value;
                OnPropertyChanged(nameof(FactureToCreate));
            }
        }


      



        public Article SelectedArticleToDelete { get;set; }





        public void Exit (AddFactureWindow win)
        {
            ManagementFactureWindow window = new ManagementFactureWindow();
            window.Show();
            win.Close();

        }//end Exit

        public void AddArticle(AddFactureWindow win)
        {
           if (FactureToCreate.Client ==null)
            {
                MessageBox.Show("Vous devez choisir un client avant de commencer a choisir des articles ! ");
            }
            else if(FactureToCreate.Employee == null)
            {
                MessageBox.Show("Vous devez choisir un employée avant de commencer a choisir des articles ! ");
            }
            else
            {
                AddArticleToNewFactureWindow addArticle = new AddArticleToNewFactureWindow(FactureToCreate);
                addArticle.Show();
                win.Close();
            }
        }


        public void DataGrid_MouseClick(AddFactureWindow win)
        {
            if(SelectedArticleToDelete != null)
            {
                FactureToCreate.Delete_art_facture(SelectedArticleToDelete.Id);
                AddFactureWindow add = new AddFactureWindow(FactureToCreate);
                add.Show();
                win.Close();
            }
        }//end DataGrid_MouseClick

        public void GetClient (AddFactureWindow win,KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (int.TryParse(win.Client_Id.Text, out int val))
                {
                    FactureToCreate.Client = DAOFactory.GetClientDAO.find(val);
                    AddFactureWindow window = new AddFactureWindow(FactureToCreate);
                    window.Show();
                    win.Close();
                }
                else
                {
                    MessageBox.Show("Format d'Id est incorrecte !!");
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
                    AddFactureWindow window = new AddFactureWindow(FactureToCreate);
                    window.Show();
                    win.Close();
                }
                else
                {
                    MessageBox.Show("Format d'Id est incorrecte !!");
                }//end if 
            }//end if 


        }//end GetEmployee


        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged

    }//end class 
}//end namespace 
