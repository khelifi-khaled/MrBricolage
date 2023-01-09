using MrBricolage.Models;
using MrBricolage.ViewModels;
using System.Windows;


namespace MrBricolage.Views
{
    
    public partial class ModifieClientWindow : Window
    {
        ModifieClientVM ModifieClientVM { get; set; }

        public ModifieClientWindow(Client selectedClient)
        {
            this.ModifieClientVM = new ModifieClientVM(selectedClient);
            DataContext = this.ModifieClientVM;
            InitializeComponent();
        }




        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.ModifieClientVM.Exit(this);

        }//end Exit_Click



        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            this.ModifieClientVM.Delete(this);

        }//end Delete_Click



        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.ModifieClientVM.Save(this);

        }//end Save_Click



    }//end class 
}//end naespace 
