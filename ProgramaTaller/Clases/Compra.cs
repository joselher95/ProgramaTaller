using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProgramaTaller.Clases
{
    class Compra
    {
        #region Variables

        SqlCommand cmd;
        SqlDataAdapter dapCompras;
        DataSet dtsCompras;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);

        private int m_ClaveCompra;
        private Proveedores m_Proveedor;
        private Empleado m_Empleado;
        private bool cargarDatos = false;

        #endregion

        #region Constructor

        public Compra(int ClaveCompra)
        {
            this.m_ClaveCompra = ClaveCompra;
            this.cargarDatos = true;
        }

        #endregion

        #region Propiedades

        public int ClaveCompra
        {
            get
            {
                return this.m_ClaveCompra;
            }
        }

        public Proveedores Proveedor
        {
            get
            {
                this.Cargar();
                return m_Proveedor;
            }
            set
            {
                this.Cargar();
                this.m_Proveedor = value;
                object objValor = DBNull.Value;
                if (value != null)
                    objValor = value.ClaveProveedor;
                this.dtsCompras.Tables[0].Rows[0]["CLVE_PROVEEDOR"] = objValor;
            }
        }

        public Empleado empleado
        {
            get
            {
                this.Cargar();
                return m_Empleado;
            }
            set
            {
                this.Cargar();
                this.m_Empleado = value;
                object objValor = DBNull.Value;
                if (value != null)
                    objValor = value.ClaveEmpleado;
                this.dtsCompras.Tables[0].Rows[0]["CLAVE_EMPLEADO_COMPRA"] = objValor;
            }
        }

        public DateTime FechaCompra
        {
            get
            {
                this.Cargar();
                if (this.dtsCompras.Tables[0].Rows[0]["FECHA_COMPRA"] == DBNull.Value)
                    return DateTime.MinValue;
                else
                    return Convert.ToDateTime(this.dtsCompras.Tables[0].Rows[0]["FECHA_COMPRA"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != DateTime.MinValue)
                    objValor = value;
                this.dtsCompras.Tables[0].Rows[0]["FECHA_COMPRA"] = objValor;
            }
        }
        public bool esNuevo
        {
            get
            {
                this.Cargar();
                return (this.dtsCompras.Tables[0].Rows[0].RowState == DataRowState.Added);
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
                    string strConsulta = "SELECT * FROM COMPRAS WHERE CLAVE_COMPRA = @CLAVE_COMPRA";
                    cmd = new SqlCommand(strConsulta, con);
                    cmd.Parameters.AddWithValue("@CLAVE_COMPRA", this.m_ClaveCompra);
                    dapCompras = new SqlDataAdapter();
                    dtsCompras = new DataSet();
                    dapCompras.SelectCommand = cmd;
                    dapCompras.Fill(dtsCompras);
                    con.Close();

                    #endregion

                    #region Siguiente consecutivo

                    if (this.dtsCompras.Tables[0].Rows.Count == 0)
                    {
                        DataRow drwUsuarios = dtsCompras.Tables[0].NewRow();
                        drwUsuarios["CLAVE_COMPRA"] = this.m_ClaveCompra;
                        dtsCompras.Tables[0].Rows.Add(drwUsuarios);
                    }
                    else
                    {
                        if (this.dtsCompras.Tables[0].Rows[0]["CLAVE_PROVEEDOR"] == DBNull.Value)
                            this.m_Proveedor = null;
                        else
                            this.m_Proveedor = new Proveedores(Convert.ToInt32(this.dtsCompras.Tables[0].Rows[0]["CLAVE_PROVEEDOR"]));
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    con.Close();
                    throw new Exception("Ocurrio un error al obtener las Compras. " + ex.Message);
                }
                cargarDatos = false;
            }
        }

        public void Guardar()
        {
            try
            {
                con.Open();
                SqlCommandBuilder cbVentas = new SqlCommandBuilder(dapCompras);
                dapCompras.InsertCommand = cbVentas.GetInsertCommand(true);
                dapCompras.UpdateCommand = cbVentas.GetUpdateCommand(true);
                dapCompras.DeleteCommand = cbVentas.GetDeleteCommand(true);
                dapCompras.Update(dtsCompras.Tables[0]);
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

