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
