using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MrBricolage.ViewModels
{
    public class ManagementEmployeeVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        


        public ManagementEmployeeVM()
        {
            Employees = DAOFactory.GetEmployeeDAO.findAll();
            EmployeeSelected = new Employee();
        }




        public ObservableCollection<Employee> Employees
        {
            get;set;
        }





        public Employee EmployeeSelected 
        {
            get; set;
        }




        public void DataGrid_MouseClick(ManagementEmpWindow win)
        {
            ModifieEmployeeWindow window = new ModifieEmployeeWindow(EmployeeSelected);
            window.Show();
            win.Close();
        }//end DataGrid_MouseClick

        public void AddEmployee(ManagementEmpWindow win)
        {
            AddEmployeeWindow window = new AddEmployeeWindow();
            window.Show();
            win.Close();
        }
       



        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged


    }//end class
}//end namespace 
