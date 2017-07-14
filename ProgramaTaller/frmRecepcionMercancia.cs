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
    public partial class frmRecepcionMercancia : Form
    {
        #region Variables privadas
        DataTable dtDatosGrid;
        #endregion

        #region Constructor
        public frmRecepcionMercancia()
        {
            InitializeComponent();
        }
        #endregion

        #region Métodos de la página
        private void RecepcionMercancia_Load(object sender, EventArgs e)
        {
            dgvProductos.DefaultCellStyle.SelectionBackColor = Color.CadetBlue;
            dgvProductos.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void txtClaveProducto_TextChanged(object sender, EventArgs e)
        {
            int resultado;
            int.TryParse(txtClaveProducto.Text, out resultado);

            if (resultado == 0)
            {
                epError.SetError(txtClaveProducto, "Clave de producto invalida.");
                txtDescripcionProducto.Text = "";
                txtCosto.Text = "";
                txtPrecioVenta.Text = "";
                txtDescripcionProducto.Visible = false;
                return;
            }

            Producto producto = new Producto(resultado);
            if (producto.esNuevo)
            {
                epError.SetError(txtClaveProducto, "El producto no existe.");
                txtDescripcionProducto.Text = "";
                txtCosto.Text = "";
                txtPrecioVenta.Text = "";
                txtDescripcionProducto.Visible = false;
                return;
            }

            txtDescripcionProducto.Text = producto.Descripcion;
            txtCosto.Text = producto.PrecioCompra.ToString();
            txtPrecioVenta.Text = producto.PrecioVenta.ToString();
            epError.Clear();
            txtDescripcionProducto.Visible = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClaveProducto.Text == "")
                    throw new Exception("Debe teclear la clave del producto.");
                if (new Producto(Convert.ToInt32(txtClaveProducto.Text)).esNuevo)
                    throw new Exception("No se puede agregar un producto que no existe.");
                if(txtPrecioVenta.Text == "")
                    throw new Exception("No puede dejar el precio de Venta vacío.");
                if (txtCosto.Text == "")
                    throw new Exception("No puede dejar el precio de Compra vacío.");
                if (txtCantidad.Text == "")
                    throw new Exception("Debe teclear la cantidad de productos a agregar.");
                Producto producto = new Producto(Convert.ToInt32(txtClaveProducto.Text));

                if (this.dtDatosGrid == null)
                    dtDatosGrid = crearTableDatosGrid();

                bool bProductoYaAgregado = false;
                int intIndex = -1;
                for (int i = 0; i < dtDatosGrid.Rows.Count; i++)
                {
                    if (this.txtClaveProducto.Text == dtDatosGrid.Rows[i]["CLAVE"].ToString())
                    {
                        bProductoYaAgregado = true;
                        intIndex = i;
                        break;
                    }
                }
                if (bProductoYaAgregado)
                {
                    decimal importe = Convert.ToDecimal(this.txtCosto.Text) * Convert.ToInt32(this.txtCantidad.Text);
                    dtDatosGrid.Rows[intIndex]["CANTIDAD"] = this.txtCantidad.Text;
                    dtDatosGrid.Rows[intIndex]["IMPORTE"] = importe;
                }
                else
                {
                    DataRow drwDatosGrid = dtDatosGrid.NewRow();

                    decimal importe = Convert.ToDecimal(this.txtCosto.Text) * Convert.ToInt32(this.txtCantidad.Text);
                    decimal precioCompra = Convert.ToDecimal(this.txtCosto.Text);
                    decimal precioVenta = Convert.ToDecimal(this.txtPrecioVenta.Text);

                    drwDatosGrid["CLAVE"] = this.txtClaveProducto.Text;
                    drwDatosGrid["DESCRIPCION"] = this.txtDescripcionProducto.Text;
                    drwDatosGrid["PRECIO_COMPRA"] = precioCompra;
                    drwDatosGrid["PRECIO_VENTA"] = precioVenta;
                    drwDatosGrid["CANTIDAD"] = this.txtCantidad.Text;
                    drwDatosGrid["IMPORTE"] = importe;
                    dtDatosGrid.Rows.Add(drwDatosGrid);
                }

                this.dgvProductos.DataSource = dtDatosGrid;

                #region Actualizar los totales
                decimal subtotal = 0;
                foreach (DataRow row in dtDatosGrid.Rows)
                {
                    subtotal += Convert.ToDecimal(row["IMPORTE"]);
                }
                decimal iva = subtotal * Convert.ToDecimal(0.16);
                decimal total = subtotal + iva;
                this.txtSubtotal.Text = subtotal.ToString("C");
                this.txtIva.Text = iva.ToString("C");
                this.txtTotal.Text = total.ToString("C");
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la venta. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void txtCosto_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtClaveProducto.Text == "")
                    throw new Exception("");
                if (txtCosto.Text == "")
                    throw new Exception("No se puede dejar el Costo vacío.");
                Producto producto = new Producto(Convert.ToInt32(txtClaveProducto.Text));
                if (producto.PrecioCompra != Convert.ToDecimal(txtCosto))
                {
                    producto.PrecioCompra = Convert.ToDecimal(txtCosto);
                    producto.Guardar();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "")
                    MessageBox.Show(ex.Message, "Advertencia.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtPrecioVenta_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtClaveProducto.Text == "")
                    throw new Exception("");
                if (txtPrecioVenta.Text == "")
                    throw new Exception("No se puede dejar el Costo vacío.");
                Producto producto = new Producto(Convert.ToInt32(txtClaveProducto.Text));
                if (producto.PrecioVenta != Convert.ToDecimal(txtPrecioVenta.Text))
                {
                    producto.PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text);
                    producto.Guardar();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "")
                    MessageBox.Show(ex.Message, "Advertencia.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancelarRecepción_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClaveProveedor.Text == "")
                    throw new Exception("Debe teclear la clave del cliente.");
                if (new Proveedores(Convert.ToInt32(txtClaveProveedor.Text)).esNuevo)
                    throw new Exception("El proveedor no existe.");


                Collection collection = new Collection();
                int claveCompra = collection.obtenerSiguienteCompra();
                Compra compra = new Compra(claveCompra);
                compra.empleado = new Empleado(Global.EmpleadoSesionActual);
                compra.Proveedor = new Proveedores(Convert.ToInt32(txtClaveProveedor.Text));
                compra.FechaCompra = DateTime.Now;
                compra.Guardar();

                foreach (DataRow row in dtDatosGrid.Rows)
                {
                    int claveDetalleCompra = collection.obtenerSiguienteDetalleCompra();
                    DetalleCompra detalleCompra = new DetalleCompra(claveDetalleCompra);

                    Producto producto = new Producto(Convert.ToInt32(row["CLAVE"]));
                    int cantidad = Convert.ToInt32(row["CANTIDAD"]);
                    decimal precio = Convert.ToDecimal(row["PRECIO_COMPRA"]);
                    detalleCompra.Compra = compra;
                    detalleCompra.Producto = producto;
                    detalleCompra.CantidadProductos = cantidad;
                    detalleCompra.PrecioUnitario = producto.PrecioCompra;
                    detalleCompra.TotalCompra = cantidad * precio;
                    detalleCompra.Guardar();

                    producto.Existencia = producto.Existencia + cantidad;
                    producto.Guardar();
                }
                limpiar();
                MessageBox.Show("Recepción de mercancía guardada exitosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al pagar. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        #endregion

        #region Métodos privados
        private DataTable crearTableDatosGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CLAVE");
            dt.Columns.Add("DESCRIPCION");
            dt.Columns.Add("PRECIO_COMPRA");
            dt.Columns.Add("PRECIO_VENTA");
            dt.Columns.Add("CANTIDAD");
            dt.Columns.Add("IMPORTE");
            return dt;
        }

        private void limpiar()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                    control.Text = "";
            }
            this.txtNombreProveedor.Visible = false;
            this.txtDescripcionProducto.Visible = false;
            this.dgvProductos.ClearSelection();
            epError.Clear();
            this.dgvProductos.CurrentCell = null;
            dtDatosGrid.Rows.Clear();
            this.dgvProductos.DataSource = dtDatosGrid;
        }

        #endregion

        private void txtClaveProveedor_TextChanged(object sender, EventArgs e)
        {
            int resultado;
            int.TryParse(txtClaveProveedor.Text, out resultado);

            if (resultado == 0)
            {
                epError.SetError(txtClaveProveedor, "Clave de cliente invalida.");
                txtNombreProveedor.Text = "";
                txtNombreProveedor.Visible = false;
                return;
            }

            Proveedores proveedor = new Proveedores(resultado);
            if (proveedor.esNuevo)
            {
                epError.SetError(txtClaveProveedor, "El cliente no existe.");
                txtNombreProveedor.Text = "";
                txtNombreProveedor.Visible = false;
                return;
            }

            txtNombreProveedor.Text = proveedor.RazonSocial;
            txtNombreProveedor.Visible = true;
        }
    }
}
