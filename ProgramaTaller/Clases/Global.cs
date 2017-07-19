using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaTaller.Clases
{
    public static class Global
    {
        #region Variables
        private static int m_Empleado;
        public static frmCatalogoClientes frmCatalogoClientes;
        public static frmCatalogoCompras frmCatalogoCompras;
        public static frmCatalogoProductos frmCatalogoProductos;
        public static frmCatalogoProveedores frmCatalogoProveedores;
        public static frmCatalogoEmpleados frmCatalogoEmpleados;
        public static frmCatalogoVentas frmCatalogoVentas;
        public static frmRecepcionMercancia frmRecepcionMercancia;
        public static frmVentaMercancia frmVentaMercancia;
        public static frmRegistroUsuario frmRegistroUsuario;
        #endregion

        #region Propiedades
        public static int EmpleadoSesionActual
        {
            get
            {
                return m_Empleado;
            }
            set
            {
                m_Empleado = value;
            }
        }
        #endregion
    }
}
