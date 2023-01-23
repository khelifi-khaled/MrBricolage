using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MrBricolage.ViewModels
{
    public  class ManagementFactureVM : INotifyPropertyChanged
    {

        private Facture _selectedFacture;


        public event PropertyChangedEventHandler PropertyChanged;


        public ManagementFactureVM ()
        {
            Factures = DAOFactory.GetFactureDAO.findAll();
            SelectedFacture = new Facture();
        }    



        public ObservableCollection<Facture>  Factures { get; set; }


        public Facture SelectedFacture
        {
            get => _selectedFacture;
            set
            {
                _selectedFacture = value;
                OnPropertyChanged(nameof(SelectedFacture));
            }
        }



        public void DataGrid_Click (ManagementFactureWindow win)
        {
            ModifieFactureWindow window = new ModifieFactureWindow(SelectedFacture);
            window.Show();
            win.Close();

        }//end DataGrid_Click



        public void AddFacture(ManagementFactureWindow win)
        {
            Facture factureToAdd = new Facture();
            AddFactureWindow add = new AddFactureWindow(factureToAdd);
            add.Show();
            win.Close();
        }



        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged


    }//end class 
}//end namespace 
