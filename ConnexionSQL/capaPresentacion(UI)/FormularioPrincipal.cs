using ConnexionSQL.capaPresentacion_UI_;
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

namespace ConnexionSQL
{
    
    public partial class FormularioPrincipal : Form
    {
      
        private SqlConnection connection;
        public FormularioPrincipal()
        {
            InitializeComponent();
            btnDesconexion.Enabled = false;
        }

        private void btnConexion_Click(object sender, EventArgs e)
        {
            AbrirConexion();
        }
        private void btnDesconexion_Click(object sender, EventArgs e)
        {
            CerrarConexion();
        }

        private void AbrirConexion()
        {
            try
            {
                //ConfiguracionBD tiene los datos para conexion a la BD
                connection = new SqlConnection(ConfiguracionBD.connectionString);
                connection.Open();
                lblEstado.Text = "Conexión abierta.";
                lblEstado.ForeColor = System.Drawing.Color.Green;
                btnConexion.Enabled = false;
                btnDesconexion.Enabled = true;
                btnAbrirTrabajos.Enabled = true;
            }
            catch (Exception ex)
            {
                lblEstado.Text = "Error al conectar: " + ex.Message;
                lblEstado.ForeColor = System.Drawing.Color.Red;
            }
        }
        private void CerrarConexion()
        {
            try
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    lblEstado.Text = "Conexión cerrada.";
                    lblEstado.ForeColor = System.Drawing.Color.Red;

                    btnConexion.Enabled = true;
                    btnDesconexion.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                lblEstado.Text = "Error al cerrar: " + ex.Message;
                lblEstado.ForeColor = System.Drawing.Color.Red;
            }
        }
        public SqlConnection ObtenerConexion()
        {
            return connection;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            FormularioTrabajos formularioTrabajos = new FormularioTrabajos();

            
            formularioTrabajos.ShowDialog();
        }
    }
    


}
