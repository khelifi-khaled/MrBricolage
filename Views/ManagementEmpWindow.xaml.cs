using MrBricolage.ViewModels;
using System.Windows;


namespace MrBricolage.Views
{


    public partial class ManagementEmpWindow : Window
    {

        ManagementEmployeeVM ManagementEmpVM { get; set; }


        public ManagementEmpWindow()
        {
            ManagementEmpVM = new ManagementEmployeeVM();
            DataContext = ManagementEmpVM;
            InitializeComponent();
        }


        


        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {

            ManagementEmpVM.AddEmployee(this);

        }//end AddEmployee_Click 

        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.ManagementEmpVM.DataGrid_MouseClick(this);
        }

        
    }//end class 
}//end namaspace 
