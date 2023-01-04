using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (DAOFactory.GetEmployeeDAO.create(EmployeeToAdd))
            {
                ManagementEmpWindow window = new ManagementEmpWindow();
                window.Show();
                win.Close();

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
