using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProgramaTaller.Clases;

namespace ProgramaTaller
{
    public partial class frmCatalogoTrabajadores : Form
    {
        #region Constructor
        public frmCatalogoTrabajadores()
        {
            InitializeComponent();
        }
        #endregion

        #region Metodos

        private DataTable crearTablaTrabajadores()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Clave");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Rfc");
            dt.Columns.Add("Domicilio");
            dt.Columns.Add("Correo_Electronico");
            dt.Columns.Add("Telefono");
            return dt;
        }
        private void llenarGrid()
        {
            Collection collection = new Collection();
            Empleado[] catalogoTrabajadores = collection.CatalogoTrabajadores();
            DataTable dtDatosGrid = crearTablaTrabajadores();
            foreach (Empleado trabajador in catalogoTrabajadores)
            {
                DataRow drwDatosGrid = dtDatosGrid.NewRow();
                drwDatosGrid["Clave"] = trabajador.ClaveEmpleado;
                drwDatosGrid["Nombre"] = trabajador.NombreCompleto;
                drwDatosGrid["Rfc"] = trabajador.Rfc;
                drwDatosGrid["Domicilio"] = trabajador.DomicilioCompleto;
                drwDatosGrid["Correo_Electronico"] = trabajador.CorreoElectronico;
                drwDatosGrid["Telefono"] = trabajador.Telefono;
                dtDatosGrid.Rows.Add(drwDatosGrid);
            }
            this.GvTrabajadores.DataSource = dtDatosGrid;

        }

        private void Default()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                    control.Text = null;
            }
            this.txtRFC.Enabled = false;
            this.txtNombre.Enabled = false;
            this.txtApellidoP.Enabled = false;
            this.txtApellidoM.Enabled = false;
            this.txtCalle.Enabled = false;
            this.txtNumero.Enabled = false;
            this.txtColonia.Enabled = false;
            this.txtCorreo.Enabled = false;
            this.txtTelefono.Enabled = false;
            this.btnGuardar.Enabled = false;
            this.GvTrabajadores.ClearSelection();
            this.GvTrabajadores.CurrentCell = null;
            GvTrabajadores.DefaultCellStyle.SelectionBackColor = Color.CadetBlue;
            GvTrabajadores.DefaultCellStyle.SelectionForeColor = Color.White;
        }


        #endregion

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Empleado trabajador = new Empleado(Convert.ToInt32(this.txtClave.Text));
                trabajador.Nombres = this.txtNombre.Text;
                trabajador.Rfc = this.txtRFC.Text;
                trabajador.ApellidoPaterno = this.txtApellidoP.Text;
                trabajador.ApellidoMaterno = this.txtApellidoM.Text;
                trabajador.CalleDomicilio = this.txtCalle.Text;
                trabajador.NumeroDomicilio = Convert.ToInt32(this.txtNumero.Text);
                trabajador.ColoniaDomicilio = this.txtColonia.Text;
                trabajador.CorreoElectronico = this.txtCorreo.Text;
                trabajador.Telefono = this.txtTelefono.Text;
                trabajador.Guardar();
                this.llenarGrid();
                MessageBox.Show("Se ha guardado el trabajador correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Default();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error guardar el trabajabador. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = GvTrabajadores.DataSource;
            bs.Filter = GvTrabajadores.Columns["Nombre"].DataPropertyName.ToString() + " LIKE '%" + txtFiltro.Text + "%'";
            GvTrabajadores.DataSource = bs.DataSource;
            //(GvTrabajadores.DataSource as DataTable).DefaultView.RowFilter = string.Format("Nombre Like '{0}'", txtFiltro.Text);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Default();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int index;
            if (this.GvTrabajadores.CurrentCell == null)
                index = -1;
            else
                index = this.GvTrabajadores.CurrentCell.RowIndex;
            if (index != -1)
            {
                DataTable datosGrid = (GvTrabajadores.DataSource as DataTable);
                Empleado trabajador = new Empleado(Convert.ToInt32(datosGrid.Rows[index]["Clave"]));
                this.txtClave.Text = trabajador.ClaveEmpleado.ToString();
                this.txtRFC.Text = trabajador.Rfc;
                this.txtNombre.Text = trabajador.Nombres;
                this.txtApellidoP.Text = trabajador.ApellidoPaterno;
                this.txtApellidoM.Text = trabajador.ApellidoMaterno;
                this.txtCalle.Text = trabajador.CalleDomicilio;
                this.txtNumero.Text = trabajador.NumeroDomicilio.ToString();
                this.txtColonia.Text = trabajador.ColoniaDomicilio;
                this.txtCorreo.Text = trabajador.CorreoElectronico;
                this.txtTelefono.Text = trabajador.Telefono;

                this.txtClave.Enabled = true;
                this.txtRFC.Enabled = true;
                this.txtNombre.Enabled = true;
                this.txtApellidoP.Enabled = true;
                this.txtApellidoM.Enabled = true;
                this.txtCalle.Enabled = true;
                this.txtNumero.Enabled = true;
                this.txtColonia.Enabled = true;
                this.txtCorreo.Enabled = true;
                this.txtTelefono.Enabled = true;
                this.btnGuardar.Enabled = true;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un trabajador a editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        { 
            this.txtRFC.Enabled = true;
            this.txtNombre.Enabled = true;
            this.txtApellidoP.Enabled = true;
            this.txtApellidoP.Focus();
            this.txtApellidoM.Enabled = true;
            this.txtCalle.Enabled = true;
            this.txtNumero.Enabled = true;
            this.txtColonia.Enabled = true;
            this.txtCorreo.Enabled = true;
            this.txtTelefono.Enabled = true;
            this.btnGuardar.Enabled = true;

            Collection collection = new Collection();
            this.txtClave.Text = collection.obtenerSiguienteTrabajador().ToString();
        }

        private void frmCatalogoTrabajadores_Load(object sender, EventArgs e)
        {
            this.llenarGrid();
            this.Default();
        }
    }
}
