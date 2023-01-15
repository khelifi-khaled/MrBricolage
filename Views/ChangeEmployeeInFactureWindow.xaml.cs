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
    public partial class ChangeEmployeeInFactureWindow : Window
    {
        ChengeEmployeeInFactureVM ChengeEmployeeInFactureVM { get; set; } 



        public ChangeEmployeeInFactureWindow(Facture facture)
        {
            this.ChengeEmployeeInFactureVM = new ChengeEmployeeInFactureVM(facture);
            DataContext = ChengeEmployeeInFactureVM;
            InitializeComponent();
        }

       

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Employee_Id_KeyDown(object sender, KeyEventArgs e)
        {
            this.ChengeEmployeeInFactureVM.GetEmployee(this,e);
        }

        private void ChangerBTN_Click(object sender, RoutedEventArgs e)
        {
            this.ChengeEmployeeInFactureVM.ChangeEmployeeBTN(this);
        }
    }//end class 
}//end namespace  
