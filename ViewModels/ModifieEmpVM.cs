using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System.ComponentModel;
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
                MessageBox.Show("L'employé est null !!! ");
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
                        if (MessageBox.Show("Vous ne pouvez pas supprimer l'employé num " + EmployeeSelected.Id + " car il existe dans des factures, voulez-vous le rendre inactif ?", "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            if (DAOFactory.GetEmployeeDAO.UpdateEmployeeStatus(EmployeeSelected, false))
                            {
                                MessageBox.Show("L'employé num " + EmployeeSelected.Id + " n'est pas supprimé de votre DB, mais il est inactif, donc vous ne pouvez plus l'insérer dans une facture", "infos");
                                ManagementEmpWindow window = new ManagementEmpWindow();
                                window.Show();
                                win.Close();
                            }//end if 
                        }//end if 
                    }
                    else // the Employee existe in facture and he is inactive 
                    {
                        MessageBox.Show("Vous ne pouvez pas supprimer l'employé num " + EmployeeSelected.Id + " de votre DB car il existe dans des factures, et il est déjà inactif.", "infos");
                    }//end if 
                }else
                {
                    if (DAOFactory.GetEmployeeDAO.delete(EmployeeSelected))
                    {
                        MessageBox.Show("L'employé " + EmployeeSelected.Name + " " + EmployeeSelected.F_Name + " a bien été supprimé de votre DB !");
                        ManagementEmpWindow window = new ManagementEmpWindow();
                        window.Show();
                        win.Close();
                    }//end if 
                }//end if 


            }else
            {
                MessageBox.Show("L'employé est null !!! ");
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
