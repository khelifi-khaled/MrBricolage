using MrBricolage.ViewModels;
using System.Windows;


namespace MrBricolage.Views
{
    /// <summary>
    /// Logique d'interaction pour SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        SignInVM SignInVM { get; set; }




        public SignInWindow()
        {
            this.SignInVM = new SignInVM();
            DataContext= this.SignInVM;
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.SignInVM.Close(this);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.SignInVM.Save(this);
        }
    }//end class 
}//end name space 
