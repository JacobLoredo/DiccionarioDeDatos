using System.Collections.Generic;
using System.Windows.Forms;

namespace Diccionario_de_datos
{
    public partial class FormBusqueda : Form
    {
        List<Entidad> Entidads = new List<Entidad>();
        List<Atributo> atribu = new List<Atributo>();
        bool atributoOentidad;
        DataGridView DATOS = new DataGridView();
        public FormBusqueda(List<Entidad> lsentidads)
        {
            atributoOentidad = false;
            Entidads = lsentidads;
            InitializeComponent();
            comboBox1.Visible = false;
        }
        public FormBusqueda(List<Atributo> atributossss, DataGridView dataGridViewDatos)
        {
            atributoOentidad = true;
            atribu = atributossss;

            InitializeComponent();
            comboBox1.Visible = true;
            DATOS = dataGridViewDatos;
            foreach (Atributo atr in atribu)
            {
                comboBox1.Items.Add(atr.tipoIndice + " - " + atr.nomAtributo);
            }
        }
        public void BuscaEntidades()
        {
            dataGridView1.Rows.Clear();
            if (textBox1.Text != "")
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
                       
                        for (int k= 0; k < Entidads[i].lsAtributo.Count; k++)
                        {
                            dataGridView1.Rows.Add(
                                Entidads[i].lsAtributo[k].nomAtributo,
                                Entidads[i].lsAtributo[k].dirAtributo, 
                                Entidads[i].lsAtributo[k].tipoDato,
                                Entidads[i].lsAtributo[k].longDato, 
                                Entidads[i].lsAtributo[k].tipoIndice, 
                                Entidads[i].lsAtributo[k].dirIndice, 
                                Entidads[i].lsAtributo[k].dirSigAtributo);
                        }
                        
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
        public void BuscaDato() {
            bool encotr = false;
            string encostrados = "";
            for (int i = 0; i < DATOS.Columns.Count - 2; i++)
            {
                string aux = comboBox1.Text.Substring(4, atribu[i].nomAtributo.Length);
                string indice = comboBox1.Text.Substring(0,2);
                if (DATOS.Columns[i + 1].Name == "Nom" + aux)
                {
                    for (int j = 0; j < DATOS.Rows.Count - 1; j++)
                    {
                        if (indice!="1 ")
                        {
                            if (DATOS.Rows[j].Cells[i + 1].Value.ToString().Replace(" ", "") == textBox1.Text.ToString())
                            {
                                encotr = true;
                                //MessageBox.Show(DATOS.Rows[j].Cells[i + 1].Value.ToString());
                                imprimeInfoDato(DATOS.Rows[j]);
                                break;
                            }
                        }
                        else
                        {
                            if (DATOS.Rows[j].Cells[i + 1].Value.ToString().Replace(" ", "") == textBox1.Text.ToString())
                            {
                                
                                encotr = true;
                               encostrados += imprimeInfoDato(DATOS.Rows[j])+"\n";
                            }
                        }
                    }

                }
            }
            if (!encotr)
            {
                MessageBox.Show("El dato no se encuentra");
            }

        }
        public string imprimeInfoDato(DataGridViewRow row ) {

            System.Text.StringBuilder stringBuilder= new System.Text.StringBuilder();
            for (int i = 0; i < row.Cells.Count-2; i++)
            {
                stringBuilder.Append(row.Cells[i + 1].Value.ToString().Replace(" ","")+"---");
            }
            MessageBox.Show(stringBuilder.ToString());
            return stringBuilder.ToString();
        }


        private void btn_aceptar_Click(object sender, System.EventArgs e)
        {
            if (!atributoOentidad)
            {
            BuscaEntidades();

            }
            else
            {
                BuscaDato();
            }
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {

        }
    }
}
