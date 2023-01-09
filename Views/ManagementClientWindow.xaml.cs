using MrBricolage.ViewModels;
using System.Windows;






namespace MrBricolage.Views
{
   
    public partial class ManagementClientWindow : Window
    {
        ManagementClientVM ManagementClientVM { get; set; }

        public ManagementClientWindow()
        {
            this.ManagementClientVM = new ManagementClientVM();
            DataContext = this.ManagementClientVM;
            InitializeComponent();
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        
    }//end class
}//end namespace 
