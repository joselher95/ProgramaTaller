using ProgramaTaller.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramaTaller
{
    public partial class frmCatalogoCompra : Form
    {
        public frmCatalogoCompra()
        {
            InitializeComponent();
        }

        private void btnEliminarCompra_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Si elimina esta compra no habrá manera de recuperar esa información que posee. "
                + "¿Está seguro que desea eliminar esta compra?", "Eliminar Compra", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(result == DialogResult.Yes)
            {
                
            }
        }

        private void frmCatalogoCompra_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtClaveCompra.Text.Trim() == "")
                    throw new Exception("Debe teclear la clave de la compra.");

                int claveCompra = Convert.ToInt32(this.txtClaveCompra.Text);

                Compra compra = new Compra(claveCompra);
                if (compra.esNuevo)
                    throw new Exception("La clave de compra no existe");
                this.txtEmpleado.Text = compra.empleado.NombreCompleto;
                switch (compra.Proveedor.TipoProveedor)
                {
                    case('F'):
                        this.txtProveedor.Text = compra.Proveedor.ApellidoPaterno + " " + compra.Proveedor.ApellidoMaterno + " " + compra.Proveedor.Nombres;
                        break;
                    case('M'):
                        this.txtProveedor.Text = compra.Proveedor.RazonSocial;
                        break;
                }
                this.dtpFecha.Value = compra.FechaCompra;

                Collection collection = new Collection();
                DetalleCompra[] arrDetalleCompra = collection.BuscarDetalleCompra(claveCompra);
                DataTable dtDatosGrid = crearTablaDatosGrid();
                foreach (DetalleCompra detalleCompra in arrDetalleCompra)
                {
                    DataRow drwDatosGrid = dtDatosGrid.NewRow();
                    drwDatosGrid["CLAVE_PRODUCTO"] = detalleCompra.Producto.ClaveProducto;
                    drwDatosGrid["DESCRIPCION"] = detalleCompra.Producto.Descripcion;
                    drwDatosGrid["PRECIO"] = detalleCompra.Producto.PrecioCompra;
                    drwDatosGrid["CANTIDAD"] = detalleCompra.CantidadProductos;
                    drwDatosGrid["IMPORTE"] = detalleCompra.TotalCompra;
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

    }
}
