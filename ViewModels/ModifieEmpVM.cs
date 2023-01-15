using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MrBricolage.ViewModels
{
    public  class ModifieEmpVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        public ModifieEmpVM(Employee employeeSelected )
        {
            EmployeeSelected = employeeSelected;
           
            
        }



        public Employee EmployeeSelected { get; set; }




        public void Exit(ModifieEmployeeWindow win )
        {
            ManagementEmpWindow window = new ManagementEmpWindow();
            window.Show();
            win.Close();
        }//Exite




        public void Save(ModifieEmployeeWindow win)
        {
            if (EmployeeSelected != null)
            {
                if (DAOFactory.GetEmployeeDAO.update(EmployeeSelected))
                {
                    MessageBox.Show("L'update a bien été effectué sur l'employé " + EmployeeSelected.Name + " " + EmployeeSelected.F_Name);
                    ManagementEmpWindow window = new ManagementEmpWindow();
                    window.Show();
                    win.Close();
                }//end if 
            }else
            {
                MessageBox.Show("L'employee est null !!! ");
            }
        }//end Save


        public void Delete(ModifieEmployeeWindow win)
        {
            if (EmployeeSelected != null)
            {
                if (DAOFactory.GetEmployeeDAO.CheckExistedEmployeeInFacture(EmployeeSelected))//if the Employee existe in facture
                {
                    if (DAOFactory.GetEmployeeDAO.CheckEmployeeStatus(EmployeeSelected))//if the employee is active 
                    {
                        if (MessageBox.Show("vous ne pouvez pas supprimé l'employee num " + EmployeeSelected.Id + " car il existe dans des factures, voulez vous le randre inactive ?", "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            if (DAOFactory.GetEmployeeDAO.UpdateEmployeeStatus(EmployeeSelected, false))
                            {
                                MessageBox.Show("L'employee num " + EmployeeSelected.Id + " n'est pas supprimé de votre DB, mais il est inactive, donc vous ne pouvez plus l'insere dans une facture", "infos");
                                ManagementEmpWindow window = new ManagementEmpWindow();
                                window.Show();
                                win.Close();
                            }//end if 
                        }//end if 
                    }
                    else // the Employee existe in facture and he is inactive 
                    {
                        MessageBox.Show("Vous ne pouvez pas supprimé l'Employee num " + EmployeeSelected.Id + " de votre DB car il existe dans des facture, et il est déjà inactive.", "infos");
                    }//end if 
                }else
                {
                    if (DAOFactory.GetEmployeeDAO.delete(EmployeeSelected))
                    {
                        MessageBox.Show("L'employé " + EmployeeSelected.Name + " " + EmployeeSelected.F_Name + " a bien été suprimé de votre DB !");
                        ManagementEmpWindow window = new ManagementEmpWindow();
                        window.Show();
                        win.Close();
                    }//end if 
                }//end if 


            }else
            {
                MessageBox.Show("L'employee est null !!! ");
            }

         
        }//end Delete





            




        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged



    }//end class 
}//end namespace 
