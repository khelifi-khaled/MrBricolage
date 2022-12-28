using MrBricolage.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MrBricolage.Utilities.DAO.DAOImplement
{
    public  class FactureDAO : DAO<Facture>
    {

        public FactureDAO(MySqlConnection conn) : base(conn)
        {

        }

        public override bool create(Facture factureToCreate)
        {
            bool flag = true;
            if (factureToCreate != null)
            {
                try
                {
                    string sql = string.Format("INSERT INTO facture (date_f , Client_num , emp_num , totalPrice) VALUES ('{0}',{1},{2},{3})", factureToCreate.Date, factureToCreate.Client.Id, factureToCreate.Employee.Id, factureToCreate.TotalAmount);

                }catch(Exception ex)
                {
                    flag = false;
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Problem de creation de la Facture !");
                }//end try catch 




            }else
            {
                flag = false;
                MessageBox.Show("La facture est null !.");
            }//end if



            return flag;
        }//end 

        public override bool delete(Facture obj)
        {
            throw new NotImplementedException();
        }

        public override Facture find(int id)
        {
            throw new NotImplementedException();
        }

        public override ObservableCollection<Facture> findAll()
        {
            throw new NotImplementedException();
        }

        public override bool update(Facture obj)
        {
            throw new NotImplementedException();
        }
    }//end class 
}//end namespace 
