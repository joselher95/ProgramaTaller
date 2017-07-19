using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaTaller.Clases
{
    class Venta
    {
        #region Variables

        SqlCommand cmd;
        SqlDataAdapter dapVentas;
        DataSet dtsVentas;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);

        private int m_ClaveVenta;
        private Clientes m_Cliente;
        private Empleado m_EmpleadoManoObra;
        private bool cargarDatos = false;

        #endregion

        #region Constructor

        public Venta(int ClaveVenta)
        {
            this.m_ClaveVenta = ClaveVenta;
            this.cargarDatos = true;
        }

        #endregion

        #region Propiedades

        public int ClaveVenta
        {
            get
            {
                return this.m_ClaveVenta;
            }
        }

        public Clientes Cliente
        {
            get
            {
                this.Cargar();
                return m_Cliente;
            }
            set
            {
                this.Cargar();
                this.m_Cliente = value;
                object objValor = DBNull.Value;
                if (value != null)
                    objValor = value.ClaveCliente;
                this.dtsVentas.Tables[0].Rows[0]["CLAVE_CLIENTE"] = objValor;
            }
        }

        public Empleado EmpleadoVenta
        {
            get
            {
                this.Cargar();
                return new Empleado(Convert.ToInt32(this.dtsVentas.Tables[0].Rows[0]["CLAVE_EMPLEADO_VENTA"]));
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != null)
                    objValor = value.ClaveEmpleado;
                this.dtsVentas.Tables[0].Rows[0]["CLAVE_EMPLEADO_VENTA"] = objValor;
            }
        }

        public Empleado EmpleadoManoObra
        {
            get
            {
                this.Cargar();
                return m_EmpleadoManoObra;
            }
            set
            {
                this.Cargar();
                this.m_EmpleadoManoObra = value;
                object objValor = DBNull.Value;
                if (value != null)
                    objValor = value.ClaveEmpleado;
                this.dtsVentas.Tables[0].Rows[0]["CLAVE_EMPLEADO_MANO_OBRA"] = objValor;
            }
        }

        public DateTime FechaVenta
        {
            get
            {
                this.Cargar();
                if (this.dtsVentas.Tables[0].Rows[0]["FECHA_VENTA"] == DBNull.Value)
                    return DateTime.MinValue;
                else
                    return Convert.ToDateTime(this.dtsVentas.Tables[0].Rows[0]["FECHA_VENTA"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if(value != DateTime.MinValue)
                    objValor = value;
                this.dtsVentas.Tables[0].Rows[0]["FECHA_VENTA"] = objValor;
            }
        }
        public bool esNuevo
        {
            get
            {
                this.Cargar();
                return (this.dtsVentas.Tables[0].Rows[0].RowState == DataRowState.Added);
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
                    string strConsulta = "SELECT * FROM VENTAS WHERE CLAVE_VENTA = @CLAVE_VENTA";
                    cmd = new SqlCommand(strConsulta, con);
                    cmd.Parameters.AddWithValue("@CLAVE_VENTA", this.m_ClaveVenta);
                    dapVentas = new SqlDataAdapter();
                    dtsVentas = new DataSet();
                    dapVentas.SelectCommand = cmd;
                    dapVentas.Fill(dtsVentas);
                    con.Close();

                    #endregion

                    #region Siguiente consecutivo

                    if (this.dtsVentas.Tables[0].Rows.Count == 0)
                    {
                        DataRow drwUsuarios = dtsVentas.Tables[0].NewRow();
                        drwUsuarios["CLAVE_VENTA"] = this.m_ClaveVenta;
                        dtsVentas.Tables[0].Rows.Add(drwUsuarios);
                    }
                    else
                    {
                        if (this.dtsVentas.Tables[0].Rows[0]["CLAVE_CLIENTE"] == DBNull.Value)
                            this.m_Cliente = null;
                        else
                            this.m_Cliente = new Clientes(Convert.ToInt32(this.dtsVentas.Tables[0].Rows[0]["CLAVE_CLIENTE"]));
                        
                        if (this.dtsVentas.Tables[0].Rows[0]["CLAVE_EMPLEADO_MANO_OBRA"] == DBNull.Value)
                            this.m_EmpleadoManoObra = null;
                        else
                            this.m_EmpleadoManoObra = new Empleado(Convert.ToInt32(this.dtsVentas.Tables[0].Rows[0]["CLAVE_EMPLEADO_MANO_OBRA"]));
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    con.Close();
                    throw new Exception("Ocurrio un error al obtener las Ventas. " + ex.Message);
                }
                cargarDatos = false;
            }
        }

        public void Guardar()
        {
            try
            {
                con.Open();
                SqlCommandBuilder cbVentas = new SqlCommandBuilder(dapVentas);
                dapVentas.InsertCommand = cbVentas.GetInsertCommand(true);
                dapVentas.UpdateCommand = cbVentas.GetUpdateCommand(true);
                dapVentas.DeleteCommand = cbVentas.GetDeleteCommand(true);
                dapVentas.Update(dtsVentas.Tables[0]);
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
