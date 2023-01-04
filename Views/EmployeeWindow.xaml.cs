using MrBricolage.Models;
using MrBricolage.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MrBricolage.Views
{
    /// <summary>
    /// Logique d'interaction pour EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {

        EmployeeWindowVM EmployeeVM { get; set; }


        public EmployeeWindow(Employee employeeConnected , ObservableCollection <Employee> employees )
        {
            this.EmployeeVM = new EmployeeWindowVM(employeeConnected, employees);
            this.DataContext = this.EmployeeVM;
            InitializeComponent();
        }

        

        private void managementEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeVM.managementEmployee();
        }

        private void managementArticle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void managementFactures_Click(object sender, RoutedEventArgs e)
        {

        }

        private void managementClients_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sign_out_Click(object sender, RoutedEventArgs e)
        {
            EmployeeVM.Sign_out(this);
        }

       

        private void change_user_Click(object sender, RoutedEventArgs e)
        {
            EmployeeVM.change_user(this);
        }

        private void ArchiveEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ArchiveArticle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ArchiveClients_Click(object sender, RoutedEventArgs e)
        {

        }
    }//end class 
}//end name space 
