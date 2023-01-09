using MrBricolage.Models;
using MrBricolage.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;


namespace MrBricolage.Views
{
    
    public partial class ModifieEmployeeWindow : Window
    {
        ModifieEmpVM ModifieVM { get; set; }

        public ModifieEmployeeWindow(Employee employee )
        {
            ModifieVM = new ModifieEmpVM(employee);
            DataContext = ModifieVM;
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            ModifieVM.Exit(this);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ModifieVM.Save(this);
        }//end Save_Click

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ModifieVM.Delete(this);
        }
    }//end class 
}//end namespace 
