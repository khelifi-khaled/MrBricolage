using MrBricolage.Models;
using MrBricolage.ViewModels;
using System.Windows;

namespace MrBricolage.Views
{
    /// <summary>
    /// Logique d'interaction pour EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {

        EmployeeWindowVM EmployeeVM { get; set; }


        public EmployeeWindow(Employee employeeConnected  )
        {
            this.EmployeeVM = new EmployeeWindowVM(employeeConnected);
            this.DataContext = this.EmployeeVM;
            InitializeComponent();
        }

        

        private void managementEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeVM.managementEmployee();
        }

        private void managementArticle_Click(object sender, RoutedEventArgs e)
        {
            EmployeeVM.ManagementArticle();
        }

        private void managementFactures_Click(object sender, RoutedEventArgs e)
        {
            EmployeeVM.ManagementFacture();
        }

        private void managementClients_Click(object sender, RoutedEventArgs e)
        {
            EmployeeVM.ManagementClient();
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
