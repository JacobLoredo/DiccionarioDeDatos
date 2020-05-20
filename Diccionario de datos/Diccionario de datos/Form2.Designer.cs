namespace Diccionario_de_datos
{
    partial class Form2
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
            this.dgvAtributos = new System.Windows.Forms.DataGridView();
            this.col_Atributo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TipoDato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_LongitudDato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_DirAtributo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TipoIndice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_DirIndice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_DirSigAtributo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bt_AgregarAtr = new System.Windows.Forms.Button();
            this.bt_ModificarAtr = new System.Windows.Forms.Button();
            this.bt_EliminarAtr = new System.Windows.Forms.Button();
            this.lb_NombreAtr = new System.Windows.Forms.Label();
            this.tb_Atributo = new System.Windows.Forms.TextBox();
            this.lb_TipoDato = new System.Windows.Forms.Label();
            this.cb_TipoDato = new System.Windows.Forms.ComboBox();
            this.lb_Longitud = new System.Windows.Forms.Label();
            this.tb_LongitudDato = new System.Windows.Forms.TextBox();
            this.lb_Entidad = new System.Windows.Forms.Label();
            this.lb_EntSeleccionado = new System.Windows.Forms.Label();
            this.lb_TipoIndice = new System.Windows.Forms.Label();
            this.cb_TipoIndice = new System.Windows.Forms.ComboBox();
            this.lb_CabF2 = new System.Windows.Forms.Label();
            this.tb_DirAtr = new System.Windows.Forms.TextBox();
            this.bt_InsertaRegistro = new System.Windows.Forms.Button();
            this.bt_VerIndices = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAtributos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAtributos
            // 
            this.dgvAtributos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAtributos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Atributo,
            this.col_TipoDato,
            this.col_LongitudDato,
            this.col_DirAtributo,
            this.col_TipoIndice,
            this.col_DirIndice,
            this.col_DirSigAtributo});
            this.dgvAtributos.Location = new System.Drawing.Point(12, 61);
            this.dgvAtributos.Name = "dgvAtributos";
            this.dgvAtributos.RowHeadersWidth = 62;
            this.dgvAtributos.Size = new System.Drawing.Size(1032, 189);
            this.dgvAtributos.TabIndex = 0;
            // 
            // col_Atributo
            // 
            this.col_Atributo.HeaderText = "Nombre";
            this.col_Atributo.MinimumWidth = 8;
            this.col_Atributo.Name = "col_Atributo";
            this.col_Atributo.Width = 120;
            // 
            // col_TipoDato
            // 
            this.col_TipoDato.HeaderText = "Tipo de Dato";
            this.col_TipoDato.MinimumWidth = 8;
            this.col_TipoDato.Name = "col_TipoDato";
            this.col_TipoDato.Width = 150;
            // 
            // col_LongitudDato
            // 
            this.col_LongitudDato.HeaderText = "Longitud del Tipo de Dato";
            this.col_LongitudDato.MinimumWidth = 8;
            this.col_LongitudDato.Name = "col_LongitudDato";
            this.col_LongitudDato.Width = 140;
            // 
            // col_DirAtributo
            // 
            this.col_DirAtributo.HeaderText = "Dirección del Atributo";
            this.col_DirAtributo.MinimumWidth = 8;
            this.col_DirAtributo.Name = "col_DirAtributo";
            this.col_DirAtributo.Width = 140;
            // 
            // col_TipoIndice
            // 
            this.col_TipoIndice.HeaderText = "Tipo de Indice";
            this.col_TipoIndice.MinimumWidth = 8;
            this.col_TipoIndice.Name = "col_TipoIndice";
            this.col_TipoIndice.Width = 120;
            // 
            // col_DirIndice
            // 
            this.col_DirIndice.HeaderText = "Dirección del Indice";
            this.col_DirIndice.MinimumWidth = 8;
            this.col_DirIndice.Name = "col_DirIndice";
            this.col_DirIndice.Width = 140;
            // 
            // col_DirSigAtributo
            // 
            this.col_DirSigAtributo.HeaderText = "Dirección del Siguiente Atributo";
            this.col_DirSigAtributo.MinimumWidth = 8;
            this.col_DirSigAtributo.Name = "col_DirSigAtributo";
            this.col_DirSigAtributo.Width = 150;
            // 
            // bt_AgregarAtr
            // 
            this.bt_AgregarAtr.Location = new System.Drawing.Point(457, 282);
            this.bt_AgregarAtr.Name = "bt_AgregarAtr";
            this.bt_AgregarAtr.Size = new System.Drawing.Size(99, 22);
            this.bt_AgregarAtr.TabIndex = 1;
            this.bt_AgregarAtr.Text = "Agregar";
            this.bt_AgregarAtr.UseVisualStyleBackColor = true;
            this.bt_AgregarAtr.Click += new System.EventHandler(this.bt_AgregarAtr_Click);
            // 
            // bt_ModificarAtr
            // 
            this.bt_ModificarAtr.Location = new System.Drawing.Point(457, 311);
            this.bt_ModificarAtr.Name = "bt_ModificarAtr";
            this.bt_ModificarAtr.Size = new System.Drawing.Size(99, 29);
            this.bt_ModificarAtr.TabIndex = 2;
            this.bt_ModificarAtr.Text = "Modificar";
            this.bt_ModificarAtr.UseVisualStyleBackColor = true;
            this.bt_ModificarAtr.Click += new System.EventHandler(this.bt_ModificarAtr_Click);
            // 
            // bt_EliminarAtr
            // 
            this.bt_EliminarAtr.Location = new System.Drawing.Point(457, 346);
            this.bt_EliminarAtr.Name = "bt_EliminarAtr";
            this.bt_EliminarAtr.Size = new System.Drawing.Size(99, 23);
            this.bt_EliminarAtr.TabIndex = 3;
            this.bt_EliminarAtr.Text = "Eliminar";
            this.bt_EliminarAtr.UseVisualStyleBackColor = true;
            this.bt_EliminarAtr.Click += new System.EventHandler(this.bt_EliminarAtr_Click);
            // 
            // lb_NombreAtr
            // 
            this.lb_NombreAtr.AutoSize = true;
            this.lb_NombreAtr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_NombreAtr.Location = new System.Drawing.Point(12, 299);
            this.lb_NombreAtr.Name = "lb_NombreAtr";
            this.lb_NombreAtr.Size = new System.Drawing.Size(69, 20);
            this.lb_NombreAtr.TabIndex = 4;
            this.lb_NombreAtr.Text = "Nombre:";
            // 
            // tb_Atributo
            // 
            this.tb_Atributo.Location = new System.Drawing.Point(87, 303);
            this.tb_Atributo.Name = "tb_Atributo";
            this.tb_Atributo.Size = new System.Drawing.Size(100, 20);
            this.tb_Atributo.TabIndex = 5;
            // 
            // lb_TipoDato
            // 
            this.lb_TipoDato.AutoSize = true;
            this.lb_TipoDato.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_TipoDato.Location = new System.Drawing.Point(12, 334);
            this.lb_TipoDato.Name = "lb_TipoDato";
            this.lb_TipoDato.Size = new System.Drawing.Size(104, 20);
            this.lb_TipoDato.TabIndex = 6;
            this.lb_TipoDato.Text = "Tipo de Dato:";
            // 
            // cb_TipoDato
            // 
            this.cb_TipoDato.FormattingEnabled = true;
            this.cb_TipoDato.Items.AddRange(new object[] {
            "E",
            "C"});
            this.cb_TipoDato.Location = new System.Drawing.Point(115, 337);
            this.cb_TipoDato.Name = "cb_TipoDato";
            this.cb_TipoDato.Size = new System.Drawing.Size(41, 21);
            this.cb_TipoDato.TabIndex = 7;
            this.cb_TipoDato.SelectedIndexChanged += new System.EventHandler(this.Cb_TipoDato_SelectedIndexChanged);
            // 
            // lb_Longitud
            // 
            this.lb_Longitud.AutoSize = true;
            this.lb_Longitud.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Longitud.Location = new System.Drawing.Point(212, 300);
            this.lb_Longitud.Name = "lb_Longitud";
            this.lb_Longitud.Size = new System.Drawing.Size(75, 20);
            this.lb_Longitud.TabIndex = 8;
            this.lb_Longitud.Text = "Longitud:";
            // 
            // tb_LongitudDato
            // 
            this.tb_LongitudDato.Location = new System.Drawing.Point(283, 302);
            this.tb_LongitudDato.Name = "tb_LongitudDato";
            this.tb_LongitudDato.Size = new System.Drawing.Size(57, 20);
            this.tb_LongitudDato.TabIndex = 9;
            // 
            // lb_Entidad
            // 
            this.lb_Entidad.AutoSize = true;
            this.lb_Entidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Entidad.Location = new System.Drawing.Point(54, 29);
            this.lb_Entidad.Name = "lb_Entidad";
            this.lb_Entidad.Size = new System.Drawing.Size(0, 24);
            this.lb_Entidad.TabIndex = 10;
            // 
            // lb_EntSeleccionado
            // 
            this.lb_EntSeleccionado.AutoSize = true;
            this.lb_EntSeleccionado.Location = new System.Drawing.Point(389, 45);
            this.lb_EntSeleccionado.Name = "lb_EntSeleccionado";
            this.lb_EntSeleccionado.Size = new System.Drawing.Size(0, 13);
            this.lb_EntSeleccionado.TabIndex = 12;
            // 
            // lb_TipoIndice
            // 
            this.lb_TipoIndice.AutoSize = true;
            this.lb_TipoIndice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_TipoIndice.Location = new System.Drawing.Point(212, 334);
            this.lb_TipoIndice.Name = "lb_TipoIndice";
            this.lb_TipoIndice.Size = new System.Drawing.Size(112, 20);
            this.lb_TipoIndice.TabIndex = 13;
            this.lb_TipoIndice.Text = "Tipo de Índice:";
            // 
            // cb_TipoIndice
            // 
            this.cb_TipoIndice.FormattingEnabled = true;
            this.cb_TipoIndice.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "6",
            "8"});
            this.cb_TipoIndice.Location = new System.Drawing.Point(325, 337);
            this.cb_TipoIndice.Name = "cb_TipoIndice";
            this.cb_TipoIndice.Size = new System.Drawing.Size(52, 21);
            this.cb_TipoIndice.TabIndex = 14;
            this.cb_TipoIndice.SelectedIndexChanged += new System.EventHandler(this.cb_TipoIndice_SelectedIndexChanged);
            // 
            // lb_CabF2
            // 
            this.lb_CabF2.AutoSize = true;
            this.lb_CabF2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_CabF2.Location = new System.Drawing.Point(448, 29);
            this.lb_CabF2.Name = "lb_CabF2";
            this.lb_CabF2.Size = new System.Drawing.Size(90, 22);
            this.lb_CabF2.TabIndex = 15;
            this.lb_CabF2.Text = "Dirección:";
            // 
            // tb_DirAtr
            // 
            this.tb_DirAtr.Enabled = false;
            this.tb_DirAtr.Location = new System.Drawing.Point(548, 30);
            this.tb_DirAtr.Name = "tb_DirAtr";
            this.tb_DirAtr.Size = new System.Drawing.Size(65, 20);
            this.tb_DirAtr.TabIndex = 16;
            // 
            // bt_InsertaRegistro
            // 
            this.bt_InsertaRegistro.Location = new System.Drawing.Point(457, 375);
            this.bt_InsertaRegistro.Name = "bt_InsertaRegistro";
            this.bt_InsertaRegistro.Size = new System.Drawing.Size(99, 31);
            this.bt_InsertaRegistro.TabIndex = 18;
            this.bt_InsertaRegistro.Text = "Datos";
            this.bt_InsertaRegistro.UseVisualStyleBackColor = true;
            this.bt_InsertaRegistro.Click += new System.EventHandler(this.bt_InsertaRegistro_Click);
            // 
            // bt_VerIndices
            // 
            this.bt_VerIndices.Location = new System.Drawing.Point(773, 398);
            this.bt_VerIndices.Name = "bt_VerIndices";
            this.bt_VerIndices.Size = new System.Drawing.Size(99, 31);
            this.bt_VerIndices.TabIndex = 19;
            this.bt_VerIndices.Text = "hash dinamica";
            this.bt_VerIndices.UseVisualStyleBackColor = true;
            this.bt_VerIndices.Click += new System.EventHandler(this.bt_VerIndices_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(773, 361);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 31);
            this.button1.TabIndex = 20;
            this.button1.Text = "secuencial_indexado";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(773, 323);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 31);
            this.button2.TabIndex = 21;
            this.button2.Text = "secuencial";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(241, 381);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 22;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 441);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bt_VerIndices);
            this.Controls.Add(this.bt_InsertaRegistro);
            this.Controls.Add(this.tb_DirAtr);
            this.Controls.Add(this.lb_CabF2);
            this.Controls.Add(this.cb_TipoIndice);
            this.Controls.Add(this.lb_TipoIndice);
            this.Controls.Add(this.lb_EntSeleccionado);
            this.Controls.Add(this.lb_Entidad);
            this.Controls.Add(this.tb_LongitudDato);
            this.Controls.Add(this.lb_Longitud);
            this.Controls.Add(this.cb_TipoDato);
            this.Controls.Add(this.lb_TipoDato);
            this.Controls.Add(this.tb_Atributo);
            this.Controls.Add(this.lb_NombreAtr);
            this.Controls.Add(this.bt_EliminarAtr);
            this.Controls.Add(this.bt_ModificarAtr);
            this.Controls.Add(this.bt_AgregarAtr);
            this.Controls.Add(this.dgvAtributos);
            this.Name = "Form2";
            this.Text = "Atributos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAtributos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAtributos;
        private System.Windows.Forms.Button bt_AgregarAtr;
        private System.Windows.Forms.Button bt_ModificarAtr;
        private System.Windows.Forms.Button bt_EliminarAtr;
        private System.Windows.Forms.Label lb_NombreAtr;
        private System.Windows.Forms.TextBox tb_Atributo;
        private System.Windows.Forms.Label lb_TipoDato;
        private System.Windows.Forms.ComboBox cb_TipoDato;
        private System.Windows.Forms.Label lb_Longitud;
        private System.Windows.Forms.TextBox tb_LongitudDato;
        private System.Windows.Forms.Label lb_Entidad;
        private System.Windows.Forms.Label lb_EntSeleccionado;
        private System.Windows.Forms.Label lb_TipoIndice;
        private System.Windows.Forms.ComboBox cb_TipoIndice;
        private System.Windows.Forms.Label lb_CabF2;
        private System.Windows.Forms.TextBox tb_DirAtr;
        private System.Windows.Forms.Button bt_InsertaRegistro;
        private System.Windows.Forms.Button bt_VerIndices;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Atributo;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TipoDato;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_LongitudDato;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_DirAtributo;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TipoIndice;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_DirIndice;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_DirSigAtributo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}