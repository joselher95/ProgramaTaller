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

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCatalogoVenta venta = new ProgramaTaller.frmCatalogoVenta();
            venta.Show();
            this.Hide();
        }

        private void recepciónDeMercancíaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRecepcionMercancia recepcion = new ProgramaTaller.frmRecepcionMercancia();
            recepcion.Show();
            this.Hide();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCatalogoCliente cliente = new frmCatalogoCliente();
            cliente.Show();
            this.Hide();
        }

        private void producosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCatalogoProductos productos = new frmCatalogoProductos();
            productos.Show();
            this.Hide();
        }

        private void trabajadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCatalogoTrabajadores trabajadores = new frmCatalogoTrabajadores();
            trabajadores.Show();
            this.Hide();
        }

        private void ProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCatalogoProveedores proveedores = new frmCatalogoProveedores();
            proveedores.Show();
            this.Hide();
        }

        private void btnEliminarCompra_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Si elimina esta compra no habrá manera de recuperar esa información que posee. "
                + "¿Está seguro que desea eliminar esta compra?", "Eliminar Compra", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(result == DialogResult.Yes)
            {
                
            }
        }
    }
}
