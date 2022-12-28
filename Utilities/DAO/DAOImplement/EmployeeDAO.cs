using MrBricolage.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MrBricolage.Utilities.DAO.DAOImplement
{
    public  class EmployeeDAO : DAO<Employee>
    {




        public EmployeeDAO(MySqlConnection conn) : base(conn)
        {

        }

       

        public override Employee find(int id)
        {
            Employee employeeToFind = null;
            try
            {
                string sql = string.Format("SELECT id_emp as id , emp_name as name, emp_f_name as f_name , Login , _password FROM {0} WHERE id_emp = {1} ;", "employee", id) ;


                //Execuction of my sql query 
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    employeeToFind = new Employee(reader.GetInt32("id"), reader.GetString("name"), reader.GetString("f_name"), reader.GetString("Login"), reader.GetString("_password"));
                }else
                {
                    MessageBox.Show("l'employee num " + id + "n'existe pas dans votre DB !");
                }

                

                
                reader.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("problem de recuperation de l'employée num "+ id );
            }//end tryCatch 

            return employeeToFind;
        }//end find






        public override ObservableCollection<Employee> findAll()
        {
            EmployeeCollection employees = new EmployeeCollection();

            try
            {
                string sql = string.Format("SELECT id_emp as id , emp_name as name, emp_f_name as f_name , Login , _password FROM {0} WHERE is_active ;", "employee");
                //Execuction of my sql query 
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();




                while (reader.Read())
                {
                    Employee e = new Employee(reader.GetInt32("id"), reader.GetString("name"), reader.GetString("f_name"), reader.GetString("Login"), reader.GetString("_password"));
                    employees.Add(e);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("problem de recuperation des employées !");
            }

            return employees;


        }//end findAll 





        public override bool create(Employee empTocreatee) 
        {
            bool flag = true;
            
            if (empTocreatee != null)
            {
                try
                {




                    string sql = string.Format("SELECT emp_name , emp_f_name , is_active FROM employee WHERE emp_name = '{0}' AND emp_f_name = '{1}' ; SELECT Login  FROM employee WHERE Login = '{2}' ;", empTocreatee.Name, empTocreatee.F_Name, empTocreatee.Login);



                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        if (reader.GetBoolean("is_active"))
                        {
                            //if the Emlpoyee with the same name and same f_name exist in our DB and he is active as well.
                            MessageBox.Show("l'employee " + empTocreatee.Name + " " + empTocreatee.F_Name + " existe deja dans votre DB, et il est bien active ! ", "Infos");
                            flag = false;
                            reader.Close();
                        }else
                        {
                            //in this case, the employee with the same name and same f_name exist in our db, he is just inactive, i will propose to my user if he wan to activate it or no
                            if (MessageBox.Show("L'employee " + empTocreatee.Name + " " + empTocreatee.F_Name + " existe deja dans votre DB, voulez vous l'activé  ?", "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                reader.Close();
                                flag = false;
                                sql = string.Format("UPDATE employee SET is_active = '{0}' WHERE  emp_name = '{1}' AND emp_f_name = '{2}'", "1", empTocreatee.Name, empTocreatee.F_Name);

                                //Execuction of my sql query 
                                MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                                MySqlDataReader reader2 = cmd2.ExecuteReader();

                                MessageBox.Show("L'employee " + empTocreatee.Name + " " + empTocreatee.F_Name + " est active maintenant ! ", "infos");
                                reader2.Close();


                            }
                            else
                            {
                                // the user don't want update this client, in this case flag is false and i do nothing 
                                flag = false;

                            }//end if 

                        }//end if 



                    }
                    else
                    {
                        //execution of my second query, the reader will have the second resultSet obj
                        reader.NextResult();
                        if (reader.Read())
                        {
                            //in this block, the Login exist in our DB.
                            MessageBox.Show("Login existe deja dans la DB , changé votre Login SVP !", "Infos");
                            flag = false;
                            reader.Close();

                        }
                        else
                        {
                            //if there's no login, in this block i will insert my new Employee 
                            reader.Close();
                            sql = string.Format("INSERT INTO employee (emp_name , emp_f_name , Login , _password,is_active) VALUES ('{0}','{1}','{2}','{3}' , '{4}');", empTocreatee.Name, empTocreatee.F_Name, empTocreatee.Login, empTocreatee.Password,"1");


                            //Execuction of my sql query
                            MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                            MySqlDataReader reader2 = cmd2.ExecuteReader();


                            MessageBox.Show("L'employee " + empTocreatee.Name + " " + empTocreatee.F_Name + " a bien ete insere dans votre DB ! ", "Infos");
                            reader2.Close();
                        }//end if 

                    }//end if 

                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("problem d'insertion d'employee " + empTocreatee.Name + " " + empTocreatee.F_Name + " dans votre DB", "Infos");
                    flag = false;


                }//end try catch
            }else
            {
                flag = false;
                MessageBox.Show("L'employee est null !", "Infos");
            }

            return flag;
           
        }//end create






        public override bool delete(Employee EmployeeToDelete)
        {
            bool flag = true;

           if (EmployeeToDelete != null)
            {
                try
                {

                    string sql = string.Format("DELETE FROM list_of_art WHERE id_facture in (SELECTE ID_f FROM facture WHERE emp_num = {0} ; ); DELETE FROM facture WHERE emp_nom = {0} ; DELETE FROM  employee WHERE id_emp = {0};", EmployeeToDelete.Id);


                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    flag = false;
                }//end trycatch
            }else
            {
                flag = false;
                MessageBox.Show("L'employee est null !", "Infos");
            }

            return flag;
        }//end delete


        public override bool update(Employee EmployeeToUpdate) 
        { 

            bool flag = true;


            if (EmployeeToUpdate!= null)
            {
                try
                {
                    string sql = string.Format("UPDATE employee SET emp_name = '{0}' , emp_f_name = '{1}' , Login = '{2}' , _password = '{3}' WHERE id_emp = {4} ", EmployeeToUpdate.Name, EmployeeToUpdate.F_Name, EmployeeToUpdate.Login, EmployeeToUpdate.Password, EmployeeToUpdate.Id);


                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    flag = false;
                }//end trycatch 
            }else
            {
                flag = false;
                MessageBox.Show("L'employee est null !", "Infos");
            }

            return flag;
        }//end update


    }//end class 
}//end namespace 
