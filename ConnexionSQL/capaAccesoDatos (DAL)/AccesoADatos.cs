using System.Data.SqlClient;
using System;
using System.Collections.Generic;

public class AccesoADatos
{
    private SqlConnection connection;

    public class Jobs
    {
        public int? JobId { get; set; }
        public string JobTitle { get; set; }
        public decimal? JobMinSalary { get; set; }
        public decimal? JobMaxSalary { get; set; }

        public Jobs(string jobTitle, decimal? jobMinSalary, decimal? jobMaxSalary)
        {
            
            JobTitle = jobTitle;
            JobMinSalary = jobMinSalary;
            JobMaxSalary = jobMaxSalary;
        }
        public override string ToString()
        {
           
            return $"{JobId}, | {JobTitle}, | {JobMinSalary}, | {JobMaxSalary}";
        }
    }

    public AccesoADatos(SqlConnection connection)
    {
        this.connection = connection;
    }

    public void InsertJob(Jobs job)
    {
        try
        {
            string sql = @"
                INSERT INTO jobs(job_title, min_salary, max_salary)
                VALUES (@jobTitle, @minSalary, @maxSalary)";
            Console.WriteLine("Consulta SQL: " + sql);

            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@jobTitle", job.JobTitle);
            cmd.Parameters.AddWithValue("@minSalary", job.JobMinSalary.HasValue ? (object)job.JobMinSalary.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@maxSalary", job.JobMaxSalary.HasValue ? (object)job.JobMaxSalary.Value : DBNull.Value);

            connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery(); 
            Console.WriteLine($"{rowsAffected} row(s) affected.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al insertar el trabajo: " + ex.Message);
        }
        finally
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close(); 
            }
        }
    }
    public List<Jobs> SelectJobs()
    {
        List<Jobs> jobs = new List<Jobs>();
        string query = "SELECT * FROM JOBS";
        SqlCommand cmd = new SqlCommand(query, connection);

        try
        {
            
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();  
            }

            SqlDataReader records = cmd.ExecuteReader(); 

            while (records.Read())
            {
                int jobId = records.GetInt32(records.GetOrdinal("job_id"));
                string jobTitle = records.GetString(records.GetOrdinal("job_title"));
                decimal? minSalary = records.IsDBNull(records.GetOrdinal("min_salary")) ? (decimal?)null : records.GetDecimal(records.GetOrdinal("min_salary"));
                decimal? maxSalary = records.IsDBNull(records.GetOrdinal("max_salary")) ? (decimal?)null : records.GetDecimal(records.GetOrdinal("max_salary"));

                Jobs job = new Jobs(jobTitle, minSalary, maxSalary)
                {
                    JobId = jobId 
                };
                jobs.Add(job);
            }
            records.Close();  
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al leer los trabajos: " + ex.Message);
        }
        finally
        {
           
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();  
            }
        }

        return jobs;
    }

    public void UpdateJob(Jobs job)
    {
        try
        {
            string sql = @"
            UPDATE jobs
            SET job_title = @jobTitle, 
                min_salary = @minSalary, 
                max_salary = @maxSalary
            WHERE job_id = @jobId";

            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@jobId", job.JobId);
            cmd.Parameters.AddWithValue("@jobTitle", job.JobTitle);
            cmd.Parameters.AddWithValue("@minSalary", job.JobMinSalary.HasValue ? (object)job.JobMinSalary.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@maxSalary", job.JobMaxSalary.HasValue ? (object)job.JobMaxSalary.Value : DBNull.Value);

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            int rowsAffected = cmd.ExecuteNonQuery(); 
            Console.WriteLine($"{rowsAffected} row(s) updated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al modificar el trabajo: " + ex.Message);
        }
        finally
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close(); 
            }
        }
    }
    public void DeleteJob(int jobId)
    {
        try
        {
            string sql = @"
                DELETE FROM jobs
                WHERE job_id = @jobId";

            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@jobId", jobId);

            if (connection.State == System.Data.ConnectionState.Closed)
            { connection.Open(); }

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine($"Trabajo con ID {jobId} eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("No se encontro el trabajo con ese ID.");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al eliminar el trabajo: " + ex.Message);
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
