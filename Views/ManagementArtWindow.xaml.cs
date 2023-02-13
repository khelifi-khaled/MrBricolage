using MrBricolage.ViewModels;
using System.Windows;
using System.Windows.Input;

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
