namespace ProgramaTaller
{
    partial class frmCatalogoProveedores
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFiltrarNombre = new System.Windows.Forms.TextBox();
            this.gvProveedores = new System.Windows.Forms.DataGridView();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtApellidoMaterno = new System.Windows.Forms.TextBox();
            this.rbPersonaMoral = new System.Windows.Forms.RadioButton();
            this.rbPersonaFisica = new System.Windows.Forms.RadioButton();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtApellidoPaterno = new System.Windows.Forms.TextBox();
            this.txtRFC = new System.Windows.Forms.TextBox();
            this.txtNombres = new System.Windows.Forms.TextBox();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RAZON_SOCIAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RFC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CORREO_ELECTRONICO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TELEFONO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIPO_PROVEEDOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvProveedores)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filtrar por Nombre:";
            // 
            // txtFiltrarNombre
            // 
            this.txtFiltrarNombre.Location = new System.Drawing.Point(202, 26);
            this.txtFiltrarNombre.Name = "txtFiltrarNombre";
            this.txtFiltrarNombre.Size = new System.Drawing.Size(482, 20);
            this.txtFiltrarNombre.TabIndex = 1;
            this.txtFiltrarNombre.TextChanged += new System.EventHandler(this.txtFiltrarNombre_TextChanged);
            // 
            // gvProveedores
            // 
            this.gvProveedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProveedores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Clave,
            this.RAZON_SOCIAL,
            this.RFC,
            this.CORREO_ELECTRONICO,
            this.TELEFONO,
            this.TIPO_PROVEEDOR});
            this.gvProveedores.Location = new System.Drawing.Point(53, 75);
            this.gvProveedores.Name = "gvProveedores";
            this.gvProveedores.Size = new System.Drawing.Size(944, 150);
            this.gvProveedores.TabIndex = 3;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(897, 245);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(100, 42);
            this.btnNuevo.TabIndex = 2;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(793, 245);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(98, 42);
            this.btnEditar.TabIndex = 3;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(693, 245);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(94, 42);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(908, 479);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 42);
            this.btnGuardar.TabIndex = 14;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Enabled = false;
            this.txtRazonSocial.Location = new System.Drawing.Point(294, 338);
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(714, 20);
            this.txtRazonSocial.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(217, 340);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 73;
            this.label12.Text = "Razón Social:";
            // 
            // txtApellidoMaterno
            // 
            this.txtApellidoMaterno.Enabled = false;
            this.txtApellidoMaterno.Location = new System.Drawing.Point(630, 364);
            this.txtApellidoMaterno.Name = "txtApellidoMaterno";
            this.txtApellidoMaterno.Size = new System.Drawing.Size(378, 20);
            this.txtApellidoMaterno.TabIndex = 9;
            // 
            // rbPersonaMoral
            // 
            this.rbPersonaMoral.AutoSize = true;
            this.rbPersonaMoral.Enabled = false;
            this.rbPersonaMoral.Location = new System.Drawing.Point(266, 306);
            this.rbPersonaMoral.Margin = new System.Windows.Forms.Padding(2);
            this.rbPersonaMoral.Name = "rbPersonaMoral";
            this.rbPersonaMoral.Size = new System.Drawing.Size(93, 17);
            this.rbPersonaMoral.TabIndex = 6;
            this.rbPersonaMoral.TabStop = true;
            this.rbPersonaMoral.Text = "Persona Moral";
            this.rbPersonaMoral.UseVisualStyleBackColor = true;
            this.rbPersonaMoral.CheckedChanged += new System.EventHandler(this.rbPersonaMoral_CheckedChanged);
            // 
            // rbPersonaFisica
            // 
            this.rbPersonaFisica.AutoSize = true;
            this.rbPersonaFisica.Enabled = false;
            this.rbPersonaFisica.Location = new System.Drawing.Point(130, 306);
            this.rbPersonaFisica.Margin = new System.Windows.Forms.Padding(2);
            this.rbPersonaFisica.Name = "rbPersonaFisica";
            this.rbPersonaFisica.Size = new System.Drawing.Size(96, 17);
            this.rbPersonaFisica.TabIndex = 5;
            this.rbPersonaFisica.TabStop = true;
            this.rbPersonaFisica.Text = "Persona Física";
            this.rbPersonaFisica.UseVisualStyleBackColor = true;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Enabled = false;
            this.txtTelefono.Location = new System.Drawing.Point(630, 421);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(212, 20);
            this.txtTelefono.TabIndex = 13;
            // 
            // txtCorreo
            // 
            this.txtCorreo.Enabled = false;
            this.txtCorreo.Location = new System.Drawing.Point(147, 421);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(368, 20);
            this.txtCorreo.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(572, 424);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 13);
            this.label11.TabIndex = 64;
            this.label11.Text = "Teléfono:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(44, 424);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 13);
            this.label10.TabIndex = 63;
            this.label10.Text = "Correo Electronico:";
            // 
            // txtApellidoPaterno
            // 
            this.txtApellidoPaterno.Enabled = false;
            this.txtApellidoPaterno.Location = new System.Drawing.Point(147, 367);
            this.txtApellidoPaterno.Name = "txtApellidoPaterno";
            this.txtApellidoPaterno.Size = new System.Drawing.Size(368, 20);
            this.txtApellidoPaterno.TabIndex = 8;
            // 
            // txtRFC
            // 
            this.txtRFC.Enabled = false;
            this.txtRFC.Location = new System.Drawing.Point(630, 392);
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.Size = new System.Drawing.Size(378, 20);
            this.txtRFC.TabIndex = 11;
            // 
            // txtNombres
            // 
            this.txtNombres.Enabled = false;
            this.txtNombres.Location = new System.Drawing.Point(147, 395);
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.Size = new System.Drawing.Size(368, 20);
            this.txtNombres.TabIndex = 10;
            // 
            // txtClave
            // 
            this.txtClave.Enabled = false;
            this.txtClave.Location = new System.Drawing.Point(147, 336);
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(59, 20);
            this.txtClave.TabIndex = 56;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(535, 367);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 55;
            this.label7.Text = "Apellido Materno:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 370);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 54;
            this.label5.Text = "Apellido Paterno:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(593, 395);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 53;
            this.label4.Text = "RFC:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 398);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Nombres:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 338);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "Clave:";
            // 
            // Clave
            // 
            this.Clave.DataPropertyName = "CLAVE";
            this.Clave.HeaderText = "Clave";
            this.Clave.Name = "Clave";
            this.Clave.ReadOnly = true;
            this.Clave.Width = 70;
            // 
            // RAZON_SOCIAL
            // 
            this.RAZON_SOCIAL.DataPropertyName = "RAZON_SOCIAL";
            this.RAZON_SOCIAL.HeaderText = "Razón Social";
            this.RAZON_SOCIAL.Name = "RAZON_SOCIAL";
            this.RAZON_SOCIAL.ReadOnly = true;
            this.RAZON_SOCIAL.Width = 300;
            // 
            // RFC
            // 
            this.RFC.DataPropertyName = "RFC";
            this.RFC.HeaderText = "RFC";
            this.RFC.Name = "RFC";
            this.RFC.ReadOnly = true;
            this.RFC.Width = 150;
            // 
            // CORREO_ELECTRONICO
            // 
            this.CORREO_ELECTRONICO.DataPropertyName = "CORREO";
            this.CORREO_ELECTRONICO.HeaderText = "Correo";
            this.CORREO_ELECTRONICO.Name = "CORREO_ELECTRONICO";
            this.CORREO_ELECTRONICO.ReadOnly = true;
            this.CORREO_ELECTRONICO.Width = 200;
            // 
            // TELEFONO
            // 
            this.TELEFONO.DataPropertyName = "TELEFONO";
            this.TELEFONO.HeaderText = "Teléfono";
            this.TELEFONO.Name = "TELEFONO";
            this.TELEFONO.ReadOnly = true;
            this.TELEFONO.Width = 80;
            // 
            // TIPO_PROVEEDOR
            // 
            this.TIPO_PROVEEDOR.DataPropertyName = "TIPO_PROVEEDOR";
            this.TIPO_PROVEEDOR.HeaderText = "Tipo de Proveedor";
            this.TIPO_PROVEEDOR.Name = "TIPO_PROVEEDOR";
            this.TIPO_PROVEEDOR.ReadOnly = true;
            // 
            // frmCatalogoProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 561);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtRazonSocial);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtApellidoMaterno);
            this.Controls.Add(this.rbPersonaMoral);
            this.Controls.Add(this.rbPersonaFisica);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtApellidoPaterno);
            this.Controls.Add(this.txtRFC);
            this.Controls.Add(this.txtNombres);
            this.Controls.Add(this.txtClave);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.gvProveedores);
            this.Controls.Add(this.txtFiltrarNombre);
            this.Controls.Add(this.label1);
            this.Name = "frmCatalogoProveedores";
            this.Text = "Proveedores";
            this.Load += new System.EventHandler(this.frmCatalogoProveedores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvProveedores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFiltrarNombre;
        private System.Windows.Forms.DataGridView gvProveedores;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtApellidoMaterno;
        private System.Windows.Forms.RadioButton rbPersonaMoral;
        private System.Windows.Forms.RadioButton rbPersonaFisica;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtApellidoPaterno;
        private System.Windows.Forms.TextBox txtRFC;
        private System.Windows.Forms.TextBox txtNombres;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn RAZON_SOCIAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn RFC;
        private System.Windows.Forms.DataGridViewTextBoxColumn CORREO_ELECTRONICO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TELEFONO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIPO_PROVEEDOR;
    }
}