using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Views;
using System.ComponentModel;
using System.Windows;

namespace MrBricolage.ViewModels
{
    public class SignInVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;




        public SignInVM()
        {
            this.ThisEmployee = new Employee();
        }


       

        public Employee ThisEmployee { get; set; }



        public void Close (SignInWindow myWindow)
        {
            MainWindow main = new MainWindow();
            main.Show();
            myWindow.Close();
        }

        public void Save(SignInWindow win)
        {
            if (ThisEmployee != null)
            {
                if (DAOFactory.GetEmployeeDAO.CheckExistedEmployeeFullName(ThisEmployee))//check if the EmployeeToAdd whith the same name and same f_name exist in our DB
                {
                    if (DAOFactory.GetEmployeeDAO.CheckEmployeeStatus(ThisEmployee))
                    {
                        //if the Emlpoyee with the same name and same f_name exist in our DB and he is active as well.
                        MessageBox.Show("L'employé " + ThisEmployee.Name + " " + ThisEmployee.F_Name + " existe déjà dans votre DB !! ", "Infos");


                    }
                    else
                    {
                        //in this case, the employee with the same name and same f_name exist in our db, he is just inactive,
                        MessageBox.Show("L'employé " + ThisEmployee.Name + " " + ThisEmployee.F_Name + " existe déjà dans votre DB !!!", "Infos");


                    }//end if 
                }
                else
                {
                    //in this block, EmployeeToAdd whith the same name and same f_name don't exist in our DB, i will check if Login EmployeeToAdd exist in our DB 
                    if (DAOFactory.GetEmployeeDAO.CheckEmployeeLogin(ThisEmployee))//check if the Employee Login is Unique 
                    {
                        MessageBox.Show("Login existe déjà dans la DB, changez votre Login SVP !", "Infos");
                    }
                    else
                    {
                        if (DAOFactory.GetEmployeeDAO.create(ThisEmployee))
                        {
                            MessageBox.Show("L'employé " + ThisEmployee.Name + " " + ThisEmployee.F_Name + " est bien créé dans votre DB ! ", "infos");
                            MainWindow window = new MainWindow();
                            window.Show();
                            win.Close();
                        }//end if 
                    }//end if 
                }

            }
            else
            {
                MessageBox.Show("L'employé est a null !");

            }//end if 

        }//end save


            //if (DAOFactory.GetEmployeeDAO.create(ThisEmployee))
            //{
            //    MainWindow main = new MainWindow();
            //    main.Show();
            //    myWindow.Close();
            //}

        /// <summary>
        /// binding  
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//end OnPropertyChanged


    }//end class 
}//end name space 
