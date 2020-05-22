using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diccionario_de_datos
{
    public partial class FormaHash : Form
    {
        List<CajonHash> cajones = new List<CajonHash>();
        List<long> principal = new List<long>();
        string namef;
        FileStream fileidx;
        public FormaHash(List<long> prin, List<CajonHash> sec, string namefile)
        {
            InitializeComponent();
            principal = prin;
            cajones = sec;
            namef = namefile;
            escribeIDX();
            inicializaprin();
            inicializacajones();
        }
        private void escribeIDX()
        {
            fileidx = File.Open(namef, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter bridx = new BinaryWriter(fileidx);
            for (int i = 0; i < 7; i++)
            {
                bridx.Write(principal[i]);
            }
            for (int i = 0; i < cajones.Count; i++)
            {
                for (int x = 0; x < cajones[i].Cajon.Count; x++)
                {
                    bridx.Write(cajones[i].Cajon[x].valint);
                    bridx.Write(cajones[i].Cajon[x].dir);
                }
                for (int x = 0; x < (86 - cajones[i].Cajon.Count); x++)
                {
                    bridx.Write((int)-1);
                    bridx.Write((long)-1);
                }

                bridx.Write((long)-1);
            }
            fileidx.Close();
        }

        private void inicializaprin()
        {
            for (int i = 0; i < 7; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = principal[i];

            }
        }
        private void inicializacajones()
        {
            for (int i = 0; i < cajones[0].Cajon.Count; i++)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = cajones[0].Cajon[i].valint;
                dataGridView2.Rows[i].Cells[1].Value = cajones[0].Cajon[i].dir;
            }
            for (int i = 0; i < cajones[1].Cajon.Count; i++)
            {
                dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = cajones[1].Cajon[i].valint;
                dataGridView3.Rows[i].Cells[1].Value = cajones[1].Cajon[i].dir;
            }
            for (int i = 0; i < cajones[2].Cajon.Count; i++)
            {
                dataGridView4.Rows.Add();
                dataGridView4.Rows[i].Cells[0].Value = cajones[2].Cajon[i].valint;
                dataGridView4.Rows[i].Cells[1].Value = cajones[2].Cajon[i].dir;
            }
            for (int i = 0; i < cajones[3].Cajon.Count; i++)
            {
                dataGridView5.Rows.Add();
                dataGridView5.Rows[i].Cells[0].Value = cajones[3].Cajon[i].valint;
                dataGridView5.Rows[i].Cells[1].Value = cajones[3].Cajon[i].dir;
            }
            for (int i = 0; i < cajones[4].Cajon.Count; i++)
            {
                dataGridView6.Rows.Add();
                dataGridView6.Rows[i].Cells[0].Value = cajones[4].Cajon[i].valint;
                dataGridView6.Rows[i].Cells[1].Value = cajones[4].Cajon[i].dir;
            }
            for (int i = 0; i < cajones[5].Cajon.Count; i++)
            {
                dataGridView7.Rows.Add();
                dataGridView7.Rows[i].Cells[0].Value = cajones[5].Cajon[i].valint;
                dataGridView7.Rows[i].Cells[1].Value = cajones[5].Cajon[i].dir;
            }
            for (int i = 0; i < cajones[6].Cajon.Count; i++)
            {
                dataGridView8.Rows.Add();
                dataGridView8.Rows[i].Cells[0].Value = cajones[6].Cajon[i].valint;
                dataGridView8.Rows[i].Cells[1].Value = cajones[6].Cajon[i].dir;
            }
        }

        private void FormaHash_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView5.Rows.Clear();
            dataGridView6.Rows.Clear();
            dataGridView7.Rows.Clear();
            dataGridView8.Rows.Clear();
          

        }
    }
}
