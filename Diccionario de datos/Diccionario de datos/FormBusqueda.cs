using System.Collections.Generic;
using System.Windows.Forms;

namespace Diccionario_de_datos
{
    public partial class FormBusqueda : Form
    {
        List<Entidad> Entidads;
        Entidad entidadAc;
        Entidad entidadAc1;
        Entidad entidadAc2;
        Entidad entidadAc3;
        Entidad entidadAc6;
        public FormBusqueda(List<Entidad> lsentidads)
        {
            Entidads = lsentidads;
            InitializeComponent();
        }
        public FormBusqueda(Entidad entidad1, Entidad entidad2, Entidad entidad3, Entidad entidad6)
        {
            entidadAc1 = entidad1;
            entidadAc2 = entidad2;
            entidadAc3 = entidad3;
            entidadAc6 = entidad6;
            InitializeComponent();
        }
        public void BuscaEntidades()
        {

        }

        private void btn_aceptar_Click(object sender, System.EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (textBox1.Text!="")
            {
                for (int i = 0; i < Entidads.Count; i++)
                {

                    if (Entidads[i].nombre == textBox1.Text.PadRight(29))
                    {
                        dataGridView1.Rows[0].Cells[0].Value = Entidads[i].nombre;
                        dataGridView1.Rows[0].Cells[1].Value = Entidads[i].dirEnt;
                        dataGridView1.Rows[0].Cells[2].Value = Entidads[i].dirAtr;
                        dataGridView1.Rows[0].Cells[3].Value = Entidads[i].dirDatos;
                        dataGridView1.Rows[0].Cells[4].Value = Entidads[i].dirSigEnt;
                        dataGridView1.Rows[0].Cells[5].Value = -1;
                        dataGridView1.Rows[0].Cells[6].Value = -1;
                        break;
                    }
                    else
                    {
                        MessageBox.Show("La entidad no existe");
                        dataGridView1.Rows.Clear();
                    }
                }
               
            }
            else
            {
                MessageBox.Show("Ingresa un nombre valido");
            }
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {

        }
    }
}
