using MrBricolage.Models;
using MrBricolage.Views;
using System.ComponentModel;


namespace MrBricolage.ViewModels
{
    public  class ModifieFactureVM : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;

        private Facture _facture;

        public ModifieFactureVM(Facture facture )
        {
            SelectedFacture = facture;
        }



        public Facture SelectedFacture
        {
            get => _facture;
            set
            {
                _facture = value;
                OnPropertyChanged(nameof(SelectedFacture));
            }
        }



        

        public void Exit (ModifieFactureWindow thisWin)
        {
            ManagementFactureWindow window = new ManagementFactureWindow();
            window.Show();
            thisWin.Close();
        }

        public void Chenge_Client (ModifieFactureWindow thisWin)
        {
            ChangeClientInFactureWindow win = new ChangeClientInFactureWindow(SelectedFacture);
            win.Show();
            thisWin.Close();
        }//end chenge-client


        public void ChangeEmpt (ModifieFactureWindow thisWin)
        {
            ChangeEmployeeInFactureWindow win = new ChangeEmployeeInFactureWindow(SelectedFacture);
            win.Show();
            thisWin.Close();

        }//end ChengeEmpt



        public void AddArticle(ModifieFactureWindow window)
        {

            AddArticleToFactureWindow add = new AddArticleToFactureWindow(SelectedFacture);
            add.Show();
            window.Close();
        }//end AddArticle



        public void DataGrid_MouseClick(ModifieFactureWindow window)
        {


        }//end DataGrid_MouseClick

        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged


    }//end class 
}//end namespace 
