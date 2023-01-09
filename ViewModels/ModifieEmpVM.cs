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
            if (DAOFactory.GetEmployeeDAO.update(EmployeeSelected))
            {
                MessageBox.Show("L'update a bien été effectué sur l'employé " + EmployeeSelected.Name + " " + EmployeeSelected.F_Name );
                ManagementEmpWindow window = new ManagementEmpWindow();
                window.Show();
                win.Close();
            }else
            {
                MessageBox.Show("Problem d'update de l'employé :" + EmployeeSelected.Name + " " + EmployeeSelected.F_Name);
            }
          
        }//end Save


        public void Delete(ModifieEmployeeWindow win)
        {

            if (DAOFactory.GetEmployeeDAO.delete(EmployeeSelected))
            {
                ManagementEmpWindow window = new ManagementEmpWindow();
                window.Show();
                win.Close();
            }//end if 
         
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
