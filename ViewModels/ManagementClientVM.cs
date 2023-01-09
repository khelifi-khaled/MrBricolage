using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace MrBricolage.ViewModels
{
    public class ManagementClientVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        public ManagementClientVM ()
        {
            Clients = DAOFactory.GetClientDAO.findAll();
            SelectedClient = new Client();
        }




        public ObservableCollection<Client> Clients { get; set; }

        public Client SelectedClient { get; set; }






        public void DataGrid_Click (ManagementClientWindow window )
        {
            ModifieClientWindow modifie = new ModifieClientWindow(SelectedClient);
            modifie.Show();
            window.Close();
        }//end DataGrid_Click





        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged

    }//end class 
}//end namespace 
