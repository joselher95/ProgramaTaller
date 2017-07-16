using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaTaller.Clases
{
    class DetalleVenta
    {
        #region Variables

        SqlCommand cmd;
        SqlDataAdapter dapDetalleDetalleVentas;
        DataSet dtsDetalleDetalleVentas;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);

        private int m_ClaveDetalleVenta;
        private Venta m_Venta;
        private Producto m_Producto;
        private bool cargarDatos = false;

        #endregion

        #region Constructor

        public DetalleVenta(int ClaveDetalleVenta)
        {
            this.m_ClaveDetalleVenta = ClaveDetalleVenta;
            this.cargarDatos = true;
        }

        #endregion

        #region Propiedades

        public int ClaveDetalleVenta
        {
            get
            {
                return this.m_ClaveDetalleVenta;
            }
        }

        public Venta Venta
        {
            get
            {
                this.Cargar();
                return m_Venta;
            }
            set
            {
                this.Cargar();
                this.m_Venta = value;
                object objValor = DBNull.Value;
                if (value != null)
                    objValor = value.ClaveVenta;
                this.dtsDetalleDetalleVentas.Tables[0].Rows[0]["CLAVE_VENTA"] = objValor;
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
                this.dtsDetalleDetalleVentas.Tables[0].Rows[0]["CLAVE_PRODUCTO"] = objValor;
            }
        }

        public int CantidadProductos
        {
            get
            {
                this.Cargar();
                if (this.dtsDetalleDetalleVentas.Tables[0].Rows[0]["CANTIDAD_PRODUCTOS"] == DBNull.Value)
                    return 0;
                else
                    return Convert.ToInt32(this.dtsDetalleDetalleVentas.Tables[0].Rows[0]["CANTIDAD_PRODUCTOS"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != 0)
                    objValor = value;
                this.dtsDetalleDetalleVentas.Tables[0].Rows[0]["CANTIDAD_PRODUCTOS"] = objValor;
            }
        }

        public decimal PrecioUnitario
        {
            get
            {
                this.Cargar();
                if (this.dtsDetalleDetalleVentas.Tables[0].Rows[0]["PRECIO_UNITARIO"] == DBNull.Value)
                    return 0;
                else
                    return Convert.ToDecimal(this.dtsDetalleDetalleVentas.Tables[0].Rows[0]["PRECIO_UNITARIO"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != 0)
                    objValor = value;
                this.dtsDetalleDetalleVentas.Tables[0].Rows[0]["PRECIO_UNITARIO"] = objValor;
            }
        }

        public decimal TotalVenta
        {
            get
            {
                this.Cargar();
                if (this.dtsDetalleDetalleVentas.Tables[0].Rows[0]["TOTAL_VENTA"] == DBNull.Value)
                    return 0;
                else
                    return Convert.ToDecimal(this.dtsDetalleDetalleVentas.Tables[0].Rows[0]["TOTAL_VENTA"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != 0)
                    objValor = value;
                this.dtsDetalleDetalleVentas.Tables[0].Rows[0]["TOTAL_VENTA"] = objValor;
            }
        }

        public bool esNuevo
        {
            get
            {
                this.Cargar();
                return (this.dtsDetalleDetalleVentas.Tables[0].Rows[0].RowState == DataRowState.Added);
            }
        }

        //pendiente propiedad tipo empleado...

        #endregion

        #region Metodos publicos

        public void Cargar()
        {
            string algo = "";
            if (cargarDatos)
            {
                try
                {
                    #region Obtener catalogo
                    con.Open();
                    string strConsulta = "SELECT * FROM DETALLE_VENTAS WHERE CLAVE_DETALLE_VENTA = @CLAVE_DETALLE_VENTA";
                    cmd = new SqlCommand(strConsulta, con);
                    cmd.Parameters.AddWithValue("@CLAVE_DETALLE_VENTA", this.m_ClaveDetalleVenta);
                    dapDetalleDetalleVentas = new SqlDataAdapter();
                    dtsDetalleDetalleVentas = new DataSet();
                    dapDetalleDetalleVentas.SelectCommand = cmd;
                    dapDetalleDetalleVentas.Fill(dtsDetalleDetalleVentas);
                    con.Close();

                    #endregion

                    #region Siguiente consecutivo

                    if (this.dtsDetalleDetalleVentas.Tables[0].Rows.Count == 0)
                    {
                        DataRow drwUsuarios = dtsDetalleDetalleVentas.Tables[0].NewRow();
                        drwUsuarios["CLAVE_DETALLE_VENTA"] = this.m_ClaveDetalleVenta;
                        dtsDetalleDetalleVentas.Tables[0].Rows.Add(drwUsuarios);
                    }
                    else
                    {
                        if (this.dtsDetalleDetalleVentas.Tables[0].Rows[0]["CLAVE_VENTA"] == DBNull.Value)
                            this.m_Venta = null;
                        else
                            this.m_Venta = new Venta(Convert.ToInt32(this.dtsDetalleDetalleVentas.Tables[0].Rows[0]["CLAVE_VENTA"]), new Empleado(Global.EmpleadoSesionActual));

                        if (this.dtsDetalleDetalleVentas.Tables[0].Rows[0]["CLAVE_PRODUCTO"] == DBNull.Value)
                            this.m_Producto = null;
                        else
                            this.m_Producto = new Producto(Convert.ToInt32(this.dtsDetalleDetalleVentas.Tables[0].Rows[0]["CLAVE_PRODUCTO"]));
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
                SqlCommandBuilder cbDetalleDetalleVentas = new SqlCommandBuilder(dapDetalleDetalleVentas);
                dapDetalleDetalleVentas.InsertCommand = cbDetalleDetalleVentas.GetInsertCommand(true);
                dapDetalleDetalleVentas.UpdateCommand = cbDetalleDetalleVentas.GetUpdateCommand(true);
                dapDetalleDetalleVentas.DeleteCommand = cbDetalleDetalleVentas.GetDeleteCommand(true);
                dapDetalleDetalleVentas.Update(dtsDetalleDetalleVentas.Tables[0]);
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
