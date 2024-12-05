using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace ConnexionSQL.AccesoDatos__DAL_
{
    public class AccesoADatosManagers
    {
        private SqlConnection connection;

        public AccesoADatosManagers(SqlConnection connection)
        {
            this.connection = connection;
        }

        public class Manager
        {
            public int ManagerId { get; set; }
            public string FullName { get; set; }

            public Manager(int managerId, string fullName)
            {
                ManagerId = managerId;
                FullName = fullName;
            }

            public override string ToString()
            {
                return $"{FullName}";
            }
        }

        public List<Manager> SelectManagers()
        {
            List<Manager> managers = new List<Manager>();
            string query = "SELECT employee_id, first_name + ' ' + last_name AS FullName FROM employees WHERE manager_id IS NOT NULL";

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
                    managers.Add(new Manager(
                        reader.GetInt32(reader.GetOrdinal("employee_id")),
                        reader.GetString(reader.GetOrdinal("FullName"))
                    ));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer los managers: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return managers;
        }
    }
}
