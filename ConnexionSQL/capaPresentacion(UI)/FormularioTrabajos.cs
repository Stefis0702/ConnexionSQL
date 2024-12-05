using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnexionSQL.capaPresentacion_UI_
{
    public partial class FormularioTrabajos : Form
    {
        private AccesoADatosJobs jobsDataAccess;
        private JobsManager jobsManager;
        private SqlConnection connection;
        

        public FormularioTrabajos()
        {
            InitializeComponent();
            connection = new SqlConnection(ConfiguracionBD.connectionString);
            jobsManager = new JobsManager(connection);
            jobsDataAccess = new AccesoADatosJobs(connection);
        }
      

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
              
                string jobTitle = txtJobTitle.Text;
                decimal? jobMinSalary = null;
                decimal? jobMaxSalary= null;


                if (!string.IsNullOrEmpty(txtMinSalary.Text))
                {
                    jobMinSalary = decimal.Parse(txtMinSalary.Text);
                }

                
                if (!string.IsNullOrEmpty(txtMaxSalary.Text))
                {
                    jobMaxSalary = decimal.Parse(txtMaxSalary.Text);
                }
                

                AccesoADatosJobs.Jobs newJob = new AccesoADatosJobs.Jobs(jobTitle, jobMinSalary, jobMaxSalary);
              
                
                jobsManager.AddJob(newJob);
                MessageBox.Show("Trabajo añadido correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al añadir trabajo: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
      
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                lbxJobs.Items.Clear();


                var jobsList = jobsDataAccess.SelectJobs();


                foreach (var job in jobsList)
                {
                    lbxJobs.Items.Add(job);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los trabajos: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (lbxJobs.SelectedItem != null)
                {
                    
                    AccesoADatosJobs.Jobs selectedJob = (AccesoADatosJobs.Jobs)lbxJobs.SelectedItem;

                   
                    selectedJob.JobTitle = txtJobTitle.Text;
                    selectedJob.JobMinSalary = string.IsNullOrEmpty(txtMinSalary.Text) ? (decimal?)null : decimal.Parse(txtMinSalary.Text);
                    selectedJob.JobMaxSalary = string.IsNullOrEmpty(txtMaxSalary.Text) ? (decimal?)null : decimal.Parse(txtMaxSalary.Text);

                    
                    jobsDataAccess.UpdateJob(selectedJob);
                    MessageBox.Show("Trabajo actualizado correctamente.");
                    List<AccesoADatosJobs.Jobs> jobsList = jobsDataAccess.SelectJobs();
                    lbxJobs.Items.Clear();
                    foreach (var job in jobsList)
                    {
                        lbxJobs.Items.Add(job);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un trabajo para modificar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el trabajo: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbxJobs.SelectedItem != null)
                {
                    
                    AccesoADatosJobs.Jobs selectedJob = (AccesoADatosJobs.Jobs)lbxJobs.SelectedItem;
                    int jobId = selectedJob.JobId.Value;

                    jobsManager.DeleteJob(jobId);

                    List<AccesoADatosJobs.Jobs> jobsList = jobsDataAccess.SelectJobs();
                    lbxJobs.Items.Clear();
                    foreach (var job in jobsList)
                    {
                        lbxJobs.Items.Add(job);
                    }

                    MessageBox.Show("Trabajo eliminado correctamente.");
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un trabajo para eliminar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el trabajo: " + ex.Message);
            }
        }
    }
}        
    

