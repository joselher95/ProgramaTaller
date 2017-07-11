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
    public partial class InicioSesion : Form
    {
        public InicioSesion()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtUsuario.Text == "")
                {
                    MessageBox.Show("Debe teclear el nombre de usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (this.txtPassword.Text == "")
                {
                    MessageBox.Show("Debe teclear la contrasenia.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Collection collection = new Collection();
                Usuario usuarios = collection.buscarUsuariosPorNombreUsuario(this.txtUsuario.Text);
                if (usuarios == null)
                    throw new Exception("El usuario no existe.");
                if (this.txtPassword.Text != usuarios.Contraseña)
                    throw new Exception("La contrasenia es incorrecta.");

                frmMenu menu = new ProgramaTaller.frmMenu();
                menu.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesion. " + ex.Message, "Error", MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("¿Desea salir?", "Salir", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void InicioSesion_Load(object sender, EventArgs e)
        {

        }
    }
}
