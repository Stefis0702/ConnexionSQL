using System.Data.SqlClient;

public class JobsManager
{
    private AccesoADatos accesoADatos;

    public JobsManager(SqlConnection connection)
    {
        accesoADatos = new AccesoADatos(connection);
    }

    public void AddJob(AccesoADatos.Jobs job)
    {
        accesoADatos.InsertJob(job);
    }
    public void DeleteJob(int jobId)
    {
        accesoADatos.DeleteJob(jobId);
    }
}