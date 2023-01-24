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
            this.AddFactureVM.DataGrid_MouseClick(this);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }
    }//end class 
}//end name space 
