using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaTaller.Clases
{
    public class Proveedores
    {
        #region Variables

        SqlCommand cmd;
        SqlDataAdapter dapProveedores;
        DataSet dtsProveedores;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);

        private int m_ClaveProveedor;
        private bool cargarDatos = false;

        #endregion

        #region Constructor

        public Proveedores(int ClaveProveedor)
        {
            this.m_ClaveProveedor = ClaveProveedor;
            this.cargarDatos = true;
        }

        #endregion

        #region Propiedades

        public int ClaveProveedor
        {
            get
            {
                return this.m_ClaveProveedor;
            }
        }

        public string Nombres
        {
            get
            {
                this.Cargar();
                if (this.dtsProveedores.Tables[0].Rows[0]["NOMBRES"] == DBNull.Value)
                    return "";
                else
                    return this.dtsProveedores.Tables[0].Rows[0]["NOMBRES"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsProveedores.Tables[0].Rows[0]["NOMBRES"] = objValor;
            }
        }

        public string Rfc
        {
            get
            {
                this.Cargar();
                if (this.dtsProveedores.Tables[0].Rows[0]["RFC"] == DBNull.Value)
                    return "";
                else
                    return this.dtsProveedores.Tables[0].Rows[0]["RFC"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsProveedores.Tables[0].Rows[0]["RFC"] = objValor;
            }
        }

        public string ApellidoPaterno
        {
            get
            {
                this.Cargar();
                if (this.dtsProveedores.Tables[0].Rows[0]["APELLIDO_PATERNO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsProveedores.Tables[0].Rows[0]["APELLIDO_PATERNO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsProveedores.Tables[0].Rows[0]["APELLIDO_PATERNO"] = objValor;
            }
        }

        public string ApellidoMaterno
        {
            get
            {
                this.Cargar();
                if (this.dtsProveedores.Tables[0].Rows[0]["APELLIDO_MATERNO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsProveedores.Tables[0].Rows[0]["APELLIDO_MATERNO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsProveedores.Tables[0].Rows[0]["APELLIDO_MATERNO"] = objValor;
            }
        }
        
        public string CorreoElectronico
        {
            get
            {
                this.Cargar();
                if (this.dtsProveedores.Tables[0].Rows[0]["CORREO_ELECTRONICO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsProveedores.Tables[0].Rows[0]["CORREO_ELECTRONICO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsProveedores.Tables[0].Rows[0]["CORREO_ELECTRONICO"] = objValor;
            }
        }

        public string RazonSocial
        {
            get
            {
                this.Cargar();
                if (this.dtsProveedores.Tables[0].Rows[0]["RAZON_SOCIAL"] == DBNull.Value)
                    return "";
                else
                    return this.dtsProveedores.Tables[0].Rows[0]["RAZON_SOCIAL"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsProveedores.Tables[0].Rows[0]["RAZON_SOCIAL"] = objValor;
            }
        }

        public string Telefono
        {
            get
            {
                this.Cargar();
                if (this.dtsProveedores.Tables[0].Rows[0]["TELEFONO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsProveedores.Tables[0].Rows[0]["TELEFONO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsProveedores.Tables[0].Rows[0]["TELEFONO"] = objValor;
            }
        }

        public char TipoProveedor
        {
            get
            {
                this.Cargar();
                if (this.dtsProveedores.Tables[0].Rows[0]["TIPO_PROVEEDOR"] == DBNull.Value)
                    return '\0';
                else
                    return Convert.ToChar(this.dtsProveedores.Tables[0].Rows[0]["TIPO_PROVEEDOR"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != '\0')
                    objValor = value;
                this.dtsProveedores.Tables[0].Rows[0]["TIPO_PROVEEDOR"] = objValor;
            }
        }

        public bool esNuevo
        {
            get
            {
                this.Cargar();
                return (this.dtsProveedores.Tables[0].Rows[0].RowState == DataRowState.Added);
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
                    string strConsulta = "SELECT * FROM PROVEEDORES WHERE CLAVE_PROVEEDOR = @CLAVE_PROVEEDOR";
                    cmd = new SqlCommand(strConsulta, con);
                    cmd.Parameters.AddWithValue("@CLAVE_PROVEEDOR", this.m_ClaveProveedor);
                    dapProveedores = new SqlDataAdapter();
                    dtsProveedores = new DataSet();
                    dapProveedores.SelectCommand = cmd;
                    dapProveedores.Fill(dtsProveedores);
                    con.Close();

                    #endregion

                    #region Siguiente consecutivo

                    if (this.dtsProveedores.Tables[0].Rows.Count == 0)
                    {
                        DataRow drwUsuarios = dtsProveedores.Tables[0].NewRow();
                        drwUsuarios["CLAVE_PROVEEDOR"] = this.m_ClaveProveedor;
                        dtsProveedores.Tables[0].Rows.Add(drwUsuarios);
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
                SqlCommandBuilder cbClientes = new SqlCommandBuilder(dapProveedores);
                dapProveedores.InsertCommand = cbClientes.GetInsertCommand(true);
                dapProveedores.UpdateCommand = cbClientes.GetUpdateCommand(true);
                dapProveedores.DeleteCommand = cbClientes.GetDeleteCommand(true);
                dapProveedores.Update(dtsProveedores.Tables[0]);
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

