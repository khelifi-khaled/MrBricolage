using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace MrBricolage.ViewModels
{
    public  class EmployeeWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        

        public EmployeeWindowVM(Employee employeeConnected )
        {
            this.EmployeeConnected = employeeConnected;
            this.Employees = DAOFactory.GetEmployeeDAO.findAll();
        }


        public Employee EmployeeConnected
        {
            get; set; 
        }
        

         

        public ObservableCollection<Employee> Employees { get; set; }





        public void Sign_out (EmployeeWindow myWin)
        {

            MainWindow main = new MainWindow();
            main.Show();
            myWin.Close();

        }//end sing_out



        public void change_user(EmployeeWindow Win)
        {
            LoginWindow login = new LoginWindow(Employees);
            login.Show();
            Win.Close();

        }//end change_user



        public void managementEmployee()
        {
            ManagementEmpWindow managementEmp = new ManagementEmpWindow();
            managementEmp.Show();
           

        }//end  managementEmployee




        public void ManagementArticle ()
        {
            ManagementArtWindow window = new ManagementArtWindow();
            window.Show();
        }




        public void ManagementClient()
        {
            ManagementClientWindow window = new ManagementClientWindow();
            window.Show();
        }


        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged



    }//end class
}//end name space 
