using MrBricolage.Models;
using MrBricolage.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;


namespace MrBricolage.Views
{
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginVM LoginVM { get;set; }

        public LoginWindow(ObservableCollection<Employee> employees)
        {
            LoginVM= new LoginVM(employees);
            DataContext= LoginVM;
            InitializeComponent();
        }

        private void Btnlogin_Click(object sender, RoutedEventArgs e)
        {
            LoginVM.BTN_Login(this);
        }

        private void BTNcancel_Click(object sender, RoutedEventArgs e)
        {
            LoginVM.BTN_Cancel(this);
        }
    }
}
