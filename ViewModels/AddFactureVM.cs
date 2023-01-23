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

namespace MrBricolage.ViewModels
{
    public  class AddFactureVM : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;

        private Facture _factureToCreate;
        private Client _clientFacture;
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


        public Client ClientFacture
        {

            get => _clientFacture;
            set
            {
                _clientFacture = value;
                OnPropertyChanged(nameof(ClientFacture));
            }
        }




        public Employee EmployeeFacture
        {

            get => _employeeFacture;
            set
            {
                _employeeFacture = value;
                OnPropertyChanged(nameof(EmployeeFacture));
            }
        }



        public Article SelectedArticleToDelete { get;set; }





        public void Exit (AddFactureWindow win)
        {
            ManagementFactureWindow window = new ManagementFactureWindow();
            window.Show();
            win.Close();

        }//end Exit

        public void AddArticle()
        {
            AddArticleToNewFactureWindow addArticle = new AddArticleToNewFactureWindow(FactureToCreate);
            addArticle.Show();
        }


        public void GetClient (AddFactureWindow win,KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (int.TryParse(win.Client_Id.Text, out int val))
                {
                    ClientFacture = DAOFactory.GetClientDAO.find(val);
                    FactureToCreate.Client = ClientFacture;

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
                    EmployeeFacture = DAOFactory.GetEmployeeDAO.find(val);
                    FactureToCreate.Employee = EmployeeFacture;
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
