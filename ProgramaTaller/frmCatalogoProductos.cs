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
    public partial class frmCatalogoProductos : Form
    {
        #region constructor
        public frmCatalogoProductos()
        {
            InitializeComponent();
        }
        #endregion


        #region Metodos

        private DataTable crearTablaProductos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Clave");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("Proveedor");
            dt.Columns.Add("Precio_Compra");
            dt.Columns.Add("Precio_Venta");
            dt.Columns.Add("Existencia");
            return dt;
        }

        private void llenarGrid()
        {
            Collection collection = new Collection();
            Producto[] catalogoProductos = collection.CatalogoProductos();
            DataTable dtDatosGrid = crearTablaProductos();
            foreach (Producto producto in catalogoProductos)
            {
                DataRow drwDatosGrid = dtDatosGrid.NewRow();
                drwDatosGrid["Clave"] = producto.ClaveProducto;
                drwDatosGrid["Nombre"] = producto.NombreCorto;
                drwDatosGrid["Descripcion"] = producto.Descripcion;
                Proveedores proveedor = new Proveedores(producto.claveProveedor);
                drwDatosGrid["Proveedor"] = proveedor.RazonSocial;
                drwDatosGrid["Precio_Compra"] = producto.PrecioCompra;
                drwDatosGrid["Precio_Venta"] = producto.PrecioCompra;
                drwDatosGrid["Existencia"] = producto.Existencia;

                dtDatosGrid.Rows.Add(drwDatosGrid);
            }
            this.gvProductos.DataSource = dtDatosGrid;
        }

        private void Default()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                    control.Text = null;
            }
            this.txtNombre.Enabled = false;
            this.txtDescripcion.Enabled = false;
            this.txtProveedor.Enabled = false;
            this.txtPrecioC.Enabled = false;
            this.txtPrecioV.Enabled = false;
            this.txtExistencia.Enabled = false;
            this.btnGuardar.Enabled = false;
            this.gvProductos.ClearSelection();
            this.gvProductos.CurrentCell = null;
            gvProductos.DefaultCellStyle.SelectionBackColor = Color.CadetBlue;
            gvProductos.DefaultCellStyle.SelectionForeColor = Color.White;
        }
        #endregion

        private void btnGuerdar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombre.Text == "")
                    throw new ArgumentException("Debe ingresar un Nombre.");
                if (txtDescripcion.Text == "")
                    throw new ArgumentException("Debe ingresar una Descripción.");
                if (txtProveedor.Text == "")
                    throw new ArgumentException("Debe ingresar un Proveedor.");
                if (txtNombreProveedor.Text == "")
                    throw new ArgumentException("El proveedor que ha ingresado no existe");
                if (txtPrecioC.Text == "")
                    throw new ArgumentException("Debe ingresar un Precio de Compra.");
                if (txtPrecioV.Text == "")
                    throw new ArgumentException("Debe ingresar un Precio de Venta.");
                if (txtExistencia.Text == "")
                    throw new ArgumentException("Debe ingresar una Existencia.");
                Producto producto = new Producto(Convert.ToInt32(this.txtClave.Text));
                producto.NombreCorto = this.txtNombre.Text;
                producto.Descripcion = this.txtDescripcion.Text;
                producto.claveProveedor = Convert.ToInt32(this.txtProveedor.Text);
                producto.PrecioCompra = Convert.ToDecimal(this.txtPrecioC.Text);
                producto.PrecioVenta = Convert.ToDecimal(this.txtPrecioV.Text);
                producto.Existencia = Convert.ToInt32(this.txtExistencia.Text);
                producto.Guardar();
                this.llenarGrid();
                MessageBox.Show("Se ha guardado el producto correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Default();
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show(aex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error guardar el producto. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = gvProductos.DataSource;
            bs.Filter = gvProductos.Columns["Nombre"].DataPropertyName.ToString() + " LIKE '%" + txtFiltro.Text + "%'";
            gvProductos.DataSource = bs.DataSource;
            //(gvProductos.DataSource as DataTable).DefaultView.RowFilter = string.Format("Nombre LIKE '{0}%'", txtFiltro.Text);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Default();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int index;
            if (this.gvProductos.CurrentCell == null)
                index = -1;
            else
                index = this.gvProductos.CurrentCell.RowIndex;
            if (index != -1)
            {
                DataTable datosGrid = (gvProductos.DataSource as DataTable);
                Producto producto = new Producto(Convert.ToInt32(datosGrid.Rows[index]["Clave"]));
                this.txtClave.Text = producto.ClaveProducto.ToString();
                this.txtNombre.Text = producto.NombreCorto;
                this.txtDescripcion.Text = producto.Descripcion;
                this.txtProveedor.Text = producto.claveProveedor.ToString();
                this.txtPrecioC.Text = producto.PrecioCompra.ToString();
                this.txtPrecioV.Text = producto.PrecioVenta.ToString();
                this.txtExistencia.Text = producto.Existencia.ToString();

                this.txtNombre.Enabled = true;
                this.txtDescripcion.Enabled = true;
                this.txtProveedor.Enabled = true;
                this.txtPrecioC.Enabled = true;
                this.txtPrecioV.Enabled = true;
                this.txtExistencia.Enabled = true;
                this.btnGuardar.Enabled = true;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto a editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.txtNombre.Enabled = true;
            this.txtNombre.Focus();
            this.txtDescripcion.Enabled = true;
            this.txtProveedor.Enabled = true;
            this.txtPrecioC.Enabled = true;
            this.txtPrecioV.Enabled = true;
            this.txtExistencia.Enabled = true;
            this.btnGuardar.Enabled = true;

            Collection collection = new Collection();
            this.txtClave.Text = collection.obtenerSiguienteProducto().ToString();
        }

        private void frmCatalogoProductos_Load(object sender, EventArgs e)
        {
            this.llenarGrid();
            this.Default();
        }

        private void txtProveedor_Leave(object sender, EventArgs e)
        {
            Proveedores proveedor = new Proveedores(Convert.ToInt32(txtProveedor.Text));
            txtNombreProveedor.Text = proveedor.RazonSocial.ToString();
        }

        private void frmCatalogoProductos_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.frmCatalogoProductos = null;
        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtPrecioC_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
