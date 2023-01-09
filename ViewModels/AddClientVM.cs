using MrBricolage.Models;
using MrBricolage.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBricolage.ViewModels
{
    public class AddClientVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;



        public AddClientVM ()
        {
            ClientToAdd = new Client();
        }



        public Client ClientToAdd { get; set; }




        public void Exit(AddClientWindow add)
        {
            ManagementClientWindow clientWindow = new ManagementClientWindow();
            clientWindow.Show();
            add.Close();
        }//end Exit




        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged


    }//end class 
}//end namespace 
