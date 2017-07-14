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
    public partial class frmVentaMercancia : Form
    {
        #region Variables privadas
        DataTable dtDatosGrid;
        #endregion

        #region Constructor
        public frmVentaMercancia()
        {
            InitializeComponent();
        }
        #endregion

        #region Metodos privados
        private DataTable crearTableDatosGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CLAVE");
            dt.Columns.Add("DESCRIPCION");
            dt.Columns.Add("PRECIO");
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
            this.txtDescripcionCliente.Visible = false;
            this.txtDescripcionProducto.Visible = false;
            this.chkManoObra.Checked = false;
            epError.Clear();
            this.dgvProductos.ClearSelection();
            this.dgvProductos.CurrentCell = null;
            dtDatosGrid.Rows.Clear();
            this.dgvProductos.DataSource = dtDatosGrid;

        }
        #endregion

        private void Venta_Load(object sender, EventArgs e)
        {
            dgvProductos.DefaultCellStyle.SelectionBackColor = Color.CadetBlue;
            dgvProductos.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void cbManoObra_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManoObra.Checked)
            {
                this.gbManoObra.Enabled = true;
            }
            else
            {
                this.gbManoObra.Enabled = false;
            }
        }

        private void txtClaveProducto_TextChanged(object sender, EventArgs e)
        {
            int resultado;
            int.TryParse(txtClaveProducto.Text, out resultado);

            if (resultado == 0)
            {
                epError.SetError(txtClaveProducto, "Clave de producto invalida.");
                txtDescripcionProducto.Text = "";
                txtPrecio.Text = "";
                txtDescripcionProducto.Visible = false;
                return;
            }

            Producto producto = new Producto(resultado);
            if (producto.esNuevo)
            {
                epError.SetError(txtClaveProducto, "El producto no existe.");
                txtDescripcionProducto.Text = "";
                txtPrecio.Text = "";
                txtDescripcionProducto.Visible = false;
                return;
            }

            txtDescripcionProducto.Text = producto.Descripcion;
            txtPrecio.Text = producto.PrecioVenta.ToString();
            epError.Clear();
            txtDescripcionProducto.Visible = true;
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClaveProducto.Text == "")
                    throw new Exception("Debe teclear la clave del producto.");
                if (new Producto(Convert.ToInt32(txtClaveProducto.Text)).esNuevo)
                    throw new Exception("No se puede agregar un producto que no existe.");
                if (txtCantidad.Text == "")
                    throw new Exception("Debe teclear la cantidad de productos a agregar.");
                Producto producto = new Producto(Convert.ToInt32(txtClaveProducto.Text));
                if(producto.Existencia < Convert.ToInt32(txtCantidad.Text))
                    throw new Exception("La cantidad deseada excede la cantidad de productos en el invetario.");
                
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
                    decimal importe = Convert.ToDecimal(this.txtPrecio.Text) * Convert.ToInt32(this.txtCantidad.Text);
                    dtDatosGrid.Rows[intIndex]["CANTIDAD"] = this.txtCantidad.Text;
                    dtDatosGrid.Rows[intIndex]["IMPORTE"] = importe;
                }
                else
                {
                    DataRow drwDatosGrid = dtDatosGrid.NewRow();

                    decimal importe = Convert.ToDecimal(this.txtPrecio.Text) * Convert.ToInt32(this.txtCantidad.Text);
                    decimal precio = Convert.ToDecimal(this.txtPrecio.Text);
                    drwDatosGrid["CLAVE"] = this.txtClaveProducto.Text;
                    drwDatosGrid["DESCRIPCION"] = this.txtDescripcionProducto.Text;
                    drwDatosGrid["PRECIO"] = precio;
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

        private void txtClaveCliente_TextChanged(object sender, EventArgs e)
        {
            int resultado;
            int.TryParse(txtClaveCliente.Text, out resultado);

            if (resultado == 0)
            {
                epError.SetError(txtClaveCliente, "Clave de Cliente invalida.");
                txtNombreEmpleado.Text = "";
                return;
            }

            Clientes cliente = new Clientes(resultado);
            if (cliente.esNuevo)
            {
                epError.SetError(txtClaveCliente, "El Cliente no existe.");
                txtNombreEmpleado.Text = "";
                return;
            }

            txtNombreEmpleado.Text = cliente.NombreCompleto;
            epError.Clear();
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClaveCliente.Text == "")
                    throw new Exception("Debe teclear la clave del cliente.");
                if (new Clientes(Convert.ToInt32(txtClaveCliente.Text)).esNuevo)
                    throw new Exception("El cliente no existe.");
                if (chkManoObra.Checked)
                {
                    if (txtClaveEmpleado.Text == "")
                        throw new Exception("Debe teclear la clave del empleado.");
                    if (new Empleado(Convert.ToInt32(txtClaveEmpleado.Text)).esNuevo)
                        throw new Exception("El empleado no existe.");
                }


                Collection collection = new Collection();
                int claveVenta = collection.obtenerSiguienteVenta();
                Venta venta = new Venta(claveVenta);
                venta.Cliente = new Clientes(Convert.ToInt32(txtClaveCliente.Text));
                venta.EmpleadoVenta = new Empleado(Global.EmpleadoSesionActual);
                if (chkManoObra.Checked)
                    venta.EmpleadoManoObra = new Empleado(Convert.ToInt32(this.txtClaveEmpleado.Text));
                venta.FechaVenta = DateTime.Now;
                venta.Guardar();

                foreach (DataRow row in dtDatosGrid.Rows)
                {
                    int claveDetalleVenta = collection.obtenerSiguienteDetalleVenta();
                    DetalleVenta detalleVenta = new DetalleVenta(claveDetalleVenta);

                    Producto producto = new Producto(Convert.ToInt32(row["CLAVE"]));
                    int cantidad = Convert.ToInt32(row["CANTIDAD"]);
                    decimal precio = Convert.ToDecimal(row["PRECIO"]);
                    detalleVenta.Venta = venta;
                    detalleVenta.Producto = producto;
                    detalleVenta.CantidadProductos = cantidad;
                    detalleVenta.PrecioUnitario = producto.PrecioVenta;
                    detalleVenta.TotalVenta = cantidad * precio;
                    detalleVenta.Guardar();
                }
                limpiar();
                MessageBox.Show("Venta guardada exitosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al pagar. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btnCancelarVenta_Click(object sender, EventArgs e)
        {
            this.limpiar();
        }

        private void txtClaveCliente_Leave(object sender, EventArgs e)
        {
            int resultado;
            int.TryParse(txtClaveCliente.Text, out resultado);

            if (resultado == 0)
            {
                epError.SetError(txtClaveCliente, "Clave de cliente invalida.");
                txtDescripcionCliente.Text = "";
                txtDescripcionCliente.Visible = false;
                return;
            }

            Clientes cliente = new Clientes(resultado);
            if (cliente.esNuevo)
            {
                epError.SetError(txtClaveCliente, "El cliente no existe.");
                txtDescripcionCliente.Text = "";
                txtDescripcionCliente.Visible = false;
                return;
            }

            txtDescripcionCliente.Text = cliente.RazonSocial;
            txtDescripcionCliente.Visible = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index;
            if (this.dgvProductos.CurrentCell == null)
                index = -1;
            else
                index = this.dgvProductos.CurrentCell.RowIndex;
            if (index != -1)
            {
                if (MessageBox.Show("¿Seguro que desea eliminar el producto seleccionado?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    dtDatosGrid.Rows[index].Delete();
                    this.dgvProductos.DataSource = dtDatosGrid;

                    #region Actualizar los totales
                    if (dtDatosGrid.Rows.Count != 0)
                    {
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
                    }
                    else
                    {
                        this.txtSubtotal.Text = "";
                        this.txtIva.Text = "";
                        this.txtTotal.Text = "";
                    }
                    #endregion
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto a eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void txtClaveEmpleado_TextChanged(object sender, EventArgs e)
        {
            int resultado;
            int.TryParse(txtClaveEmpleado.Text, out resultado);

            if (resultado == 0)
            {
                epError.SetError(txtClaveEmpleado, "Clave de empleado invalida.");
                txtNombreEmpleado.Text = "";
                return;
            }

            Empleado empleado = new Empleado(resultado);
            if (empleado.esNuevo)
            {
                epError.SetError(txtClaveEmpleado, "El empleado no existe.");
                txtNombreEmpleado.Text = "";
                return;
            }

            txtNombreEmpleado.Text = empleado.NombreCompleto;
            epError.Clear();
        }
    }
}
