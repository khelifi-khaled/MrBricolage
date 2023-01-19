using MrBricolage.Models;
using MrBricolage.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

using System.Windows;

namespace MrBricolage.ViewModels
{
    public  class LoginVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;



        public LoginVM(ObservableCollection <Employee> employees) 
        {
            this.Employees = employees;
            this.EmployeeConnected = new Employee();

        }

        public ObservableCollection<Employee> Employees { get; set; }   


        public Employee EmployeeConnected { get; set; }




        public void BTN_Cancel(LoginWindow win )
        {
            MainWindow main = new MainWindow();
            main.Show();
            win.Close();
        }//end BTN_Cancel


        public void BTN_Login(LoginWindow win)
        {
            foreach(Employee e in this.Employees)
            {
                
                if (e.Login.Equals(this.EmployeeConnected.Login))
                {
                    if (e.Password.Equals(this.EmployeeConnected.Password))
                    {
                        this.EmployeeConnected = e;
                        EmployeeWindow empWin = new EmployeeWindow(this.EmployeeConnected);
                        empWin.Show();
                        win.Close();
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Login ou Password est incorrect, vous devez vérifier vos infos SVP.");
                        break;
                    }
                }else
                {
                    MessageBox.Show("Login ou Password est incorrect, vous devez vérifier vos infos SVP.");
                    break;
                }//end if 
            }//end foreach loop 

        }//end BTN_Login



        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged


    }//end class 
}//end namespace 
