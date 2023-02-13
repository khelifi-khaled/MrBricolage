using MrBricolage.ViewModels;
using System.Windows;
using System.Windows.Input;
namespace MrBricolage.Views
{
    
    public partial class ManagementFactureWindow : Window
    {
        ManagementFactureVM  ManagementFactureVM { get; set; }


        public ManagementFactureWindow()
        {
            this.ManagementFactureVM = new ManagementFactureVM();
            DataContext = this.ManagementFactureVM;
            InitializeComponent();
        }

        private void AddFacture_Click(object sender, RoutedEventArgs e)
        {
            this.ManagementFactureVM.AddFacture(this);
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.ManagementFactureVM.DataGrid_Click(this);
        }
    }//end class 
}//end namespace 
