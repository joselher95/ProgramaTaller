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
    public partial class frmRegistroUsuario : Form
    {
        public frmRegistroUsuario()
        {
            InitializeComponent();
        }

        private void frmRegistroUsuario_Load(object sender, EventArgs e)
        {
            ddlEmpleados.Items.Clear();
            Collection collection = new Collection();
            var dataSource = new List<Empleado>();
            foreach (Empleado empleado in collection.CatalogoTrabajadores())
            {
                dataSource.Add(empleado);
            }

            //Setup data binding
            this.ddlEmpleados.DataSource = dataSource;
            ddlEmpleados.DisplayMember = "NombreCompleto";
            ddlEmpleados.ValueMember = "ClaveEmpleado";
        }

        private void btnEmpleadoNuevo_Click(object sender, EventArgs e)
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombreUsuario.Text == "")
                    throw new Exception("Debe ingresar un nombre de usuario.");
                if (txtContraseña.Text == "")
                    throw new Exception("Debe ingresar una contraseña.");
                Collection collection = new Collection();
                foreach(Usuario usuario in collection.catalogoUsuario())
                {
                    if(usuario.NombreUsuario == txtNombreUsuario.Text)
                        throw new Exception("Este nombre de usuario ya está siendo ocupado.");
                    if(ddlEmpleados.SelectedValue.ToString() == usuario.Empleado.ClaveEmpleado.ToString())
                        throw new Exception("Este Empleado ya está siendo usado por otro Usuario.");
                }
                if(txtConfirmarContraseña.Text == "")
                    throw new Exception("Debe confirmar la contraseña.");
                if(txtConfirmarContraseña.Text != txtContraseña.Text)
                    throw new Exception("Las contraseñas no coinciden.");

                Usuario user = new Usuario(collection.obtenerSiguienteUsuario());
                user.NombreUsuario = txtNombreUsuario.Text;
                user.Contraseña = txtContraseña.Text;
                user.Empleado = new Empleado(Convert.ToInt16(ddlEmpleados.SelectedValue));
                user.Guardar();
                MessageBox.Show("Se ha guardado el usuario correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Default();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void Default()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                    control.Text = null;
            }
            ddlEmpleados.SelectedValue = 1;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Default();
        }

        private void frmRegistroUsuario_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.frmRegistroUsuario = null;
        }
    }
}
