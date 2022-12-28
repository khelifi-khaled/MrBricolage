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
