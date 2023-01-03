using MrBricolage.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace MrBricolage.Utilities.DAO.DAOImplement
{
    public  class EmployeeDAO : DAO<Employee>
    {




        public EmployeeDAO(MySqlConnection conn) : base(conn)
        {

        }

       
        //test ok 
        public override Employee find(int id)
        {
            Employee employeeToFind = null;
            try
            {
                string sql ="SELECT id_emp as id , emp_name as name, emp_f_name as f_name , Login , _password FROM employee WHERE id_emp = @id ;";


                 
                MySqlCommand cmd = new MySqlCommand(sql,conn);
                cmd.Parameters.AddWithValue("@id", id);

                //Execuction of my sql query
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





        //test ok 
        public override ObservableCollection<Employee> findAll()
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

            try
            {
                string sql = "SELECT id_emp as id , emp_name as name, emp_f_name as f_name , Login , _password FROM employee WHERE is_active  ;";
                
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                

                //Execuction of my sql query 
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




        //test ok  
        public override bool create(Employee empTocreatee) 
        {
            bool flag = true;
            
            if (empTocreatee != null)
            {
                try
                {




                    string sql = "SELECT emp_name , emp_f_name , is_active FROM employee WHERE emp_name = @name AND emp_f_name = @f_name ; SELECT Login  FROM employee WHERE Login = @Lg ;";



                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", empTocreatee.Name);
                    cmd.Parameters.AddWithValue("@f_name", empTocreatee.F_Name);
                    cmd.Parameters.AddWithValue("@Lg", empTocreatee.Login);
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
                                sql ="UPDATE employee SET is_active = @Bool WHERE  emp_name = @name AND emp_f_name = @f_name ;";

                                
                                MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                                cmd2.Parameters.AddWithValue("@Bool", true);
                                cmd2.Parameters.AddWithValue("@name", empTocreatee.Name);
                                cmd2.Parameters.AddWithValue("@f_name", empTocreatee.F_Name);

                                //Execuction of my sql query 
                                cmd2.ExecuteNonQuery();

                                MessageBox.Show("L'employee " + empTocreatee.Name + " " + empTocreatee.F_Name + " est active maintenant ! ", "infos");


                            }
                            else
                            {
                                // the user don't want update this client, in this case flag is false          and i do nothing 
                                flag = false;
                                reader.Close();
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

                            sql ="INSERT INTO employee (emp_name , emp_f_name , Login , _password , is_active) VALUES (@name , @f_name , @Login , @pwrd , @Bool);";


                            
                            MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                            cmd2.Parameters.AddWithValue("@name", empTocreatee.Name);
                            cmd2.Parameters.AddWithValue("@f_name", empTocreatee.F_Name);
                            cmd2.Parameters.AddWithValue("@Login", empTocreatee.Login);
                            cmd2.Parameters.AddWithValue("@pwrd", empTocreatee.Password);
                            cmd2.Parameters.AddWithValue("@Bool", true);

                            //Execuction of my sql query
                            cmd2.ExecuteNonQuery();


                            MessageBox.Show("L'employee " + empTocreatee.Name + " " + empTocreatee.F_Name + " a bien ete insere dans votre DB ! ", "Infos");
                            
                        }//end if 

                    }//end if 


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





        // test ok 
        public override bool delete(Employee EmployeeToDelete)
        {
            bool flag = true;

            if (EmployeeToDelete != null)
            {
                string sql = "SELECT e.id_emp , e.is_active FROM facture f inner join employee e WHERE e.id_emp = @num ;";

               
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@num", EmployeeToDelete.Id);

                //Execuction of my sql query
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())  //if the Employee existe in facture
                {
                   if (reader.GetBoolean("is_active")) //if the employee is active 
                    {

                        if (MessageBox.Show("vous ne pouvez pas supprimer l'employee num " + EmployeeToDelete.Id + " car il existe dans des factures, voulez vous le randre inactive ?", "infos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            reader.Close();
                            sql = "UPDATE employee SET is_active = @bool WHERE  ID_emp = @id ; ";


                            MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                            cmd2.Parameters.AddWithValue("@bool", false);
                            cmd2.Parameters.AddWithValue("@id", EmployeeToDelete.Id);

                            //Execuction of my sql query
                            cmd2.ExecuteNonQuery();

                            MessageBox.Show("L'employee num " + EmployeeToDelete.Id + " n'est pas supprimé de votre DB, mais il est inactive, donc vous ne pouvez plus  l'insere dans une facture", "infos");
                        }
                        else
                        {
                            // the user don't want update this employee, in this case flag is false and i do nothing 
                            flag = false;
                            reader.Close();

                        }//end if 
                    }else // the Employee existe in facture and he is inactive 
                    {
                        flag = false;
                        reader.Close();
                        MessageBox.Show("Vous ne pouvez pas supprimé l'Employee num " + EmployeeToDelete.Id + " de votre DB, et il est déjà inactive.", "infos");

                    }//end if 
                    

                }
                else // here our employee don't existe in my factures 
                {
                    reader.Close();
                    sql = "DELETE FROM employee WHERE id_emp = @id ; ";

                    
                    MySqlCommand cmd3 = new MySqlCommand(sql, conn);
                    cmd3.Parameters.AddWithValue("@id",EmployeeToDelete.Id);

                    //Execuction of my sql query
                    cmd3.ExecuteNonQuery();
                }//end if 
            }
            else
            {
                flag = false;
                MessageBox.Show("L'employee est null !", "infos");
            }//end if 


            return flag;
        }//end delete



        //no test 
        public override bool update(Employee EmployeeToUpdate) 
        { 

            bool flag = true;


            if (EmployeeToUpdate!= null)
            {
                try
                {
                    string sql = "UPDATE employee SET emp_name = @name , emp_f_name = @f_name, _password = @passwrd WHERE id_emp = @id ";


                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", EmployeeToUpdate.Name);
                    cmd.Parameters.AddWithValue("@f_name", EmployeeToUpdate.F_Name);
                    cmd.Parameters.AddWithValue("@passwrd", EmployeeToUpdate.Password);
                    cmd.Parameters.AddWithValue("@id", EmployeeToUpdate.Id);

                    //Execuction of my sql query
                    cmd.ExecuteNonQuery();

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

            }//end if 

            return flag;


        }//end update


    }//end class 
}//end namespace 
