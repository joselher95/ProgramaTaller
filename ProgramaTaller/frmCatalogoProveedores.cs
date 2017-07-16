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
    public partial class frmCatalogoProveedores : Form
    {
        public frmCatalogoProveedores()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtRazonSocial.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            txtNombres.Text = "";
            txtRFC.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";

            gvProveedores.CurrentCell = null;

            rbPersonaFisica.Enabled = true;
            rbPersonaFisica.Checked = true;
            rbPersonaMoral.Enabled = true;
            txtApellidoPaterno.Enabled = true;
            txtApellidoPaterno.Focus();
            txtApellidoMaterno.Enabled = true;
            txtNombres.Enabled = true;
            txtRFC.Enabled = true;
            txtCorreo.Enabled = true;
            txtTelefono.Enabled = true;
            btnGuardar.Enabled = true;

            Collection collection = new Collection();
            txtClave.Text = collection.obtenerSiguienteProveedor().ToString();
        }

        private void rbPersonaMoral_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPersonaMoral.Checked)
            {
                txtRazonSocial.Enabled = true;
            }
            else
            {
                txtRazonSocial.Enabled = false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!rbPersonaFisica.Checked)
                {
                    if (txtRazonSocial.Text == "")
                        throw new ArgumentException("No puede dejar el campo Razón Social vacío.");
                }
                if (txtApellidoPaterno.Text == "")
                    throw new ArgumentException("No puede dejar el campo Apellido Paterno vacío.");
                if (txtApellidoMaterno.Text == "")
                    throw new ArgumentException("No puede dejar el campo Apellido Materno vacío.");
                if (txtNombres.Text == "")
                    throw new ArgumentException("No puede dejar el campo Nombre vacío.");
                if (txtRFC.Text == "")
                    throw new ArgumentException("No puede dejar el campo RFC vacío.");

                Proveedores proveedor = new Proveedores(Convert.ToInt32(this.txtClave.Text));
                proveedor.Rfc = this.txtRFC.Text;

                proveedor.Nombres = this.txtNombres.Text;
                proveedor.ApellidoPaterno = this.txtApellidoPaterno.Text;
                proveedor.ApellidoMaterno = this.txtApellidoMaterno.Text;
                if (rbPersonaFisica.Checked)
                {
                    proveedor.TipoProveedor = 'F';
                    proveedor.RazonSocial = proveedor.ApellidoPaterno + " " + proveedor.ApellidoMaterno + " " + proveedor.Nombres;
                }
                else
                {
                    proveedor.TipoProveedor = 'M';
                    proveedor.RazonSocial = this.txtRazonSocial.Text;
                }
                proveedor.CorreoElectronico = this.txtCorreo.Text;
                proveedor.Telefono = this.txtTelefono.Text;
                proveedor.Guardar();
                this.llenarGrid();

                MessageBox.Show("Se ha guardado el proveedor correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                Default();
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show(aex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error guardar el proveedor. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCatalogoProveedores_Load(object sender, EventArgs e)
        {
            this.llenarGrid();
            this.Default();
        }

        #region Métodos Privados
        private DataTable crearTablaProveedores()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CLAVE");
            dt.Columns.Add("RAZON_SOCIAL");
            dt.Columns.Add("RFC");
            dt.Columns.Add("CORREO");
            dt.Columns.Add("TELEFONO");
            dt.Columns.Add("TIPO_PROVEEDOR");
            return dt;
        }
        private void llenarGrid()
        {
            Collection collection = new Collection();
            Proveedores[] catalogoProveedores = collection.CatalogoProveedores();
            DataTable dtDatosGrid = crearTablaProveedores();
            foreach (Proveedores proveedor in catalogoProveedores)
            {
                DataRow drwDatosGrid = dtDatosGrid.NewRow();
                drwDatosGrid["Clave"] = proveedor.ClaveProveedor;
                drwDatosGrid["RFC"] = proveedor.Rfc;
                drwDatosGrid["RAZON_SOCIAL"] = proveedor.RazonSocial;
                drwDatosGrid["CORREO"] = proveedor.CorreoElectronico;
                drwDatosGrid["Telefono"] = proveedor.Telefono;
                switch (proveedor.TipoProveedor)
                {
                    case ('F'):
                        drwDatosGrid["TIPO_PROVEEDOR"] = "Persona Física";
                        break;
                    case ('M'):
                        drwDatosGrid["TIPO_PROVEEDOR"] = "Persona Moral";
                        break;
                }
                dtDatosGrid.Rows.Add(drwDatosGrid);
            }

            this.gvProveedores.DataSource = dtDatosGrid;
        }

        private void Default()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                    control.Text = null;
            }
            this.btnGuardar.Enabled = false;
            this.gvProveedores.ClearSelection();
            this.gvProveedores.CurrentCell = null;
            rbPersonaFisica.Enabled = false;
            rbPersonaMoral.Enabled = false;
            txtRazonSocial.Enabled = false;
            txtApellidoPaterno.Enabled = false;
            txtApellidoMaterno.Enabled = false;
            txtNombres.Enabled = false;
            txtRFC.Enabled = false;
            txtCorreo.Enabled = false;
            txtTelefono.Enabled = false;
            btnGuardar.Enabled = false;
            //gvClientes.ForeColor = Color.Gray;
            //gvClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            gvProveedores.DefaultCellStyle.SelectionBackColor = Color.CadetBlue;
            gvProveedores.DefaultCellStyle.SelectionForeColor = Color.White;
        }
        #endregion

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Default();
        }

        private void txtFiltrarNombre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = gvProveedores.DataSource;
                bs.Filter = gvProveedores.Columns[1].DataPropertyName.ToString() + " LIKE '%" + txtFiltrarNombre.Text + "%'";
                gvProveedores.DataSource = bs.DataSource;
                //(gvProveedores.DataSource as DataTable).DefaultView.RowFilter = string.Format("Nombre LIKE '{0}%'", txtFiltrarNombre.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int index;
            if (this.gvProveedores.CurrentCell == null)
                index = -1;
            else
                index = this.gvProveedores.CurrentCell.RowIndex;
            if (index != -1)
            {
                DataTable datosGrid = (gvProveedores.DataSource as DataTable);
                Proveedores proveedor = new Proveedores(Convert.ToInt32(datosGrid.Rows[index]["CLAVE"]));
                this.txtClave.Text = proveedor.ClaveProveedor.ToString();
                this.txtRFC.Text = proveedor.Rfc;
                this.txtRazonSocial.Text = proveedor.RazonSocial;
                this.txtApellidoPaterno.Text = proveedor.ApellidoPaterno;
                this.txtApellidoMaterno.Text = proveedor.ApellidoMaterno;
                this.txtNombres.Text = proveedor.Nombres;
                switch (proveedor.TipoProveedor)
                {
                    case ('F'):
                        this.rbPersonaFisica.Checked = true;
                        break;
                    case ('M'):
                        this.rbPersonaMoral.Checked = true;
                        this.txtRazonSocial.Enabled = true;
                        break;
                }
                this.txtCorreo.Text = proveedor.CorreoElectronico;
                this.txtTelefono.Text = proveedor.Telefono;

                txtApellidoPaterno.Enabled = true;
                txtApellidoMaterno.Enabled = true;
                txtNombres.Enabled = true;
                this.txtRFC.Enabled = true;
                this.txtCorreo.Enabled = true;
                this.txtTelefono.Enabled = true;
                this.rbPersonaFisica.Enabled = true;
                this.rbPersonaMoral.Enabled = true;
                this.btnGuardar.Enabled = true;
            }
        }

        private void frmCatalogoProveedores_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.frmCatalogoProveedores = null;
        }
    }
}
