using MrBricolage.ViewModels;
using System.Windows;


namespace MrBricolage.Views
{
   
    public partial class AddClientWindow : Window
    {
        AddClientVM AddClientVM { get; set; }

        public AddClientWindow()
        {
            this.AddClientVM = new AddClientVM();
            DataContext = this.AddClientVM;
            InitializeComponent();
        }





        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.AddClientVM.Exit(this);

        }//end Exit_Click





        private void Save_Click(object sender, RoutedEventArgs e)
        {

            this.AddClientVM.Save(this);

        }//end Save_Click


    }//end class 
}//end namespace 
