using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaTaller.Clases
{
    public class Producto
    {
        #region Variables

        SqlCommand cmd;
        SqlDataAdapter dapProductos;
        DataSet dtsProductos;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);

        private int m_ClaveProducto;
        private bool cargarDatos = false;

        #endregion

        #region Constructor

        public Producto (int ClaveProducto)
        {
            this.m_ClaveProducto = ClaveProducto;
            this.cargarDatos = true;
        }

        #endregion

        #region Propiedades

        public int ClaveProducto
        {
            get
            {
                return this.m_ClaveProducto;
            }
        }

        public string NombreCorto
        {
            get
            {
                this.Cargar();
                if (this.dtsProductos.Tables[0].Rows[0]["NOMBRE_CORTO"] == DBNull.Value)
                    return "";
                else
                    return this.dtsProductos.Tables[0].Rows[0]["NOMBRE_CORTO"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsProductos.Tables[0].Rows[0]["NOMBRE_CORTO"] = objValor;
            }
        }

        public string Descripcion
        {
            get
            {
                this.Cargar();
                if (this.dtsProductos.Tables[0].Rows[0]["DESCRIPCION"] == DBNull.Value)
                    return "";
                else
                    return this.dtsProductos.Tables[0].Rows[0]["DESCRIPCION"].ToString();
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != "")
                    objValor = value;
                this.dtsProductos.Tables[0].Rows[0]["DESCRIPCION"] = objValor;
            }
        }

        public int claveProveedor
        {
            get
            {
                this.Cargar();
                if (this.dtsProductos.Tables[0].Rows[0]["PROVEEDOR"] == DBNull.Value)
                    return 0;
                else
                    return Convert.ToInt32(this.dtsProductos.Tables[0].Rows[0]["PROVEEDOR"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != 0)
                    objValor = value;
                this.dtsProductos.Tables[0].Rows[0]["PROVEEDOR"] = objValor;
            }
        }

        public decimal PrecioVenta
        {
            get
            {
                this.Cargar();
                if (this.dtsProductos.Tables[0].Rows[0]["PRECIO_VENTA"] == DBNull.Value)
                    return 0;
                else
                    return Convert.ToDecimal(this.dtsProductos.Tables[0].Rows[0]["PRECIO_VENTA"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value > 0)
                    objValor = value;
                this.dtsProductos.Tables[0].Rows[0]["PRECIO_VENTA"] = objValor;
            }
        }

        public decimal PrecioCompra
        {
            get
            {
                this.Cargar();
                if (this.dtsProductos.Tables[0].Rows[0]["PRECIO_COMPRA"] == DBNull.Value)
                    return 0;
                else
                    return Convert.ToDecimal(this.dtsProductos.Tables[0].Rows[0]["PRECIO_COMPRA"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value > 0)
                    objValor = value;
                this.dtsProductos.Tables[0].Rows[0]["PRECIO_COMPRA"] = objValor;
            }
        }

        public int Existencia
        {
            get
            {
                this.Cargar();
                if (this.dtsProductos.Tables[0].Rows[0]["EXISTENCIA"] == DBNull.Value)
                    return 0;
                else
                    return Convert.ToInt32(this.dtsProductos.Tables[0].Rows[0]["EXISTENCIA"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != 0)
                    objValor = value;
                this.dtsProductos.Tables[0].Rows[0]["EXISTENCIA"] = objValor;
            }
        }

        public bool esNuevo
        {
            get
            {
                this.Cargar();
                return (this.dtsProductos.Tables[0].Rows[0].RowState == DataRowState.Added);
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
                    string strConsulta = "SELECT * FROM PRODUCTOS WHERE CLAVE_PRODUCTO = @CLAVE_PRODUCTO";
                    cmd = new SqlCommand(strConsulta, con);
                    cmd.Parameters.AddWithValue("@CLAVE_PRODUCTO", this.m_ClaveProducto);
                    dapProductos = new SqlDataAdapter();
                    dtsProductos = new DataSet();
                    dapProductos.SelectCommand = cmd;
                    dapProductos.Fill(dtsProductos);
                    con.Close();

                    #endregion

                    #region Siguiente consecutivo

                    if (this.dtsProductos.Tables[0].Rows.Count == 0)
                    {
                        DataRow drwUsuarios = dtsProductos.Tables[0].NewRow();
                        drwUsuarios["CLAVE_PRODUCTO"] = this.m_ClaveProducto;
                        dtsProductos.Tables[0].Rows.Add(drwUsuarios);
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    con.Close();
                    throw new Exception("Ocurrio un error al obtener los Productos. " + ex.Message);
                }
                cargarDatos = false;
            }
        }

        public void Guardar()
        {
            try
            {
                con.Open();
                SqlCommandBuilder cbProductos = new SqlCommandBuilder(dapProductos);
                dapProductos.InsertCommand = cbProductos.GetInsertCommand(true);
                dapProductos.UpdateCommand = cbProductos.GetUpdateCommand(true);
                dapProductos.DeleteCommand = cbProductos.GetDeleteCommand(true);
                dapProductos.Update(dtsProductos.Tables[0]);
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
