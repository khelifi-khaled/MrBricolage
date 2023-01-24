using MrBricolage.Models;
using MrBricolage.ViewModels;
using System.Windows;
using System.Windows.Input;
namespace MrBricolage.Views
{
    /// <summary>
    /// Logique d'interaction pour AddArticleToNewFactureWindow.xaml
    /// </summary>
    public partial class AddArticleToNewFactureWindow : Window
    {

        AddArticleToNewFactureVM AddArticleToNewFactureVM { get; set; }


        public AddArticleToNewFactureWindow(Facture facture)
        {
            this.AddArticleToNewFactureVM = new AddArticleToNewFactureVM(facture);
            DataContext = this.AddArticleToNewFactureVM;
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.AddArticleToNewFactureVM.Exit(this);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.AddArticleToNewFactureVM.AddArticleBTN(this);
        }

        private void Article_Number_KeyDown(object sender, KeyEventArgs e)
        {
            this.AddArticleToNewFactureVM.Getarticle(this, e);
        }
    }
}
