namespace Diccionario_de_datos
{
    partial class FormBusqueda
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Buscarlb = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DirEntDirAtri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DirAtriTipoDato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DDLD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DSETI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DSA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(170, 41);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Buscarlb
            // 
            this.Buscarlb.AutoSize = true;
            this.Buscarlb.Location = new System.Drawing.Point(188, 25);
            this.Buscarlb.Name = "Buscarlb";
            this.Buscarlb.Size = new System.Drawing.Size(40, 13);
            this.Buscarlb.TabIndex = 1;
            this.Buscarlb.Text = "Buscar";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.DirEntDirAtri,
            this.DirAtriTipoDato,
            this.DDLD,
            this.DSETI,
            this.DI,
            this.DSA});
            this.dataGridView1.Location = new System.Drawing.Point(64, 67);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(747, 197);
            this.dataGridView1.TabIndex = 2;
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            // 
            // DirEntDirAtri
            // 
            this.DirEntDirAtri.HeaderText = "DirEnt/DirAtri";
            this.DirEntDirAtri.Name = "DirEntDirAtri";
            // 
            // DirAtriTipoDato
            // 
            this.DirAtriTipoDato.HeaderText = "DirAtri/TipoDato";
            this.DirAtriTipoDato.Name = "DirAtriTipoDato";
            // 
            // DDLD
            // 
            this.DDLD.HeaderText = "DD/LD";
            this.DDLD.Name = "DDLD";
            // 
            // DSETI
            // 
            this.DSETI.HeaderText = "DSE/TI";
            this.DSETI.Name = "DSETI";
            // 
            // DI
            // 
            this.DI.HeaderText = "DI";
            this.DI.Name = "DI";
            // 
            // DSA
            // 
            this.DSA.HeaderText = "DSA";
            this.DSA.Name = "DSA";
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Location = new System.Drawing.Point(286, 38);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(75, 23);
            this.btn_aceptar.TabIndex = 3;
            this.btn_aceptar.Text = "Aceptar";
            this.btn_aceptar.UseVisualStyleBackColor = true;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(64, 41);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(83, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Buscar por indice:";
            // 
            // FormBusqueda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 287);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Buscarlb);
            this.Controls.Add(this.textBox1);
            this.Name = "FormBusqueda";
            this.Text = "FormBusqueda";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Buscarlb;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_aceptar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn DirEntDirAtri;
        private System.Windows.Forms.DataGridViewTextBoxColumn DirAtriTipoDato;
        private System.Windows.Forms.DataGridViewTextBoxColumn DDLD;
        private System.Windows.Forms.DataGridViewTextBoxColumn DSETI;
        private System.Windows.Forms.DataGridViewTextBoxColumn DI;
        private System.Windows.Forms.DataGridViewTextBoxColumn DSA;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}