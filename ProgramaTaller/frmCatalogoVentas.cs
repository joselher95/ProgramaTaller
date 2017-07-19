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
    public partial class frmCatalogoVentas : Form
    {
        public frmCatalogoVentas()
        {
            InitializeComponent();
        }

        private void btnBuscarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtClaveCompra.Text.Trim() == "")
                    throw new Exception("Debe teclear la clave de la Venta.");

                int claveVenta = Convert.ToInt32(this.txtClaveCompra.Text);

                Venta venta = new Venta(claveVenta);
                if (venta.esNuevo)
                    throw new Exception("La clave de venta no existe");
                this.txtEmpleado.Text = venta.EmpleadoVenta.NombreCompleto;
                if (venta.EmpleadoManoObra != null)
                    this.txtEmpleadoMO.Text = venta.EmpleadoManoObra.NombreCompleto;
                else
                    this.txtEmpleadoMO.Text = "No aplica.";
                txtCliente.Text = venta.Cliente.RazonSocial;
                this.dtpFecha.Value = venta.FechaVenta;

                Collection collection = new Collection();
                DetalleVenta[] arrDetalleCompra = collection.BuscarDetalleVenta(claveVenta);
                DataTable dtDatosGrid = crearTablaDatosGrid();
                foreach (DetalleVenta detalleCompra in arrDetalleCompra)
                {
                    DataRow drwDatosGrid = dtDatosGrid.NewRow();
                    drwDatosGrid["CLAVE_PRODUCTO"] = detalleCompra.Producto.ClaveProducto;
                    drwDatosGrid["DESCRIPCION"] = detalleCompra.Producto.Descripcion;
                    drwDatosGrid["PRECIO"] = detalleCompra.Producto.PrecioCompra;
                    drwDatosGrid["CANTIDAD"] = detalleCompra.CantidadProductos;
                    drwDatosGrid["IMPORTE"] = detalleCompra.TotalVenta;
                    dtDatosGrid.Rows.Add(drwDatosGrid);
                }
                this.dataGridView1.DataSource = dtDatosGrid;
                this.dataGridView1.ClearSelection();
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
                MessageBox.Show(ex.Message, "Advertencia.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private DataTable crearTablaDatosGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CLAVE_PRODUCTO");
            dt.Columns.Add("DESCRIPCION");
            dt.Columns.Add("PRECIO");
            dt.Columns.Add("CANTIDAD");
            dt.Columns.Add("IMPORTE");
            return dt;
        }

        private void frmCatalogoVentas_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.frmCatalogoVentas = null;
        }
    }
}
