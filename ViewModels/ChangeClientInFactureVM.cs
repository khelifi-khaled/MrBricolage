using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MrBricolage.ViewModels
{
    public class ChangeClientInFactureVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;



        private Facture _facture;

        private Client _clientToRepace;

        public ChangeClientInFactureVM(Facture facture)
        {
            FactureToModifie = facture;
        }



        public Facture FactureToModifie
        {
            get => _facture;
            set
            {
                _facture = value;
                OnPropertyChanged(nameof(FactureToModifie));
            }
        }



        public Client ClientToReplace
        {
            get => _clientToRepace;
            set
            {
                _clientToRepace = value;
                OnPropertyChanged(nameof(ClientToReplace));
            }
        }



        public void Cancel(ChangeClientInFactureWindow thisWin)
        {
            ModifieFactureWindow window = new ModifieFactureWindow(FactureToModifie);
            window.Show();
            thisWin.Close();
        }



        public void  GetClient(ChangeClientInFactureWindow win, KeyEventArgs e)
        {

            if(e.Key == Key.Enter)
            {
                if (int.TryParse(win.Client_Id.Text, out int val))
                {
                    ClientToReplace = DAOFactory.GetClientDAO.find(val);
                }
                else
                {
                    MessageBox.Show("Format d'Id est incorrecte !!");
                }//end if 
            }//end if 


            
        }//end GetClient



        public void BtnChange(ChangeClientInFactureWindow win)
        {

           if(ClientToReplace != null)
            {
                FactureToModifie.Client = ClientToReplace;

                ModifieFactureWindow window = new ModifieFactureWindow(FactureToModifie);
                window.Show();
                win.Close();
            }
            else 
            {
                MessageBox.Show("pour ne pouvez pas inserer un  client vide a la place de " + FactureToModifie.Client.F_name + " " + FactureToModifie.Client.Name, "infos");
            }//end if 

        }//end BtnChenge


        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged


    }//ebd class
}//end namespace 
