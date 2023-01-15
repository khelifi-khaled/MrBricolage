using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MrBricolage.ViewModels
{
    public class ChengeEmployeeInFactureVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        private Facture _facture;

        private Employee _employeeToRepace;



        public ChengeEmployeeInFactureVM(Facture factureToModifie)
        {
            FactureToModifie = factureToModifie;
        }









        public Facture FactureToModifie
        {
            get => _facture;
            set
            {
                _facture = value;
                OnPropertyChanged(nameof(FactureToModifie));
            }
        }


        public Employee EmployeeToRepace
        {
            get => _employeeToRepace;
            set
            {
                _employeeToRepace = value;
                OnPropertyChanged(nameof(EmployeeToRepace));
            }
        }





        public void GetEmployee(ChangeEmployeeInFactureWindow win, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (int.TryParse(win.Employee_Id.Text, out int val))
                {
                    EmployeeToRepace = DAOFactory.GetEmployeeDAO.find(val);
                }
                else
                {
                    MessageBox.Show("Format d'Id est incorrecte !!");
                }//end if 
            }//end if 

        }//end GetEmployee


        public void ChangeEmployeeBTN(ChangeEmployeeInFactureWindow thisWin)
        {
            if (EmployeeToRepace != null)
            {
                FactureToModifie.Employee = EmployeeToRepace;
                ModifieFactureWindow window = new ModifieFactureWindow(FactureToModifie);
                window.Show();
                thisWin.Close();

            }else
            {
                MessageBox.Show("pour ne pouvez pas inserer un  employee vide a la place de " + FactureToModifie.Employee.F_Name + " " + FactureToModifie.Employee.Name, "infos");
            }//end if 
        }//end ChangeEmployeeBTN



        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged

    }//end class 
}//end namespace 
