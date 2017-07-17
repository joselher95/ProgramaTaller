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
    public partial class frmCatalogoClientes : Form
    {
        #region Constructor
        public frmCatalogoClientes()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos de los controles

        private void Cliente_Load(object sender, EventArgs e)
        {
            this.llenarGrid();
            this.Default();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Default();
            txtRFC.Enabled = true;
            rbPersonaFisica.Enabled = true;
            rbPersonaMoral.Enabled = true;
            txtApellidoMaterno.Enabled = true;
            txtApellidoPaterno.Enabled = true;
            txtNombres.Enabled = true;
            txtCalle.Enabled = true;
            txtColonia.Enabled = true;
            txtNumero.Enabled = true;
            txtCorreo.Enabled = true;
            txtTelefono.Enabled = true;
            this.rbPersonaFisica.Checked = true;
            btnGuardar.Enabled = true;

            Collection collection = new Collection();
            this.txtClave.Text = collection.obtenerSiguienteCliente().ToString();
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
                this.txtApellidoPaterno.Text = cliente.ApellidoPaterno;
                this.txtApellidoMaterno.Text = cliente.ApellidoMaterno;
                this.txtNombres.Text = cliente.Nombres;
                this.txtRazonSocial.Text = cliente.RazonSocial;
                this.txtRFC.Enabled = true;
                this.txtApellidoPaterno.Enabled = true;
                this.txtApellidoMaterno.Enabled = true;
                this.txtNombres.Enabled = true;
                switch (cliente.TipoCliente)
                {
                    case ('F'):
                        this.rbPersonaFisica.Checked = true;
                        break;
                    case ('M'):
                        this.rbPersonaMoral.Checked = true;
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
                MessageBox.Show("Debe seleccionar un Cliente a editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Default();
        }

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
                llenarGrid();
                Default();
                MessageBox.Show("Se ha guardado el cliente correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error guardar el cliente. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        private void rbPersonaFisica_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPersonaFisica.Checked)
            {
                txtRazonSocial.Enabled = false;
            }
            else
            {
                txtRazonSocial.Enabled = true;
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            (gvClientes.DataSource as DataTable).DefaultView.RowFilter = string.Format("Razon_Social LIKE '{0}%'", txtFiltro.Text);
        }

        private void frmCatalogoClientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.frmCatalogoClientes = null;
        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ';
        }

        private void txtRazonSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ';
        }

        private void txtApellidoPaterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtApellidoMaterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ';
        }

        private void txtRFC_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsNumber(e.KeyChar);
        }

        private void txtCalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ';
        }

        private void txtColonia_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ';
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '-';
        }
        #endregion

        #region Metodos

        private DataTable crearTablaClientes()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Clave");
            dt.Columns.Add("RFC");
            dt.Columns.Add("Razon_Social");
            dt.Columns.Add("Domicilio");
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
                drwDatosGrid["Razon_Social"] = cliente.NombreCompleto;
                drwDatosGrid["Telefono"] = cliente.Telefono;
                drwDatosGrid["Domicilio"] = cliente.Domicilio;
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
            txtRFC.Enabled = false;
            rbPersonaFisica.Enabled = false;
            rbPersonaMoral.Enabled = false;
            txtApellidoMaterno.Enabled = false;
            txtApellidoPaterno.Enabled = false;
            txtNombres.Enabled = false;
            txtCalle.Enabled = false;
            txtColonia.Enabled = false;
            txtNumero.Enabled = false;
            txtCorreo.Enabled = false;
            txtTelefono.Enabled = false;
            this.btnGuardar.Enabled = false;
            this.gvClientes.ClearSelection();
            this.gvClientes.CurrentCell = null;
            //gvClientes.ForeColor = Color.Gray;
            //gvClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            gvClientes.DefaultCellStyle.SelectionBackColor = Color.CadetBlue;
            gvClientes.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        #endregion 
    }
}
