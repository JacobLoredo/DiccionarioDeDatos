using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Diccionario_de_datos
{
    public partial class Form1 : Form
    {
        List<Entidad> lsEntidad = new List<Entidad>();
        public List<Entidad> lsEntidadAux = new List<Entidad>();

        FileStream archivo;
        public long cabecera;
        const int TAMENTIDAD = 62;
        public long tamArchivo = 0;

        string nombreArchivo;
        string entSeleccionada = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Método que cierra la ventana al hacer clic en la opcion cerrar
        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            escribeArchivo(nombreArchivo);
            lsEntidad.Clear();
            dgvEntidades.Rows.Clear();
            cabecera = -1;
            tamArchivo = 0;
            nombreArchivo = "";
            tb_Cabecera.Text = "";
        }

        //Evento que inicia al hacer clic en abrir
        private void abrirArchivoToolStripMenuItem_Click(object sender, EventArgs e)

        {
            bt_CrearEntidad.Visible = false;
            lb_entidad.Visible = true;
            lb_cabecera.Visible = true;
            lb_NombreEnt.Visible = true;

            tb_Cabecera.Visible = true;
            bt_AgregarEnt.Visible = false;

            dgvEntidades.Visible = true;

            AbrirArchivo();
        }

        //Método que abre un archivo existente.
        public void AbrirArchivo()
        {
            FileStream abrirArch;
            OpenFileDialog abre = new OpenFileDialog();
            if (abre.ShowDialog() == DialogResult.OK)
            {
                nombreArchivo = abre.FileName;

                abrirArch = File.Open(nombreArchivo, FileMode.Open, FileAccess.Read);

                abrirArch.Seek(0, SeekOrigin.Begin);

                BinaryReader br = new BinaryReader(abrirArch);

                cabecera = br.ReadInt64();
                tb_Cabecera.Text = Convert.ToString(cabecera);

                lb_Actual.Text = nombreArchivo;
                tamArchivo = abrirArch.Length;

                leeArchivo(abrirArch);

                abrirArch.Close();

                nombreArchivo = nombreArchivo.Remove(nombreArchivo.Length - 4, 4);//remueve el .bin que vuelve a agregarse al abrirlo

                tb_Entidad.Visible = true;
                bt_AgregarEnt.Visible = true;
                bt_EliminarEnt.Visible = true;
                bt_ModificarEnt.Visible = true;
                bt_AgregaAtr.Visible = true;
            }
            else
                MessageBox.Show("No se ha elegido ningun archivo");
        }

        //Método que lee la informacion del archivo que se va a abrir
        public void leeArchivo(FileStream file)
        {
            file.Position = cabecera;
            long aux = cabecera;
            Entidad agrega;

            string nombre;
            long dirE = 0;
            long dirA = 0;
            long dirD = 0;
            long dirSE = 0;


            while (aux != -1)
            {
                file.Position = aux;
                file.Seek(file.Position, SeekOrigin.Begin);
                BinaryReader br = new BinaryReader(file);

                nombre = br.ReadString();
                dirE = br.ReadInt64();
                dirA = br.ReadInt64();
                dirD = br.ReadInt64();
                dirSE = br.ReadInt64();

                agrega = new Entidad(nombre, dirE, dirA, dirD, dirSE);
                lsEntidad.Add(agrega);
                aux = dirSE;



            }

            foreach (Entidad ent in lsEntidad)
            {
                int n = dgvEntidades.Rows.Add();

                dgvEntidades.Rows[n].Cells[0].Value = ent.nombre;
                dgvEntidades.Rows[n].Cells[1].Value = ent.dirEnt;
                dgvEntidades.Rows[n].Cells[2].Value = ent.dirAtr;
                dgvEntidades.Rows[n].Cells[3].Value = ent.dirDatos;
                dgvEntidades.Rows[n].Cells[4].Value = ent.dirSigEnt;
            }
        }

        //Evento que inicia al hacer clic en crear
        private void crearArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bt_CrearEntidad.Visible = false;

            SaveFileDialog save = new SaveFileDialog();
            if (save.ShowDialog() == DialogResult.OK)
            {
                nombreArchivo = save.FileName;
                //DirectoryInfo di = Directory.CreateDirectory(nombreArchivo);
                archivo = new FileStream(nombreArchivo + ".bin", FileMode.Create);
                lb_Actual.Text = archivo.Name;
                cabecera = -1;
                tb_Cabecera.Text = Convert.ToString(cabecera);
                archivo.Close();
            }
            bt_CrearEntidad.Visible = true;
        }

        //Método que guarda el archivo por entidad
        private void escribeArchivo(string nomArchivo)
        {
            BinaryWriter bw;
            //archivo = new FileStream(nomArchivo, FileMode.Create, FileAccess.Write);
            FileStream guardar = new FileStream(nomArchivo + ".bin", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            // lsEntidadAux.Clear();
            bw = new BinaryWriter(guardar);

            if (tamArchivo == 0)
            {
                cabecera = 8;
                tb_Cabecera.Text = "8";
                bw.Seek(0, SeekOrigin.Begin);
                bw.Write(cabecera);
                tamArchivo = guardar.Length;
            }
            bw.Seek(0, SeekOrigin.Begin);
            bw.Write(cabecera);

            long aux;

            foreach (Entidad ent in lsEntidad)//Guarda la información por cada entidad
            {
                aux = ent.dirEnt;
                bw.Seek((int)aux, SeekOrigin.Begin);
                bw.Write(ent.nombre);
                bw.Write(ent.dirEnt);
                bw.Write(ent.dirAtr);
                bw.Write(ent.dirDatos);
                bw.Write(ent.dirSigEnt);

            }
            tamArchivo = guardar.Length;
            guardar.Close();
        }

        //Evento de hacer clic en el boton de Crear Entidad
        private void bt_CrearEntidad_Click(object sender, EventArgs e)
        {
            bt_CrearEntidad.Visible = false;

            lb_entidad.Visible = true;
            lb_cabecera.Visible = true;
            lb_NombreEnt.Visible = true;

            tb_Cabecera.Visible = true;
            tb_Entidad.Visible = true;

            bt_AgregarEnt.Visible = true;
            bt_EliminarEnt.Visible = true;
            bt_ModificarEnt.Visible = true;

            dgvEntidades.Visible = true;
        }

        /*Evento de hacer clic en el boton de agregar*/
        private void bt_AgregarEnt_Click(object sender, EventArgs e)
        {
            if (tb_Entidad.Text != "")
            {
                int res;
                string nombreEnt = tb_Entidad.Text;
                res = comparaEntidades(nombreEnt.PadRight(29));

                if (res == 0)//Inserta entidad cuando no se encuentra en la lista
                {
                    Entidad nuevo;
                    if (tamArchivo == 0)
                        nuevo = new Entidad(nombreEnt.PadRight(29), 8, -1, 0, -1);    //PadRight rellena la cadena para que el campo nom mida 35.
                    else
                        nuevo = new Entidad(nombreEnt.PadRight(29), tamArchivo, -1, 0, -1);    //PadRight rellena la cadena para que el campo nom mida 35.
                    lsEntidad.Add(nuevo);
                    lsEntidadAux.Add(nuevo);
                    int n = dgvEntidades.Rows.Add();

                    dgvEntidades.Rows[n].Cells[0].Value = tb_Entidad.Text;
                    dgvEntidades.Rows[n].Cells[1].Value = nuevo.dirEnt;
                    dgvEntidades.Rows[n].Cells[2].Value = nuevo.dirAtr;
                    dgvEntidades.Rows[n].Cells[3].Value = nuevo.dirDatos;
                    dgvEntidades.Rows[n].Cells[4].Value = nuevo.dirSigEnt;

                    if (n >= 1)
                        ordenaEntidades();

                    escribeArchivo(nombreArchivo);
                }
                else
                    MessageBox.Show("Esta entidad ya esta insertada!!");
                tb_Entidad.Text = "";
                bt_AgregaAtr.Visible = true;
            }
            else
                MessageBox.Show("No has escrito el nombre de la entidad!!");

        }
        private void leeAtributos(Entidad entidad, FileStream fileStream)
        {
            entidad.lsAtributo.Clear();
            long cabeceraAtr;
            if (entidad != null)
            {
                cabeceraAtr = entidad.DireccionAtributo;
                List<Atributo> atri = new List<Atributo>();

                long aux = cabeceraAtr;
                Atributo lee;
                string nom = "";
                char tipoI;
                int longT = 0;
                long dirA = 0;
                int tInd = 0;
                long dirInd = 0;
                long dirSigAtr = 0;
                while (aux != -1)
                {
                    fileStream.Position = aux;
                    fileStream.Seek(fileStream.Position, SeekOrigin.Begin);

                    BinaryReader reader = new BinaryReader(fileStream);

                    nom = reader.ReadString();
                    tipoI = reader.ReadChar();
                    longT = reader.ReadInt32();
                    dirA = reader.ReadInt64();
                    tInd = reader.ReadInt32();
                    dirInd = reader.ReadInt64();
                    dirSigAtr = reader.ReadInt64();

                    lee = new Atributo(nom, tipoI, longT, dirA, tInd, dirInd, dirSigAtr);
                    entidad.lsAtributo.Add(lee);
                    atri.Add(lee);
                    aux = dirSigAtr;
                }
                for (int i = 0; i < lsEntidad.Count; i++)
                {
                    if (lsEntidadAux[i].nombre == entidad.nombre)
                    {
                        lsEntidadAux[i].lsAtributo = atri;
                        break;
                    }
                }
            }

        }

        /*Método que compara si la entidad a agregar ya se encuentra en la lista de Entidades*/
        public int comparaEntidades(string nombre)
        {
            int res = 0;
            foreach (Entidad ent in lsEntidad)
            {
                if (ent.nombre == nombre)
                {
                    res = 1;
                    break;
                }
            }
            return res;
        }

        //Funcion que ordena las entidades de manera logica
        private void ordenaEntidades()
        {
            List<long> direcciones = new List<long>();

            string nombreEnt;

            long dirMenor = lsEntidad[0].dirEnt;
            while (direcciones.Count < lsEntidad.Count)
            {
                nombreEnt = "ZZZZZZZ";
                foreach (Entidad ent in lsEntidad)
                {
                    if (ent.dirEnt == -2)//Cuando la entidad está eliminada
                    {
                        ent.dirSigEnt = -2;
                    }
                    else if (nombreEnt.CompareTo(ent.nombre) == 1 && !(direcciones.Contains(ent.dirEnt)))
                    {
                        nombreEnt = ent.nombre;
                        dirMenor = ent.dirEnt;
                    }
                }
                direcciones.Add(dirMenor);
            }

            if (cabecera != direcciones[0])
            {
                cabecera = direcciones[0];
                tb_Cabecera.Text = Convert.ToString(cabecera);
            }

            int i = 0;

            foreach (long dir in direcciones)//Lista con direcciones ordenadas alfabeticamente
            {
                foreach (Entidad ent in lsEntidad)//Checa en cada entidad   PRIMERA ENTIDAD
                {
                    if (ent.dirEnt == dir)//Checa si la entidad pertenece a esa direccion
                    {
                        if (i < direcciones.Count - 1)
                        {
                            ent.dirSigEnt = direcciones[i + 1];
                            dgvEntidades.Rows[lsEntidad.IndexOf(ent)].Cells[4].Value = ent.dirSigEnt;
                        }
                        else
                        {
                            ent.dirSigEnt = -1;
                            dgvEntidades.Rows[lsEntidad.IndexOf(ent)].Cells[4].Value = ent.dirSigEnt;
                        }
                    }
                }
                i++;
            }
        }

        /*Evento de modificar el nombre de una entidad, lo camnbia  y lo guarda en la entidad*/
        private void bt_ModificarEnt_Click(object sender, EventArgs e)
        {
            entSeleccionada = tb_Entidad.Text;
            int res;
            if (entSeleccionada != "")
            {
                string compara = (string)dgvEntidades.CurrentRow.Cells[0].Value;
                res = comparaEntidades(entSeleccionada.PadRight(29));
                if (res == 0)
                {
                    foreach (Entidad mod in lsEntidad)
                    {

                        if (String.Compare(compara.PadRight(29), mod.nombre) == 0)
                        {
                            mod.nombre = entSeleccionada.PadRight(29);
                            ordenaEntidades();
                            escribeArchivo(nombreArchivo);
                        }
                    }
                    dgvEntidades.CurrentRow.Cells[0].Value = entSeleccionada;
                    tb_Entidad.Text = "";
                }
                else
                {
                    MessageBox.Show("No se puede modificar");
                    tb_Entidad.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Selecciones una entidad");
            }

        }

        /*Evento de eliminar una entidad*/
        private void bt_EliminarEnt_Click(object sender, EventArgs e)
        {


            entSeleccionada = (string)dgvEntidades.CurrentRow.Cells[0].Value;

            foreach (Entidad ent in lsEntidad)
            {

                if (String.Compare(entSeleccionada.PadRight(29), ent.nombre) == 0)
                {
                    if (File.Exists(entSeleccionada.PadRight(29) + ".dat"))
                    {
                        MessageBox.Show("La entidad contiene datos, imposible eliminar");
                        break;
                    }
                    else
                    {
                        //ent.dirEnt = -2;
                        lsEntidad.Remove(ent);
                        dgvEntidades.Rows.Remove(dgvEntidades.CurrentRow);
                        break;
                    }
                   
                }
            }
            if (lsEntidad.Count != 0)
            {
                ordenaEntidades();
            }
            else
            {
                MessageBox.Show("Eliminaste la ultima entidad");
                lsEntidad.Clear();
                escribeArchivo(nombreArchivo);
                tb_Cabecera.Text = Convert.ToString(-1);
            }

        }
        public void ActualizaAtributos(List<Entidad> entidads, Entidad ent)
        {
            int ter = 0;
            foreach (Entidad item in entidads)
            {
                if (item.nombre == ent.nombre)
                {
                    lsEntidadAux[ter].lsAtributo = item.lsAtributo;
                    break;
                }
                ter++;
            }

        }
        /*Evento que abre la ventana para agregar atributos de una entidad*/
        private void bt_AgregaAtr_Click(object sender, EventArgs e)
        {
            entSeleccionada = (string)dgvEntidades.CurrentRow.Cells[0].Value;
            if (entSeleccionada != null)
            {
                //cambio
                for (int i = 0; i < lsEntidad.Count; i++)
                {
                    if (string.Compare(entSeleccionada.PadRight(29), lsEntidad[i].nombre) == 0)
                    {
                        lsEntidadAux.Clear();
                        RecuperaRegistros();
                        //RecuperaDatos(lsEntidad[i]);
                        Form2 f2 = new Form2(lsEntidad[i], nombreArchivo, tamArchivo, lsEntidadAux);
                        f2.ShowDialog();
                        
                        break;
                    }
                }
                ordenaEntidades();
                //escribeArchivo(nombreArchivo);
                actualizaDGV();
                escribeArchivo(nombreArchivo);
            }
            else
            {
                MessageBox.Show("seleccione una entidad");
            }

        }



        /*Metodo que actulaiza los valores del DGV cuando hay un cambio*/
        private void actualizaDGV()
        {
            dgvEntidades.Rows.Clear();

            foreach (Entidad ent in lsEntidad)
            {
                int n = dgvEntidades.Rows.Add();

                dgvEntidades.Rows[n].Cells[0].Value = ent.nombre;
                dgvEntidades.Rows[n].Cells[1].Value = ent.dirEnt;
                dgvEntidades.Rows[n].Cells[2].Value = ent.dirAtr;
                dgvEntidades.Rows[n].Cells[3].Value = ent.dirDatos;
                dgvEntidades.Rows[n].Cells[4].Value = ent.dirSigEnt;
            }
        }
        public void RecuperaDatos(Entidad entidad)
        {
            if (File.Exists(entSeleccionada + ".dat"))
            {
                FileStream abre;
                abre = File.Open(entSeleccionada + ".dat", FileMode.Open);
                BinaryReader br = new BinaryReader(abre);
                long dirSiguiente = 0;
                long posicion = entidad.dirDatos;
                long tam = abre.Length;
                abre.Seek(posicion, SeekOrigin.Begin);
                while (dirSiguiente != -1)
                //while (abre.Position != tam)
                {

                    int celda = 0;

                    //abre.Seek(abre.Position, SeekOrigin.Begin);
                    abre.Seek(posicion, SeekOrigin.Begin);
                    //long dir = br.ReadInt64();

                    posicion = br.ReadInt64();


                    for (int i = 0; i < lsEntidadAux.Count; i++)
                    {
                        for (int j = 0; j < lsEntidadAux[i].lsAtributo.Count; j++)
                        {
                            if (lsEntidadAux[i].lsAtributo[j].tipoIndice == 2)
                            {
                                switch (lsEntidadAux[i].lsAtributo[j].tipoDato)
                                {
                                    case 'E':
                                        int entero = br.ReadInt32();
                                        lsEntidadAux[i].lsDatos.Add(entero.ToString());
                                        break;

                                    case 'C':
                                        string cadena = br.ReadString();
                                        lsEntidadAux[i].lsDatos.Add(cadena);
                                        break;
                                }
                                break;
                            }
                        }
                        dirSiguiente = br.ReadInt64();

                        posicion = dirSiguiente;
                    }


                }
            }
        }



        public void RecuperaRegistros()
        {
            BinaryWriter bw;
            //archivo = new FileStream(nomArchivo, FileMode.Create, FileAccess.Write);
            FileStream guardar = new FileStream(nombreArchivo + ".bin", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            // lsEntidadAux.Clear();
            bw = new BinaryWriter(guardar);

            if (tamArchivo == 0)
            {
                cabecera = 8;
                tb_Cabecera.Text = "8";
                bw.Seek(0, SeekOrigin.Begin);
                bw.Write(cabecera);
                tamArchivo = guardar.Length;
            }
            bw.Seek(0, SeekOrigin.Begin);
            bw.Write(cabecera);

            long aux;

            foreach (Entidad ent in lsEntidad)//Guarda la información por cada entidad
            {
                aux = ent.dirEnt;
                bw.Seek((int)aux, SeekOrigin.Begin);
                bw.Write(ent.nombre);
                bw.Write(ent.dirEnt);
                bw.Write(ent.dirAtr);
                bw.Write(ent.dirDatos);
                bw.Write(ent.dirSigEnt);
                lsEntidadAux.Add(ent);
                leeAtributos(ent, guardar);
                
            }
            tamArchivo = guardar.Length;
            guardar.Close();


        }
        private void btn_buscarEntidades_Click(object sender, EventArgs e)
        {
            lsEntidadAux.Clear();
            RecuperaRegistros();
           
            FormBusqueda formBusqueda = new FormBusqueda(lsEntidadAux);
            formBusqueda.Show();
        }
    }
}
