using MrBricolage.Models;
using MrBricolage.ViewModels;
using System.Windows;
using System.Windows.Input;
namespace MrBricolage.Views
{
    
    public partial class AddFactureWindow : Window
    {

        AddFactureVM AddFactureVM { get; set; }

        public AddFactureWindow(Facture facture )
        {
            this.AddFactureVM = new AddFactureVM(facture);
            DataContext= this.AddFactureVM;
            InitializeComponent();
        }

        private void Client_Id_KeyDown(object sender, KeyEventArgs e)
        {
           
            this.AddFactureVM.GetClient(this,e);
        }

        private void Employee_Id_KeyDown(object sender, KeyEventArgs e)
        
        {
            this.AddFactureVM.GetEmployee(this,e);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.AddFactureVM.Exit(this);
        }

        private void AddArticle_Click(object sender, RoutedEventArgs e)
        {
            this.AddFactureVM.AddArticle(this);
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.AddFactureVM.DataGrid_MouseClick();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.AddFactureVM.AddFacture(this);
        }
    }//end class 
}//end name space 
