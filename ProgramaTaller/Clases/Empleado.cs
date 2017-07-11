using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaTaller.Clases
{
    class Empleado
    {
        #region Variables

        SqlCommand cmd;
        SqlDataAdapter dapEmpleados;
        DataSet dtsEmpleados;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);

        private int m_ClaveEmpleado;
        private bool cargarDatos = false;

        #endregion

        #region Constructor

        public Empleado(int ClaveEmpleado)
        {
            this.m_ClaveEmpleado = ClaveEmpleado;
            this.cargarDatos = true;
        }

        #endregion

        #region Propiedades

        public int ClaveEmpleado
        {
            get
            {
                return this.m_ClaveEmpleado;
            }
        }

        public string NombreCompleto
        {
            get
            {
                this.Cargar();
                return Nombres + " " + ApellidoPaterno + " " + ApellidoMaterno;
            }
        }

        public string Nombres
        {
            get
            {
                this.Cargar();
                if (this.dtsEmpleados.Tables[0].Rows[0]["NOMBRES"] == DBNull.Value)
                    return "";
                else
                    return this.dtsEmpleados.Tables[0].Rows[0]["NOMBRES"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsEmpleados.Tables[0].Rows[0]["NOMBRES"] = objValor;
            }
        }

        public string Rfc
        {
            get
            {
                this.Cargar();
                if (this.dtsEmpleados.Tables[0].Rows[0]["RFC"] == DBNull.Value)
                    return "";
                else
                    return this.dtsEmpleados.Tables[0].Rows[0]["RFC"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value; 
                this.dtsEmpleados.Tables[0].Rows[0]["RFC"] = objValor;
            }
        }

        public string ApellidoPaterno
        {
            get
            {
                this.Cargar();
                if (this.dtsEmpleados.Tables[0].Rows[0]["APELLIDO_PATERNO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsEmpleados.Tables[0].Rows[0]["APELLIDO_PATERNO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsEmpleados.Tables[0].Rows[0]["APELLIDO_PATERNO"] = objValor;
            }
        }

        public string ApellidoMaterno
        {
            get
            {
                this.Cargar();
                if (this.dtsEmpleados.Tables[0].Rows[0]["APELLIDO_MATERNO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsEmpleados.Tables[0].Rows[0]["APELLIDO_MATERNO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsEmpleados.Tables[0].Rows[0]["APELLIDO_MATERNO"] = objValor;
            }
        }

        public string CalleDomicilio
        {
            get
            {
                this.Cargar();
                if (this.dtsEmpleados.Tables[0].Rows[0]["CALLE_DOMICILIO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsEmpleados.Tables[0].Rows[0]["CALLE_DOMICILIO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsEmpleados.Tables[0].Rows[0]["CALLE_DOMICILIO"] = objValor;
            }
        }

        public string ColoniaDomicilio
        {
            get
            {
                this.Cargar();
                if (this.dtsEmpleados.Tables[0].Rows[0]["COLONIA_DOMICILIO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsEmpleados.Tables[0].Rows[0]["COLONIA_DOMICILIO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsEmpleados.Tables[0].Rows[0]["COLONIA_DOMICILIO"] = objValor;
            }
        }

        public int NumeroDomicilio
        {
            get
            {
                this.Cargar();
                if (this.dtsEmpleados.Tables[0].Rows[0]["NUMERO_DOMICILIO"] == DBNull.Value)
                    return 0;
                else
                    return Convert.ToInt32(this.dtsEmpleados.Tables[0].Rows[0]["NUMERO_DOMICILIO"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != 0)
                    objValor = value;
                this.dtsEmpleados.Tables[0].Rows[0]["NUMERO_DOMICILIO"] = objValor;
            }
        }

        public string DomicilioCompleto
        {
            get
            {
                this.Cargar();
                return CalleDomicilio + "" + NumeroDomicilio + "" + ColoniaDomicilio;
            }
        }

        public string CorreoElectronico
        {
            get
            {
                this.Cargar();
                if (this.dtsEmpleados.Tables[0].Rows[0]["CORREO_ELECTRONICO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsEmpleados.Tables[0].Rows[0]["CORREO_ELECTRONICO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsEmpleados.Tables[0].Rows[0]["CORREO_ELECTRONICO"] = objValor;
            }
        }

        public string Telefono
        {
            get
            {
                this.Cargar();
                if (this.dtsEmpleados.Tables[0].Rows[0]["TELEFONO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsEmpleados.Tables[0].Rows[0]["TELEFONO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsEmpleados.Tables[0].Rows[0]["TELEFONO"] = objValor;
            }
        }

        public bool esNuevo
        {
            get
            {
                this.Cargar();
                return (this.dtsEmpleados.Tables[0].Rows[0].RowState == DataRowState.Added);
            }
        }
        
        //pendiente propiedad tipo empleado...

        #endregion

        #region Metodos publicos

        public void Cargar()
        {
            if (cargarDatos)
            {
                try
                {
                    #region Obtener catalogo
                    con.Open();
                    string strConsulta = "SELECT * FROM EMPLEADOS WHERE CLAVE_EMPLEADO = @CLAVE_EMPLEADO";
                    cmd = new SqlCommand(strConsulta, con);
                    cmd.Parameters.AddWithValue("@CLAVE_EMPLEADO", this.m_ClaveEmpleado);
                    dapEmpleados = new SqlDataAdapter();
                    dtsEmpleados = new DataSet();
                    dapEmpleados.SelectCommand = cmd;
                    dapEmpleados.Fill(dtsEmpleados);
                    con.Close();

                    #endregion

                    #region Siguiente consecutivo

                    if (this.dtsEmpleados.Tables[0].Rows.Count == 0)
                    {
                        DataRow drwUsuarios = dtsEmpleados.Tables[0].NewRow();
                        drwUsuarios["CLAVE_EMPLEADO"] = this.m_ClaveEmpleado;
                        dtsEmpleados.Tables[0].Rows.Add(drwUsuarios);
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    con.Close();
                    throw new Exception("Ocurrio un error al obtener los Empleados. " + ex.Message);
                }
                cargarDatos = false;
            }
        }

        public void Guardar()
        {
            try
            {
                con.Open();
                SqlCommandBuilder cbEmpleados = new SqlCommandBuilder(dapEmpleados);
                dapEmpleados.InsertCommand = cbEmpleados.GetInsertCommand(true);
                dapEmpleados.UpdateCommand = cbEmpleados.GetUpdateCommand(true);
                dapEmpleados.DeleteCommand = cbEmpleados.GetDeleteCommand(true);
                dapEmpleados.Update(dtsEmpleados.Tables[0]);
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw new Exception(ex.Message);
            }
        }

        
        #endregion
    }
}
