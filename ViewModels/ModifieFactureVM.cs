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




        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged


    }//end class 
}//end namespace 
