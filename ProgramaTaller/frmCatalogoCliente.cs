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
    public partial class frmCatalogoCliente : Form
    {
        #region Constructor
        public frmCatalogoCliente()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos de los controles
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtRFC.Enabled = true;
            rbPersonaFisica.Enabled = true;
            rbPersonaMoral.Enabled = true;
            txtCalle.Enabled = true;
            txtColonia.Enabled = true;
            txtNumero.Enabled = true;
            txtCorreo.Enabled = true;
            txtTelefono.Enabled = true;
            this.rbPersonaFisica.Checked = true;

            Collection collection = new Collection();
            this.txtClave.Text = collection.obtenerSiguienteCliente().ToString();
        }

        private void rbPersonaFisica_CheckedChanged(object sender, EventArgs e)
        {
            txtApellidoMaterno.Text = null;
            txtApellidoPaterno.Text = null;
            txtNombres.Text = null;
            txtRazonSocial.Text = null;
            if (rbPersonaFisica.Checked)
            {
                txtApellidoMaterno.Enabled = true;
                txtApellidoPaterno.Enabled = true;
                txtNombres.Enabled = true;
                txtRazonSocial.Enabled = false;
            }
            else
            {
                txtRazonSocial.Enabled = true;
                txtNombres.Enabled = false;
                txtApellidoPaterno.Enabled = false;
                txtApellidoMaterno.Enabled = false;
            }
        }

        private void Cliente_Load(object sender, EventArgs e)
        {
            this.llenarGrid();
            this.Default();
        }
        #endregion

        #region Metodos

        private DataTable crearTablaClientes()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Clave");
            dt.Columns.Add("RFC");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Telefono");
            return dt;
        }

        private void llenarGrid()
        {
            Collection collection = new Collection();
            Clientes[] catalogoClientes = collection.CatalogoClientes();
            DataTable dtDatosGrid = crearTablaClientes();
            foreach (Clientes cliente in catalogoClientes)
            {
                DataRow drwDatosGrid = dtDatosGrid.NewRow();
                drwDatosGrid["Clave"] = cliente.ClaveCliente;
                drwDatosGrid["RFC"] = cliente.Rfc;
                drwDatosGrid["Nombre"] = cliente.NombreCompleto;
                drwDatosGrid["Telefono"] = cliente.Telefono;
                dtDatosGrid.Rows.Add(drwDatosGrid);
            }

            this.gvClientes.DataSource = dtDatosGrid;
        }

        private void Default()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                    control.Text = null;
            }
            this.btnGuardar.Enabled = true;
            this.gvClientes.ClearSelection();
            this.gvClientes.CurrentCell = null;
            //gvClientes.ForeColor = Color.Gray;
            //gvClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            gvClientes.DefaultCellStyle.SelectionBackColor = Color.CadetBlue;
            gvClientes.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        #endregion

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Clientes cliente = new Clientes(Convert.ToInt32(this.txtClave.Text));
                cliente.Rfc = this.txtRFC.Text;

                cliente.Nombres = this.txtNombres.Text;
                cliente.ApellidoPaterno = this.txtApellidoPaterno.Text;
                cliente.ApellidoMaterno = this.txtApellidoMaterno.Text;
                if (rbPersonaFisica.Checked)
                {
                    cliente.TipoCliente = 'F';
                    cliente.RazonSocial = cliente.NombreCompleto;
                }
                else
                {
                    cliente.TipoCliente = 'M';
                    cliente.RazonSocial = this.txtRazonSocial.Text;
                }
                cliente.CalleDomicilio = this.txtCalle.Text;
                cliente.ColoniaDomicilio = this.txtColonia.Text;
                cliente.NumeroDomicilio = Convert.ToInt32(this.txtNumero.Text);
                cliente.CorreoElectronico = this.txtCorreo.Text;
                cliente.Telefono = this.txtTelefono.Text;
                cliente.Guardar();
                this.llenarGrid();
                MessageBox.Show("Se ha guardado el cliente correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error guardar el cliente. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (gvClientes.DataSource as DataTable).DefaultView.RowFilter = string.Format("Nombre LIKE '{0}%'", txtFiltro.Text);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Default();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int index;
            if (this.gvClientes.CurrentCell == null)
                index = -1;
            else
                index = this.gvClientes.CurrentCell.RowIndex;
            if (index != -1)
            {
                DataTable datosGrid = (gvClientes.DataSource as DataTable);
                Clientes cliente = new Clientes(Convert.ToInt32(datosGrid.Rows[index]["Clave"]));
                this.txtClave.Text = cliente.ClaveCliente.ToString();
                this.txtRFC.Text = cliente.Rfc;
                switch (cliente.TipoCliente)
                {
                    case ('F'):
                        this.rbPersonaFisica.Checked = true;
                        this.txtApellidoPaterno.Text = cliente.ApellidoPaterno;
                        this.txtApellidoMaterno.Text = cliente.ApellidoMaterno;
                        this.txtNombres.Text = cliente.Nombres;
                        break;
                    case ('M'):
                        this.txtRazonSocial.Text = cliente.RazonSocial;
                        break;
                }
                this.txtCalle.Text = cliente.CalleDomicilio;
                this.txtColonia.Text = cliente.ColoniaDomicilio;
                this.txtNumero.Text = cliente.NumeroDomicilio.ToString();
                this.txtCorreo.Text = cliente.CorreoElectronico;
                this.txtTelefono.Text = cliente.Telefono;

                this.txtRFC.Enabled = true;
                this.txtCalle.Enabled = true;
                this.txtColonia.Enabled = true;
                this.txtNumero.Enabled = true;
                this.txtCorreo.Enabled = true;
                this.txtTelefono.Enabled = true;
                this.rbPersonaFisica.Enabled = true;
                this.rbPersonaMoral.Enabled = true;
                this.btnGuardar.Enabled = true;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Proveedor a editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        
    }
}
