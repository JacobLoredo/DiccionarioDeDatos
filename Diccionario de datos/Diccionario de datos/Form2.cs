using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Diccionario_de_datos
{
    public partial class Form2 : Form
    {
        Entidad entidadSel;
        string nomArchivo;
        long tamArchivo;
        long cabeceraAtr;
        List<Entidad> Aux = new List<Entidad>();
        /*Constructor del Form2*/
        public Form2(Entidad agrega, string archivo, long tam,List<Entidad> ls)
        {
            InitializeComponent();
            entidadSel = agrega;
            nomArchivo = archivo;
            tamArchivo = tam;
            Aux = ls;
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            lb_Entidad.Text = entidadSel.nombre;
            leeAtributos();
            
        }

        /*Método que se encarga de leer los atributos que esten guardados en el archivo*/
        private void leeAtributos()
        {
            if(entidadSel.DireccionAtributo != -1)
            {
                FileStream abrirArchivo;
                abrirArchivo = File.Open(nomArchivo+".bin", FileMode.Open, FileAccess.Read);
                abrirArchivo.Seek(entidadSel.DireccionAtributo, SeekOrigin.Begin);

                cabeceraAtr = entidadSel.DireccionAtributo;
                tb_DirAtr.Text = Convert.ToString(cabeceraAtr);

                long aux = cabeceraAtr;
                Atributo lee;
                string nom = "";
                char tipoI;
                int longT = 0;
                long dirA = 0;
                int tInd = 0;
                long dirInd = 0;
                long dirSigAtr = 0;
                while(aux != -1)
                {
                    abrirArchivo.Position = aux;
                    abrirArchivo.Seek(abrirArchivo.Position, SeekOrigin.Begin);

                    BinaryReader reader = new BinaryReader(abrirArchivo);

                    nom = reader.ReadString();
                    tipoI = reader.ReadChar();
                    longT = reader.ReadInt32();
                    dirA = reader.ReadInt64();
                    tInd = reader.ReadInt32();
                    dirInd = reader.ReadInt64();
                    dirSigAtr = reader.ReadInt64();

                    lee = new Atributo(nom, tipoI, longT, dirA, tInd, dirInd, dirSigAtr);
                    entidadSel.lsAtributo.Add(lee);
                    aux = dirSigAtr;
                }
                abrirArchivo.Close();
                actualizaDGVAtr();
                bt_InsertaRegistro.Visible = true;
                //dgvRegistros.Visible = true;
                //actualizaDGVRegistros();
            }
        }
        private bool checaAtributoRepetido() {
            bool repetido = false;

            foreach (Atributo item in entidadSel.lsAtributo)
            {
                if (tb_Atributo.Text.PadRight(29) ==item.nomAtributo.ToString())
                {
                    repetido = true;
                    break;
                }
            }
            
                
            return repetido;
        
        }

        /*Evento que agrega la informacion a un atributio*/
        private void bt_AgregarAtr_Click(object sender, EventArgs e)
        {
            if (!checaAtributoRepetido())
            {

                Atributo atr = new Atributo(tb_Atributo.Text.PadRight(29), Convert.ToChar(cb_TipoDato.Text), int.Parse(tb_LongitudDato.Text), tamArchivo, int.Parse(cb_TipoIndice.Text), -1, -1);
                entidadSel.agregaAtributo(atr);

                int n = dgvAtributos.Rows.Add();
                dgvAtributos.Rows[n].Cells[0].Value = atr.nomAtributo;
                dgvAtributos.Rows[n].Cells[1].Value = atr.tipoDato;
                dgvAtributos.Rows[n].Cells[2].Value = atr.longDato;
                dgvAtributos.Rows[n].Cells[3].Value = atr.dirAtributo;
                dgvAtributos.Rows[n].Cells[4].Value = atr.tipoIndice;
                dgvAtributos.Rows[n].Cells[5].Value = atr.dirIndice;
                dgvAtributos.Rows[n].Cells[6].Value = atr.dirSigAtributo;
                if (n >= 1)//Asigna las direcciones siguiente a la lista
                {
                    dgvAtributos.Rows[n - 1].Cells[6].Value = atr.dirAtributo;
                }
                escribeAtributo();

                actualizaDGVAtr();

                tb_Atributo.Text = "";
                tb_LongitudDato.Text = "";
                cb_TipoDato.Text = "";
                cb_TipoIndice.Text = "";
                // bt_InsertaRegistro.Visible = false;
            }
            else {
                MessageBox.Show("EL ATRIBUTO YA EXISTE ");
            }
        }

        /*Método que escribe el atributo en el archivo*/
        private void escribeAtributo()
        {
            BinaryWriter bw;
            FileStream guardar = new FileStream(nomArchivo + ".bin", FileMode.OpenOrCreate, FileAccess.Write);

            bw = new BinaryWriter(guardar);

            if (entidadSel.DireccionAtributo == -1)
            {
                cabeceraAtr = tamArchivo;
                tb_DirAtr.Text = Convert.ToString(cabeceraAtr);
                entidadSel.dirAtr = cabeceraAtr;
            }

            long aux;
            int i = 0;
            foreach(Atributo atr in entidadSel.lsAtributo)
            {
                aux = atr.dirAtributo;
                bw.Seek((int)aux, SeekOrigin.Begin);
                bw.Write(atr.nomAtributo);
                bw.Write(atr.tipoDato);
                bw.Write(atr.longDato);
                bw.Write(atr.dirAtributo);
                bw.Write(atr.tipoIndice);
                bw.Write(atr.dirIndice);
                atr.dirSigAtributo = (long)dgvAtributos.Rows[i].Cells[6].Value;
                bw.Write(atr.dirSigAtributo);
                i++;
            }
            tamArchivo = guardar.Length;
            guardar.Close();
        }

        /*Método que actuliza el DGV en cada cambio que hay*/
        private void actualizaDGVAtr()
        {
            dgvAtributos.Rows.Clear();

            foreach(Atributo atr in entidadSel.lsAtributo)
            {
                int n = dgvAtributos.Rows.Add();

                dgvAtributos.Rows[n].Cells[0].Value = atr.nomAtributo;
                dgvAtributos.Rows[n].Cells[1].Value = atr.tipoDato;
                dgvAtributos.Rows[n].Cells[2].Value = atr.longDato;
                dgvAtributos.Rows[n].Cells[3].Value = atr.dirAtributo;
                dgvAtributos.Rows[n].Cells[4].Value = atr.tipoIndice;
                dgvAtributos.Rows[n].Cells[5].Value = atr.dirIndice;
                dgvAtributos.Rows[n].Cells[6].Value = atr.dirSigAtributo;
            }
        }

        /*Evento de apretar el boton de modificar atributo*/
        private void bt_ModificarAtr_Click(object sender, EventArgs e)
        {
            string compara = (string)dgvAtributos.CurrentRow.Cells[0].Value;
            if (!checaAtributoRepetido())
            {

            foreach(Atributo atr in entidadSel.lsAtributo)
            {
                if(String.Compare(compara.PadRight(0), atr.nomAtributo) == 0)
                {
                    if(tb_Atributo.Text.Length != 0)
                        atr.nomAtributo = tb_Atributo.Text.PadRight(29);
                    if(cb_TipoDato.Text.Length != 0)
                        atr.tipoDato = Convert.ToChar(cb_TipoDato.Text);
                    if (tb_LongitudDato.Text.Length != 0)
                        atr.longDato = int.Parse(tb_LongitudDato.Text);
                    if (cb_TipoIndice.Text.Length != 0)
                        atr.tipoIndice = int.Parse(cb_TipoIndice.Text);
                    break;
                }
            }
            escribeAtributo();
            actualizaDGVAtr();
            tb_Atributo.Text = "";
            cb_TipoDato.Text = "";
            tb_LongitudDato.Text = "";
            cb_TipoIndice.Text = "";
            }
            else
            {
                MessageBox.Show("No se puede cambiar, ya existe");
            }
        }

        /*Evento de eliminar un atributo*/
        private void bt_EliminarAtr_Click(object sender, EventArgs e)
        {
            string elimina = (string)dgvAtributos.CurrentRow.Cells[0].Value;
            foreach (Atributo atr in entidadSel.lsAtributo)
            {
                if (String.Compare(elimina.PadRight(29), atr.nomAtributo) == 0)
                {
                    if (entidadSel.dirAtr != atr.dirAtributo)//Cuando no es el primer atributo en la lista
                    {
                        int pos = entidadSel.lsAtributo.IndexOf(atr);
                        entidadSel.lsAtributo[pos - 1].dirSigAtributo = atr.dirSigAtributo;
                        entidadSel.lsAtributo.Remove(atr);
                    }
                    else//Cuando es el primer atributo en la lista
                    {
                        entidadSel.dirAtr = atr.dirSigAtributo;
                        cabeceraAtr = atr.dirSigAtributo;
                        tb_DirAtr.Text = Convert.ToString(cabeceraAtr);
                        entidadSel.lsAtributo.Remove(atr);
                    }
                    dgvAtributos.Rows.Remove(dgvAtributos.CurrentRow);
                    break;
                }
            }
            actualizaDGVAtr();
        }

        /*Evento de insertar un nuevo registro de información*/
        private void bt_InsertaRegistro_Click(object sender, EventArgs e)
        {
            int t = tamañoDeRegistro();
            Form3 f3 = new Form3(entidadSel.nombre, entidadSel.lsAtributo, t, entidadSel.dirDatos, entidadSel);
            f3.ShowDialog();
            entidadSel.dirDatos = f3.regresaCabecera();
            actualizaDGVAtr();
            escribeAtributo();
        }

        /*Evento de cerrar la ventana*/
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(true)
            {
                entidadSel.lsAtributo.Clear();
            }
        }

        /*Método que regresa el valor de un registro con todos sus atributos*/
        private int tamañoDeRegistro()
        {
            int tamRegistro = 0;
            foreach(Atributo atr in entidadSel.lsAtributo)
            {
                tamRegistro += atr.longDato;
            }
            return tamRegistro;
        }

        /*Método que muestra los indices de un atributo*/
        private void bt_VerIndices_Click(object sender, EventArgs e)
        {
            string compara = (string)dgvAtributos.CurrentRow.Cells[0].Value;
            foreach (Atributo atr in entidadSel.lsAtributo)
            {
                if (String.Compare(compara.PadRight(29), atr.nomAtributo) == 0 && (atr.tipoIndice == 2 || atr.tipoIndice == 3))
                //Si el atributo elegido tiene indice primario o secundario, abre un nuevo form con la info de los indices.
                {
                    Form4 f4 = new Form4(entidadSel.nombre, atr);
                    f4.ShowDialog();
                }
                if(String.Compare(compara.PadRight(29), atr.nomAtributo) == 0 && atr.tipoIndice == 6)//Hash Dinámico
                {
                    Form4 f4 = new Form4(entidadSel.nombre, entidadSel.lsAtributo, atr);
                    f4.ShowDialog();
                }
            }
        }

        private void Cb_TipoDato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToChar(cb_TipoDato.Text) == 'E')
            {
                tb_LongitudDato.Text = "4";
                tb_LongitudDato.Enabled = false;
            }
            else if (Convert.ToChar(cb_TipoDato.Text) == 'C')
            {
                tb_LongitudDato.Text = "";
                tb_LongitudDato.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bt_VerIndices_Click(sender,e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bt_VerIndices_Click(sender, e);
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void cb_TipoIndice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_TipoIndice.SelectedIndex == 3)
            {
                comboBox1.Enabled = true;
                for (int i = 0; i < Aux.Count; i++)
                {
                    comboBox1.Items.Add(Aux[i].nombre.ToString());
                }
                

            }
            else
            {
                comboBox1.Enabled = false;
            }
        }
    }
}
