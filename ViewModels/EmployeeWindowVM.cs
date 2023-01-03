using MrBricolage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBricolage.ViewModels
{
    public  class EmployeeWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        

        public EmployeeWindowVM(Employee employee)
        {
            EmployeeConnected = employee;
        }


        public Employee EmployeeConnected { get; set; }
        














        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged



    }//end class
}//end name space 
