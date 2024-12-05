using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace ConnexionSQL.AccesoDatos__DAL_
{
    public class AccesoADatosDepartments
    {
        private SqlConnection connection;

        public AccesoADatosDepartments(SqlConnection connection)
        {
            this.connection = connection;
        }

        public class Department
        {
            public int DepartmentId { get; set; }
           

            public Department(int departmentId)
            {
                DepartmentId = departmentId;
               
            }

            public override string ToString()
            {
                return $"{DepartmentId}";
            }
        }

        public List<Department> SelectDepartments()
        {
            List<Department> departments = new List<Department>();
            string query = "SELECT department_id FROM departments";

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
                    departments.Add(new Department(
                        reader.GetInt32(reader.GetOrdinal("department_id"))
                    ));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer los departamentos: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return departments;
        }
    }
}

