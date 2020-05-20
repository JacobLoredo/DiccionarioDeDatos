namespace Diccionario_de_datos
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvEntidades = new System.Windows.Forms.DataGridView();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDir_Ent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDir_Atr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDir_Datos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDir_Sig_Ent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lb_entidad = new System.Windows.Forms.Label();
            this.lb_cabecera = new System.Windows.Forms.Label();
            this.tb_Cabecera = new System.Windows.Forms.TextBox();
            this.bt_AgregarEnt = new System.Windows.Forms.Button();
            this.bt_ModificarEnt = new System.Windows.Forms.Button();
            this.bt_EliminarEnt = new System.Windows.Forms.Button();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crearArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.lb_NombreEnt = new System.Windows.Forms.Label();
            this.tb_Entidad = new System.Windows.Forms.TextBox();
            this.lbArch = new System.Windows.Forms.Label();
            this.lb_Actual = new System.Windows.Forms.Label();
            this.bt_CrearEntidad = new System.Windows.Forms.Button();
            this.bt_AgregaAtr = new System.Windows.Forms.Button();
            this.btn_buscarEntidades = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntidades)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvEntidades
            // 
            this.dgvEntidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEntidades.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNombre,
            this.colDir_Ent,
            this.colDir_Atr,
            this.colDir_Datos,
            this.colDir_Sig_Ent});
            this.dgvEntidades.Location = new System.Drawing.Point(12, 121);
            this.dgvEntidades.Name = "dgvEntidades";
            this.dgvEntidades.RowHeadersWidth = 62;
            this.dgvEntidades.Size = new System.Drawing.Size(794, 150);
            this.dgvEntidades.TabIndex = 1;
            this.dgvEntidades.Visible = false;
            // 
            // colNombre
            // 
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.MinimumWidth = 8;
            this.colNombre.Name = "colNombre";
            this.colNombre.Width = 150;
            // 
            // colDir_Ent
            // 
            this.colDir_Ent.HeaderText = "Dir.Ent";
            this.colDir_Ent.MinimumWidth = 8;
            this.colDir_Ent.Name = "colDir_Ent";
            this.colDir_Ent.Width = 150;
            // 
            // colDir_Atr
            // 
            this.colDir_Atr.HeaderText = "Dir.Atri";
            this.colDir_Atr.MinimumWidth = 8;
            this.colDir_Atr.Name = "colDir_Atr";
            this.colDir_Atr.Width = 150;
            // 
            // colDir_Datos
            // 
            this.colDir_Datos.HeaderText = "Dir.Datos";
            this.colDir_Datos.MinimumWidth = 8;
            this.colDir_Datos.Name = "colDir_Datos";
            this.colDir_Datos.Width = 150;
            // 
            // colDir_Sig_Ent
            // 
            this.colDir_Sig_Ent.HeaderText = "Dir.Sig.Ent";
            this.colDir_Sig_Ent.MinimumWidth = 8;
            this.colDir_Sig_Ent.Name = "colDir_Sig_Ent";
            this.colDir_Sig_Ent.Width = 150;
            // 
            // lb_entidad
            // 
            this.lb_entidad.AutoSize = true;
            this.lb_entidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lb_entidad.Location = new System.Drawing.Point(343, 91);
            this.lb_entidad.Name = "lb_entidad";
            this.lb_entidad.Size = new System.Drawing.Size(94, 24);
            this.lb_entidad.TabIndex = 2;
            this.lb_entidad.Text = "Entidades";
            this.lb_entidad.Visible = false;
            // 
            // lb_cabecera
            // 
            this.lb_cabecera.AutoSize = true;
            this.lb_cabecera.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_cabecera.Location = new System.Drawing.Point(377, 46);
            this.lb_cabecera.Name = "lb_cabecera";
            this.lb_cabecera.Size = new System.Drawing.Size(112, 26);
            this.lb_cabecera.TabIndex = 3;
            this.lb_cabecera.Text = "Cabecera:";
            this.lb_cabecera.Visible = false;
            // 
            // tb_Cabecera
            // 
            this.tb_Cabecera.Enabled = false;
            this.tb_Cabecera.Location = new System.Drawing.Point(496, 51);
            this.tb_Cabecera.Name = "tb_Cabecera";
            this.tb_Cabecera.Size = new System.Drawing.Size(41, 20);
            this.tb_Cabecera.TabIndex = 4;
            this.tb_Cabecera.Visible = false;
            // 
            // bt_AgregarEnt
            // 
            this.bt_AgregarEnt.Location = new System.Drawing.Point(303, 311);
            this.bt_AgregarEnt.Name = "bt_AgregarEnt";
            this.bt_AgregarEnt.Size = new System.Drawing.Size(75, 23);
            this.bt_AgregarEnt.TabIndex = 5;
            this.bt_AgregarEnt.Text = "Agregar";
            this.bt_AgregarEnt.UseVisualStyleBackColor = true;
            this.bt_AgregarEnt.Visible = false;
            this.bt_AgregarEnt.Click += new System.EventHandler(this.bt_AgregarEnt_Click);
            // 
            // bt_ModificarEnt
            // 
            this.bt_ModificarEnt.Location = new System.Drawing.Point(303, 342);
            this.bt_ModificarEnt.Name = "bt_ModificarEnt";
            this.bt_ModificarEnt.Size = new System.Drawing.Size(75, 19);
            this.bt_ModificarEnt.TabIndex = 6;
            this.bt_ModificarEnt.Text = "Modificar";
            this.bt_ModificarEnt.UseVisualStyleBackColor = true;
            this.bt_ModificarEnt.Visible = false;
            this.bt_ModificarEnt.Click += new System.EventHandler(this.bt_ModificarEnt_Click);
            // 
            // bt_EliminarEnt
            // 
            this.bt_EliminarEnt.Location = new System.Drawing.Point(303, 367);
            this.bt_EliminarEnt.Name = "bt_EliminarEnt";
            this.bt_EliminarEnt.Size = new System.Drawing.Size(75, 21);
            this.bt_EliminarEnt.TabIndex = 7;
            this.bt_EliminarEnt.Text = "Eliminar";
            this.bt_EliminarEnt.UseVisualStyleBackColor = true;
            this.bt_EliminarEnt.Visible = false;
            this.bt_EliminarEnt.Click += new System.EventHandler(this.bt_EliminarEnt_Click);
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crearArchivoToolStripMenuItem,
            this.abrirArchivoToolStripMenuItem,
            this.cerrarToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 22);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // crearArchivoToolStripMenuItem
            // 
            this.crearArchivoToolStripMenuItem.Name = "crearArchivoToolStripMenuItem";
            this.crearArchivoToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.crearArchivoToolStripMenuItem.Text = "Crear";
            this.crearArchivoToolStripMenuItem.Click += new System.EventHandler(this.crearArchivoToolStripMenuItem_Click);
            // 
            // abrirArchivoToolStripMenuItem
            // 
            this.abrirArchivoToolStripMenuItem.Name = "abrirArchivoToolStripMenuItem";
            this.abrirArchivoToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.abrirArchivoToolStripMenuItem.Text = "Abrir";
            this.abrirArchivoToolStripMenuItem.Click += new System.EventHandler(this.abrirArchivoToolStripMenuItem_Click);
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.cerrarToolStripMenuItem.Text = "Cerrar";
            this.cerrarToolStripMenuItem.Click += new System.EventHandler(this.cerrarToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(843, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // lb_NombreEnt
            // 
            this.lb_NombreEnt.AutoSize = true;
            this.lb_NombreEnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_NombreEnt.Location = new System.Drawing.Point(26, 340);
            this.lb_NombreEnt.Name = "lb_NombreEnt";
            this.lb_NombreEnt.Size = new System.Drawing.Size(69, 20);
            this.lb_NombreEnt.TabIndex = 8;
            this.lb_NombreEnt.Text = "Nombre:";
            this.lb_NombreEnt.Visible = false;
            // 
            // tb_Entidad
            // 
            this.tb_Entidad.Location = new System.Drawing.Point(101, 342);
            this.tb_Entidad.Name = "tb_Entidad";
            this.tb_Entidad.Size = new System.Drawing.Size(111, 20);
            this.tb_Entidad.TabIndex = 9;
            this.tb_Entidad.Visible = false;
            // 
            // lbArch
            // 
            this.lbArch.AutoSize = true;
            this.lbArch.Location = new System.Drawing.Point(12, 24);
            this.lbArch.Name = "lbArch";
            this.lbArch.Size = new System.Drawing.Size(134, 13);
            this.lbArch.TabIndex = 10;
            this.lbArch.Text = "Nombre del archivo actual:";
            this.lbArch.Visible = false;
            // 
            // lb_Actual
            // 
            this.lb_Actual.AutoSize = true;
            this.lb_Actual.Location = new System.Drawing.Point(146, 24);
            this.lb_Actual.Name = "lb_Actual";
            this.lb_Actual.Size = new System.Drawing.Size(0, 13);
            this.lb_Actual.TabIndex = 11;
            this.lb_Actual.Visible = false;
            // 
            // bt_CrearEntidad
            // 
            this.bt_CrearEntidad.Location = new System.Drawing.Point(361, 180);
            this.bt_CrearEntidad.Name = "bt_CrearEntidad";
            this.bt_CrearEntidad.Size = new System.Drawing.Size(120, 40);
            this.bt_CrearEntidad.TabIndex = 12;
            this.bt_CrearEntidad.Text = "Crear Entidad";
            this.bt_CrearEntidad.UseVisualStyleBackColor = true;
            this.bt_CrearEntidad.Visible = false;
            this.bt_CrearEntidad.Click += new System.EventHandler(this.bt_CrearEntidad_Click);
            // 
            // bt_AgregaAtr
            // 
            this.bt_AgregaAtr.Location = new System.Drawing.Point(398, 338);
            this.bt_AgregaAtr.Name = "bt_AgregaAtr";
            this.bt_AgregaAtr.Size = new System.Drawing.Size(109, 26);
            this.bt_AgregaAtr.TabIndex = 14;
            this.bt_AgregaAtr.Text = "Agrega Atributos";
            this.bt_AgregaAtr.UseVisualStyleBackColor = true;
            this.bt_AgregaAtr.Visible = false;
            this.bt_AgregaAtr.Click += new System.EventHandler(this.bt_AgregaAtr_Click);
            // 
            // btn_buscarEntidades
            // 
            this.btn_buscarEntidades.Location = new System.Drawing.Point(398, 364);
            this.btn_buscarEntidades.Name = "btn_buscarEntidades";
            this.btn_buscarEntidades.Size = new System.Drawing.Size(109, 23);
            this.btn_buscarEntidades.TabIndex = 15;
            this.btn_buscarEntidades.Text = "Buscar";
            this.btn_buscarEntidades.UseVisualStyleBackColor = true;
            this.btn_buscarEntidades.Click += new System.EventHandler(this.btn_buscarEntidades_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 450);
            this.Controls.Add(this.btn_buscarEntidades);
            this.Controls.Add(this.bt_AgregaAtr);
            this.Controls.Add(this.bt_CrearEntidad);
            this.Controls.Add(this.lb_Actual);
            this.Controls.Add(this.lbArch);
            this.Controls.Add(this.tb_Entidad);
            this.Controls.Add(this.lb_NombreEnt);
            this.Controls.Add(this.bt_EliminarEnt);
            this.Controls.Add(this.bt_ModificarEnt);
            this.Controls.Add(this.bt_AgregarEnt);
            this.Controls.Add(this.tb_Cabecera);
            this.Controls.Add(this.lb_cabecera);
            this.Controls.Add(this.lb_entidad);
            this.Controls.Add(this.dgvEntidades);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Diccionario de Datos";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntidades)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvEntidades;
        private System.Windows.Forms.Label lb_entidad;
        private System.Windows.Forms.Label lb_cabecera;
        private System.Windows.Forms.TextBox tb_Cabecera;
        private System.Windows.Forms.Button bt_AgregarEnt;
        private System.Windows.Forms.Button bt_ModificarEnt;
        private System.Windows.Forms.Button bt_EliminarEnt;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crearArchivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirArchivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label lb_NombreEnt;
        private System.Windows.Forms.TextBox tb_Entidad;
        private System.Windows.Forms.Label lbArch;
        private System.Windows.Forms.Label lb_Actual;
        private System.Windows.Forms.Button bt_CrearEntidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDir_Ent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDir_Atr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDir_Datos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDir_Sig_Ent;
        private System.Windows.Forms.Button bt_AgregaAtr;
        private System.Windows.Forms.Button btn_buscarEntidades;
    }
}

