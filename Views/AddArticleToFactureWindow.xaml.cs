using MrBricolage.Models;
using MrBricolage.ViewModels;
using System.Windows;
using System.Windows.Input;





namespace MrBricolage.Views
{
    
    public partial class AddArticleToFactureWindow : Window
    {
        AddArticleToFactureVM AddArticleVM { get; set; }


        public AddArticleToFactureWindow(Facture facture)
        {
            AddArticleVM = new AddArticleToFactureVM(facture);
            DataContext = AddArticleVM;
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            AddArticleVM.Exit(this);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddArticleVM.AddArticleBTN(this);
        }

        private void Article_Number_KeyDown(object sender, KeyEventArgs e)
        
        {
            AddArticleVM.Getarticle(this, e);
        }
    }
}
