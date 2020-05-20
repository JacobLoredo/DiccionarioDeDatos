namespace Diccionario_de_datos
{
    partial class Form3
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
            this.bt_Guardar = new System.Windows.Forms.Button();
            this.dgvRegistros = new System.Windows.Forms.DataGridView();
            this.DirReg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bt_ModificarReg = new System.Windows.Forms.Button();
            this.bt_EliminaReg = new System.Windows.Forms.Button();
            this.btn_busqueda = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistros)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_Guardar
            // 
            this.bt_Guardar.Location = new System.Drawing.Point(329, 24);
            this.bt_Guardar.Name = "bt_Guardar";
            this.bt_Guardar.Size = new System.Drawing.Size(82, 32);
            this.bt_Guardar.TabIndex = 0;
            this.bt_Guardar.Text = "Agregar";
            this.bt_Guardar.UseVisualStyleBackColor = true;
            this.bt_Guardar.Click += new System.EventHandler(this.bt_Guardar_Click);
            // 
            // dgvRegistros
            // 
            this.dgvRegistros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRegistros.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DirReg});
            this.dgvRegistros.Location = new System.Drawing.Point(37, 294);
            this.dgvRegistros.Name = "dgvRegistros";
            this.dgvRegistros.Size = new System.Drawing.Size(757, 276);
            this.dgvRegistros.TabIndex = 1;
            // 
            // DirReg
            // 
            this.DirReg.HeaderText = "Dir. Registro";
            this.DirReg.Name = "DirReg";
            // 
            // bt_ModificarReg
            // 
            this.bt_ModificarReg.Location = new System.Drawing.Point(443, 24);
            this.bt_ModificarReg.Name = "bt_ModificarReg";
            this.bt_ModificarReg.Size = new System.Drawing.Size(82, 32);
            this.bt_ModificarReg.TabIndex = 2;
            this.bt_ModificarReg.Text = "Modificar";
            this.bt_ModificarReg.UseVisualStyleBackColor = true;
            this.bt_ModificarReg.Click += new System.EventHandler(this.bt_ModificarReg_Click);
            // 
            // bt_EliminaReg
            // 
            this.bt_EliminaReg.Location = new System.Drawing.Point(695, 24);
            this.bt_EliminaReg.Name = "bt_EliminaReg";
            this.bt_EliminaReg.Size = new System.Drawing.Size(99, 32);
            this.bt_EliminaReg.TabIndex = 3;
            this.bt_EliminaReg.Text = "Elimina Registro";
            this.bt_EliminaReg.UseVisualStyleBackColor = true;
            this.bt_EliminaReg.Click += new System.EventHandler(this.bt_EliminaReg_Click);
            // 
            // btn_busqueda
            // 
            this.btn_busqueda.Location = new System.Drawing.Point(695, 81);
            this.btn_busqueda.Name = "btn_busqueda";
            this.btn_busqueda.Size = new System.Drawing.Size(99, 23);
            this.btn_busqueda.TabIndex = 4;
            this.btn_busqueda.Text = "Busqueda";
            this.btn_busqueda.UseVisualStyleBackColor = true;
            this.btn_busqueda.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 483);
            this.Controls.Add(this.btn_busqueda);
            this.Controls.Add(this.bt_EliminaReg);
            this.Controls.Add(this.bt_ModificarReg);
            this.Controls.Add(this.dgvRegistros);
            this.Controls.Add(this.bt_Guardar);
            this.Name = "Form3";
            this.Text = "Captura la información";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistros)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_Guardar;
        private System.Windows.Forms.DataGridView dgvRegistros;
        private System.Windows.Forms.DataGridViewTextBoxColumn DirReg;
        private System.Windows.Forms.Button bt_ModificarReg;
        private System.Windows.Forms.Button bt_EliminaReg;
        private System.Windows.Forms.Button btn_busqueda;
    }
}