using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaTaller.Clases
{
    class ManoObra
    {
        #region Variables

        SqlCommand cmd;
        SqlDataAdapter dapUsuarios;
        DataSet dtsManoObra;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);

        private int m_ClaveManoObra;
        private Empleado m_Empleado;
        private Venta m_Venta;
        private bool cargarDatos = false;

        #endregion

        #region Constructor

        public ManoObra(int ClaveManoObra)
        {
            this.m_ClaveManoObra = ClaveManoObra;
            this.cargarDatos = true;
        }

        #endregion

        #region Propiedades

        public int ClaveManoObra
        {
            get
            {
                return this.m_ClaveManoObra;
            }
        }

        public Venta Venta
        {
            get
            {
                this.Cargar();
                return this.m_Venta;
            }
            set
            {
                this.Cargar();
                this.m_Venta = value;
                object objValor = DBNull.Value;
                if (value != null)
                    objValor = value.ClaveVenta;
                this.dtsManoObra.Tables[0].Rows[0]["CLAVE_VENTA"] = objValor;
            }
        }

        public Empleado Empleado
        {
            get
            {
                this.Cargar();
                return this.m_Empleado;
            }
            set
            {
                this.Cargar();
                this.m_Empleado = value;
                object objValor = DBNull.Value;
                if (value != null)
                    objValor = value.ClaveEmpleado;
                this.dtsManoObra.Tables[0].Rows[0]["CLAVE_EMPLEADO"] = objValor;

            }
        }

        public decimal Monto
        {
            get
            {
                this.Cargar();
                if (this.dtsManoObra.Tables[0].Rows[0]["MONTO"] == DBNull.Value)
                    return 0;
                else
                    return Convert.ToDecimal(this.dtsManoObra.Tables[0].Rows[0]["MONTO"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != 0)
                    objValor = value;
                this.dtsManoObra.Tables[0].Rows[0]["MONTO"] = objValor;
            }
        }

        public char Estatus
        {
            get
            {
                this.Cargar();
                if (this.dtsManoObra.Tables[0].Rows[0]["ESTATUS"] == DBNull.Value)
                    return '\0';
                else
                    return Convert.ToChar(this.dtsManoObra.Tables[0].Rows[0]["ESTATUS"]);
            }
            set
            {
                this.Cargar();
                object objValor = DBNull.Value;
                if (value != '\0')
                    objValor = value;
                this.dtsManoObra.Tables[0].Rows[0]["ESTATUS"] = objValor;
            }
        }

        public bool esNuevo
        {
            get
            {
                this.Cargar();
                return (this.dtsManoObra.Tables[0].Rows[0].RowState == DataRowState.Added);
            }
        }



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
                    string strConsulta = "SELECT * FROM MANO_OBRA WHERE CLAVE_MANO_OBRA = @CLAVE_MANO_OBRA";
                    cmd = new SqlCommand(strConsulta, con);
                    cmd.Parameters.AddWithValue("@CLAVE_MANO_OBRA", this.m_ClaveManoObra);
                    dapUsuarios = new SqlDataAdapter();
                    dtsManoObra = new DataSet();
                    dapUsuarios.SelectCommand = cmd;
                    dapUsuarios.Fill(dtsManoObra);
                    con.Close();

                    #endregion

                    #region Siguiente consecutivo

                    if (this.dtsManoObra.Tables[0].Rows.Count == 0)
                    {
                        DataRow drwUsuarios = dtsManoObra.Tables[0].NewRow();
                        drwUsuarios["CLAVE_MANO_OBRA"] = this.m_ClaveManoObra;
                        dtsManoObra.Tables[0].Rows.Add(drwUsuarios);
                    }
                    else
                    {
                        if (this.dtsManoObra.Tables[0].Rows[0]["CLAVE_EMPLEADO"] == DBNull.Value)
                            this.m_Empleado = null;
                        else
                            this.m_Empleado = new Empleado(Convert.ToInt32(this.dtsManoObra.Tables[0].Rows[0]["CLAVE_EMPLEADO"]));

                        if (this.dtsManoObra.Tables[0].Rows[0]["CLAVE_VENTA"] == DBNull.Value)
                            this.m_Venta = null;
                        else
                            this.m_Venta = new Venta(Convert.ToInt32(this.dtsManoObra.Tables[0].Rows[0]["CLAVE_VENTA"]));
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    con.Close();
                    throw new Exception("Ocurrio un error al obtener los detalles de la mano de obra" + ex.Message);
                }
                cargarDatos = false;
            }
        }

        public void Guardar()
        {
            con.Open();
            dapUsuarios.InsertCommand = cmd;
            dapUsuarios.UpdateCommand = cmd;
            dapUsuarios.DeleteCommand = cmd;
            dapUsuarios.Update(dtsManoObra.Tables[0]);
            con.Close();
        }

      
        #endregion
    }
}
