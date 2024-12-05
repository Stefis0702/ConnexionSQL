using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AccesoADatosJobs;
using static ConnexionSQL.AccesoDatos__DAL_.AccesoADatosEmployees;

namespace ConnexionSQL.AccesoDatos__DAL_
{
    public class AccesoADatosEmployees
    {
        private SqlConnection connection;

        public AccesoADatosEmployees(SqlConnection connection)
        {
            this.connection = connection;
        }

        public class Employees
        {
            public int? EmployeeId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public DateTime? HireDate { get; set; }
            public int? JobId { get; set; }
            public decimal? Salary { get; set; }
            public int? ManagerId { get; set; }
            public int? DepartmentId { get; set; }

        

            public Employees (string firstName, string lastName, string email, string phoneNumber, DateTime hireDate, int jobId, decimal? salary, int? managerId, int? departmentId)
             {
                FirstName = firstName;
                LastName = lastName;
                Email = email;
                PhoneNumber = phoneNumber;
                HireDate = hireDate;
                JobId = jobId;
                Salary = salary;
                ManagerId = managerId;
                DepartmentId = departmentId;

            }
            public override string ToString()
            {
                return $"{EmployeeId}, {FirstName} {LastName}, {Email}, {PhoneNumber},{HireDate}, {JobId}, {Salary}, {ManagerId},{DepartmentId}";
            }

        }
        public void InsertEmployee(Employees employee)
        {
            try
            {
                string sql = @"
                    INSERT INTO employees (first_name, last_name, email, phone_number, hire_date,job_id, salary, manager_id, department_id) 
                    VALUES (@firstName, @lastName, @email, @phoneNumber, @hireDate, @jobId, @salary, @managerId, @departmentId)";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@firstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@lastName", employee.LastName);
                cmd.Parameters.AddWithValue("@email", employee.Email);
                cmd.Parameters.AddWithValue("@phoneNumber", employee.PhoneNumber);
                cmd.Parameters.AddWithValue("@hireDate", employee.HireDate);
                cmd.Parameters.AddWithValue("@jobId", employee.JobId);
                cmd.Parameters.AddWithValue("@salary", employee.Salary ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@managerId", employee.ManagerId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@departmentId", employee.DepartmentId ?? (object)DBNull.Value);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el empleado:" + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public List<Employees> SelectEmployees()
        {
            List<Employees> employees = new List<Employees>();
            string query = "SELECT * FROM employees";
            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var employee = new Employees(
                        reader.GetString(reader.GetOrdinal("first_name")),
                        reader.GetString(reader.GetOrdinal("last_name")),
                        reader.GetString(reader.GetOrdinal("email")),
                        reader.IsDBNull(reader.GetOrdinal("phone_number")) ? "No Phone" : reader.GetString(reader.GetOrdinal("phone_number")),
                        reader.GetDateTime(reader.GetOrdinal("hire_date")),
                        reader.GetInt32(reader.GetOrdinal("job_id")),
                        reader.IsDBNull(reader.GetOrdinal("salary")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("salary")),
                        reader.IsDBNull(reader.GetOrdinal("manager_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("manager_id")),
                        reader.IsDBNull(reader.GetOrdinal("department_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("department_id"))
                    )
                    {
                        EmployeeId = reader.GetInt32(reader.GetOrdinal("employee_id"))
                    };

                    employees.Add(employee);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer los empleados: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return employees;
        }
        public void UpdateEmployee(Employees employee)
        {
            try
            {
                string sql = @"
            UPDATE employees 
            SET 
                first_name = @firstName,
                last_name = @lastName,
                email = @email,
                phone_number = @phoneNumber,
                hire_date = @hireDate,
                job_id = @jobId,
                salary = @salary,
                manager_id = @managerId,
                department_id = @departmentId
            WHERE employee_id = @employeeId";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@firstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@lastName", employee.LastName);
                cmd.Parameters.AddWithValue("@email", employee.Email);
                cmd.Parameters.AddWithValue("@phoneNumber", employee.PhoneNumber);
                cmd.Parameters.AddWithValue("@hireDate", employee.HireDate);
                cmd.Parameters.AddWithValue("@jobId", employee.JobId);
                cmd.Parameters.AddWithValue("@salary", employee.Salary ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@managerId", employee.ManagerId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@departmentId", employee.DepartmentId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@employeeId", employee.EmployeeId);

                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} row(s) updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar el Empleado: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public void DeleteEmployee(int employeeId)
        {
            try
            {
                string sql = @"
                DELETE FROM employees
                WHERE employee_id = @employeeId";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@employeeId", employeeId);

                if (connection.State == System.Data.ConnectionState.Closed)
                { connection.Open(); }

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Empleado con ID {employeeId} eliminado correctamente.");
                }
                else
                {
                    Console.WriteLine("No se encontro el empleado con ese ID.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el empleado: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }


}
