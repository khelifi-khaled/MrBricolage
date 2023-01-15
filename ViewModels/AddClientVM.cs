using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System.ComponentModel;
using System.Windows;

namespace MrBricolage.ViewModels
{
    public class AddClientVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;



        public AddClientVM()
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


        public  void Save (AddClientWindow add)//test ok 
        {
            if (ClientToAdd != null)
            {
                if (DAOFactory.GetClientDAO.CheckExistedClientFullName(ClientToAdd))
                {
                    //if the client existe with the same name and same f_name so i check, if he is active or not 
                    if (DAOFactory.GetClientDAO.CheckClientStatus(ClientToAdd))
                    {
                        MessageBox.Show("Le client " + ClientToAdd.Name + " " + ClientToAdd.F_name + " existe deja dans votre DB, et il est bien active  ", "infos");
                    }else
                    {
                        //in this case, the client with the same name and same f_name exist in our db, he is just inactive, i will propose to my user if he wan to activate it or no
                        if(MessageBox.Show("Le client " + ClientToAdd.Name + " " + ClientToAdd.F_name + " Existe deja dans votre DB, il est juste inActive, voulez-vous l'active ?", "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                           if(DAOFactory.GetClientDAO.UpdateClientStatus(ClientToAdd,true))
                            {
                                //if the update of client status is ok , so i will open ManagementClientWindow again
                                ManagementClientWindow window = new ManagementClientWindow();
                                window.Show();
                                add.Close();

                            }//enf if 
                        }//end if 
                    }//end if 
                }else
                {
                    if (DAOFactory.GetClientDAO.create(ClientToAdd))
                    {
                        ManagementClientWindow window = new ManagementClientWindow();
                        window.Show();
                        add.Close();
                    }//end if 
                }//end if 
            }
            else
            {
                MessageBox.Show("Le client est null !", "infos");

            }//end if 


        }//end Save



        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged


    }//end class 
}//end namespace 
