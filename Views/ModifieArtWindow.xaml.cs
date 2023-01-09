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
    
    public partial class ModifieArtWindow : Window
    {


        ModifieArtVM  ModifieArtVM { get; set; }



        public ModifieArtWindow(Article art)
        {
            this.ModifieArtVM = new ModifieArtVM(art);
            DataContext = this.ModifieArtVM;
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.ModifieArtVM.Exit(this);

        }//end Exit_Click

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            this.ModifieArtVM.Delete(this);

        }//end Delete_Click

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.ModifieArtVM.Save(this);

        }//end Save_Click





    }//end class 
}//end namespace 
