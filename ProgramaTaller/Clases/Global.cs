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
