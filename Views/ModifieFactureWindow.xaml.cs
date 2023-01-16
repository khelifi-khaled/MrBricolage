using MrBricolage.Models;
using MrBricolage.ViewModels;
using System;
using System.Collections.Generic;
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
            this.ModifieFactureVM.AddArticle();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }
    }//end class 
}//end namespace  
