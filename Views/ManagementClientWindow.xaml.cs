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

        }//end AddClient_Click




        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ManagementClientVM.DataGrid_Click(this);

        }//end DataGrid_MouseDoubleClick


    }//end class
}//end namespace 
