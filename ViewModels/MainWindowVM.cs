using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;


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

        public void Login (MainWindow win)
        {
            LoginWindow login = new LoginWindow(Employees);
            login.Show();
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
