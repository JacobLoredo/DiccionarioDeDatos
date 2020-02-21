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
    public partial class Form4 : Form
    {
        Atributo atr;
        string nomEntidad;
        List<Atributo> lsAtr;

        public Form4(string entidad, Atributo atri)
        {
            atr = atri;
            nomEntidad = entidad;
            InitializeComponent();
        }

        public Form4(string entidad, List<Atributo> atributos, Atributo atri)
        {
            atr = atri;
            lsAtr = atributos;
            nomEntidad = entidad;
            InitializeComponent();
        }

        /*Constructor del Form 4*/
        private void Form4_Load(object sender, EventArgs e)
        {
            if (atr.tipoIndice == 3)//Secundario
                bt_Cajon.Visible = true;
            if (atr.tipoIndice == 6)//Hash dinamico
            {
                dgvPrimario.Visible = false;
                dgvHash.Visible = true;
                lb_Indice.Visible = true;
                lb_numIndice.Visible = true;
                bt_Cajon.Visible = true;
            }
            lb_atr.Text = atr.nomAtributo;
            creaListas();
        }

        /*Método que segun el tipo de dato crea listas para guardar informacion del indice*/
        private void creaListas()
        {
            int registros = 0;
            List<int> lisInt;
            List<string> lisStr;
            List<long> listDir = new List<long>();
            if (atr.tipoIndice == 6)//Hash dinamico
            {
                lisStr = new List<string>();
                registros = 1040 / 8;
                leeidxHash();
            }
            else
            {
                switch (atr.tipoDato)
                {
                    case 'E':
                        lisInt = new List<int>();
                        registros = 86;
                        leeArchivoidx(lisInt, listDir, registros);
                        break;
                    case 'C':
                        lisStr = new List<string>();
                        registros = 1040 / (atr.longDato + 8);
                        leeArchivoidx(lisStr, listDir, registros);
                        break;
                }
            }
        }

        /*Método que lee los valores enteros y los escribe en un dgv*/
        private void leeArchivoidx(List<int> lsInt, List<long> lsDir, long reg)
        {
            FileStream abrir;
            abrir = File.Open(nomEntidad + ".idx", FileMode.Open, FileAccess.Read);
            abrir.Seek(atr.dirIndice, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(abrir);

            for (int i = 0; i < reg; i++)
            {
                int n = dgvPrimario.Rows.Add();//Añade un renglon
                lsInt.Add(reader.ReadInt32());
                dgvPrimario.Rows[n].Cells[0].Value = lsInt[i];
                lsDir.Add(reader.ReadInt64());
                dgvPrimario.Rows[n].Cells[1].Value = lsDir[i];
            }
            dgvPrimario.Rows[dgvPrimario.Rows.Count - 2].Cells[2].Value = reader.ReadInt64();
            reader.Close();
            abrir.Close();
        }

        /*Método que lee los valores de tipo string y los escribe en un dgv*/
        private void leeArchivoidx(List<string> lsStr, List<long> lsDir, long reg)
        {
            FileStream abrir;
            abrir = File.Open(nomEntidad + ".idx", FileMode.Open, FileAccess.Read);
            abrir.Seek(atr.dirIndice, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(abrir);

            if (atr.tipoIndice == 6)
            {
                lb_numIndice.Text = Convert.ToString(reader.ReadInt32());
            }

            for (int i = 0; i < reg; i++)
            {
                int n = dgvPrimario.Rows.Add();
                lsStr.Add(reader.ReadString());
                dgvPrimario.Rows[n].Cells[0].Value = lsStr[i];
                lsDir.Add(reader.ReadInt64());
                dgvPrimario.Rows[n].Cells[1].Value = lsDir[i];
            }
            dgvPrimario.Rows[dgvPrimario.Rows.Count - 2].Cells[2].Value = reader.ReadInt64();
            reader.Close();
            abrir.Close();
        }

        /*Evento para mostrar el contenido de un cajon en un dgv*/
        private void bt_Cajon_Click(object sender, EventArgs e)
        {

            switch (atr.tipoIndice)
            {
                case 3://Indice Secundario
                    if (!dgvCajon.Visible)
                        dgvCajon.Visible = true;
                    else
                    {
                        //dgvCajon.Columns.Clear();
                        dgvCajon.Rows.Clear();
                        lb_Cajon.Text = "";
                    }
                    if (atr.tipoDato == 'E')
                        lb_Cajon.Text = Convert.ToString(dgvPrimario.CurrentRow.Cells[0].Value);
                    else
                        lb_Cajon.Text = (string)dgvPrimario.CurrentRow.Cells[0].Value;
                    leeidxCajon();
                    break;

                case 6://Indice Hash Dinamico
                    if (!dgvCajonHash.Visible)
                        dgvCajonHash.Visible = true;
                    else
                    {
                        dgvCajonHash.Columns.Clear();
                        dgvCajonHash.Rows.Clear();
                        lb_Cajon.Text = "";
                    }
                    //lb_Cajon.Text = Convert.ToString(dgvPrimario.CurrentRow.Cells[0].Value);
                    if (dgvCajonHash.Rows.Count < 2)
                    {
                        foreach (Atributo atr in lsAtr)//Agrega nuevas columnas segun los atributos existentes.
                        {
                            dgvCajonHash.Columns.Add("Nom" + atr.nomAtributo, atr.nomAtributo);
                        }
                        dgvCajonHash.Columns.Add("desborda", "Desbordamiento");
                    }
                    leeidxCajonesHash();
                    break;
            }
        }

        /*Método que lee el contenido de un cajon del indice secundario*///recuerdaquehacer
        private void leeidxCajon()
        {
            FileStream abrir;
            abrir = File.Open(nomEntidad + ".idx", FileMode.Open, FileAccess.Read);
            long direccionCajon = (long)dgvPrimario.CurrentRow.Cells[1].Value;
            abrir.Seek(direccionCajon, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(abrir);
            long registros = 1040 / (atr.longDato + 8);

            for (int i = 0; i < registros; i++)
            {
                int n = dgvCajon.Rows.Add();
                dgvCajon.Rows[n].Cells[0].Value = reader.ReadInt64();
            }
            dgvCajon.Rows[dgvCajon.Rows.Count - 2].Cells[1].Value = reader.ReadInt64();
            reader.Close();
            abrir.Close();
        }

        /*Método que lee la caja principal del indice Hash dinámico y lo muestra en eun DGV*/
        private void leeidxHash()
        {
            FileStream abrir;
            abrir = File.Open(nomEntidad + ".idx", FileMode.Open, FileAccess.Read);
            long direccionCajon = atr.dirIndice;
            abrir.Seek(direccionCajon, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(abrir);
            int registros = 1032 / 8;
            int indiceHash = reader.ReadInt32();

            double nBits = Math.Pow(2, indiceHash);
            List<string> lsBin = new List<string>();

            for (int i = 0; i < nBits; i++)
            {
                lsBin.Add(numerosBinarios(i, indiceHash));
            }

            lb_numIndice.Text = Convert.ToString(indiceHash);

            for (int i = 0; i < registros; i++)
            {
                int n = dgvHash.Rows.Add();
                if(i < lsBin.Count)
                    dgvHash.Rows[n].Cells[0].Value = lsBin[i];
                dgvHash.Rows[n].Cells[1].Value = reader.ReadInt64();
            }
            long sigTabla = reader.ReadInt64();
            dgvHash.Rows[dgvHash.Rows.Count - 2].Cells[2].Value = sigTabla;


            if(sigTabla != -1)
            {
                reader.BaseStream.Position = sigTabla + 4;
                for (int i = 0; i < registros; i++)
                {
                    int n = dgvHash.Rows.Add();
                    if (i + 129 < lsBin.Count)
                        dgvHash.Rows[n].Cells[0].Value = lsBin[i + 129];
                    dgvHash.Rows[n].Cells[1].Value = reader.ReadInt64();
                }
                sigTabla = reader.ReadInt64();
                dgvHash.Rows[dgvHash.Rows.Count - 2].Cells[2].Value = sigTabla;
            }
            reader.Close();
            abrir.Close();
        }

        /*Métodp que lee un cajon de tipo Hash Dinámico y lo muestra en un DGV*/
        private void leeidxCajonesHash()
        {
            FileStream abrir;
            abrir = File.Open(nomEntidad + ".idx", FileMode.Open, FileAccess.Read);
            long direccionCajon = (long)dgvHash.CurrentRow.Cells[1].Value;
            abrir.Seek(direccionCajon, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(abrir);
            int longAtr = regresaLongituddeAtributos();
            long registros = 1036 / longAtr;

            lb_Cajon.Text = Convert.ToString(reader.ReadInt32());//Lee el indice del cajon
            for (int i = 0; i < registros; i++)
            {
                int celda = 0;
                int n = dgvCajonHash.Rows.Add();
                foreach(Atributo atr in lsAtr)
                {
                    switch(atr.tipoDato)
                    {
                        case 'E':
                            int valor = reader.ReadInt32();
                            dgvCajonHash.Rows[n].Cells[celda].Value = valor;
                            break;

                        case 'C':
                            string valorStr = reader.ReadString();
                            dgvCajonHash.Rows[n].Cells[celda].Value = valorStr;
                            break;
                    }
                    celda++;
                }
            }
            dgvCajonHash.Rows[dgvCajonHash.Rows.Count - 2].Cells[dgvCajonHash.ColumnCount - 1].Value = reader.ReadInt64();
            reader.Close();
            abrir.Close();
        }

        /*Método que regresa un valor con la longitud de los atributos*/
        private int regresaLongituddeAtributos()
        {
            int longAtributos = 0;
            foreach (Atributo atr in lsAtr)
            {
                longAtributos += atr.longDato;
            }
            return longAtributos;
        }

        /*Método que regresa una cadena con el valor binario de un numero*/
        private string numerosBinarios(int valor, int indice)
        {
            string binario = "";
            while (valor > 0)
            {
                if (valor % 2 == 0)
                {
                    binario = "0" + binario;
                }
                else
                {
                    binario = "1" + binario;
                }
                valor = (int)(valor / 2);
            }
            if (binario.Length < indice)//Si el numero en binario es inferior a 8 bits, se llena de 0´s en la parte izquierda de la cadena
            {
                string ceros = "";
                int espacios = indice - binario.Length;
                for (int i = 0; i < espacios; i++)
                    ceros = ceros + '0';
                binario = ceros + binario;
            }
            return binario;
        }

    }
}
