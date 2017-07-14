using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProgramaTaller.Clases
{
    class DetalleCompra
    {
        #region Variables

        SqlCommand cmd;
        SqlDataAdapter dapDetalleDetalleCompras;
        DataSet dtsDetalleDetalleCompras;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);

        private int m_ClaveDetalleCompra;
        private Compra m_Compra;
        private Producto m_Producto;
        private bool cargarDatos = false;

        #endregion

        #region Constructor

        public DetalleCompra(int ClaveDetalleCompra)
        {
            this.m_ClaveDetalleCompra = ClaveDetalleCompra;
            this.cargarDatos = true;
        }

        #endregion

        #region Propiedades

        public int ClaveDetalleVenta
        {
            get
            {
                return this.m_ClaveDetalleCompra;
            }
        }

        public Compra Compra
        {
            get
            {
                this.Cargar();
                return m_Compra;
            }
            set
            {
                this.Cargar();
                this.m_Compra = value;
                object objValor = DBNull.Value;
                if (value != null)
                    objValor = value.ClaveCompra;
                this.dtsDetalleDetalleCompras.Tables[0].Rows[0]["CLAVE_COMPRA"] = objValor;
            }
        }

        public Producto Producto
        {
            get
            {
                this.Cargar();
                return m_Producto;
            }
            set
            {
                this.Cargar();
                this.m_Producto = value;
                object objValor = DBNull.Value;
                if (value != null)
                    objValor = value.ClaveProducto;
                this.dtsDetalleDetalleCompras.Tables[0].Rows[0]["CLAVE_PRODUCTO"] = objValor;
            }
        }

        public int CantidadProductos
        {
            get
            {
                this.Cargar();
                if (this.dtsDetalleDetalleCompras.Tables[0].Rows[0]["CANTIDAD_PRODUCTOS"] == DBNull.Value)
                    return 0;
                else
                    return Convert.ToInt32(this.dtsDetalleDetalleCompras.Tables[0].Rows[0]["CANTIDAD_PRODUCTOS"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != 0)
                    objValor = value;
                this.dtsDetalleDetalleCompras.Tables[0].Rows[0]["CANTIDAD_PRODUCTOS"] = objValor;
            }
        }

        public decimal PrecioUnitario
        {
            get
            {
                this.Cargar();
                if (this.dtsDetalleDetalleCompras.Tables[0].Rows[0]["PRECIO_UNITARIO"] == DBNull.Value)
                    return 0;
                else
                    return Convert.ToDecimal(this.dtsDetalleDetalleCompras.Tables[0].Rows[0]["PRECIO_UNITARIO"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != 0)
                    objValor = value;
                this.dtsDetalleDetalleCompras.Tables[0].Rows[0]["PRECIO_UNITARIO"] = objValor;
            }
        }

        public decimal TotalVenta
        {
            get
            {
                this.Cargar();
                if (this.dtsDetalleDetalleCompras.Tables[0].Rows[0]["TOTAL_COMPRA"] == DBNull.Value)
                    return 0;
                else
                    return Convert.ToDecimal(this.dtsDetalleDetalleCompras.Tables[0].Rows[0]["TOTAL_COMPRA"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != 0)
                    objValor = value;
                this.dtsDetalleDetalleCompras.Tables[0].Rows[0]["TOTAL_VENTA"] = objValor;
            }
        }

        public bool esNuevo
        {
            get
            {
                this.Cargar();
                return (this.dtsDetalleDetalleCompras.Tables[0].Rows[0].RowState == DataRowState.Added);
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
                    string strConsulta = "SELECT * FROM DETALLE_COMPRAS WHERE CLAVE_DETALLE_VCOMPRA = @CLAVE_DETALLE_COMPRA";
                    cmd = new SqlCommand(strConsulta, con);
                    cmd.Parameters.AddWithValue("@CLAVE_DETALLE_COMPRA", this.m_ClaveDetalleCompra);
                    dapDetalleDetalleCompras = new SqlDataAdapter();
                    dtsDetalleDetalleCompras = new DataSet();
                    dapDetalleDetalleCompras.SelectCommand = cmd;
                    dapDetalleDetalleCompras.Fill(dtsDetalleDetalleCompras);
                    con.Close();

                    #endregion

                    #region Siguiente consecutivo

                    if (this.dtsDetalleDetalleCompras.Tables[0].Rows.Count == 0)
                    {
                        DataRow drwUsuarios = dtsDetalleDetalleCompras.Tables[0].NewRow();
                        drwUsuarios["CLAVE_DETALLE_COMPRA"] = this.m_ClaveDetalleCompra;
                        dtsDetalleDetalleCompras.Tables[0].Rows.Add(drwUsuarios);
                    }
                    else
                    {
                        if (this.dtsDetalleDetalleCompras.Tables[0].Rows[0]["CLAVE_COMPRA"] == DBNull.Value)
                            this.m_Compra = null;
                        else
                            this.m_Compra = new Compra(Convert.ToInt32(this.dtsDetalleDetalleCompras.Tables[0].Rows[0]["CLAVE_COMPRA"]));

                        if (this.dtsDetalleDetalleCompras.Tables[0].Rows[0]["CLAVE_PRODUCTO"] == DBNull.Value)
                            this.m_Producto = null;
                        else
                            this.m_Producto = new Producto(Convert.ToInt32(this.dtsDetalleDetalleCompras.Tables[0].Rows[0]["CLAVE_PRODUCTO"]));
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    con.Close();
                    throw new Exception("Ocurrio un error al obtener el Detalle de Venta. " + ex.Message);
                }
                cargarDatos = false;
            }
        }

        public void Guardar()
        {
            try
            {
                con.Open();
                SqlCommandBuilder cbDetalleDetalleVentas = new SqlCommandBuilder(dapDetalleDetalleCompras);
                dapDetalleDetalleCompras.InsertCommand = cbDetalleDetalleVentas.GetInsertCommand(true);
                dapDetalleDetalleCompras.UpdateCommand = cbDetalleDetalleVentas.GetUpdateCommand(true);
                dapDetalleDetalleCompras.DeleteCommand = cbDetalleDetalleVentas.GetDeleteCommand(true);
                dapDetalleDetalleCompras.Update(dtsDetalleDetalleCompras.Tables[0]);
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
