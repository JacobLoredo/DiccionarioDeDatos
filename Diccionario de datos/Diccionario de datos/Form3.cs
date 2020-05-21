using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Diccionario_de_datos
{
    public partial class Form3 : Form
    {
        string nombreEntidad;
        public List<Atributo> atributos = new List<Atributo>();
        public List<TextBox> textBoxes = new List<TextBox>();//Crea una lista de TextBoxes para manipularlos y tener acceso a su informacion.
        Point p1 = new Point(15, 30);//Posicion X,Y de los label.
        Point p2 = new Point(125, 30);//Posicion X,Y de los TextBox.

        long tamArchivoData = 0;
        int tamRegistro = 0;
        long cabecera = 0;
        Entidad entActual;
        Entidad entActualCla1;
        Entidad entActualCla2;
        Entidad entActualCla3;
        Entidad entActualCla6;

        /*Constructor del Form 3*/
        public Form3(string nEnt, List<Atributo> lista, int tam, long cabRegistros, Entidad ent)
        {
            InitializeComponent();
            nombreEntidad = nEnt;
            atributos = lista;//Se le pasa la lista que se ha creado en el Form2 que contiene los atributos.
            tamRegistro = tam;
            cabecera = cabRegistros;
            entActual = ent;
            entActualCla1 = ent;
            entActualCla2 = ent;
            entActualCla3 = ent;
            entActualCla6 = ent;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            foreach (Atributo atr in atributos)//Dibuja las herramientas necesarias para capturar la informacion.
            {
                if (atr.tipoIndice == 8)
                {
                    Label lb = new Label();
                    ComboBox comboBox = new ComboBox();
                    comboBox.Enabled = true;
                    foreach (var item in entActual.lsDatos)
                    {
                        comboBox.Items.Add(item);
                    }
                    lb.Text = atr.nomAtributo;
                    lb.Location = p1;
                    lb.Visible = true;
                    comboBox.Visible = true;
                    comboBox.Location = p2;
                    this.Controls.Add(lb);
                    this.Controls.Add(comboBox);
                    p1.Y = p1.Y + 30;
                    p2.Y = p2.Y + 30;
                }
                else
                {

                    Label lb = new Label();
                    TextBox tb = new TextBox();

                    lb.Enabled = true;
                    tb.Enabled = true;

                    lb.Visible = true;
                    tb.Visible = true;

                    lb.Location = p1;
                    tb.Location = p2;

                    lb.Text = atr.nomAtributo;

                    this.Controls.Add(lb);
                    this.Controls.Add(tb);

                    textBoxes.Add(tb);

                    p1.Y = p1.Y + 30;
                    p2.Y = p2.Y + 30;
                }
            }
            p1.Y = p1.Y + 20;
            dgvRegistros.Location = p1;

            if (dgvRegistros.Rows.Count < 2)
            {
                foreach (Atributo atr in atributos)//Agrega nuevas columnas segun los atributos existentes.
                {
                    dgvRegistros.Columns.Add("Nom" + atr.nomAtributo, atr.nomAtributo);
                }
                dgvRegistros.Columns.Add("SigReg", "Dir.SigReg");
            }
            abreArchivoRegistros();
        }
        private bool checa()
         {
            bool res = false;
            
            //crear una lista de datos para checar repetidos

            int n = dgvRegistros.Rows.Count;

           

            List<List<string>> Listas = new List<List<string>>();
            for (int i = 0; i < n-1; i++)
            {
                List<string> aux = new List<string>();
                for (int j = 0; j < dgvRegistros.Columns.Count-2; j++)
                {
                    aux.Add(dgvRegistros.Rows[i].Cells[j+1].Value.ToString());
                }
                Listas.Add(aux);
            }
            int indece2 = 0;
            for (int i = 0; i < atributos.Count; i++)
            {
                if (atributos[i].tipoIndice == 2)
                {
                    indece2=i;
                }
            }
                
            
            foreach (TextBox tb in textBoxes)
            {
                for (int i = 0; i < Listas.Count; i++)
                {
                    for (int j = 0; j <Listas[i].Count; j++)
                    {
                        if (dgvRegistros.Rows[i].Cells[indece2 + 1].Value.ToString()==tb.Text.ToString())
                        {
                            res = true;
                            break;
                        }
                    }
                }
            }
          
            return res;
        }
        private bool checa2()
        {
            bool res = false;

            //crear una lista de datos para checar repetidos

            int n = dgvRegistros.Rows.Count;

            List<string> aux = new List<string>();
            List<string> aux2 = new List<string>();
            List<string> aux3 = new List<string>();

            for (int i = 0; i < n - 1; i++)
            {
                aux2.Add(dgvRegistros.Rows[i].Cells[1].Value.ToString());
                aux.Add(dgvRegistros.Rows[i].Cells[2].Value.ToString());
                aux3.Add(dgvRegistros.Rows[i].Cells[3].Value.ToString());
            }

            foreach (TextBox tb in textBoxes)
            {
                if (aux2.Contains(tb.Text))
                {
                    res = true;
                    break;

                }


            }
            return res;
        }
        /*Evento al hacer clic en el boton de guardar*/
        private void bt_Guardar_Click(object sender, EventArgs e)
        {
            if (!checa())
            {
                int res = 0;
                foreach (TextBox tb in textBoxes)
                {
                    if (tb.Text == "")
                    {
                        MessageBox.Show("Faltan campos por capturar!!");
                        res = 1;
                        break;
                    }
                }
                if (res == 0)//Si todos los campos estan llenos.
                {
                    if (ChecaTamaño())
                        escribeRegistros();
                    else
                        MessageBox.Show("El tamaño de alguna dato no es correcto. Vuelva a ingresar los datos.");
                    //this.Close();
                }
            }
            else
            {
                MessageBox.Show("elemento ya existente");
            }
        }
        private bool ChecaTamaño()
        {
            bool cumple = true;
            int i = 0;

            //int n = dgvRegistros.Rows.Add();

            foreach (TextBox tb in textBoxes)
            {
                if (cumple)
                {
                    switch (atributos[i].tipoDato)//Agrega el valor nuevo al DGV.
                    {
                        case 'C':
                            string cadena = tb.Text.PadRight(atributos[i].longDato - 1);
                            if (cadena.Length > atributos[i].longDato)
                            {
                                cumple = false;

                                break;
                            }
                            else
                            {
                                //dgvRegistros.Rows[n].Cells[i + 1].Value = cadena;
                                break;
                            }
                    }

                    i++;
                }
                else
                {
                    return cumple;
                }
            }

            return cumple;
        }
        /*Método que solamente escribe los datos en el DGV*/
        private void escribeRegistros()
        {
            long dirRegistroActual = 0;
            long dirSigReg = 0;


            BinaryWriter bw;
            FileStream guardar = new FileStream(nombreEntidad + ".dat", FileMode.OpenOrCreate, FileAccess.Write);
            bw = new BinaryWriter(guardar);

            if (guardar.Length == 0)
            {
                tamArchivoData = 0;
            }

            tamArchivoData = guardar.Length;
            dirRegistroActual = guardar.Length;

            bw.Seek((int)tamArchivoData, SeekOrigin.Begin);

            int i = 0;  // i es el textbox que se esta recorriendo

            int n = dgvRegistros.Rows.Add();
            dgvRegistros.Rows[n].Cells[0].Value = dirRegistroActual;

            //bw.Write(dirRegistroActual);
            //direcciones.Add(dirRegistroActual);

            foreach (TextBox tb in textBoxes)
            {
                switch (atributos[i].tipoDato)//Agrega el valor nuevo al DGV.
                {
                    case 'E':
                        int valor = int.Parse(tb.Text);
                        dgvRegistros.Rows[n].Cells[i + 1].Value = valor;

                        break;

                    case 'C':
                        string cadena = tb.Text.PadRight(atributos[i].longDato - 1);
                        dgvRegistros.Rows[n].Cells[i + 1].Value = cadena;
                        // MessageBox.Show(cadena.Length.ToString()); validar
                        break;
                }

                switch (atributos[i].tipoIndice)//Verifica el tipo de indice a escribir en el archivo .idx
                {
                    case 2: //Indice Primario
                        indicePrimarioChido(atributos[i], tb.Text, dirRegistroActual);//tipo de atributo, valor que entra y direccion del registro
                        //entActual.lsDatos.Add(tb.Text);
                        break;

                    case 3: //Indice Secundario
                        indiceSecundario(atributos[i], tb.Text, dirRegistroActual);//tipo de atributo, valor que entra y direccion del registro
                        break;
                }
                tb.Text = "";
                i++;
            }

            if (dgvRegistros.Rows.Count > 2)
            {
                dgvRegistros.Rows[n - 1].Cells[i + 1].Value = dirRegistroActual;
            }

            dirSigReg = -1;
            dgvRegistros.Rows[n].Cells[i + 1].Value = dirSigReg;
            guardar.Close();
            bw.Close();

            guardaDGV();

            int col = 1;
            foreach (Atributo atr in atributos)
            {
                switch (atr.tipoIndice)
                {
                    case 1: //Clave de búsqueda//cambiar caso 
                        if (dgvRegistros.Rows.Count > 2)
                            claveDeBusqueda(atr, col);
                        break;
                    
                }
                col++;
            }

            entActual.dirDatos = cabecera;

            col = 1;
            foreach (Atributo atr in atributos)
            {
                if (atr.tipoIndice == 6)
                {
                    hashDinamico(atributos[col - 1], Convert.ToString(dgvRegistros.Rows[n].Cells[col].Value), dirRegistroActual);
                }
                col++;
            }
            guardaDGV();
        }


        private void escribeRegistrosPorIndice2()
        {
            long dirRegistroActual = 0;
            long dirSigReg = 0;


            BinaryWriter bw;
            FileStream guardar = new FileStream(nombreEntidad + ".dat", FileMode.OpenOrCreate, FileAccess.Write);
            bw = new BinaryWriter(guardar);

            if (guardar.Length == 0)
            {
                tamArchivoData = 0;
            }

            tamArchivoData = guardar.Length;
            dirRegistroActual = guardar.Length;

            bw.Seek((int)tamArchivoData, SeekOrigin.Begin);

            int i = 0;  // i es el textbox que se esta recorriendo

            int n = dgvRegistros.Rows.Add();
            dgvRegistros.Rows[n].Cells[0].Value = dirRegistroActual;

            //bw.Write(dirRegistroActual);
            //direcciones.Add(dirRegistroActual);
            foreach (TextBox tb in textBoxes)
            {
                switch (atributos[i].tipoDato)//Agrega el valor nuevo al DGV.
                {
                    case 'E':
                        int valor = int.Parse(tb.Text);
                        dgvRegistros.Rows[n].Cells[i + 1].Value = valor;
                        break;

                    case 'C':
                        string cadena = tb.Text.PadRight(atributos[i].longDato - 1);
                        dgvRegistros.Rows[n].Cells[i + 1].Value = cadena;
                        // MessageBox.Show(cadena.Length.ToString()); validar
                        break;
                }

                switch (atributos[i].tipoIndice)//Verifica el tipo de indice a escribir en el archivo .idx
                {
                    case 2: //Indice Primario
                        indicePrimarioChido(atributos[i], tb.Text, dirRegistroActual);//tipo de atributo, valor que entra y direccion del registro
                        break;

                    case 3: //Indice Secundario
                        indiceSecundario(atributos[i], tb.Text, dirRegistroActual);//tipo de atributo, valor que entra y direccion del registro
                        break;
                }
                tb.Text = "";
                i++;
            }

            if (dgvRegistros.Rows.Count > 2)
            {
                dgvRegistros.Rows[n - 1].Cells[i + 1].Value = dirRegistroActual;
            }

            dirSigReg = -1;
            dgvRegistros.Rows[n].Cells[i + 1].Value = dirSigReg;
            guardar.Close();
            bw.Close();

            guardaDGV();

            int col = 1;
            foreach (Atributo atr in atributos)
            {
                switch (atr.tipoIndice)
                {
                    case 1: //Clave de búsqueda//cambiar caso 
                        if (dgvRegistros.Rows.Count > 2)
                            claveDeBusqueda(atr, col);
                        break;
                }
                col++;
            }

            entActual.dirDatos = cabecera;

            col = 1;
            foreach (Atributo atr in atributos)
            {
                if (atr.tipoIndice == 6)
                {
                    hashDinamico(atributos[col - 1], Convert.ToString(dgvRegistros.Rows[n].Cells[col].Value), dirRegistroActual);
                }
                col++;
            }
            guardaDGV();
        }
        /*Método que elige si la clave de busqueda se hara con datos int o string*/
        private void claveDeBusqueda(Atributo atr, int col)
        {
            switch (atr.tipoDato)
            {
                case 'E':
                    List<int> listaInt = new List<int>();
                    for (int num = 0; num < dgvRegistros.Rows.Count - 1; num++)//filas
                    {
                        listaInt.Add((int)dgvRegistros.Rows[num].Cells[col].Value);
                    }
                    ordenaClavedeBusquedaInt(listaInt, col);
                    break;

                case 'C':
                    List<string> listaStr = new List<string>();
                    for (int num = 0; num < dgvRegistros.Rows.Count - 1; num++)
                    {
                        listaStr.Add((string)dgvRegistros.Rows[num].Cells[col].Value);
                    }
                    ordenaClavedeBusquedaString(listaStr, col);
                    break;
            }
        }

        /*Método que crea una instancia de la clase Indice para crear el archivo idx y guardar los datos*/
        private void indicePrimarioChido(Atributo atr, string valor, long direccion)
        {
            Indice ind;
            switch (atr.tipoDato)
            {
                case 'E':
                    ind = new Indice(atr, nombreEntidad);
                    if (atr.dirIndice == -1)
                        ind.escribeIndice();
                    ind.guardaDatoEnteroIndicePrimario(int.Parse(valor), direccion);
                    break;

                case 'C':
                    ind = new Indice(atr, nombreEntidad);
                    if (atr.dirIndice == -1)
                        ind.escribeIndice();
                    ind.guardaDatoStringIndicePrimario(valor.PadRight(atr.longDato - 1), direccion);
                    break;
            }
        }

        /*Método que crea una instancia de la clase indice e inserta el dato en el indice secundario*/
        private void indiceSecundario(Atributo atr, string valor, long direccion)
        {
            Indice ind;
            switch (atr.tipoDato)
            {
                case 'E':
                    ind = new Indice(atr, nombreEntidad);
                    if (atr.dirIndice == -1)
                        ind.escribeIndice();
                    //ind.guardaDatoEnteroIndicePrimario(int.Parse(valor), direccion);
                    ind.indiceSecundario(int.Parse(valor), direccion);
                    break;

                case 'C':
                    ind = new Indice(atr, nombreEntidad);
                    if (atr.dirIndice == -1)
                        ind.escribeIndice();
                    ind.indiceSecundario(valor.PadRight(atr.longDato - 1), direccion);
                    break;
            }
        }

        /*Método que crea una instancia de la clase indice e inserta el dato de tipo hash dinamico*/
        private void hashDinamico(Atributo atr, string valor, long direccionReg)
        {//Recibe el atributo con indice hash, el valor convertido a binario y la direccion de la informacion en el registro de datos
            Indice ind = new Indice(entActual, atr, nombreEntidad, direccionReg);
            if (atr.dirIndice == -1)//Si el indice aun no ha sido creado en el archivo
            {

                ind.creaIndiceHashDinamico();
                int valorInt = Int32.Parse(valor.ToString());
                string valorBinario = regresavalorBinario(valorInt);
                ind.HashDinamico(valorBinario, atributos, entActual);
            }
            else
            {
                int valorInt = int.Parse(valor);
                string valorBinario = regresavalorBinario(valorInt);
                ind.HashDinamico(valorBinario, atributos, entActual);
            }

        }

        /*Método que regresa una cadena que contiene el valor en binario de un número entero*/
        private string regresavalorBinario(int valor)
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
            if (binario.Length < 8)//Si el numero en binario es inferior a 8 bits, se llena de 0´s en la parte izquierda de la cadena
            {
                string ceros = "";
                int espacios = 8 - binario.Length;
                for (int i = 0; i < espacios; i++)
                    ceros = ceros + '0';
                binario = ceros + binario;
            }
            return binario;
        }

        /*Método que guarda la informacion del DGV en el archivo '.dat'*/
        private void guardaDGV()
        {
            BinaryWriter bw;
            FileStream guardar = new FileStream(nombreEntidad + ".dat", FileMode.OpenOrCreate, FileAccess.Write);
            bw = new BinaryWriter(guardar);
            //int posDirSig = dgvRegistros.ColumnCount;
            //guardar.Position = 0;
            for (int i = 0; i < dgvRegistros.Rows.Count - 1; i++)//fila
            {
                int columna = 0;
                long dirActual = (long)dgvRegistros.Rows[i].Cells[columna].Value;
                //guardar.Position = dirActual;

                guardar.Seek(dirActual, SeekOrigin.Begin);
                bw.Write(dirActual);
                columna++;
                foreach (Atributo atr in atributos)
                {
                    switch (atr.tipoDato)
                    {
                        case 'E':
                            int valor = (int)dgvRegistros.Rows[i].Cells[columna].Value;
                            //entActual.lsDatos.Add(valor.ToString());
                            bw.Write(valor);
                            break;

                        case 'C':
                            string cadena = (string)dgvRegistros.Rows[i].Cells[columna].Value;
                            //entActual.lsDatos.Add(cadena);
                            bw.Write(cadena);
                            break;
                    }
                    columna++;
                }
                long dirSig = (long)dgvRegistros.Rows[i].Cells[columna].Value;
                bw.Write(dirSig);
                /*if(dirSig != -1)
                    guardar.Position = dirSig;*/
            }
            guardar.Close();
        }

        /*Método que abre el archivo .dat*/
        private void abreArchivoRegistros()
        {
            entActualCla1.lsDatos.Clear();
            entActualCla2.lsDatos.Clear();
            entActualCla3.lsDatos.Clear();
            entActualCla6.lsDatos.Clear();
            if (File.Exists(nombreEntidad + ".dat"))
            {
                FileStream abre;
                abre = File.Open(nombreEntidad + ".dat", FileMode.Open);
                BinaryReader br = new BinaryReader(abre);
                long dirSiguiente = 0;
                long posicion = cabecera;
                long tam = abre.Length;
                abre.Seek(posicion, SeekOrigin.Begin);

                while (dirSiguiente != -1)
                //while (abre.Position != tam)
                {
                    int n = dgvRegistros.Rows.Add();
                    int celda = 0;

                    //abre.Seek(abre.Position, SeekOrigin.Begin);
                    abre.Seek(posicion, SeekOrigin.Begin);
                    //long dir = br.ReadInt64();

                    posicion = br.ReadInt64();
                    dgvRegistros.Rows[n].Cells[celda].Value = posicion;
                    celda++;
                    foreach (Atributo atr in atributos)
                    {
                        switch (atr.tipoDato)
                        {
                            case 'E':
                                int entero = br.ReadInt32();

                                dgvRegistros.Rows[n].Cells[celda].Value = entero;
                                if (atr.tipoIndice==2)
                                {
                                entActual.lsDatos.Add(entero.ToString());

                                }

                                break;

                            case 'C':
                                string cadena = br.ReadString();
                                dgvRegistros.Rows[n].Cells[celda].Value = cadena.PadRight(atr.longDato - 1);
                                if (atr.tipoIndice == 2)
                                {
                                entActual.lsDatos.Add(cadena.ToString());   

                                }

                                break;
                        }
                        celda++;
                    }
                    dirSiguiente = br.ReadInt64();
                    dgvRegistros.Rows[n].Cells[celda].Value = dirSiguiente;
                    posicion = dirSiguiente;
                    //}
                }
                tamArchivoData = abre.Length;
                abre.Close();
            }
        }

        /*Método que ordena segun el atributo que tenga la clave de busqueda en su tipo de indice, en este caso solo lo hace 
         con valores de tipo int*/
        private void ordenaClavedeBusquedaInt(List<int> listaInt, int col)
        {
            List<int> ordenados = new List<int>();
            List<long> direcciones = new List<long>();

            while (ordenados.Count < listaInt.Count)//Generar lista con los valores ordenados
            {
                int menor = 99999999;
                foreach (int valor in listaInt)
                {
                    if (valor < menor && !(ordenados.Contains(valor)))
                    {
                        menor = valor;
                    }
                }
                ordenados.Add(menor);
            }

            foreach (int num in ordenados)//Aqui se ordenan las direcciones segun la lista de valores ordenados
            {
                for (int pos = 0; pos < ordenados.Count; pos++)
                {
                    if (num == (int)dgvRegistros.Rows[pos].Cells[col].Value)
                    {
                        long dir = (long)dgvRegistros.Rows[pos].Cells[0].Value;
                        direcciones.Add(dir);
                        break;
                    }
                }
            }

            int mas = 1;
            int posUltCol = dgvRegistros.ColumnCount - 1;
            foreach (int ord in ordenados)//Pone las direcciones debidamente ordenadas en el DGV
            {
                for (int i = 0; i < ordenados.Count; i++)
                {
                    if (ord == (int)dgvRegistros.Rows[i].Cells[col].Value)
                    {
                        if (ord == ordenados[ordenados.Count - 1])
                        {
                            long n = -1;
                            dgvRegistros.Rows[i].Cells[posUltCol].Value = n;
                        }
                        else
                        {
                            if (ord == ordenados[0])//Si es el primer dato ordenado, cabecera es igual a la direccion de ese dato
                            {
                                cabecera = (long)dgvRegistros.Rows[i].Cells[0].Value;
                            }
                            long pos = direcciones[mas];
                            dgvRegistros.Rows[i].Cells[posUltCol].Value = pos;
                            mas++;
                        }
                        break;
                    }
                }
            }
        }

        /*Método que ordena segun el atributo que tenga la clave de busqueda en su tipo de indice, en este caso solo lo hace 
         con valores de tipo string*/
        private void ordenaClavedeBusquedaString(List<string> listaStr, int col)
        {
            List<string> ordenados = new List<string>();
            List<long> direcciones = new List<long>();

            while (ordenados.Count < listaStr.Count)//Generar lista con los valores ordenados
            {
                string compara = "ZZZZZZZZZZZZ";
                foreach (string valor in listaStr)//Generar lista de cadenas que guarda las cadenas de manera ordenada
                {
                    if (compara.CompareTo(valor) == 1 && !(ordenados.Contains(valor)))
                    {
                        compara = valor;
                    }
                }
                ordenados.Add(compara);
            }

            foreach (string valor in ordenados)//Aqui se ordenan las direcciones segun la lista de valores ordenados
            {
                for (int pos = 0; pos < ordenados.Count; pos++)
                {
                    if (valor == (string)dgvRegistros.Rows[pos].Cells[col].Value)
                    {
                        long dir = (long)dgvRegistros.Rows[pos].Cells[0].Value;
                        direcciones.Add(dir);
                        break;
                    }
                }
            }

            int mas = 1;
            int posUltCol = dgvRegistros.ColumnCount - 1;
            foreach (string ord in ordenados)//Pone las direcciones debidamente ordenadas en el DGV
            {
                for (int i = 0; i < ordenados.Count; i++)
                {
                    if (ord == (string)dgvRegistros.Rows[i].Cells[col].Value)
                    {
                        if (ord == ordenados[ordenados.Count - 1])
                        {
                            long n = -1;
                            dgvRegistros.Rows[i].Cells[posUltCol].Value = n;
                        }
                        else
                        {
                            if (ord == ordenados[0])//Si es el primer dato ordenado, cabecera es igual a la direccion de ese dato
                            {
                                cabecera = (long)dgvRegistros.Rows[i].Cells[0].Value;
                            }
                            long pos = direcciones[mas];
                            dgvRegistros.Rows[i].Cells[posUltCol].Value = pos;
                            mas++;
                        }
                        break;
                    }
                }
            }
        }

        /*Evento de hacer clic en el boton de modificar*/
        private void bt_ModificarReg_Click(object sender, EventArgs e)
        {
            string strBin = "";
            int modificame = 0;
            int valorBin = 0;
            long dirRegistro = 0;
            int posAtr = 0;
            int nuevo = 0;

            if (!checa2())
            {
                if (dgvRegistros.Rows.Count > 1)
                {
                    //Indice ind = n
                    int celda = 1;
                    foreach (TextBox tb in textBoxes)//Verifica en cada textbox
                    {
                        if (tb.Text != "")//Si no hay nada en el textBox, no entra. Solamente cuando hay algo por modificar.
                        {
                            switch (atributos[celda - 1].tipoIndice)     //SWITCH PARA MODIFICAR INDICES
                            {
                                case 2:   //Primario
                                    if (atributos[celda - 1].tipoDato == 'E')//entero
                                    {
                                        int num = int.Parse(tb.Text);
                                        int valoraCambiar = (int)dgvRegistros.CurrentRow.Cells[celda].Value;
                                        long direccion = (long)dgvRegistros.CurrentRow.Cells[0].Value;

                                        Indice ind = new Indice(atributos[celda - 1], nombreEntidad);
                                        ind.eliminaIndice(valoraCambiar);
                                        ind.guardaDatoEnteroIndicePrimario(num, direccion);
                                        //ind.modificaIndicePrimario(valoraCambiar, direccion, num);
                                    }
                                    else//cadena
                                    {
                                        string cadena = tb.Text.PadRight(atributos[celda - 1].longDato - 1);
                                        string valoraCambiar = (string)dgvRegistros.CurrentRow.Cells[celda].Value;
                                        long direccion = (long)dgvRegistros.CurrentRow.Cells[0].Value;

                                        Indice ind = new Indice(atributos[celda - 1], nombreEntidad);
                                        ind.eliminaIndice(valoraCambiar);
                                        ind.guardaDatoStringIndicePrimario(cadena, direccion);
                                        //ind.modificaIndicePrimario(valoraCambiar, direccion, cadena);
                                    }
                                    break;

                                case 3:   //Secundario
                                    if (atributos[celda - 1].tipoDato == 'E')    //Entero
                                    {
                                        int num = int.Parse(tb.Text);
                                        int valoraCambiar = (int)dgvRegistros.CurrentRow.Cells[celda].Value;
                                        long direccion = (long)dgvRegistros.CurrentRow.Cells[0].Value;

                                        Indice ind = new Indice(atributos[celda - 1], nombreEntidad);
                                        ind.eliminaIndiceSec(valoraCambiar, direccion);
                                        ind.indiceSecundario(num, direccion);
                                    }
                                    else//String
                                    {
                                        string cadena = tb.Text.PadRight(atributos[celda - 1].longDato - 1);
                                        string valoraCambiar = (string)dgvRegistros.CurrentRow.Cells[celda].Value;
                                        long direccion = (long)dgvRegistros.CurrentRow.Cells[0].Value;

                                        Indice ind = new Indice(atributos[celda - 1], nombreEntidad);
                                        ind.eliminaIndiceSec(valoraCambiar, direccion);
                                        ind.indiceSecundario(cadena, direccion);
                                    }
                                    break;

                                case 6: //Hash Dinámico
                                    modificame = 1;
                                    valorBin = (int)dgvRegistros.CurrentRow.Cells[celda].Value;
                                    dirRegistro = (long)dgvRegistros.CurrentRow.Cells[0].Value;
                                    posAtr = celda - 1;
                                    nuevo = int.Parse(tb.Text);
                                    //guardaDGV();

                                    //indModifica.HashDinamico(strBin, atributos, entActual);
                                    break;
                            }

                            switch (atributos[celda - 1].tipoDato)  //Aqui solo actualiza el DGV despues de modificar
                            {
                                case 'E':
                                    int num = int.Parse(tb.Text);
                                    dgvRegistros.CurrentRow.Cells[celda].Value = num;
                                    break;

                                case 'C':
                                    string cadena = tb.Text.PadRight(atributos[celda - 1].longDato - 1);
                                    dgvRegistros.CurrentRow.Cells[celda].Value = cadena;
                                    break;
                            }
                            tb.Text = "";
                        }
                        celda++;
                    }

                    if (dgvRegistros.Rows.Count > 2)//Ordena los valores segun la clave de busqueda para posteriormente ser guardados
                                                    //en el archivo .dat
                    {
                        celda = 1;
                        foreach (Atributo atr in atributos)
                        {
                            switch (atr.tipoIndice)
                            {
                                case 1://Clave de busqueda
                                    switch (atr.tipoDato)
                                    {
                                        case 'E':
                                            List<int> listaInt = new List<int>();
                                            for (int num = 0; num < dgvRegistros.Rows.Count - 1; num++)//filas
                                            {
                                                listaInt.Add((int)dgvRegistros.Rows[num].Cells[celda].Value);
                                            }
                                            ordenaClavedeBusquedaInt(listaInt, celda);
                                            break;

                                        case 'C':
                                            List<string> listaStr = new List<string>();
                                            for (int num = 0; num < dgvRegistros.Rows.Count - 1; num++)//filas
                                            {
                                                listaStr.Add((string)dgvRegistros.Rows[num].Cells[celda].Value);
                                            }
                                            ordenaClavedeBusquedaString(listaStr, celda);
                                            break;
                                    }
                                    break;
                            }
                            celda++;
                        }
                    }
                    guardaDGV();

                    foreach (Atributo atr in atributos)
                    {
                        if (atr.tipoIndice == 6)
                        {
                            modificame = 1;
                            break;
                        }
                    }

                    if (modificame == 1)
                    {
                        Indice indModifica = new Indice(entActual, atributos[posAtr], nombreEntidad, dirRegistro);
                        indModifica.encuentraRegistroaEliminar(valorBin, atributos[posAtr].dirIndice, atributos);

                        strBin = indModifica.numerosBinarios(nuevo);
                        indModifica.HashDinamico(strBin, atributos, entActual);
                    }
                }
                else
                    MessageBox.Show("No hay registros existentes!!");
            }
            else
            {
                MessageBox.Show("dato repetido");
            }


        }

        /*Evento de hacer clic en el boton de eliminar*/
        private void bt_EliminaReg_Click(object sender, EventArgs e)
        {
            if (dgvRegistros.Rows.Count > 1)//Ordena los valores segun la clave de busqueda para posteriormente ser guardados.
            {
                //dgvRegistros.Rows.Remove(dgvRegistros.CurrentRow);
                int celda = 1;
                int res = 0;
                foreach (Atributo atr in atributos)
                {
                    Indice ind;
                    switch (atr.tipoIndice)
                    {

                        case 0: //Sin clave de búsqueda
                            if (res == 0)
                            {
                                int ren = dgvRegistros.CurrentRow.Index;
                                if (dgvRegistros.CurrentRow.Index != 0)//Cuando el renglon a eliminar no es el primero
                                {
                                    dgvRegistros.Rows[ren - 1].Cells[dgvRegistros.ColumnCount - 1].Value = dgvRegistros.Rows[ren].Cells[dgvRegistros.ColumnCount - 1].Value;
                                }
                                else//Cuando el renglon a eliminar es el primero, hay que modificar la cabecera de
                                    //la direccion de los registros.
                                    //cabecera = (long)dgvRegistros.Rows[ren + 1].Cells[0].Value;
                                    continue;
                                //dgvRegistros.Rows.Remove(dgvRegistros.CurrentRow);//Elimina el renglon del registro en el DGV
                            }
                            break;

                        case 2://Indice Primario
                            ind = new Indice(atr, nombreEntidad);
                            if (atr.tipoDato == 'E')
                            {
                                int valor = (int)dgvRegistros.CurrentRow.Cells[celda].Value;
                                ind.eliminaIndice(valor,entActual);
                            }
                            else
                            {
                                string valorStr = (string)dgvRegistros.CurrentRow.Cells[celda].Value;
                                // ind.eliminaIndice(valorStr);
                            }
                            //dgvRegistros.Rows.Remove(dgvRegistros.CurrentRow);//Elimina el renglon del registro en el DGV
                            break;

                        case 3://Indice Secundario
                            ind = new Indice(atr, nombreEntidad);
                            if (atr.tipoDato == 'E')
                            {
                                int valor = (int)dgvRegistros.CurrentRow.Cells[celda].Value;
                                long dir = (long)dgvRegistros.CurrentRow.Cells[0].Value;
                                ind.eliminaIndiceSec(valor, dir);
                            }
                            else
                            {
                                string valorStr = (string)dgvRegistros.CurrentRow.Cells[celda].Value;
                                long dir = (long)dgvRegistros.CurrentRow.Cells[0].Value;
                                ind.eliminaIndiceSec(valorStr, dir);
                            }
                            break;

                        case 6://Hash Dinamico
                            int valorBin = (int)dgvRegistros.CurrentRow.Cells[celda].Value;
                            long dirRegistro = (long)dgvRegistros.CurrentRow.Cells[0].Value;
                            ind = new Indice(entActual, atr, nombreEntidad, dirRegistro);
                            ind.encuentraRegistroaEliminar(valorBin, atr.dirIndice, atributos);
                            break;
                    }
                    celda++;
                }

                celda = 1;
                foreach (Atributo atr in atributos)
                {
                    if (atr.tipoIndice == 1)
                    {
                        dgvRegistros.Rows.Remove(dgvRegistros.CurrentRow);//Elimina el renglon del registro en el DGV
                        switch (atr.tipoDato)
                        {
                            case 'E':
                                List<int> listaInt = new List<int>();
                                for (int num = 0; num < dgvRegistros.Rows.Count - 1; num++)//filas
                                {
                                    listaInt.Add((int)dgvRegistros.Rows[num].Cells[celda].Value);
                                }
                                ordenaClavedeBusquedaInt(listaInt, celda);
                                break;
                            case 'C':
                                List<string> listaStr = new List<string>();
                                for (int num = 0; num < dgvRegistros.Rows.Count - 1; num++)//filas
                                {
                                    listaStr.Add((string)dgvRegistros.Rows[num].Cells[celda].Value);
                                }
                                ordenaClavedeBusquedaString(listaStr, celda);
                                break;
                        }
                        res = 1;
                        break;
                    }
                    celda++;
                }
                if (res == 0)
                    dgvRegistros.Rows.Remove(dgvRegistros.CurrentRow);
            }
            guardaDGV();
        }

        /*Método que regresa la cabecera del registro de datos*/
        public long regresaCabecera()
        {
            return cabecera;
        }
        public List<string> regresaDatos()
        {

            return entActual.lsDatos;
        }
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormBusqueda form = new FormBusqueda(atributos, dgvRegistros);
            form.Show();
        }
    }
}
