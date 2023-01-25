using MrBricolage.Models;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Windows;

namespace MrBricolage.Utilities.DAO.DAOImplement
{
    public  class EmployeeDAO : DAO<Employee>
    {




        public EmployeeDAO(MySqlConnection conn) : base(conn)
        {

        }


        /// <summary>
        /// find a Employee from our DB
        /// </summary>
        /// <param name="id">Employee id that we are looking for </param>
        /// <returns>the Employee if found, null if not</returns>
        public override Employee find(int id)
        {
           
            Employee employeeToFind = null;

            MySqlTransaction mySqlTransaction = conn.BeginTransaction();

            try
            {
                string sql ="SELECT id_emp as id , emp_name as name, emp_f_name as f_name , Login , _password FROM employee WHERE id_emp = @id ;";


                 
                MySqlCommand cmd = new MySqlCommand(sql,conn, mySqlTransaction);
                cmd.Parameters.AddWithValue("@id", id);

                //Execution of my sql query
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    employeeToFind = new Employee(reader.GetInt32("id"), reader.GetString("name"), reader.GetString("f_name"), reader.GetString("Login"), reader.GetString("_password"));
                }else
                {
                    MessageBox.Show("l'employee num " + id + "n'existe pas dans votre DB !");
                }


                


                reader.Close();


                mySqlTransaction.Commit();

            }
            catch 
            {
                mySqlTransaction.Rollback();
                MessageBox.Show("problème de recuperation de l'employée num " + id );
            }//end tryCatch 

            return employeeToFind;
        }//end find















        /// <summary>
        /// get all Active Emplpotee existed from our DB
        /// </summary>
        /// <returns> Observable Collection type Employee  </returns>        
        public override ObservableCollection<Employee> findAll()
        {
            MySqlTransaction mySqlTransaction = conn.BeginTransaction();
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

            try
            {
                string sql = "SELECT id_emp as id , emp_name as name, emp_f_name as f_name , Login , _password FROM employee WHERE is_active  ;";
                
                MySqlCommand cmd = new MySqlCommand(sql, conn, mySqlTransaction);
                

                //Execution of my sql query 
                MySqlDataReader reader = cmd.ExecuteReader();




                while (reader.Read())
                {
                    Employee e = new Employee(reader.GetInt32("id"), reader.GetString("name"), reader.GetString("f_name"), reader.GetString("Login"), reader.GetString("_password"));
                    employees.Add(e);
                }

                
                reader.Close();

                mySqlTransaction.Commit();


            }
            catch 
            {
                mySqlTransaction.Rollback();
                MessageBox.Show("problème de recuperation des employées !");
            }

            return employees;


        }//end findAll 


















        /// <summary>
        /// get  the emp status from our DB 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee status from our DB</returns>
        public bool CheckEmployeeStatus(Employee employee)
        {
            bool flag = false;
            MySqlTransaction mySqlTransaction = conn.BeginTransaction();

            try
            {
                string sql = "SELECT is_active  FROM employee WHERE  emp_name = @name AND emp_f_name =  @f_name ;";


                MySqlCommand cmd = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd.Parameters.AddWithValue("@name", employee.Name);
                cmd.Parameters.AddWithValue("@f_name", employee.F_Name);

                //Execution of mysql query
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    flag = reader.GetBoolean("is_active");
                }

               
                reader.Close();

                mySqlTransaction.Commit();

            }
            catch
            {
                mySqlTransaction.Rollback();
                MessageBox.Show("problème de GetEmployeeStatus !");
            }

            return flag;


        }//end CheckEmployeeStatus
















        /// <summary>
        /// check if employee Login existe in our DB
        /// </summary>
        /// <param name="employee">Employee to check </param>
        /// <returns>true if Login exist, false if not</returns>

        public bool CheckEmployeeLogin(Employee employee)
        {
            bool flag = false;
            MySqlTransaction mySqlTransaction = conn.BeginTransaction();
            try
            {
                string sql = "SELECT Login  FROM employee WHERE Login = @Lg ;";


                MySqlCommand cmd = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd.Parameters.AddWithValue("@Lg", employee.Login);

                //Execution of mysql query
                MySqlDataReader reader = cmd.ExecuteReader();

                flag = reader.Read();

                

                reader.Close();

                mySqlTransaction.Commit();

            }
            catch
            {
                mySqlTransaction.Rollback();
                MessageBox.Show("problème de GetEmployeeStatus !");
            }

            return flag;

        }//end GetEmployeeStatus

















        /// <summary>
        /// check if we have an existed employee with same FullName in our DB
        /// </summary>
        /// <param name="employee"> emp to check </param>
        /// <returns>true if exist, false if not </returns>
        public bool CheckExistedEmployeeFullName(Employee employee)
        {
            bool flag = false;
            MySqlTransaction mySqlTransaction = conn.BeginTransaction();
            try
            {
                string sql = "SELECT emp_name , emp_f_name , is_active FROM employee WHERE emp_name = @name AND emp_f_name = @f_name ;";

                MySqlCommand cmd = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd.Parameters.AddWithValue("@name", employee.Name);
                cmd.Parameters.AddWithValue("@f_name", employee.F_Name);

                //Execution of my sql query 
                MySqlDataReader reader = cmd.ExecuteReader();

                flag = reader.Read();

                
                reader.Close();

                mySqlTransaction.Commit();
            }
            catch
            {
                mySqlTransaction.Rollback();
                MessageBox.Show("problème de CheckExistedEmploueeFullName !");
                
            }

            return flag;
        }//end checkExistedEmploueeFullName













        /// <summary>
        /// update emp status 
        /// </summary>
        /// <param name="employee">emp to update </param>
        /// <param name="status">the status that we want to insert in our DB</param>
        /// <returns>true , if update is done , false if not </returns>
        public bool UpdateEmployeeStatus(Employee employee , bool status )
        {

            bool flag = true;
            MySqlTransaction mySqlTransaction = conn.BeginTransaction();

            try
            {
                string sql = "UPDATE employee SET is_active = @Bool WHERE  emp_name = @name AND emp_f_name = @f_name ;";


                MySqlCommand cmd2 = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd2.Parameters.AddWithValue("@Bool", status);
                cmd2.Parameters.AddWithValue("@name", employee.Name);
                cmd2.Parameters.AddWithValue("@f_name", employee.F_Name);

                //Execution of my sql query 
                cmd2.ExecuteNonQuery();

                mySqlTransaction.Commit();

            }
            catch
            {
                mySqlTransaction.Rollback();
                MessageBox.Show("problème de UpdateEmployeeStatus !");
                flag = false;
            }

            return flag;

        }//end UpdateEmployeeStatus










        /// <summary>
        /// create an employee in our DB
        /// </summary>
        /// <param name="empTocreatee">employee to create </param>
        /// <returns>true if the employee has been created , false if not </returns>        
        public override bool create(Employee empTocreatee) 
        {
            bool flag = true;

            MySqlTransaction mySqlTransaction = conn.BeginTransaction();

            try
           {

                string sql = "INSERT INTO employee (emp_name , emp_f_name , Login , _password , is_active) VALUES (@name , @f_name , @Login , @pwrd , @Bool);";

                MySqlCommand cmd2 = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd2.Parameters.AddWithValue("@name", empTocreatee.Name);
                cmd2.Parameters.AddWithValue("@f_name", empTocreatee.F_Name);
                cmd2.Parameters.AddWithValue("@Login", empTocreatee.Login);
                cmd2.Parameters.AddWithValue("@pwrd", empTocreatee.Password);
                cmd2.Parameters.AddWithValue("@Bool", true);

                //Execuion of my sql query
                cmd2.ExecuteNonQuery();

                mySqlTransaction.Commit();

           }
            catch
           {
                mySqlTransaction.Rollback();
                MessageBox.Show("problème de create !");
                flag = false;
           }
           
            return flag;
           
        }//end create

















        /// <summary>
        /// check if emloyee exist in Facture table in our DB
        /// </summary>
        /// <param name="employee">employee ti check </param>
        /// <returns>true if exist , false if not </returns>
        public bool CheckExistedEmployeeInFacture (Employee employee)
        {
            bool flag;

            MySqlTransaction mySqlTransaction = conn.BeginTransaction();

            try
            {
                string sql = "SELECT emp_num FROM facture WHERE emp_num = @num ;";

                MySqlCommand cmd = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd.Parameters.AddWithValue("@num", employee.Id);

                //Execution of my sql query
                MySqlDataReader reader = cmd.ExecuteReader();

                flag = reader.Read();

                

                reader.Close();

                mySqlTransaction.Commit();

            }
            catch
            {
                mySqlTransaction.Rollback();
                MessageBox.Show("problème de CheckExistedEmployeeInFacture !");
                flag = false;
            }


            return flag;

        }//end CheckExistedEmployeeInFacture
















        /// <summary>
        /// delete an employee from our DB
        /// </summary>
        /// <param name="EmployeeToDelete">employee to delete </param>
        /// <returns>true if our employee has been deleted , false if not </returns>        
        public override bool delete(Employee EmployeeToDelete)
        {
            bool flag = true;

            MySqlTransaction mySqlTransaction = conn.BeginTransaction();

            try
            {
                string sql = "DELETE FROM employee WHERE id_emp = @id ; ";


                MySqlCommand cmd3 = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd3.Parameters.AddWithValue("@id", EmployeeToDelete.Id);

                //Execution of my sql query
                cmd3.ExecuteNonQuery();

                mySqlTransaction.Commit();
            }
            catch
            {
                MessageBox.Show("problème de delete !");
                flag = false;
                mySqlTransaction.Rollback();
            }
            
            return flag;
        }//end delete












        /// <summary>
        /// update employee infos in our DB
        /// </summary>
        /// <param name="EmployeeToUpdate"> employee to update </param>
        /// <returns>true if employee has been updated , false if not </returns>        
        public override bool update(Employee EmployeeToUpdate) 
        { 

            bool flag = true;

            MySqlTransaction mySqlTransaction = conn.BeginTransaction();

            try
            {
                string sql = "UPDATE employee SET emp_name = @name , emp_f_name = @f_name, _password = @passwrd WHERE id_emp = @id ";


                MySqlCommand cmd = new MySqlCommand(sql, conn, mySqlTransaction);
                cmd.Parameters.AddWithValue("@name", EmployeeToUpdate.Name);
                cmd.Parameters.AddWithValue("@f_name", EmployeeToUpdate.F_Name);
                cmd.Parameters.AddWithValue("@passwrd", EmployeeToUpdate.Password);
                cmd.Parameters.AddWithValue("@id", EmployeeToUpdate.Id);

                //Execuction of my sql query
                cmd.ExecuteNonQuery();


                mySqlTransaction.Commit();
            }
            catch 
            {
                mySqlTransaction.Rollback();
                MessageBox.Show("problème de update !");
                flag = false;
            }//end trycatch 

            return flag;


        }//end update


    }//end class 
}//end namespace 
