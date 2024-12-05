using ConnexionSQL.AccesoDatos__DAL_;
using ConnexionSQL.capaLogicaNegocio__BLL_;
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
using static ConnexionSQL.AccesoDatos__DAL_.AccesoADatosEmployees;

namespace ConnexionSQL.capaPresentacion_UI_
{
    public partial class EmployeesForm : Form
    {
        private SqlConnection connection;
        private EmployeeManager employeeManager;
        private AccesoADatosEmployees employeeDataAccess;
        private AccesoADatosJobs jobsDataAccess;  
        private List<AccesoADatosJobs.Jobs> jobsList; 
        private List<AccesoADatosManagers.Manager> managersList;
        private List<AccesoADatosDepartments.Department> departmentsList;


        public EmployeesForm()
        {
            InitializeComponent();
            connection = new SqlConnection(ConfiguracionBD.connectionString);
            employeeManager = new EmployeeManager(connection);
            employeeDataAccess = new AccesoADatosEmployees(connection);
            jobsDataAccess = new AccesoADatosJobs(connection);
        }
        private void EmployeesForm_Load(object sender, EventArgs e)
        {
            try
            {
                
                jobsList = jobsDataAccess.SelectJobs();

                cmbJobs.DisplayMember = "JobTitle"; 
                cmbJobs.ValueMember = "JobId"; 

                cmbJobs.DataSource = jobsList;

                managersList = new AccesoADatosManagers(connection).SelectManagers();
                cbxManager.DisplayMember = "FullName";
                cbxManager.ValueMember = "ManagerId";
                cbxManager.DataSource = managersList;

                
                departmentsList = new AccesoADatosDepartments(connection).SelectDepartments();
                cmbDepartment.DisplayMember = "DepartmentId";
                cmbDepartment.ValueMember = "DepartmentId";
                cmbDepartment.DataSource = departmentsList;

                if (cmbJobs.Items.Count > 0)
                {
                    cmbJobs.SelectedIndex = 0; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los trabajos: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                string email = txtEmail.Text;
                string phoneNumber = txtPhone.Text;
                DateTime hireDate = dateTimeHireDate.Value;
                int jobId = (int)cmbJobs.SelectedValue; 
                decimal? salary = string.IsNullOrEmpty(txtSalary.Text) ? (decimal?)null : decimal.Parse(txtSalary.Text);
                int? managerId = (int?)cbxManager.SelectedValue;
                int? departmentId = (int?)cmbDepartment.SelectedValue;


                AccesoADatosEmployees.Employees newEmployee = new AccesoADatosEmployees.Employees(firstName, lastName, email, phoneNumber, hireDate, jobId, salary, managerId, departmentId);

           
                employeeManager.AddEmployee(newEmployee); 
                MessageBox.Show("Empleado añadido correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al añadir el empleado: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                lbxEmployees.Items.Clear();


                var employeeList = employeeDataAccess.SelectEmployees();


                foreach (var employee in employeeList)
                {
                    lbxEmployees.Items.Add(employee);
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

                if (lbxEmployees.SelectedItem != null)
                {

                    AccesoADatosEmployees.Employees selectedEmployee = (AccesoADatosEmployees.Employees)lbxEmployees.SelectedItem;


                    selectedEmployee.FirstName = txtFirstName.Text;
                    selectedEmployee.LastName = txtLastName.Text;
                    selectedEmployee.Email = txtEmail.Text;
                    selectedEmployee.PhoneNumber = txtPhone.Text;
                    selectedEmployee.HireDate = dateTimeHireDate.Value;
                    selectedEmployee.JobId = (int)cmbJobs.SelectedValue; 
                    selectedEmployee.Salary = string.IsNullOrEmpty(txtSalary.Text) ? (decimal?)null : decimal.Parse(txtSalary.Text);
                    selectedEmployee.ManagerId = (int)cbxManager.SelectedValue;
                    selectedEmployee.DepartmentId = (int)cmbDepartment.SelectedValue;


                    employeeDataAccess.UpdateEmployee(selectedEmployee);
                    MessageBox.Show("Empleado actualizado correctamente.");
                    List<AccesoADatosEmployees.Employees> employeesList = employeeDataAccess.SelectEmployees();
                    lbxEmployees.Items.Clear();
                    foreach (var employee in employeesList)
                    {
                        lbxEmployees.Items.Add(employee);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un Empleado para modificar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el empleado: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbxEmployees.SelectedItem != null)
                {

                    AccesoADatosEmployees.Employees selectedEmployee = (AccesoADatosEmployees.Employees)lbxEmployees.SelectedItem;
                    int employeeId = selectedEmployee.EmployeeId.Value;

                    employeeManager.DeleteEmployee(employeeId);

                    List<AccesoADatosEmployees.Employees> employeesList = employeeDataAccess.SelectEmployees();
                    lbxEmployees.Items.Clear();
                    foreach (var employee in employeesList)
                    {
                        lbxEmployees.Items.Add(employee);
                    }

                    MessageBox.Show("Empleado eliminado correctamente.");
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un empleado para eliminar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el empleado: " + ex.Message);
            }
        }
    }
}
