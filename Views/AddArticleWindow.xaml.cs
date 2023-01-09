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
    
    public partial class AddArticleWindow : Window
    {


         
        AddArticleVM AddArticleVM { get;set; }


        public AddArticleWindow()
        {
            this.AddArticleVM = new AddArticleVM();
            DataContext = this.AddArticleVM;
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            AddArticleVM.Exit(this);

        }//end Exit_Click

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            AddArticleVM.Save(this);

        }//end Save_Click
    }//end class 
}//end namespace 
