using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System;
using System.ComponentModel;
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
                    MessageBox.Show("Format d'ID incorrect !!");
                }//end if 
            }//end if 

        }//end GetEmployee


        public void ChangeEmployeeBTN(ChangeEmployeeInFactureWindow thisWin)
        {
            if (EmployeeToRepace != null)
            {
                if (DAOFactory.GetEmployeeDAO.CheckEmployeeStatus(EmployeeToRepace))
                {
                    FactureToModifie.Employee = EmployeeToRepace;
                    FactureToModifie.Date= DateTime.Now;
                    if (DAOFactory.GetFactureDAO.Update_employee_facture(FactureToModifie))
                    {
                        MessageBox.Show("L'employé " + EmployeeToRepace.F_Name + " " + EmployeeToRepace.Name + " est bien inséré dans la facture N° " + FactureToModifie.Id + " dans votre DB", "infos");
                        
                        ModifieFactureWindow window = new ModifieFactureWindow(FactureToModifie);
                        window.Show();
                        thisWin.Close();
                    }//end if 
                    
                }
                else
                {
                    MessageBox.Show("Vous ne pouvez pas insérer l'employé  " + EmployeeToRepace.F_Name + " " + EmployeeToRepace.Name + " car il est inactif dans votre DB", "infos");
                }//end if 

            }else
            {
                MessageBox.Show("Vous ne pouvez pas insérer un  employé vide à la place de " + FactureToModifie.Employee.F_Name + " " + FactureToModifie.Employee.Name, "infos");
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
