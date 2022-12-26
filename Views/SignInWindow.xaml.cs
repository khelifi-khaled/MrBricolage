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
    /// Logique d'interaction pour SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        SignInVM signInVM { get; set; }




        public SignInWindow()
        {
            signInVM = new SignInVM();
            DataContext= signInVM;
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            signInVM.Close(this);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            signInVM.save(this);
        }
    }//end class 
}//end name space 
