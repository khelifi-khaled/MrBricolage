using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System;
using System.ComponentModel;
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
                    MessageBox.Show("Format d'ID incorrect !!");
                }//end if 
            }//end if 


            
        }//end GetClient



        public void BtnChange(ChangeClientInFactureWindow win)
        {

           if(ClientToReplace != null)
            {
                if (DAOFactory.GetClientDAO.CheckClientStatus(ClientToReplace))
                {
                    FactureToModifie.Client = ClientToReplace;
                    FactureToModifie.Date = DateTime.Now;

                    if (DAOFactory.GetFactureDAO.Update_client_facture(FactureToModifie))
                    {
                        MessageBox.Show("Le client " + ClientToReplace.F_name + " " + ClientToReplace.Name + " est bien inséré dans la facture N° " + FactureToModifie.Id + " dans votre DB ", "infos");
                        ModifieFactureWindow window = new ModifieFactureWindow(FactureToModifie);
                        window.Show();
                        win.Close();
                       
                    }//end if 
                    
                }else
                {
                    MessageBox.Show("Vous ne pouvez pas insérer le client "+ ClientToReplace.F_name + " "+ ClientToReplace.Name + " car il est inactif dans votre DB", "infos");

                }//end if 
            }
            else 
            {
                MessageBox.Show("Vous ne pouvez pas insérer un client vide à la place de " + FactureToModifie.Client.F_name + " " + FactureToModifie.Client.Name, "infos");
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
