using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaTaller.Clases
{
    class Usuario
    {
        #region Variables

        SqlCommand cmd;
        SqlDataAdapter dapUsuarios;
        DataSet dtsUsuarios;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);

        private int m_ClaveUsuario;
        private Empleado m_Empleado;
        private bool cargarDatos = false;

        #endregion

        #region Constructor

        public Usuario(int ClaveUsuario)
        {
            this.m_ClaveUsuario = ClaveUsuario;
            this.cargarDatos = true;
        }

        #endregion

        #region Propiedades

        public int ClaveUsuario
        {
            get
            {
                return this.m_ClaveUsuario;
            }
        }

        public string NombreUsuario
        {
            get
            {
                this.Cargar();
                return this.dtsUsuarios.Tables[0].Rows[0]["USUARIO"].ToString();
            }
        }

        public string Contraseña
        {
            get
            {
                this.Cargar();
                return this.dtsUsuarios.Tables[0].Rows[0]["CONTRASENIA_USUARIO"].ToString();
            }
        }

        public Empleado Empleado
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
                this.dtsUsuarios.Tables[0].Rows[0]["CLAVE_EMPLEADO"] = objValor;
            }
        }

        public bool esNuevo
        {
            get
            {
                this.Cargar();
                return (this.dtsUsuarios.Tables[0].Rows[0].RowState == DataRowState.Added);
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
                    string strConsulta = "SELECT * FROM Usuarios WHERE CLAVE_USUARIO = @CLAVE_USUARIO";
                    cmd = new SqlCommand(strConsulta, con);
                    cmd.Parameters.AddWithValue("@CLAVE_USUARIO", this.m_ClaveUsuario);
                    dapUsuarios = new SqlDataAdapter();
                    dtsUsuarios = new DataSet();
                    dapUsuarios.SelectCommand = cmd;
                    dapUsuarios.Fill(dtsUsuarios);
                    con.Close();

                    #endregion

                    #region Siguiente consecutivo

                    if (this.dtsUsuarios.Tables[0].Rows.Count == 0)
                    {
                        DataRow drwUsuarios = dtsUsuarios.Tables[0].NewRow();
                        drwUsuarios["CLAVE_USUARIO"] = this.m_ClaveUsuario;
                        dtsUsuarios.Tables[0].Rows.Add(drwUsuarios);
                    }
                    else
                    {
                        if (this.dtsUsuarios.Tables[0].Rows[0]["CLAVE_EMPLEADO"] == DBNull.Value)
                            this.m_Empleado = null;
                        else
                            this.m_Empleado = new Empleado(Convert.ToInt32(this.dtsUsuarios.Tables[0].Rows[0]["CLAVE_EMPLEADO"]));
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    con.Close();
                    throw new Exception("Ocurrio un error al obtener los usuarios" + ex.Message);
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
            dapUsuarios.Update(dtsUsuarios.Tables[0]);
            con.Close();
        }

        public int siguienteConsecutivo()
        {
            #region Consulta
            con.Open();
            string strConsulta = "SELECT MAX(CLAVE_USUARIO)+1 FROM USUARIOS";
            cmd = new SqlCommand(strConsulta, con);
            dapUsuarios = new SqlDataAdapter(cmd);
            DataTable dtResultado = new DataTable();
            dapUsuarios.Fill(dtResultado);
            con.Close();
            
            #endregion

            #region Validar que no regrese 0
            int iResultado = Convert.ToInt32(dtResultado.Rows[0]["CLAVE_USUARIO"]);
            if (iResultado == 0)
                iResultado++;
            #endregion

            return iResultado;
        }
        #endregion
    }
}
