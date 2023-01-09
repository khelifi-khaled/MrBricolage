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
    

    public partial class ManagementArtWindow : Window
    {
        ManagementArticleVM ManagementArtVM { get; set; }

        public ManagementArtWindow()
        {
            ManagementArtVM = new ManagementArticleVM();
            DataContext = ManagementArtVM;
            InitializeComponent();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ManagementArtVM.DataGrid_Click(this);

        }//end DataGrid_MouseDoubleClick



        private void AddArt_Click(object sender, RoutedEventArgs e)
        {

            ManagementArtVM.Add_art(this);

        }
    }//end class 
}//end namespace 
