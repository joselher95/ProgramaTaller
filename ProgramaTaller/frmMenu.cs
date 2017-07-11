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
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void ventaDeMercancíaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVentaMercancia venta = new frmVentaMercancia();
            venta.Show();
        }

        private void recepciónDeMercancíaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRecepcionMercancia rMercancia = new frmRecepcionMercancia();
            rMercancia.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCatalogoCliente cliente = new frmCatalogoCliente();
            cliente.Show();
        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCatalogoCompra compra = new frmCatalogoCompra();
            compra.Show();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCatalogoProveedores proveedores = new frmCatalogoProveedores();
            proveedores.Show();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCatalogoProductos productos = new frmCatalogoProductos();
            productos.Show();
        }

        private void trabajadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCatalogoTrabajadores trabajadores = new frmCatalogoTrabajadores();
            trabajadores.Show();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCatalogoVenta venta = new frmCatalogoVenta();
            venta.Show();
        }
    }
}
