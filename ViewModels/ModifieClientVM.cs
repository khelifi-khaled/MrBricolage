using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (DAOFactory.GetClientDAO.delete(SelectedClient))
            {
                ManagementClientWindow clientWindow = new ManagementClientWindow();
                clientWindow.Show();
                window.Close();
            }//end if 

        }//end Delete



        public void Save(ModifieClientWindow window)
        {
            if (DAOFactory.GetClientDAO.update(SelectedClient))
            {
                ManagementClientWindow clientWindow = new ManagementClientWindow();
                clientWindow.Show();
                window.Close();

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
