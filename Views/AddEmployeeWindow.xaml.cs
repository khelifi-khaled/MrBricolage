using MrBricolage.ViewModels;
using System.Windows;


namespace MrBricolage.Views
{
    /// <summary>
    /// Logique d'interaction pour AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        AddEmployeeVM AddEmpVm { get; set; }


        public AddEmployeeWindow()
        {
            AddEmpVm = new AddEmployeeVM();
            DataContext= AddEmpVm;
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            AddEmpVm.Exite(this);

        }//end Exit_Click

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            AddEmpVm.Save(this);


        }//end Save_Click
    }//end class
}//end namespace 
