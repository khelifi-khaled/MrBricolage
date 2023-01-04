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
