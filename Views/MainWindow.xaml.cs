using MrBricolage.Models;
using MrBricolage.Utilities.DAO;
using MrBricolage.Utilities.DAO.DAOImplement;
using MrBricolage.ViewModels;
using MrBricolage.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MrBricolage
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindowVM MainVM { get; set; }



        public MainWindow()
        {
            MainVM = new MainWindowVM();
            DataContext = MainVM;
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }//end Exit_Click

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            MainVM.Login(this);

        }//end Login_Click

        private void register_Click(object sender, RoutedEventArgs e)
        {
            SignInWindow win = new SignInWindow();
            win.Show();
            this.Close();

        }//end register_Click

        private void TestDAOArticle_Click(object sender, RoutedEventArgs e)
        {
            //test find ok !!! 
            Article r = DAOFactory.GetArticleDAO.find(1);


            //test Create ok !!!
            Article a = new Article(12, "Marteau ", 10.98);
            DAOFactory.GetArticleDAO.create(a);


            //test delete ok !!!  
            Article c = new Article(2);
            DAOFactory.GetArticleDAO.delete(c);


            //test findAll ok !!!
            ObservableCollection<Article> toto =  DAOFactory.GetArticleDAO.findAll();


            //test update test ok !!! 

            Article aa = new Article(7, "Marteau ", 10.98);
            DAOFactory.GetArticleDAO.update(aa);

        }//end TestDAOArticle_Click

        private void TestDAOClient_Click(object sender, RoutedEventArgs e)
        {

            ////test find ok !!!
            //Client c = DAOFactory.GetClientDAO.find(1);


            ////test Create ok !!!

            //Client ccc = new Client(11,true,"khaled","khelifi","toto@gamil.com","7000 Mons ");
            //DAOFactory.GetClientDAO.create(ccc);


            ////test delete ok !!! 
            //Client t = new Client(4);
            //DAOFactory.GetClientDAO.delete(t);



            ////test findAll ok !!!
            //ObservableCollection<Client> toto = new ObservableCollection<Client>();
            //toto = DAOFactory.GetClientDAO.findAll();


            //test update ok 
            Client csssss = new Client(2,true,"myCompanyName",null,"company@gmail.com", "chemin de la procession , 7000 Mons");

            DAOFactory.GetClientDAO.update(csssss);




        }//end TestDAOClient_Click

        private void TestDAOEmployee_Click(object sender, RoutedEventArgs e)
        {
            ////test find ok !!! 
            DAOFactory.GetEmployeeDAO.find(1);


            //test findAll ok !!!
            ObservableCollection<Employee> toto = new ObservableCollection<Employee>();
            toto = DAOFactory.GetEmployeeDAO.findAll();

            // test create  ok !!!
            Employee emp = new Employee(3, "toto", "kheltotoifi", "test12345", "toto");
            DAOFactory.GetEmployeeDAO.create(emp);


            //test delete ok !!! 
            Employee emp2 = new Employee(2);
            DAOFactory.GetEmployeeDAO.delete(emp2);

            //test update 
            DAOFactory.GetEmployeeDAO.update(emp);


        }//end TestDAOEmployee_Click

        private void TestDAOFacture_Click(object sender, RoutedEventArgs e)
        {

            DAOFactory.GetFactureDAO.findAll();

        }//end TestDAOFacture_Click
    }//end class 
}//end MainWindow 
