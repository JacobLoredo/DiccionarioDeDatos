using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace Diccionario_de_datos
{
    public partial class Form2 : Form
    {

        public Entidad entidadSel;
        public string nomArchivo;
        public long tamArchivo;
        public long cabeceraAtr;
        public List<Entidad> Aux = new List<Entidad>();
        public List<Entidad> Aux2 = new List<Entidad>();
        public List<CajonHash> CajonesHash = new List<CajonHash>();
        public List<CajonHash> CajonesHashAUX = new List<CajonHash>();

        public List<long> CajPrinHach = new List<long>();
        public List<long> CajPrinHachAUX = new List<long>();
        public List<long> CajPrinHachAUX2 = new List<long>();
        public List<int> vs = new List<int>();
        public List<long> vs2 = new List<long>();
        FileStream fileidx;

        /*Constructor del Form2*/
        public Form2(Entidad agrega, string archivo, long tam, List<Entidad> lsEntidad)
        {
            InitializeComponent();
            entidadSel = agrega;
            nomArchivo = archivo;
            tamArchivo = tam;
            Aux = lsEntidad;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lb_Entidad.Text = entidadSel.nombre;
            leeAtributos();


        }

        /*Método que se encarga de leer los atributos que esten guardados en el archivo*/
        private void leeAtributos()
        {
            if (entidadSel.DireccionAtributo != -1)
            {
                entidadSel.lsAtributo.Clear();
                FileStream abrirArchivo;
                abrirArchivo = File.Open(nomArchivo + ".bin", FileMode.Open, FileAccess.Read);
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

                while (aux != -1)
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
        private bool checaAtributoRepetido()
        {
            bool repetido = false;

            foreach (Atributo item in entidadSel.lsAtributo)
            {
                if (tb_Atributo.Text.PadRight(29) == item.nomAtributo.ToString())
                {
                    repetido = true;
                    break;
                }
                if (cb_TipoIndice.Text == "2" && item.tipoIndice == 2)
                {
                    repetido = true;
                    MessageBox.Show("Ya existe un atributo con clave 2 ");
                    break;
                }
            }



            return repetido;

        }
        public int detectaInd()
        {
            int num = -1;
            for (int i = 0; i < Aux.Count; i++)
            {
                if (cb_TipoIndice.Text == "8")
                {
                    if (Aux[i].nombre == comboBox1.Text)
                    {
                        num = i;
                        break;
                    }
                }
                else
                {

                    if (Aux[i].nombre == entidadSel.nombre)
                    {
                        num = i;
                        break;
                    }

                }
            }
            return num;
        }
        public int sumaTamAtributos(Entidad entida)
        {
            int tamT = 0;
            for (int i = 0; i < entida.lsAtributo.Count; i++)
            {
                tamT += entida.lsAtributo[i].longDato;
            }

            return tamT;
        }
        /*Evento que agrega la informacion a un atributio*/
        private void bt_AgregarAtr_Click(object sender, EventArgs e)
        {
            if (!checaAtributoRepetido())
            {
                if (cb_TipoIndice.Text == "8")
                {
                    // abreArchivoRegistros();
                    int r = detectaInd();
                    if (Aux[detectaInd()].lsDatos.Count > 0)
                    {


                        Atributo atr = new Atributo(Aux[detectaInd()].nombre, Convert.ToChar("E"), sumaTamAtributos(Aux[detectaInd()]), tamArchivo, int.Parse(cb_TipoIndice.Text), -1, -1);
                        entidadSel.agregaAtributo(atr);
                        int n = dgvAtributos.Rows.Add();
                        dgvAtributos.Rows[n].Cells[0].Value = Aux[detectaInd()].nombre;
                        dgvAtributos.Rows[n].Cells[1].Value = atr.tipoDato;
                        dgvAtributos.Rows[n].Cells[2].Value = atr.longDato;
                        dgvAtributos.Rows[n].Cells[3].Value = atr.dirAtributo;
                        dgvAtributos.Rows[n].Cells[4].Value = atr.tipoIndice;
                        dgvAtributos.Rows[n].Cells[5].Value = Aux[detectaInd()].dirAtr;
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

                    }
                    else
                    {
                        tb_Atributo.Text = "";
                        tb_LongitudDato.Text = "";
                        cb_TipoDato.Text = "";
                        cb_TipoIndice.Text = "";
                        comboBox1.Enabled = false;
                        comboBox1.Items.Clear();
                        MessageBox.Show("La entidad seleccionado no contiene Datos");
                    }
                }
                else
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
            }
            else
            {
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
            foreach (Atributo atr in entidadSel.lsAtributo)
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

            foreach (Atributo atr in entidadSel.lsAtributo)
            {
                int n = dgvAtributos.Rows.Add();

                dgvAtributos.Rows[n].Cells[0].Value = atr.nomAtributo;
                dgvAtributos.Rows[n].Cells[1].Value = atr.tipoDato;
                dgvAtributos.Rows[n].Cells[2].Value = atr.longDato;
                dgvAtributos.Rows[n].Cells[3].Value = atr.dirAtributo;
                dgvAtributos.Rows[n].Cells[4].Value = atr.tipoIndice;
                dgvAtributos.Rows[n].Cells[5].Value = atr.dirIndice;
                dgvAtributos.Rows[n].Cells[6].Value = atr.dirSigAtributo;
                if (atr.tipoIndice == 6)
                {
                    dgvAtributos.Rows[n].Cells[5].Value = 0;
                }
            }
        }

        /*Evento de apretar el boton de modificar atributo*/
        private void bt_ModificarAtr_Click(object sender, EventArgs e)
        {
            string compara = (string)dgvAtributos.CurrentRow.Cells[0].Value;
            if (!checaAtributoRepetido())
            {
                foreach (Atributo atr in entidadSel.lsAtributo)
                {
                    if (String.Compare(compara.PadRight(0), atr.nomAtributo) == 0)
                    {
                        if (tb_Atributo.Text.Length != 0)
                            atr.nomAtributo = tb_Atributo.Text.PadRight(29);
                        if (cb_TipoDato.Text.Length != 0)
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
        public List<string> regresaDatos()
        {
            return entidadSel.lsDatos;
        }
        /*Evento de insertar un nuevo registro de información*/
        private void bt_InsertaRegistro_Click(object sender, EventArgs e)
        {
            int t = tamañoDeRegistro();
            if (entidadSel.dirDatos == -1)
            {
                entidadSel.dirDatos = 0;
            }
            Form3 f3 = new Form3(entidadSel.nombre, entidadSel.lsAtributo, t, entidadSel.dirDatos, entidadSel, Aux);
            f3.ShowDialog();
            entidadSel.dirDatos = f3.regresaCabecera();

            if (f3.regresaDatos().Count > 0)
            {

                entidadSel.lsDatos = f3.regresaDatos();

            }
            actualizaDGVAtr();
            escribeAtributo();
        }

        /*Evento de cerrar la ventana*/
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (true)
            {
                Aux.Clear();
                entidadSel.lsAtributo.Clear();
            }
        }

        /*Método que regresa el valor de un registro con todos sus atributos*/
        private int tamañoDeRegistro()
        {
            int tamRegistro = 0;
            foreach (Atributo atr in entidadSel.lsAtributo)
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
                if (String.Compare(compara.PadRight(29), atr.nomAtributo) == 0 && atr.tipoIndice == 6)//Hash Dinámico
                {
                    Form4 f4 = new Form4(entidadSel.nombre, entidadSel.lsAtributo, atr);
                    f4.ShowDialog();
                }
            }
        }

        internal void ActualizaAtributos(ref List<Entidad> lsEntidadAux, Entidad ent)
        {
            for (int i = 0; i < Aux.Count; i++)
            {
                if (Aux[i].nombre == entidadSel.nombre)
                {
                    Aux[i].lsAtributo = entidadSel.lsAtributo;
                    break;
                }
            }
            entidadSel.lsAtributo.Clear();

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
            bt_VerIndices_Click(sender, e);
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
            if (cb_TipoIndice.Text == "8")
            {
                comboBox1.Enabled = true;
                for (int i = 0; i < Aux.Count; i++)
                {
                    if (Aux[i].nombre != entidadSel.nombre && File.Exists(Aux[i].nombre + ".dat"))
                    {
                        comboBox1.Items.Add(Aux[i].nombre.ToString());

                    }
                }
            }
            else
            {
                comboBox1.Enabled = false;
            }
        }
        public void creaCajonHASHpRINCIPAL()
        {/*
            long poscajon = 56;
            CajonHash aux = new CajonHash();
            aux.dircajon = poscajon;
            CajonesHash.Add(aux);
            CajPrinHach.Add(poscajon);
            */
            /*
            poscajon = (1040 * 5) + 56;
            CajonHash aux2 = new CajonHash();
            aux2.dircajon = (1040*5)+56;
            CajonesHash.Add(aux2);
            CajPrinHach.Add(poscajon);

            poscajon = (1040 * 2) + 56;
            CajonHash aux3 = new CajonHash();
            aux3.dircajon = (1040 * 2) + 56;
            CajonesHash.Add(aux3);
            CajPrinHach.Add(poscajon);

            poscajon = (1040 * 3) + 56;
            CajonHash aux4 = new CajonHash();
            aux4.dircajon = (1040 * 3) + 56;
            CajonesHash.Add(aux4);
            CajPrinHach.Add(poscajon);

            poscajon = (1040 * 1) + 56;
            CajonHash aux5 = new CajonHash();
            aux5.dircajon = (1040 * 1) + 56;
            CajonesHash.Add(aux5);
            CajPrinHach.Add(poscajon);

            poscajon = (1040 * 4) + 56;
            CajonHash aux6 = new CajonHash();
            aux6.dircajon = (1040 * 6) + 56;
            CajonesHash.Add(aux6);
            CajPrinHach.Add(poscajon);

            poscajon = -1;
            CajonHash aux7 = new CajonHash();
            aux7.dircajon = -1;
            CajonesHash.Add(aux7);
            CajPrinHach.Add(poscajon);
            */
            /*
            for (int i = 0; i < 7; i++)
            {
                CajonHash aux2 = new CajonHash();
                CajonesHashAUX.Add(aux2);      
                CajPrinHachAUX.Add(poscajon);
                CajPrinHachAUX2.Add(poscajon);
                poscajon += 1040;
            }*/
            //LLENO UN ARREGLO LONG CON LOS VALORES DEL CAJON PRINCIPAL,56,1096,ETC
            long poscajon = 56;

            for (int i = 0; i < 7; i++)
            {
                CajonHash aux = new CajonHash();
                aux.dircajon = poscajon;
                CajonesHash.Add(aux);

                CajPrinHach.Add(poscajon);

                poscajon += 1040;

            }
        }
        //Funcion que abre mi archivo de datos para asi usarlos despues en el hash
        private void abreArchivoRegistros()
        {
            int tam2 = entidadSel.ToString().Length;
            string vv = Aux[detectaInd()].nombre.ToString() + ".dat";
            string arhicvoDatosDistinto8 = entidadSel.nombre + ".dat";
            if (cb_TipoIndice.Text == "8")
            {
                arhicvoDatosDistinto8 = vv;
            }



            if (File.Exists(arhicvoDatosDistinto8))
            {
                List<string> datosaux = new List<string>();
                FileStream abre;
                if (cb_TipoIndice.Text == "8")
                {
                    abre = File.Open(arhicvoDatosDistinto8, FileMode.Open);
                }
                else
                {
                    abre = File.Open(entidadSel.nombre + ".dat", FileMode.Open);

                }
                BinaryReader br = new BinaryReader(abre);
                long dirSiguiente = 0;
                long posicion = entidadSel.dirDatos;
                long tam = abre.Length;
                abre.Seek(posicion, SeekOrigin.Begin);

                while (dirSiguiente != -1)
                //while (abre.Position != tam)
                {

                    //abre.Seek(abre.Position, SeekOrigin.Begin);
                    abre.Seek(posicion, SeekOrigin.Begin);
                    //long dir = br.ReadInt64();

                    posicion = br.ReadInt64();


                    foreach (Atributo atr in entidadSel.lsAtributo)
                    {
                        switch (atr.tipoDato)
                        {
                            case 'E':
                                int entero = br.ReadInt32();
                                if (atr.tipoIndice == 6)
                                {
                                    vs.Add(entero);//arreglo que guarda el valor de la clave 6
                                    vs2.Add(posicion);//arreglo que guarda la direccion donde esta ese dato
                                }
                                if (atr.tipoIndice == 2)
                                {

                                    datosaux.Add(entero.ToString());

                                }

                                break;

                            case 'C':
                                string cadena = br.ReadString();


                                break;
                        }

                    }
                    dirSiguiente = br.ReadInt64();

                    posicion = dirSiguiente;

                }

                abre.Close();
                foreach (var item in datosaux)
                {
                    Aux[detectaInd()].lsDatos.Add(item.ToString());
                }


            }
        }
        //no funciona
        private void cargaIDXHash()
        {
            fileidx = File.Open(entidadSel.nombre + ".idxs", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader bridx = new BinaryReader(fileidx);

            fileidx.Close();

        }
        //checar clase cajon y DatoCajon
        private void hashEsbtn_Click(object sender, EventArgs e)
        {
            //limbiar listas para que los datos no se dupliquen 
            CajPrinHach.Clear();
            CajonesHash.Clear();
            CajonesHashAUX.Clear();
            CajPrinHachAUX.Clear();
            vs.Clear();
            vs2.Clear();
            //se crea el cajon principal
            creaCajonHASHpRINCIPAL();
            //cargamos los datos ya ordenados por clave 6 
            abreArchivoRegistros();
            for (int i = 0; i < vs.Count; i++)
            {
                DatoCajonHash auxcaj = new DatoCajonHash();
                auxcaj.valint = vs[i];//arreglo donde esta el valor de la clave
                auxcaj.dir = vs2[i];//valor donde esta la direccion

                int res = (auxcaj.valint % 7);//sacar el modulo y ver en que cajon va
                CajonesHash[res].Cajon.Add(auxcaj);//se pone en el cajon correspondiente los datos
                                                   //creaCajonBajoDemanda(res, auxcaj);


            }
            cargaIDXHash();//no funciona
            FormaHash frm = new FormaHash(CajPrinHach, CajonesHash, fileidx.Name);//le pasas las lista donde estan las direcciones del cajonprincipal, y la lista de cajones
            frm.Show();
        }
        //no le hagas caso a esto
        private void creaCajonBajoDemanda(int res, DatoCajonHash auxcaj)
        {
            if (res == 0)
            {
                CajonesHash[res].Cajon.Add(auxcaj);
            }
            else if (res == 1)
            {
                if (CajPrinHach.Contains(CajPrinHachAUX[res]))
                {
                    for (int i = 0; i < CajonesHash.Count; i++)
                    {
                        if (CajPrinHachAUX[res + 1] == CajonesHash[i].dircajon)
                        {

                            CajonesHash[i].Cajon.Add(auxcaj);
                            break;
                        }
                    }
                }
                else
                {
                    CajonHash aux = new CajonHash();
                    if (CajonesHash.Count == 1)
                    {
                        aux.dircajon = 1040 + 56;
                        CajonesHash.Add(aux);
                        CajPrinHach.Add(1040 + 56);
                    }
                    else if (CajonesHash.Count == 2)
                    {
                        aux.dircajon = (1040 * 2) + 56;
                        CajonesHash.Add(aux);
                        CajPrinHach.Add((1040 * 2) + 56);
                    }
                    else if (CajonesHash.Count == 3)
                    {
                        aux.dircajon = (1040 * 3) + 56;

                    }
                    else if (CajonesHash.Count == 4)
                    {
                        aux.dircajon = (1040 * 4) + 56;
                    }
                    else if (CajonesHash.Count == 5)
                    {
                        aux.dircajon = (1040 * 5) + 56;
                    }


                    for (int y = 0; y < CajonesHash.Count; y++)
                    {
                        if (CajPrinHachAUX[res] == CajonesHash[y].dircajon)
                        {
                            CajonesHash[y].Cajon.Add(auxcaj);
                            break;
                        }
                    }
                }
            }
            else if (res == 2)
            {
                if (CajPrinHach.Contains(CajPrinHachAUX[res]))
                {
                    for (int i = 0; i < CajonesHash.Count; i++)
                    {
                        if (CajPrinHachAUX[res] == CajonesHash[i].dircajon)
                        {

                            CajonesHash[i].Cajon.Add(auxcaj);
                            break;
                        }
                    }
                }
                else
                {
                    CajonHash aux = new CajonHash();
                    if (CajonesHash.Count == 1)
                    {
                        aux.dircajon = 1040 + 56;
                        aux.Cajon.Add(auxcaj);
                        CajonesHash.Add(aux);
                        CajPrinHach.Add(1040 + 56);
                    }
                    else if (CajonesHash.Count == 2)
                    {
                        aux.dircajon = (1040 * 2) + 56;
                        CajonesHash.Add(aux);
                        CajPrinHach.Add((1040 * 2) + 56);
                    }
                    else if (CajonesHash.Count == 3)
                    {
                        aux.dircajon = (1040 * 3) + 56;

                    }
                    else if (CajonesHash.Count == 4)
                    {
                        aux.dircajon = (1040 * 4) + 56;
                    }
                    else if (CajonesHash.Count == 5)
                    {
                        aux.dircajon = (1040 * 5) + 56;
                    }

                    for (int y = 0; y < CajonesHash.Count; y++)
                    {
                        if (CajPrinHachAUX[res] == CajonesHash[y].dircajon)
                        {
                            CajonesHash[y].Cajon.Add(auxcaj);
                            break;
                        }
                    }
                }
            }
            else if (res == 3)
            {
                if (CajPrinHach.Contains(CajPrinHachAUX[res]))
                {
                    for (int i = 0; i < CajonesHash.Count; i++)
                    {
                        if (CajPrinHachAUX[res] == CajonesHash[i].dircajon)
                        {

                            CajonesHash[i].Cajon.Add(auxcaj);
                            break;
                        }
                    }
                }
                else
                {
                    CajonHash aux = new CajonHash();
                    if (CajonesHash.Count == 1)
                    {

                        aux.dircajon = 1040 + 56;
                        aux.Cajon.Add(auxcaj);
                        CajonesHash.Add(aux);
                        CajPrinHach.Add(1040 + 56);
                    }
                    else if (CajonesHash.Count == 2)
                    {
                        aux.dircajon = (1040 * 2) + 56;
                        aux.Cajon.Add(auxcaj);
                        CajonesHash.Add(aux);
                        CajPrinHach.Add((1040 * 2) + 56);
                    }
                    else if (CajonesHash.Count == 3)
                    {
                        aux.dircajon = (1040 * 3) + 56;
                        aux.Cajon.Add(auxcaj);
                        CajonesHash.Add(aux);
                        CajPrinHach.Add((1040 * 3) + 56);

                    }
                    else if (CajonesHash.Count == 4)
                    {
                        aux.dircajon = (1040 * 4) + 56;
                        aux.Cajon.Add(auxcaj);
                        CajonesHash.Add(aux);
                        CajPrinHach.Add((1040 * 4) + 56);
                    }
                    else if (CajonesHash.Count == 5)
                    {
                        aux.dircajon = (1040 * 5) + 56;
                        aux.Cajon.Add(auxcaj);
                        CajonesHash.Add(aux);
                        CajPrinHach.Add((1040 * 5) + 56);
                    }

                    for (int y = 0; y < CajonesHash.Count; y++)
                    {
                        if (CajPrinHachAUX[res] == CajonesHash[y].dircajon)
                        {
                            CajonesHash[y].Cajon.Add(auxcaj);
                            break;
                        }
                    }
                }
            }
            else if (res == 4)
            {
                if (CajPrinHach.Contains(CajPrinHachAUX[res]))
                {
                    for (int i = 0; i < CajonesHash.Count; i++)
                    {
                        if (CajPrinHachAUX[res] == CajonesHash[i].dircajon)
                        {

                            CajonesHash[i].Cajon.Add(auxcaj);
                            break;
                        }
                    }
                }
                else
                {
                    CajonHash aux = new CajonHash();
                    if (CajonesHash.Count == 1)
                    {
                        aux.dircajon = 1040 + 56;
                        aux.Cajon.Add(auxcaj);
                        CajonesHash.Add(aux);
                        CajPrinHach.Add(1040 + 56);
                    }
                    else if (CajonesHash.Count == 2)
                    {
                        aux.dircajon = (1040 * 2) + 56;
                        aux.Cajon.Add(auxcaj);
                        CajonesHash.Add(aux);
                        CajPrinHach.Add((1040 * 2) + 56);
                    }
                    else if (CajonesHash.Count == 3)
                    {
                        aux.dircajon = (1040 * 3) + 56;
                        aux.Cajon.Add(auxcaj);
                        CajonesHash.Add(aux);
                        CajPrinHach.Add((1040 * 3) + 56);

                    }
                    else if (CajonesHash.Count == 4)
                    {
                        aux.dircajon = (1040 * 4) + 56;
                        aux.Cajon.Add(auxcaj);
                        CajonesHash.Add(aux);
                        CajPrinHach.Add((1040 * 4) + 56);
                    }
                    else if (CajonesHash.Count == 5)
                    {
                        aux.dircajon = (1040 * 5) + 56;
                        aux.Cajon.Add(auxcaj);
                        CajonesHash.Add(aux);
                        CajPrinHach.Add((1040 * 5) + 56);
                    }




                    for (int y = 0; y < CajonesHash.Count; y++)
                    {
                        if (CajPrinHachAUX[res] == CajonesHash[y].dircajon)
                        {
                            CajonesHash[y].Cajon.Add(auxcaj);
                            break;
                        }
                    }
                }
            }
            else if (res == 5)
            {
                if (CajPrinHach.Contains(CajPrinHachAUX[res]))
                {
                    for (int i = 0; i < CajonesHash.Count; i++)
                    {
                        if (CajPrinHachAUX[res] == CajonesHash[i].dircajon)
                        {

                            CajonesHash[i].Cajon.Add(auxcaj);
                            break;
                        }
                    }
                }
                else
                {
                    CajonHash aux = new CajonHash();
                    if (CajonesHash.Count == 1)
                    {
                        aux.dircajon = 1040 + 56;
                        aux.Cajon.Add(auxcaj);
                        CajonesHash.Add(aux);
                        CajPrinHach.Add(1040 + 56);
                    }
                    else if (CajonesHash.Count == 2)
                    {
                        aux.dircajon = 1040 + 56;
                        aux.Cajon.Add(auxcaj);
                        CajonesHash.Add(aux);
                        CajPrinHach.Add(1040 + 56);
                    }
                    else if (CajonesHash.Count == 3)
                    {
                        aux.dircajon = 1040 + 56;
                        aux.Cajon.Add(auxcaj);
                        CajonesHash.Add(aux);
                        CajPrinHach.Add(1040 + 56);

                    }
                    else if (CajonesHash.Count == 4)
                    {
                        aux.dircajon = 1040 + 56;
                        aux.Cajon.Add(auxcaj);
                        CajonesHash.Add(aux);
                        CajPrinHach.Add(1040 + 56);
                    }
                    else if (CajonesHash.Count == 5)
                    {
                        aux.dircajon = 1040 + 56;
                        aux.Cajon.Add(auxcaj);
                        CajonesHash.Add(aux);
                        CajPrinHach.Add(1040 + 56);
                    }




                    for (int y = 0; y < CajonesHash.Count; y++)
                    {
                        if (CajPrinHachAUX[res] == CajonesHash[y].dircajon)
                        {
                            CajonesHash[y].Cajon.Add(auxcaj);
                            break;
                        }
                    }
                }
            }
            else if (res == 6)
            {

            }

            /*
            if (res == 0)
            {
                CajonesHash[res].Cajon.Add(auxcaj);
            }
            else
            {
                
                long poscajon = (1040 * (res + 1)) + 56;
                CajonHash aux = new CajonHash();
                aux.dircajon = poscajon;
                if (CajPrinHach.Contains(poscajon))
                {
                    for (int i = 0; i < CajonesHash.Count; i++)
                    {
                        if (poscajon == CajonesHash[i].dircajon)
                        {

                            CajonesHash[i].Cajon.Add(auxcaj);
                            break;
                        }
                    }
                }
                else
                {
                    CajonesHash.Add(aux);
                    CajPrinHach.Add(poscajon);
                    for (int y = 0; y < CajonesHash.Count; y++)
                    {
                        if (poscajon == CajonesHash[y].dircajon)
                        {
                            CajonesHash[y].Cajon.Add(auxcaj);
                            break;
                        }
                    }
                }
            }*/
        }
    }
}
