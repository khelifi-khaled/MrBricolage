using MrBricolage.Models;
using MrBricolage.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace MrBricolage.Views
{
    
    public partial class ModifieFactureWindow : Window
    {
        ModifieFactureVM ModifieFactureVM { get; set; }

        public ModifieFactureWindow(Facture selectedFacture)
        {
            this.ModifieFactureVM = new ModifieFactureVM(selectedFacture);
            DataContext = this.ModifieFactureVM;
            InitializeComponent();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.ModifieFactureVM.DataGrid_MouseClick(this);
        }

        private void ChengeClientBtn_Click(object sender, RoutedEventArgs e)
        {
            this.ModifieFactureVM.Chenge_Client(this);
        }

        private void ChangeEmptBtn_Click(object sender, RoutedEventArgs e)
        {
            this.ModifieFactureVM.ChangeEmpt(this);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.ModifieFactureVM.Exit(this);
        }

        private void AddArticle_Click(object sender, RoutedEventArgs e)
        {
            this.ModifieFactureVM.AddArticle(this);
        }

        private void BTNDelete_Click(object sender, RoutedEventArgs e)
        {
            this.ModifieFactureVM.Delete(this);
        }
    }//end class 
}//end namespace  
