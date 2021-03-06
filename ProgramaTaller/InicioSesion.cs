﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProgramaTaller.Clases;
using System.Data.SqlClient;
using System.IO;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer;
using System.Text.RegularExpressions;

namespace ProgramaTaller
{
    public partial class InicioSesion : Form
    {
        #region Variables
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);
        SqlConnection conMaster = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionMaster"].ConnectionString);
        #endregion

        #region Eventos
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
                    throw new Exception("La contraseña es incorrecta.");

                Global.EmpleadoSesionActual = usuarios.ClaveUsuario;

                this.Hide();
                frmMenu menu = new ProgramaTaller.frmMenu();
                menu.ShowDialog();
                this.Close();
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
            if (!probarConexion())
            {
                string direccion = Environment.CurrentDirectory + "\\DB TALLER.sql";
                string script = File.ReadAllText(direccion);

                IEnumerable<string> commandStrings = Regex.Split(script, @"^\s*GO\s*$",
                           RegexOptions.Multiline | RegexOptions.IgnoreCase);

                conMaster.Open();
                foreach (string commandString in commandStrings)
                {
                    if (commandString.Trim() != "")
                    {
                        using (cmd= new SqlCommand(commandString, conMaster))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                conMaster.Close();
            }
        }
        #endregion

        #region Eventos

        private bool probarConexion()
        {
            try
            {
                con.Open();
                con.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion
    }
}
