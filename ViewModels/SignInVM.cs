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
    public class SignInVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;




        public SignInVM()
        {
            this.ThisEmployee = new Employee();
        }


       

        public Employee ThisEmployee { get; set; }



        public void Close (SignInWindow myWindow)
        {
            myWindow.Close();
        }

        public void save(SignInWindow myWindow)
        {

            if (DAOFactory.GetEmployeeDAO.create(ThisEmployee))
            {
                MainWindow main = new MainWindow();
                main.Show();
                myWindow.Close();
            }

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
