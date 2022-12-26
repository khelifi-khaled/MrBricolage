using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBricolage.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public MainWindowVM()
        { 
            Employees = DAOFactory.GetEmployeeDAO.findAll();

        }
       


        public ObservableCollection<Employee> Employees { get; set; }



        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged
 


    }//end class 
}//end namespace 
