namespace ProgramaTaller
{
    partial class frmMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmVentaMercancia = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRecepcionMercancia = new System.Windows.Forms.ToolStripMenuItem();
            this.catálogosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCatalogoClientes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCatalogoCompras = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCatalogoVentas = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCatalogoProveedores = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCatalogoProductos = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCatalogoEmpleados = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAyuda = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmVentaMercancia,
            this.tsmRecepcionMercancia,
            this.catálogosToolStripMenuItem,
            this.tsmAyuda,
            this.tsmCerrarSesion,
            this.tsmSalir});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1064, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmVentaMercancia
            // 
            this.tsmVentaMercancia.Name = "tsmVentaMercancia";
            this.tsmVentaMercancia.Size = new System.Drawing.Size(122, 20);
            this.tsmVentaMercancia.Text = "Venta de Mercancía";
            this.tsmVentaMercancia.Click += new System.EventHandler(this.tsmVentaMercancia_Click);
            // 
            // tsmRecepcionMercancia
            // 
            this.tsmRecepcionMercancia.Name = "tsmRecepcionMercancia";
            this.tsmRecepcionMercancia.Size = new System.Drawing.Size(148, 20);
            this.tsmRecepcionMercancia.Text = "Recepción de Mercancía";
            this.tsmRecepcionMercancia.Click += new System.EventHandler(this.tsmRecepcionMercancia_Click);
            // 
            // catálogosToolStripMenuItem
            // 
            this.catálogosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCatalogoClientes,
            this.tsmCatalogoCompras,
            this.tsmCatalogoVentas,
            this.tsmCatalogoProveedores,
            this.tsmCatalogoProductos,
            this.tsmCatalogoEmpleados});
            this.catálogosToolStripMenuItem.Name = "catálogosToolStripMenuItem";
            this.catálogosToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.catálogosToolStripMenuItem.Text = "Catálogos";
            // 
            // tsmCatalogoClientes
            // 
            this.tsmCatalogoClientes.Name = "tsmCatalogoClientes";
            this.tsmCatalogoClientes.Size = new System.Drawing.Size(206, 22);
            this.tsmCatalogoClientes.Text = "Clientes";
            this.tsmCatalogoClientes.Click += new System.EventHandler(this.tsmCatalogoClientes_Click);
            // 
            // tsmCatalogoCompras
            // 
            this.tsmCatalogoCompras.Name = "tsmCatalogoCompras";
            this.tsmCatalogoCompras.Size = new System.Drawing.Size(206, 22);
            this.tsmCatalogoCompras.Text = "Información de Compras";
            this.tsmCatalogoCompras.Click += new System.EventHandler(this.tsmCatalogoCompras_Click);
            // 
            // tsmCatalogoVentas
            // 
            this.tsmCatalogoVentas.Name = "tsmCatalogoVentas";
            this.tsmCatalogoVentas.Size = new System.Drawing.Size(206, 22);
            this.tsmCatalogoVentas.Text = "Información de Ventas";
            this.tsmCatalogoVentas.Click += new System.EventHandler(this.tsmCatalogoVentas_Click);
            // 
            // tsmCatalogoProveedores
            // 
            this.tsmCatalogoProveedores.Name = "tsmCatalogoProveedores";
            this.tsmCatalogoProveedores.Size = new System.Drawing.Size(206, 22);
            this.tsmCatalogoProveedores.Text = "Proveedores";
            this.tsmCatalogoProveedores.Click += new System.EventHandler(this.tsmCatalogoProveedores_Click);
            // 
            // tsmCatalogoProductos
            // 
            this.tsmCatalogoProductos.Name = "tsmCatalogoProductos";
            this.tsmCatalogoProductos.Size = new System.Drawing.Size(206, 22);
            this.tsmCatalogoProductos.Text = "Productos";
            this.tsmCatalogoProductos.Click += new System.EventHandler(this.tsmCatalogoProductos_Click);
            // 
            // tsmCatalogoEmpleados
            // 
            this.tsmCatalogoEmpleados.Name = "tsmCatalogoEmpleados";
            this.tsmCatalogoEmpleados.Size = new System.Drawing.Size(206, 22);
            this.tsmCatalogoEmpleados.Text = "Empleados";
            this.tsmCatalogoEmpleados.Click += new System.EventHandler(this.tsmCatalogoEmpleados_Click);
            // 
            // tsmAyuda
            // 
            this.tsmAyuda.Name = "tsmAyuda";
            this.tsmAyuda.Size = new System.Drawing.Size(53, 20);
            this.tsmAyuda.Text = "Ayuda";
            // 
            // tsmCerrarSesion
            // 
            this.tsmCerrarSesion.Name = "tsmCerrarSesion";
            this.tsmCerrarSesion.Size = new System.Drawing.Size(88, 20);
            this.tsmCerrarSesion.Text = "Cerrar Sesión";
            this.tsmCerrarSesion.Click += new System.EventHandler(this.tsmCerrarSesion_Click);
            // 
            // tsmSalir
            // 
            this.tsmSalir.Name = "tsmSalir";
            this.tsmSalir.Size = new System.Drawing.Size(41, 20);
            this.tsmSalir.Text = "Salir";
            this.tsmSalir.Click += new System.EventHandler(this.tsmSalir_Click);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 661);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menú";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMenu_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmVentaMercancia;
        private System.Windows.Forms.ToolStripMenuItem tsmRecepcionMercancia;
        private System.Windows.Forms.ToolStripMenuItem catálogosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmCatalogoClientes;
        private System.Windows.Forms.ToolStripMenuItem tsmCatalogoCompras;
        private System.Windows.Forms.ToolStripMenuItem tsmCatalogoProveedores;
        private System.Windows.Forms.ToolStripMenuItem tsmCatalogoProductos;
        private System.Windows.Forms.ToolStripMenuItem tsmCatalogoEmpleados;
        private System.Windows.Forms.ToolStripMenuItem tsmCatalogoVentas;
        private System.Windows.Forms.ToolStripMenuItem tsmAyuda;
        private System.Windows.Forms.ToolStripMenuItem tsmCerrarSesion;
        private System.Windows.Forms.ToolStripMenuItem tsmSalir;
    }
}