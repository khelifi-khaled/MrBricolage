﻿using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MrBricolage.ViewModels
{
    public  class AddEmployeeVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;



        public AddEmployeeVM()
        {
            EmployeeToAdd = new Employee();
        }






        public Employee EmployeeToAdd { get; set; }



        public void Exite (AddEmployeeWindow win)
        {
            ManagementEmpWindow window = new ManagementEmpWindow();
            window.Show();
            win.Close();
        }//end exite



        public void Save(AddEmployeeWindow win)
        {
            if (EmployeeToAdd!=null)
            {
                if (DAOFactory.GetEmployeeDAO.CheckExistedEmployeeFullName(EmployeeToAdd))//check if the EmployeeToAdd whith the same name and same f_name exist in our DB
                {
                   if (DAOFactory.GetEmployeeDAO.CheckEmployeeStatus(EmployeeToAdd))
                    {
                        //if the Emlpoyee with the same name and same f_name exist in our DB and he is active as well.
                        MessageBox.Show("l'employee " + EmployeeToAdd.Name + " " + EmployeeToAdd.F_Name + " existe deja dans votre DB, et il est bien active ! ", "Infos");
                        

                    }else
                    {
                        //in this case, the employee with the same name and same f_name exist in our db, he is just inactive, i will propose to my user if he wan to activate it or no
                        if (MessageBox.Show("L'employee " + EmployeeToAdd.Name + " " + EmployeeToAdd.F_Name + " existe deja dans votre DB, voulez vous l'activé  ?", "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            if (DAOFactory.GetEmployeeDAO.UpdateEmployeeStatus(EmployeeToAdd, true))
                            {
                                MessageBox.Show("L'employee " + EmployeeToAdd.Name + " " + EmployeeToAdd.F_Name + " est active maintenant ! ", "infos");
                                ManagementEmpWindow window = new ManagementEmpWindow();
                                window.Show();
                                win.Close();
                            }//end if 
                            
                        }//end if 
                    }//end if 
                }
                else
                {
                    //in this block, EmployeeToAdd whith the same name and same f_name don't exist in our DB, i will check if Login EmployeeToAdd exist in our DB 
                    if (DAOFactory.GetEmployeeDAO.CheckEmployeeLogin(EmployeeToAdd))//check if the Employee Login is Unique 
                    {
                        MessageBox.Show("Login existe deja dans la DB , changé votre Login SVP !", "Infos");
                    }else
                    {
                        if (DAOFactory.GetEmployeeDAO.create(EmployeeToAdd))
                        {
                            MessageBox.Show("L'employee " + EmployeeToAdd.Name + " " + EmployeeToAdd.F_Name + " est bien cree dans votre DB ! ", "infos");
                            ManagementEmpWindow window = new ManagementEmpWindow();
                            window.Show();
                            win.Close();
                        }//end if 
                    }//end if 
                }

            }else
            {
                MessageBox.Show("L'employeeest a null !");

            }//end if 
        }//end save



        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged

    }//end class 
}//end name space 
