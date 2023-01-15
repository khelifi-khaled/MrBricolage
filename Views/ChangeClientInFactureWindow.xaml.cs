using MrBricolage.Models;
using MrBricolage.ViewModels;
using System.Windows;
using System.Windows.Input;
namespace MrBricolage.Views
{
    
    public partial class ChangeClientInFactureWindow : Window
    {

        ChangeClientInFactureVM ChangeClientInFactureVM { get; set; }
        public ChangeClientInFactureWindow(Facture selectedFacture)
        {
            this.ChangeClientInFactureVM = new ChangeClientInFactureVM(selectedFacture);
            DataContext = this.ChangeClientInFactureVM;
            InitializeComponent();
        }

        private void Client_Id_TextChanged(object sender, KeyEventArgs e)
        {
            
            this.ChangeClientInFactureVM.GetClient(this,e);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeClientInFactureVM.Cancel(this);
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeClientInFactureVM.BtnChange(this);
        }
    }//end class 
}//end namespace 
