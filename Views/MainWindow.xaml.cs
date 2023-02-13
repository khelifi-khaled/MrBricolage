using MrBricolage.ViewModels;
using MrBricolage.Views;
using System.Windows;


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

        }//end register_Click

       
    }//end class 
}//end MainWindow 
