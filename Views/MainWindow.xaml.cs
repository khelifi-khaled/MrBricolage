using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.ViewModels;
using MrBricolage.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MrBricolage
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindowVM MainVM { get; set; }



        public MainWindow()
        {
            MainVM = new MainWindowVM();
            DataContext = MainVM;
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }//end Exit_Click

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            MainVM.Login(this);

        }//end Login_Click

        private void register_Click(object sender, RoutedEventArgs e)
        {
            SignInWindow win = new SignInWindow();
            win.Show();
            this.Close();

        }
    }//end class 
}//end MainWindow 
