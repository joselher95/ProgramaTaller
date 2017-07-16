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
    public partial class frmMenu : Form
    {
        #region Variables

        bool bCerrarSesion = false;
        bool bTsmSalir = false;

        #endregion  
        public frmMenu()
        {
            InitializeComponent();
        }

        private void tsmSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cerrar la aplicación?", "Cerrar aplicacion",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Asterisk,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                bTsmSalir = true;
                Application.Exit();
            }
        }

        private void tsmCerrarSesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cerrar la sesión actual?", "Cerrar sesión",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Asterisk,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                bCerrarSesion = true;
                this.Hide();
                InicioSesion frmInicioSesion = new InicioSesion();
                frmInicioSesion.txtUsuario.Focus();
                frmInicioSesion.ShowDialog();
                this.Close();
            }
        }

        private void frmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(bCerrarSesion == false && bTsmSalir == false)
            if (MessageBox.Show("¿Está seguro que desea cerrar la aplicación?", "Cerrar aplicacion",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Asterisk,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes)

                e.Cancel = true;
        }

        private void tsmCatalogoClientes_Click(object sender, EventArgs e)
        {
            if (Global.frmCatalogoClientes == null)
            {
                Global.frmCatalogoClientes = new frmCatalogoClientes();
                Global.frmCatalogoClientes.Show();
            }
            else
            {
                Global.frmCatalogoClientes.BringToFront();
            }
        }

        private void tsmCatalogoCompras_Click(object sender, EventArgs e)
        {
            if (Global.frmCatalogoCompras == null)
            {
                Global.frmCatalogoCompras = new frmCatalogoCompras();
                Global.frmCatalogoCompras.Show();
            }
            else
            {
                Global.frmCatalogoCompras.BringToFront();
            }
        }

        private void tsmCatalogoProductos_Click(object sender, EventArgs e)
        {
            if (Global.frmCatalogoProductos == null)
            {
                Global.frmCatalogoProductos = new frmCatalogoProductos();
                Global.frmCatalogoProductos.Show();
            }
            else
            {
                Global.frmCatalogoProductos.BringToFront();
            }
        }

        private void tsmCatalogoProveedores_Click(object sender, EventArgs e)
        {
            if (Global.frmCatalogoProveedores == null)
            {
                Global.frmCatalogoProveedores = new frmCatalogoProveedores();
                Global.frmCatalogoProveedores.Show();
            }
            else
            {
                Global.frmCatalogoProveedores.BringToFront();
            }
        }

        private void tsmCatalogoEmpleados_Click(object sender, EventArgs e)
        {
            if (Global.frmCatalogoEmpleados == null)
            {
                Global.frmCatalogoEmpleados = new frmCatalogoEmpleados();
                Global.frmCatalogoEmpleados.Show();
            }
            else
            {
                Global.frmCatalogoEmpleados.BringToFront();
            } 
        }

        private void tsmCatalogoVentas_Click(object sender, EventArgs e)
        {
            if (Global.frmCatalogoVentas == null)
            {
                Global.frmCatalogoVentas = new frmCatalogoVentas();
                Global.frmCatalogoVentas.Show();
            }
            else
            {
                Global.frmCatalogoVentas.BringToFront();
            }
        }

        private void tsmVentaMercancia_Click(object sender, EventArgs e)
        {
            if (Global.frmVentaMercancia == null)
            {
                Global.frmVentaMercancia = new frmVentaMercancia();
                Global.frmVentaMercancia.Show();
            }
            else
            {
                Global.frmVentaMercancia.BringToFront();
            }
        }

        private void tsmRecepcionMercancia_Click(object sender, EventArgs e)
        {
            if (Global.frmRecepcionMercancia == null)
            {
                Global.frmRecepcionMercancia = new frmRecepcionMercancia();
                Global.frmRecepcionMercancia.Show();
            }
            else
            {
                Global.frmRecepcionMercancia.BringToFront();
            }
        }
    }
}
