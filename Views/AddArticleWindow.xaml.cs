using MrBricolage.ViewModels;
using System.Windows;



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
