namespace Diccionario_de_datos
{
    partial class Form4
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
            this.dgvPrimario = new System.Windows.Forms.DataGridView();
            this.Col_Dato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desborda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCajon = new System.Windows.Forms.DataGridView();
            this.DirReg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desbordaa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bt_Cajon = new System.Windows.Forms.Button();
            this.lb_atr = new System.Windows.Forms.Label();
            this.lb_Cajon = new System.Windows.Forms.Label();
            this.lb_Indice = new System.Windows.Forms.Label();
            this.lb_numIndice = new System.Windows.Forms.Label();
            this.dgvCajonHash = new System.Windows.Forms.DataGridView();
            this.dgvHash = new System.Windows.Forms.DataGridView();
            this.Numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrimario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajonHash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHash)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPrimario
            // 
            this.dgvPrimario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrimario.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_Dato,
            this.Direccion,
            this.Desborda});
            this.dgvPrimario.Location = new System.Drawing.Point(31, 62);
            this.dgvPrimario.Name = "dgvPrimario";
            this.dgvPrimario.Size = new System.Drawing.Size(357, 350);
            this.dgvPrimario.TabIndex = 0;
            // 
            // Col_Dato
            // 
            this.Col_Dato.HeaderText = "Dato";
            this.Col_Dato.Name = "Col_Dato";
            // 
            // Direccion
            // 
            this.Direccion.HeaderText = "Direccion";
            this.Direccion.Name = "Direccion";
            // 
            // Desborda
            // 
            this.Desborda.HeaderText = "Desbordamiento";
            this.Desborda.Name = "Desborda";
            // 
            // dgvCajon
            // 
            this.dgvCajon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCajon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DirReg,
            this.Desbordaa});
            this.dgvCajon.Location = new System.Drawing.Point(555, 62);
            this.dgvCajon.Name = "dgvCajon";
            this.dgvCajon.Size = new System.Drawing.Size(260, 150);
            this.dgvCajon.TabIndex = 1;
            this.dgvCajon.Visible = false;
            // 
            // DirReg
            // 
            this.DirReg.HeaderText = "Direccion Registro";
            this.DirReg.Name = "DirReg";
            // 
            // Desbordaa
            // 
            this.Desbordaa.HeaderText = "Desbordamiento";
            this.Desbordaa.Name = "Desbordaa";
            // 
            // bt_Cajon
            // 
            this.bt_Cajon.Location = new System.Drawing.Point(426, 62);
            this.bt_Cajon.Name = "bt_Cajon";
            this.bt_Cajon.Size = new System.Drawing.Size(94, 34);
            this.bt_Cajon.TabIndex = 2;
            this.bt_Cajon.Text = "Mostrar Cajon";
            this.bt_Cajon.UseVisualStyleBackColor = true;
            this.bt_Cajon.Visible = false;
            this.bt_Cajon.Click += new System.EventHandler(this.bt_Cajon_Click);
            // 
            // lb_atr
            // 
            this.lb_atr.AutoSize = true;
            this.lb_atr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_atr.Location = new System.Drawing.Point(31, 26);
            this.lb_atr.Name = "lb_atr";
            this.lb_atr.Size = new System.Drawing.Size(0, 20);
            this.lb_atr.TabIndex = 3;
            // 
            // lb_Cajon
            // 
            this.lb_Cajon.AutoSize = true;
            this.lb_Cajon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Cajon.Location = new System.Drawing.Point(551, 26);
            this.lb_Cajon.Name = "lb_Cajon";
            this.lb_Cajon.Size = new System.Drawing.Size(0, 20);
            this.lb_Cajon.TabIndex = 4;
            // 
            // lb_Indice
            // 
            this.lb_Indice.AutoSize = true;
            this.lb_Indice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Indice.Location = new System.Drawing.Point(242, 26);
            this.lb_Indice.Name = "lb_Indice";
            this.lb_Indice.Size = new System.Drawing.Size(49, 17);
            this.lb_Indice.TabIndex = 5;
            this.lb_Indice.Text = "Indice:";
            this.lb_Indice.Visible = false;
            // 
            // lb_numIndice
            // 
            this.lb_numIndice.AutoSize = true;
            this.lb_numIndice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_numIndice.Location = new System.Drawing.Point(298, 26);
            this.lb_numIndice.Name = "lb_numIndice";
            this.lb_numIndice.Size = new System.Drawing.Size(16, 17);
            this.lb_numIndice.TabIndex = 6;
            this.lb_numIndice.Text = "0";
            this.lb_numIndice.Visible = false;
            // 
            // dgvCajonHash
            // 
            this.dgvCajonHash.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCajonHash.Location = new System.Drawing.Point(551, 62);
            this.dgvCajonHash.Name = "dgvCajonHash";
            this.dgvCajonHash.Size = new System.Drawing.Size(350, 350);
            this.dgvCajonHash.TabIndex = 7;
            this.dgvCajonHash.Visible = false;
            // 
            // dgvHash
            // 
            this.dgvHash.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHash.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Numero,
            this.dir,
            this.desb});
            this.dgvHash.Location = new System.Drawing.Point(35, 62);
            this.dgvHash.Name = "dgvHash";
            this.dgvHash.Size = new System.Drawing.Size(353, 350);
            this.dgvHash.TabIndex = 8;
            this.dgvHash.Visible = false;
            // 
            // Numero
            // 
            this.Numero.HeaderText = "Numero";
            this.Numero.Name = "Numero";
            // 
            // dir
            // 
            this.dir.HeaderText = "Apuntador";
            this.dir.Name = "dir";
            // 
            // desb
            // 
            this.desb.HeaderText = "Desbordamiento";
            this.desb.Name = "desb";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 450);
            this.Controls.Add(this.dgvHash);
            this.Controls.Add(this.dgvCajonHash);
            this.Controls.Add(this.lb_numIndice);
            this.Controls.Add(this.lb_Indice);
            this.Controls.Add(this.lb_Cajon);
            this.Controls.Add(this.lb_atr);
            this.Controls.Add(this.bt_Cajon);
            this.Controls.Add(this.dgvCajon);
            this.Controls.Add(this.dgvPrimario);
            this.Name = "Form4";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrimario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajonHash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHash)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPrimario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Dato;
        private System.Windows.Forms.DataGridViewTextBoxColumn Direccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desborda;
        private System.Windows.Forms.DataGridView dgvCajon;
        private System.Windows.Forms.Button bt_Cajon;
        private System.Windows.Forms.DataGridViewTextBoxColumn DirReg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desbordaa;
        private System.Windows.Forms.Label lb_atr;
        private System.Windows.Forms.Label lb_Cajon;
        private System.Windows.Forms.Label lb_Indice;
        private System.Windows.Forms.Label lb_numIndice;
        private System.Windows.Forms.DataGridView dgvCajonHash;
        private System.Windows.Forms.DataGridView dgvHash;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn dir;
        private System.Windows.Forms.DataGridViewTextBoxColumn desb;
    }
}