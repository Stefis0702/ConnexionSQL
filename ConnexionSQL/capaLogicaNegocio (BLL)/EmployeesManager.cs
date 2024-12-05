using ConnexionSQL.AccesoDatos__DAL_;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnexionSQL.capaLogicaNegocio__BLL_
{
    public class EmployeeManager
    {
        private AccesoADatosEmployees accesoADatosEmployee;

        public EmployeeManager(SqlConnection connection)
        {
            accesoADatosEmployee = new AccesoADatosEmployees(connection);
        }

        public void AddEmployee(AccesoADatosEmployees.Employees employee)
        {
            accesoADatosEmployee.InsertEmployee(employee);
        }
        public void DeleteEmployee(int employeeId)
        {
            accesoADatosEmployee.DeleteEmployee(employeeId);
        }

    }

}
