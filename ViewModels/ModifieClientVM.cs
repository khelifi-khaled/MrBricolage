using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System.ComponentModel;
using System.Windows;

namespace MrBricolage.ViewModels
{
    public class ModifieClientVM : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;


        public ModifieClientVM(Client selectedClient )
        {
            SelectedClient = selectedClient;
        }




        public Client SelectedClient { get; set; }




        public void Exit(ModifieClientWindow window )
        {
            ManagementClientWindow clientWindow = new ManagementClientWindow();
            clientWindow.Show();
            window.Close();
        }//end Exit


        public void Delete(ModifieClientWindow window)
        {
            if (SelectedClient != null)
            {
                if (DAOFactory.GetClientDAO.CheckExistedClientInFacture(SelectedClient))//if the client existe in facture
                {
                    if (DAOFactory.GetClientDAO.CheckClientStatus(SelectedClient))//in this block i will check if the client is active or not 
                    {
                        if (MessageBox.Show("vous ne pouvez pas supprimer le client num " + SelectedClient.Id + " car il existe dans des factures, voulez vous le randre inactive ?", "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            if (DAOFactory.GetClientDAO.UpdateClientStatus(SelectedClient, false))
                            {
                                ManagementClientWindow clientWindow = new ManagementClientWindow();
                                clientWindow.Show();
                                window.Close();
                            }//end if 
                        }//end if 
                    }else
                    {
                        // Here the client existe in  facture, and he is inactive 
                        MessageBox.Show("Le client num " + SelectedClient.Id + " n'est pas supprimé de votre DB, et il est inactive.", "infos");
                    }//end if 
                }else
                {
                    // here our client don't existe in my factures 
                    if (DAOFactory.GetClientDAO.delete(SelectedClient))
                    {
                        ManagementClientWindow clientWindow = new ManagementClientWindow();
                        clientWindow.Show();
                        window.Close();
                    }//end if 
                }//end if 

            }
            else
            {
                MessageBox.Show("le client est null");
            }


            
        }//end Delete



        public void Save(ModifieClientWindow window)
        {
           if (SelectedClient!=null)
            {
                if (DAOFactory.GetClientDAO.update(SelectedClient))
                {
                    ManagementClientWindow clientWindow = new ManagementClientWindow();
                    clientWindow.Show();
                    window.Close();

                }//end if 
            }//end if 

        }//end Update







        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged

    }//end class
}//end namespace 
