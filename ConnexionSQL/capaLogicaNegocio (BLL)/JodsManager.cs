using System.Data.SqlClient;

public class JobsManager
{
    private AccesoADatosJobs accesoADatos;

    public JobsManager(SqlConnection connection)
    {
        accesoADatos = new AccesoADatosJobs(connection);
    }

    public void AddJob(AccesoADatosJobs.Jobs job)
    {
        accesoADatos.InsertJob(job);
    }
    public void DeleteJob(int jobId)
    {
        accesoADatos.DeleteJob(jobId);
    }
}