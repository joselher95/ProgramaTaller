using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaTaller.Clases
{
    class Clientes
    {
         #region Variables

        SqlCommand cmd;
        SqlDataAdapter dapClientes;
        DataSet dtsClientes;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);

        private int m_ClaveCliente;
        private bool cargarDatos = false;

        #endregion

        #region Constructor

        public Clientes(int ClaveCliente)
        {
            this.m_ClaveCliente = ClaveCliente;
            this.cargarDatos = true;
        }

        #endregion

        #region Propiedades

        public int ClaveCliente
        {
            get
            {
                return this.m_ClaveCliente;
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
                if (this.dtsClientes.Tables[0].Rows[0]["NOMBRES"] == DBNull.Value)
                    return "";
                else
                    return this.dtsClientes.Tables[0].Rows[0]["NOMBRES"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsClientes.Tables[0].Rows[0]["NOMBRES"] = objValor;
            }
        }

        public string Rfc
        {
            get
            {
                this.Cargar();
                if (this.dtsClientes.Tables[0].Rows[0]["RFC"] == DBNull.Value)
                    return "";
                else
                    return this.dtsClientes.Tables[0].Rows[0]["RFC"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value; 
                this.dtsClientes.Tables[0].Rows[0]["RFC"] = objValor;
            }
        }

        public string ApellidoPaterno
        {
            get
            {
                this.Cargar();
                if (this.dtsClientes.Tables[0].Rows[0]["APELLIDO_PATERNO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsClientes.Tables[0].Rows[0]["APELLIDO_PATERNO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsClientes.Tables[0].Rows[0]["APELLIDO_PATERNO"] = objValor;
            }
        }

        public string ApellidoMaterno
        {
            get
            {
                this.Cargar();
                if (this.dtsClientes.Tables[0].Rows[0]["APELLIDO_MATERNO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsClientes.Tables[0].Rows[0]["APELLIDO_MATERNO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsClientes.Tables[0].Rows[0]["APELLIDO_MATERNO"] = objValor;
            }
        }

        public string Domicilio
        {
            get
            {
                string dom = this.CalleDomicilio + " #" + this.NumeroDomicilio + " Col." + this.ColoniaDomicilio;
                return dom;
            }
        }

        public string CalleDomicilio
        {
            get
            {
                this.Cargar();
                if (this.dtsClientes.Tables[0].Rows[0]["CALLE_DOMICILIO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsClientes.Tables[0].Rows[0]["CALLE_DOMICILIO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsClientes.Tables[0].Rows[0]["CALLE_DOMICILIO"] = objValor;
            }
        }

        public string ColoniaDomicilio
        {
            get
            {
                this.Cargar();
                if (this.dtsClientes.Tables[0].Rows[0]["COLONIA_DOMICILIO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsClientes.Tables[0].Rows[0]["COLONIA_DOMICILIO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsClientes.Tables[0].Rows[0]["COLONIA_DOMICILIO"] = objValor;
            }
        }

        public int NumeroDomicilio
        {
            get
            {
                this.Cargar();
                if (this.dtsClientes.Tables[0].Rows[0]["NUMERO_DOMICILIO"] == DBNull.Value)
                    return 0;
                else
                    return Convert.ToInt32(this.dtsClientes.Tables[0].Rows[0]["NUMERO_DOMICILIO"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != 0)
                    objValor = value;
                this.dtsClientes.Tables[0].Rows[0]["NUMERO_DOMICILIO"] = objValor;
            }
        }

        public string CorreoElectronico
        {
            get
            {
                this.Cargar();
                if (this.dtsClientes.Tables[0].Rows[0]["CORREO_ELECTRONICO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsClientes.Tables[0].Rows[0]["CORREO_ELECTRONICO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsClientes.Tables[0].Rows[0]["CORREO_ELECTRONICO"] = objValor;
            }
        }

        public string RazonSocial
        {
            get
            {
                this.Cargar();
                if (this.dtsClientes.Tables[0].Rows[0]["RAZON_SOCIAL"] == DBNull.Value)
                    return "";
                else
                    return this.dtsClientes.Tables[0].Rows[0]["RAZON_SOCIAL"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsClientes.Tables[0].Rows[0]["RAZON_SOCIAL"] = objValor;
            }
        }

        public string Telefono
        {
            get
            {
                this.Cargar();
                if (this.dtsClientes.Tables[0].Rows[0]["TELEFONO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsClientes.Tables[0].Rows[0]["TELEFONO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsClientes.Tables[0].Rows[0]["TELEFONO"] = objValor;
            }
        }

        public char TipoCliente
        {
            get
            {
                this.Cargar();
                if (this.dtsClientes.Tables[0].Rows[0]["TIPO_CLIENTE"] == DBNull.Value)
                    return '\0';
                else
                    return Convert.ToChar(this.dtsClientes.Tables[0].Rows[0]["TIPO_CLIENTE"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != '\0')
                    objValor = value;
                this.dtsClientes.Tables[0].Rows[0]["TIPO_CLIENTE"] = objValor;
            }
        }

        public bool esNuevo
        {
            get
            {
                this.Cargar();
                return (this.dtsClientes.Tables[0].Rows[0].RowState == DataRowState.Added);
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
                    string strConsulta = "SELECT * FROM CLIENTES WHERE CLAVE_CLIENTE = @CLAVE_CLIENTE";
                    cmd = new SqlCommand(strConsulta, con);
                    cmd.Parameters.AddWithValue("@CLAVE_CLIENTE", this.m_ClaveCliente);
                    dapClientes = new SqlDataAdapter();
                    dtsClientes = new DataSet();
                    dapClientes.SelectCommand = cmd;
                    dapClientes.Fill(dtsClientes);
                    con.Close();

                    #endregion

                    #region Siguiente consecutivo

                    if (this.dtsClientes.Tables[0].Rows.Count == 0)
                    {
                        DataRow drwUsuarios = dtsClientes.Tables[0].NewRow();
                        drwUsuarios["CLAVE_CLIENTE"] = this.m_ClaveCliente;
                        dtsClientes.Tables[0].Rows.Add(drwUsuarios);
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    con.Close();
                    throw new Exception("Ocurrio un error al obtener los clientes. " + ex.Message);
                }
                cargarDatos = false;
            }
        }

        public void Guardar()
        {
            try
            {
                con.Open();
                SqlCommandBuilder cbClientes = new SqlCommandBuilder(dapClientes);
                dapClientes.InsertCommand = cbClientes.GetInsertCommand(true);
                dapClientes.UpdateCommand = cbClientes.GetUpdateCommand(true);
                dapClientes.DeleteCommand = cbClientes.GetDeleteCommand(true);
                dapClientes.Update(dtsClientes.Tables[0]);
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
