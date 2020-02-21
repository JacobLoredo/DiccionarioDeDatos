using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Diccionario_de_datos
{
    public class Indice
    {
        Entidad entidadActual;
        Atributo atr;
        string nombreEntidad;
        long cabecera = 0;
        long tam = 0;

        long tamBloque = 0;
        long dirRegistro = 0;

        /*Constructor de la clase Indice */
        public Indice(Atributo atributo, string nombreEnt)
        {
            nombreEntidad = nombreEnt;
            atr = atributo;
        }

        public Indice(Entidad ent, Atributo atributo, string nombreEnt, long dirReg)
        {
            entidadActual = ent; 
            nombreEntidad = nombreEnt;
            atr = atributo;
            dirRegistro = dirReg;
        }

        /*Método que regresa el tamaño del bloque de un indice con su bloque de desbordamiento*/
        private long regresaTamañodelBloque()
        {
            long total = 0;
            tam = 8 + atr.longDato;

            long registros = 1040 / tam;
            total = (registros * tam) + 8;
            return total;
        }

        /*Método que crea el archivo .idx y llena el bloque con datos que son iguales a -1*/
        public void escribeIndice()
        {
            BinaryWriter bw;
            FileStream guardar = new FileStream(nombreEntidad + ".idx", FileMode.OpenOrCreate, FileAccess.Write);
            bw = new BinaryWriter(guardar);

            tamBloque = regresaTamañodelBloque();
            long registros = 1040 / tam;

            if(atr.dirIndice == -1)
            {
                atr.dirIndice = guardar.Length;

                bw.Seek((int)atr.dirIndice, SeekOrigin.Begin);
                int i = 0;
                for (i = 0; i < registros; i++)
                {
                    switch (atr.tipoDato)
                    {
                        case 'E':
                            int val = -1;
                            bw.Write(val);
                            break;

                        case 'C':
                            string cadena = "-1".PadRight(atr.longDato - 1);
                            bw.Write(cadena);
                            break;
                    }
                    long sigDir = -1;
                    bw.Write(sigDir);
                }
                long desbordamiento = -1;
                bw.Write(desbordamiento);
            }
            guardar.Close();
            bw.Close();
        }

        /*Método que lee el archivo y genera una lista con la informacion ordenada*/
        public void guardaDatoEnteroIndicePrimario(int valor, long direccion)
        {
            int totRegistros = 86;
            int regActuales = 0;
            long direccionIndice = atr.dirIndice;
            List<int> valores = new List<int>();
            List<long> direcciones = new List<long>();

            FileStream abrir;
            abrir = File.Open(nombreEntidad + ".idx", FileMode.Open, FileAccess.Read);
            abrir.Seek(atr.dirIndice, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(abrir);
            int aux = 0;
            while(aux != -1)
            {
                valores.Add(reader.ReadInt32());
                direcciones.Add(reader.ReadInt64());
                aux = valores.Last();
            }
            valores.Remove(-1);
            direcciones.Remove(-1);
            abrir.Close();
            reader.Close();

            regActuales = valores.Count;

            if(regActuales == totRegistros)//Cuando se llena el bloque de un indice, aqui tendriamos que crear otro bloque
            {

            }

            if(!valores.Contains(valor)) //Si el valor a agregar no se encuentra en la lista
            {
                List<int> nuevoV = new List<int>();
                List<long> nuevoD = new List<long>();
                int res = 0;

                if (valores.Count == 0)//Esto solamente lo hace con el primer dato que entra al archivo
                {
                    nuevoV.Add(valor);
                    nuevoD.Add(direccion);
                }
                else
                {
                    for (int i = 0; i < valores.Count; i++)
                    {
                        if (valor < valores[i] && res == 0)
                        {
                            nuevoV.Add(valor);  //valor entrante
                            nuevoD.Add(direccion);
                            nuevoV.Add(valores[i]);
                            nuevoD.Add(direcciones[i]);
                            res = 1;
                        }
                        else
                        {
                            nuevoV.Add(valores[i]);
                            nuevoD.Add(direcciones[i]);
                        }
                    }
                    if (res == 0)//Cuando el nuevo dato es el ultimo en la lista
                    {
                        nuevoV.Add(valor);
                        nuevoD.Add(direccion);
                    }
                }
                valores.Clear();
                direcciones.Clear();
                escribeDatosidx(nuevoV, nuevoD);
            }
        }

        /*Método que lee el archivo y genera una lista con las cadenas de manera ordenada*/
        public void guardaDatoStringIndicePrimario(string valor, long direccion)
        {
            long direccionIndice = atr.dirIndice;
            List<string> valores = new List<string>();
            List<long> direcciones = new List<long>();

            FileStream abrir;
            abrir = File.Open(nombreEntidad + ".idx", FileMode.Open, FileAccess.Read);
            abrir.Seek(atr.dirIndice, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(abrir);
            string aux = "0";
            string menos = "-1";
            while (aux != menos.PadRight(atr.longDato - 1))
            {
                valores.Add(reader.ReadString());
                direcciones.Add(reader.ReadInt64());
                aux = valores.Last();
            }
            valores.Remove(menos.PadRight(atr.longDato - 1));
            direcciones.Remove(-1);
            reader.Close();
            abrir.Close();
            if (!valores.Contains(valor)) //Si el valor a agregar no se encuentra en la lista
            {
                List<string> nuevoV = new List<string>();
                List<long> nuevoD = new List<long>();
                int res = 0;

                if (valores.Count == 0)//Esto solamente lo hace con el primer dato que entra al archivo
                {
                    nuevoV.Add(valor);
                    nuevoD.Add(direccion);
                }
                else
                {
                    for (int i = 0; i < valores.Count; i++)
                    {
                        if (valor.CompareTo(valores[i]) < 0 && res == 0)
                        {
                            nuevoV.Add(valor);  //valor entrante
                            nuevoD.Add(direccion);
                            nuevoV.Add(valores[i]);
                            nuevoD.Add(direcciones[i]);
                            res = 1;
                        }
                        else
                        {
                            nuevoV.Add(valores[i]);
                            nuevoD.Add(direcciones[i]);
                        }
                    }
                    if (res == 0)//Cuando el nuevo dato es el ultimo en la lista
                    {
                        nuevoV.Add(valor);
                        nuevoD.Add(direccion);
                    }
                }
                valores.Clear();
                direcciones.Clear();
                escribeDatosidx(nuevoV, nuevoD);
            }

        }

        /*Método principal para agregar un dato entero de indice secundario al archivo.*/
        public void indiceSecundario(int valor, long direccion)
        {
            //int totRegistros = 180;
            int regActuales = 0;
            long direccionIndice = atr.dirIndice;
            List<int> valores = new List<int>();
            List<long> direcciones = new List<long>();

            FileStream abrir;
            abrir = File.Open(nombreEntidad + ".idx", FileMode.Open, FileAccess.Read);
            abrir.Seek(atr.dirIndice, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(abrir);
            int aux = 0;
            while (aux != -1)
            {
                valores.Add(reader.ReadInt32());
                direcciones.Add(reader.ReadInt64());
                aux = valores.Last();
            }
            valores.Remove(-1);
            direcciones.Remove(-1);
            abrir.Close();
            regActuales = valores.Count;

            if (!valores.Contains(valor))
            {
                long direccionCajon = creaCajonesIndiceSecundario();    //Crea los cajones
                List<int> nuevoV = new List<int>();
                List<long> nuevoD = new List<long>();
                int res = 0;

                if (valores.Count == 0)//Esto solamente lo hace con el primer dato que entra al archivo
                {
                    nuevoV.Add(valor);
                    nuevoD.Add(direccionCajon);
                    //nuevoD.Add(direccion);
                }
                else
                {
                    for (int i = 0; i < valores.Count; i++)
                    {
                        if (valor < valores[i] && res == 0)
                        {
                            nuevoV.Add(valor);  //valor entrante
                            nuevoD.Add(direccionCajon);
                            nuevoV.Add(valores[i]);
                            nuevoD.Add(direcciones[i]);
                            res = 1;
                        }
                        else
                        {
                            nuevoV.Add(valores[i]);
                            nuevoD.Add(direcciones[i]);
                        }
                    }
                    if (res == 0)//Cuando el nuevo dato es el ultimo en la lista
                    {
                        nuevoV.Add(valor);
                        nuevoD.Add(direccionCajon);
                    }
                }
                valores.Clear();
                direcciones.Clear();
                reader.Close();
                escribeDatosidx(nuevoV, nuevoD);
                guardaDireccionenCajon(direccionCajon, direccion, 0);//El 1° valor es la direccion del cajon, el 2° es la direccion del registro, el 3° es para guardar
            }
            else
            {
                for (int i = 0; i < valores.Count; i++)
                {
                    if (valor == valores[i])
                    {
                        long direccionCajon = direcciones[i];
                        guardaDireccionenCajon(direccionCajon, direccion, 0);//El 1° valor es la direccion del cajon, el 2° es la direccion del registro, el 3° es para guardar
                    }
                }
            }
        }

        /*Método principal para agregar una cadena de indice secundario al archivo*/
        public void indiceSecundario(string valor, long direccion)
        {
            int regActuales = 0;
            long direccionIndice = atr.dirIndice;
            List<string> valores = new List<string>();
            List<long> direcciones = new List<long>();

            FileStream abrir;
            abrir = File.Open(nombreEntidad + ".idx", FileMode.Open, FileAccess.Read);
            abrir.Seek(atr.dirIndice, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(abrir);
            string aux = "0";
            string menos = "-1";
            while(aux != menos.PadRight(atr.longDato - 1))
            {
                valores.Add(reader.ReadString());
                direcciones.Add(reader.ReadInt64());
                aux = valores.Last();
            }
            valores.Remove(menos.PadRight(atr.longDato - 1));
            direcciones.Remove(-1);
            abrir.Close();
            regActuales = valores.Count;

            if(!valores.Contains(valor))//Cuando no contiene el valor en la lista
            {
                long direccionCajon = creaCajonesIndiceSecundario();    //Crea los cajones
                List<string> nuevoV = new List<string>();
                List<long> nuevoD = new List<long>();
                int res = 0;

                if (valores.Count == 0)//Esto solamente lo hace con el primer dato que entra al archivo
                {
                    nuevoV.Add(valor);
                    nuevoD.Add(direccionCajon);
                    //nuevoD.Add(direccion);
                }
                else
                {
                    for (int i = 0; i < valores.Count; i++)
                    {
                        if (valor.CompareTo(valores[i]) < 0 && res == 0)
                        {
                            nuevoV.Add(valor);  //valor entrante
                            nuevoD.Add(direccionCajon);
                            nuevoV.Add(valores[i]);
                            nuevoD.Add(direcciones[i]);
                            res = 1;
                        }
                        else
                        {
                            nuevoV.Add(valores[i]);
                            nuevoD.Add(direcciones[i]);
                        }
                    }
                    if (res == 0)//Cuando el nuevo dato es el ultimo en la lista
                    {
                        nuevoV.Add(valor);
                        nuevoD.Add(direccionCajon);
                    }
                }
                valores.Clear();
                direcciones.Clear();
                reader.Close();
                escribeDatosidx(nuevoV, nuevoD);
                guardaDireccionenCajon(direccionCajon, direccion, 0);//El 1° valor es la direccion del cajon, el 2° es la direccion del registro, el 3° es para guardar
            }
            else
            {
                for (int i = 0; i < valores.Count; i++)
                {
                    if (valor == valores[i])
                    {
                        long direccionCajon = direcciones[i];
                        guardaDireccionenCajon(direccionCajon, direccion, 0);//El 1° valor es la direccion del cajon, el 2° es la direccion del registro, el 3° es para guardar
                    }
                }
            }
        }

        /*Método que guarda la direccion de un registro en el cajon del indice secundario*/
        //El primer parametro es la direccion del cajon, el segundo el valor a escribir en el archivo, el tercero es la opcion para guardar
        //o eliminar un valor, 0 = Guardar valor 1 = Eliminar valor 
        public void guardaDireccionenCajon(long dirBloque, long valor, int operacion)
        {
            List<long> dirValores = new List<long>();
            BinaryWriter bw;
            BinaryReader reader;
            FileStream abre = new FileStream(nombreEntidad + ".idx", FileMode.OpenOrCreate, FileAccess.Read);
            abre.Seek(dirBloque, SeekOrigin.Begin);
            //bw = new BinaryWriter(guardar);
            reader = new BinaryReader(abre);
            //reader.BaseStream.Position = dirBloque;
            long res = 0;
            while(res != -1)
            {
                res = reader.ReadInt64();
                dirValores.Add(res);
            }
            if (operacion == 0)   //Para guardar un dato en el cajon
            {
                dirValores.Remove(-1);
                dirValores.Add(valor);
            }
            else//Para eliminar un dato en el cajon.
                dirValores.Remove(valor);
            
            reader.Close();
            abre.Close();
            //bw.Close();
            //guardar.Close();

            FileStream guardar = new FileStream(nombreEntidad + ".idx", FileMode.OpenOrCreate, FileAccess.Write);
            bw = new BinaryWriter(guardar);
            bw.Seek((int)dirBloque, SeekOrigin.Begin);
            for (int i = 0; i < dirValores.Count; i++)
            {
                bw.Write(dirValores[i]);
            }
            bw.Close();
            guardar.Close();
        }

        /*Método que crea los cajones del indice secundario y los llena con valores de -1*/
        public long creaCajonesIndiceSecundario()
        {
            long dir = 0;
            BinaryWriter bw;
            FileStream guardar = new FileStream(nombreEntidad + ".idx", FileMode.OpenOrCreate, FileAccess.Write);
            bw = new BinaryWriter(guardar);
            dir = guardar.Length;
            long b = 1040 / 8;
            tamBloque = (8 * b) + 8;

            bw.Seek((int)dir, SeekOrigin.Begin);
            int i = 0;
            long menos = -1;
            for (i = 0; i < b; i++)
            {
                bw.Write(menos);
            }
            bw.Write(menos); //desbordamiento
            guardar.Close();
            return dir;
        }

        /*Método que escribe la informacion en el archivo idx cuando el valor es entero*/
        private void escribeDatosidx(List<int> val, List<long> dir)
        {
            BinaryWriter bw;
            FileStream guardar = new FileStream(nombreEntidad + ".idx", FileMode.Open, FileAccess.Write);
            bw = new BinaryWriter(guardar);
            bw.Seek((int)atr.dirIndice, SeekOrigin.Begin);
            for(int i = 0; i < val.Count; i++)
            {
                bw.Write(val[i]);
                bw.Write(dir[i]);
            }
            guardar.Close();
        }

        /*Método que escribe la informacion en el archivo idx cuando el valor es una cadena*/
        private void escribeDatosidx(List<string> val, List<long> dir)
        {
            BinaryWriter bw;
            FileStream guardar = new FileStream(nombreEntidad + ".idx", FileMode.Open, FileAccess.Write);
            bw = new BinaryWriter(guardar);
            bw.Seek((int)atr.dirIndice, SeekOrigin.Begin);
            for (int i = 0; i < val.Count; i++)
            {
                bw.Write(val[i]);
                bw.Write(dir[i]);
            }
            guardar.Close();
        }

        /*Método para eliminar un un indice en el archivo idx*/
        public void eliminaIndice(int valor)
        {
            long direccionIndice = atr.dirIndice;
            List<int> valores = new List<int>();
            List<long> direcciones = new List<long>();

            FileStream abrir;
            abrir = File.Open(nombreEntidad + ".idx", FileMode.Open, FileAccess.Read);
            abrir.Seek(atr.dirIndice, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(abrir);
            int aux = 0;
            while (aux != -1)
            {
                valores.Add(reader.ReadInt32());
                direcciones.Add(reader.ReadInt64());
                aux = valores.Last();
            }
            //valores.Remove(-1);
            //direcciones.Remove(-1);
            reader.Close();
            abrir.Close();

            for(int i = 0; i < valores.Count; i++)
            {
                if(valores[i] == valor)//Elimina el dato que queremos quitar
                {
                    valores.Remove(valores[i]);
                    direcciones.Remove(direcciones[i]);
                    break;
                }
            }
            escribeDatosidx(valores, direcciones);
            valores.Clear();
            direcciones.Clear();
        }

        /*Método para eliminar un indice  primario de tipo string*/
        public void eliminaIndice(string valor)
        {
            long direccionIndice = atr.dirIndice;
            List<string> valores = new List<string>();
            List<long> direcciones = new List<long>();

            FileStream abrir;
            abrir = File.Open(nombreEntidad + ".idx", FileMode.Open, FileAccess.Read);
            abrir.Seek(atr.dirIndice, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(abrir);
            string aux = "0";
            string menos = "-1";
            while (aux != menos.PadRight(atr.longDato - 1))
            {
                valores.Add(reader.ReadString());
                direcciones.Add(reader.ReadInt64());
                aux = valores.Last();
            }
            //valores.Remove(-1);
            //direcciones.Remove(-1);
            reader.Close();
            abrir.Close();

            for (int i = 0; i < valores.Count; i++)
            {
                if (valores[i] == valor)//Elimina el dato que queremos quitar
                {
                    valores.Remove(valores[i]);
                    direcciones.Remove(direcciones[i]);
                    break;
                }
            }
            escribeDatosidx(valores, direcciones);
            valores.Clear();
            direcciones.Clear();
        }

        /*Método para eliminar un valor de indice secundario en el archivo idx de tipo entero*/
        public void eliminaIndiceSec(int valor, long direccion)
        {
            long direccionIndice = atr.dirIndice;

            FileStream abrir;
            abrir = File.Open(nombreEntidad + ".idx", FileMode.Open, FileAccess.Read);
            abrir.Seek(atr.dirIndice, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(abrir);
            int aux = 0;
            long direccionCajon = 0;

            while (aux != 1)//Busca la direccion del cajon que contiene el dato a eliminar
            {
                if (valor != reader.ReadInt32())
                    reader.BaseStream.Position += 8;
                else
                {
                    aux = 1;
                    direccionCajon = reader.ReadInt64();
                    reader.BaseStream.Position = direccionCajon;
                }
            }
            reader.Close();
            abrir.Close();
            guardaDireccionenCajon(direccionCajon, direccion, 1);//El 1° valor es la direccion del cajon, el 2° es la direccion del registro, el 3° es para eliminar
        }

        /*Método para eliminar un valor de indice secundario en el archivo idx de tipo string*/
        public void eliminaIndiceSec(string valor, long direccion)
        {
            long direccionIndice = atr.dirIndice;

            FileStream abrir;
            abrir = File.Open(nombreEntidad + ".idx", FileMode.Open, FileAccess.Read);
            abrir.Seek(atr.dirIndice, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(abrir);
            int aux = 0;
            long direccionCajon = 0;

            while (aux != 1)//Busca la direccion del cajon que contiene el dato a eliminar
            {
                if (valor != reader.ReadString())
                    reader.BaseStream.Position += 8;
                else
                {
                    aux = 1;
                    direccionCajon = reader.ReadInt64();
                    reader.BaseStream.Position = direccionCajon;
                }
            }
            reader.Close();
            abrir.Close();
            guardaDireccionenCajon(direccionCajon, direccion, 1);//El 1° valor es la direccion del cajon, el 2° es la direccion del registro, el 3° es para eliminar
        }

        /*Método que crea el bloque en el archivo idx para guardar los apuntadores a los cajones*/
        public void creaIndiceHashDinamico()
        {
            BinaryWriter bw;
            FileStream guardar = new FileStream(nombreEntidad + ".idx", FileMode.OpenOrCreate, FileAccess.Write);
            bw = new BinaryWriter(guardar);

            //tamBloque = regresaTamañodelBloque();
            tam = 8;
            long registros = 1036 / tam;//1048 - debordamiento - indice = 1036

            atr.dirIndice = guardar.Length;
            bw.Seek((int)atr.dirIndice, SeekOrigin.Begin);
            int i = 0;
            int indice = -1;
            bw.Write(indice);//Escribe el indice antes de crear el espacio para los apuntadores
            for (i = 0; i < registros; i++)
            {
                long apuntador = -1;
                bw.Write(apuntador);
            }
            long desbordamiento = -1;
            bw.Write(desbordamiento);

            guardar.Close();
            bw.Close();
        }

        public long creaIndiceHashDinamico(FileStream file)
        {
            BinaryWriter bw;
            bw = new BinaryWriter(file);
            long tamInicial = file.Length;
            //tamBloque = regresaTamañodelBloque();
            tam = 8;
            long registros = 1036 / tam;//1048 - debordamiento - indice = 1036

            //atr.dirIndice = guardar.Length;
           
            bw.Seek((int)file.Length, SeekOrigin.Begin);
            int i = 0;
            int indice = -1;
            bw.Write(indice);//Escribe el indice antes de crear el espacio para los apuntadores
            for (i = 0; i < registros; i++)
            {
                long apuntador = -1;
                bw.Write(apuntador);
            }
            long desbordamiento = -1;
            bw.Write(desbordamiento);

            return tamInicial;
            //file.Close();
            //bw.Close();
        }


        /*Método que escribe un valor con Indice Hash Dinámico*/
        public void HashDinamico(string valorBinario, List<Atributo> lsAtr, Entidad ent)
        {
            int regActuales = 0;
            long direccionIndice = atr.dirIndice;
            //List<string> valores = new List<string>();
            List<long> direcciones = new List<long>();

            FileStream abrir;
            abrir = File.Open(nombreEntidad + ".idx", FileMode.Open, FileAccess.Read);
            abrir.Seek(atr.dirIndice, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(abrir);
            long aux = 0;

            int indiceTabla = reader.ReadInt32();

            while (aux != -1)
            {
                direcciones.Add(reader.ReadInt64());
                aux = direcciones.Last();
            }
            direcciones.Remove(-1);
            if (direcciones.Count > 128)
            {

            }
            reader.Close();
            abrir.Close();
            regActuales = direcciones.Count;
            //regActuales = 3;


            List<long> nuevoD = new List<long>();
            int insercionExitosa = 0;

            if (direcciones.Count == 0)//-------------------Cuando es el primer dato a insertar en el indice hash---------------
            {
                indiceTabla = 0;
                long direccionCajon = creaCajonHashDinamico(indiceTabla, lsAtr);    //Crea los cajones
                nuevoD.Add(direccionCajon);
                insercionExitosa = escribeCajonesHash(indiceTabla, direccionCajon, dirRegistro, lsAtr);
            }
            else//----------------------Cuando no es el primero y ya hay cajones creados.---------------------
            {
                if(indiceTabla == 0)
                {
                    long direccionCajon = direcciones[0];
                    insercionExitosa = escribeCajonesHash(indiceTabla, direccionCajon, dirRegistro, lsAtr);
                    if(insercionExitosa == 0)//Cuando ya no se pueden guardar mas registros en los cajones
                    {

                        indiceTabla++;
                        int indiceCajonHash = indiceTabla;
                        actualizaIndices(direccionCajon, indiceTabla);//Actualiza el indice del cajon lleno

                        direcciones.Add(creaCajonHashDinamico(indiceTabla, lsAtr));//Crea un nuevo cajon
                        
                        escribeHashDinamico(indiceTabla, direcciones);//Guarda la tabla hash
                        
                        List<string> lsBin = new List<string>();

                        double nBits = Math.Pow(2, indiceTabla);

                        for (int i = 0; i < nBits; i++)
                        {
                            lsBin.Add(numerosBinarios(i, indiceTabla));
                        }

                        reacomodaRegistros(direccionCajon, lsAtr, lsBin, direcciones, indiceTabla);

                        for (int i = 0; i < lsBin.Count; i++)
                        {
                            if (lsBin[i] == valorBinario.Substring(0, indiceTabla))
                            {
                                insercionExitosa = escribeCajonesHash(indiceTabla, direcciones[i], dirRegistro, lsAtr);
                                if(insercionExitosa == 0)
                                {
                                    string actual = lsBin[i];
                                    escribeHashDinamico(indiceTabla, direcciones);
                                    //List<long> nuevoD = new List<long>();
                                    nuevoD = mueveRegistrosInd(indiceTabla, direcciones[i], lsAtr, direcciones, actual, indiceCajonHash, lsBin);
                                    indiceTabla++;

                                    List<string> nuevasPosiciones = new List<string>();
                                    double numBits = Math.Pow(2, indiceTabla);

                                    for (int j = 0; j < numBits; j++)
                                    {
                                        nuevasPosiciones.Add(numerosBinarios(j, indiceTabla));
                                    }

                                    for (int j = 0; j < nuevoD.Count; j++)
                                    {
                                        if(nuevasPosiciones[j] == valorBinario.Substring(0, indiceTabla))
                                            escribeCajonesHash(indiceTabla, nuevoD[j], dirRegistro, lsAtr);
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
                else//-----------------Cuando el indice no es 0-----------------------
                {
                    List<string> lsBin = new List<string>();

                    double nBits = Math.Pow(2, indiceTabla);

                    for (int i = 0; i < nBits; i++)//GENERA LOS NUMEROS BINARIOS CON LOS QUE SE VA A COMPARAR
                    {
                        lsBin.Add(numerosBinarios(i, indiceTabla));
                    }

                    for (int i = 0; i < lsBin.Count; i++)
                    {
                        if (lsBin[i] == valorBinario.Substring(0, indiceTabla))
                        {
                            insercionExitosa = escribeCajonesHash(indiceTabla, direcciones[i], dirRegistro, lsAtr);

                            if (insercionExitosa == 0)//Si no se pudo escribir, hay que duplicar la tabla o agregar más cajones
                            {
                                int indiceCajonHash = regresaIndiceCajonHash(direcciones[i]);
                                //string actual = lsBin[i];
                                escribeHashDinamico(indiceTabla, direcciones);
                                if (indiceCajonHash == indiceTabla)//-----SI SON IGUALES HAY QUE DUPLICAR LA TABLA---------
                                {
                                    string numActual = lsBin[i];
                                    indiceTabla++;
                                    indiceCajonHash++;

                                    actualizaIndices(direcciones[i], indiceTabla);

                                    List<string> lisBin = new List<string>();

                                    double numBits = Math.Pow(2, indiceTabla);
                                    int exito = 0;

                                    for (int j = 0; j < numBits; j++)//Nueva lista con las posiciones en binario
                                    {
                                        lisBin.Add(numerosBinarios(j, indiceTabla));
                                        if (lisBin[j].Substring(0, indiceCajonHash - 1) == numActual && exito == 0)//Aqui con el cajon a reemplazar
                                        {
                                            if (j <= direcciones.Count - 1)  //ESTE FUNCIONA PARA AMBOS
                                            //if (j <= direcciones.Count) //ESTE FUNCIONA PARA LOS 0
                                            {
                                                nuevoD.Add(direcciones[j]);
                                                nuevoD.Add(creaCajonHashDinamico(indiceTabla, lsAtr));
                                            }
                                            else
                                            {
                                                int posi = lsBin.IndexOf(numActual);
                                                nuevoD.Add(direcciones[posi]);
                                                nuevoD.Add(creaCajonHashDinamico(indiceTabla, lsAtr));
                                            }
                                            exito = 1;
                                        }
                                        else
                                        {
                                            for (int cajon = 0; cajon < lsBin.Count; cajon++)
                                            {
                                                if (lisBin[j].Substring(0, indiceCajonHash - 1) == lsBin[cajon] && exito == 0)
                                                {
                                                    //nuevasDirecciones[j] = direcciones[cajon];
                                                    nuevoD.Add(direcciones[cajon]);
                                                    break;
                                                }
                                            }
                                            exito = 0;
                                        }
                                    }

                                    reacomodaRegistros(direcciones[i], lsAtr, lisBin, nuevoD, indiceTabla);

                                    for (int j = 0; j < lisBin.Count; j++)
                                    {
                                        if (lisBin[j] == valorBinario.Substring(0, indiceTabla) && insercionExitosa != 1)
                                        {
                                            insercionExitosa = escribeCajonesHash(indiceTabla, nuevoD[j], dirRegistro, lsAtr);
                                            if (insercionExitosa == 0)
                                            {
                                                string actual = lisBin[j];
                                                escribeHashDinamico(indiceTabla, direcciones);
                                                //List<long> nuevoD = new List<long>();
                                                nuevoD = mueveRegistrosInd(indiceTabla, nuevoD[j], lsAtr, nuevoD, actual, indiceCajonHash, lisBin);
                                                indiceTabla++;

                                                List<string> nuevasPosiciones = new List<string>();
                                                double numeBits = Math.Pow(2, indiceTabla);

                                                for (int k = 0; k < numeBits; k++)
                                                {
                                                    nuevasPosiciones.Add(numerosBinarios(k, indiceTabla));
                                                }

                                                for (int k = 0; k < nuevoD.Count; k++)
                                                {
                                                    if (nuevasPosiciones[k] == valorBinario.Substring(0, indiceTabla))
                                                        escribeCajonesHash(indiceTabla, nuevoD[k], dirRegistro, lsAtr);
                                                }
                                            }
                                            break;
                                        }
                                    }
                                }
                                else//--------------------SI NO SOLO HAY QUE CREAR UN CAJON MÁS-------------------------
                                {
                                    string numActual = lsBin[i];
                                    indiceCajonHash++;
                                    actualizaIndices(direcciones[i], indiceCajonHash);

                                    double numBits = Math.Pow(2, indiceTabla);
                                    int exito = 0;

                                    //int menos = indiceTabla - indiceCajonHash;
                                    long dirNueva = 0;
                                    for (int j = 0; j < numBits; j++)//Nueva lista con las posiciones en binario
                                    {
                                        //lsBin.Add(numerosBinarios(j, indiceTabla));
                                        //if (lsBin[j].Substring(0, indiceCajonHash /*- menos*/) == numActual && exito == 0)//Aqui con el cajon a reemplazar
                                        if (lsBin[j].Substring(0, indiceTabla /*- menos*/) == numActual && exito == 0)
                                        {
                                            if (j <= direcciones.Count - 1)  //ESTE FUNCIONA PARA AMBOS
                                            //if (j <= direcciones.Count) //ESTE FUNCIONA PARA LOS 0
                                            {
                                                //nuevoD.Add(direcciones[j]);
                                                nuevoD.Add(direcciones[j]);
                                                dirNueva = creaCajonHashDinamico(indiceCajonHash, lsAtr);
                                                nuevoD.Add(dirNueva);
                                            }
                                            else
                                            {
                                                nuevoD.Add(direcciones[direcciones.Count - 1]);
                                                //nuevoD.Add(creaCajonHashDinamico(indiceTabla, lsAtr));
                                                nuevoD.Add(creaCajonHashDinamico(indiceCajonHash, lsAtr));
                                            }
                                            exito = 1;
                                        }
                                        else
                                        {
                                            for (int cajon = 0; cajon < lsBin.Count; cajon++)
                                            {
                                                if (lsBin[j].Substring(0, indiceTabla /*- menos*/) == lsBin[cajon] && exito == 0)
                                                //if (lsBin[j].Substring(0, indiceCajonHash /*- menos*/) == lsBin[cajon] && exito == 0)
                                                {
                                                    //nuevasDirecciones[j] = direcciones[cajon];
                                                    nuevoD.Add(direcciones[cajon]);
                                                    break;
                                                }
                                            }
                                            exito = 0;

                                        }
                                    }

                                    for (int j = 0; j < nuevoD.Count; j++)
                                    {
                                        if (lsBin[j].Substring(0, indiceCajonHash) == numActual.Substring(0, indiceCajonHash))
                                        {
                                            nuevoD[j] = dirNueva;
                                        }
                                    }

                                    reacomodaRegistros(direcciones[i], lsAtr, lsBin, nuevoD, indiceTabla);

                                    for (int j = 0; j < lsBin.Count; j++)
                                    {
                                        if (lsBin[j] == valorBinario.Substring(0, indiceTabla) && insercionExitosa != 1)
                                        {
                                            insercionExitosa = escribeCajonesHash(indiceTabla, nuevoD[j], dirRegistro, lsAtr);
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
            direcciones.Clear();
            
            escribeHashDinamico(indiceTabla, nuevoD);
        }

        private List<long> mueveRegistrosInd(int indiceTabla, long direccionCajon, List<Atributo> lsAtr, List<long> direcciones, string numActual, int indCajonHash, List<string> posBin)
        {
            if(indiceTabla == indCajonHash)
            {
                indiceTabla++;
            }
            indCajonHash++;
            actualizaIndices(direccionCajon, indCajonHash);//Actualiza el indice del cajon lleno

            List<string> lsBin = new List<string>();

            double nBits = Math.Pow(2, indiceTabla);

            for (int i = 0; i < nBits; i++)
            {
                lsBin.Add(numerosBinarios(i, indiceTabla));
            }
            int exito = 0;
            long dirNueva = 0;

            List<long> nuevoD = new List<long>();


            for(int j = 0; j < nBits; j++)
            {
                if(lsBin[j].Substring(0, indCajonHash -1 ) == numActual && exito == 0)
                {
                    int pos = posBin.IndexOf(numActual);
                    nuevoD.Add(direcciones[pos]);
                    //nuevoD.Add(creaCajonHashDinamico(indiceTabla, lsAtr));
                    nuevoD.Add(creaCajonHashDinamico(indCajonHash, lsAtr));
                    exito = 1;
                }
                else
                {
                    for (int cajon = 0; cajon < direcciones.Count; cajon++)
                    {
                        if (lsBin[j].Substring(0, indCajonHash - 1) == posBin[cajon] && exito == 0)
                        //if (lsBin[j].Substring(0, indiceCajonHash) == lsBin[cajon] && exito == 0)
                        {
                            //nuevasDirecciones[j] = direcciones[cajon];
                            nuevoD.Add(direcciones[cajon]);
                            break;
                        }
                    }
                    exito = 0;
                }
            }

            reacomodaRegistros(direccionCajon, lsAtr, lsBin, nuevoD, indiceTabla);
            escribeHashDinamico(indiceTabla, nuevoD);//Guarda la tabla hash

            return nuevoD;
        }

        /*Método que actualiza el indice de una cajon que se ha llenado*/
        private void actualizaIndices(long dirCajon, int ind)
        {
            BinaryWriter bw;
            FileStream guardar = new FileStream(nombreEntidad + ".idx", FileMode.OpenOrCreate, FileAccess.Write);
            bw = new BinaryWriter(guardar);
            bw.Seek((int)dirCajon, SeekOrigin.Begin);
            bw.Write(ind);
            bw.Close();
            guardar.Close();
        }

        
        private void actualizaIndices(FileStream guardar, long dirCajon, int ind)
        {
            BinaryWriter bw;
            bw = new BinaryWriter(guardar);
            bw.Seek((int)dirCajon, SeekOrigin.Begin);
            bw.Write(ind);
        }

        /*Método que crea un cajon para el indice hash dinámico*/
        private long creaCajonHashDinamico(int ind, List<Atributo> lsAtr)
        {
            BinaryWriter bw;
            FileStream guardar = new FileStream(nombreEntidad + ".idx", FileMode.OpenOrCreate, FileAccess.Write);
            bw = new BinaryWriter(guardar);

            //tamBloque = regresaTamañodelBloque();
            int longAtributos = regresaLongituddeAtributos(lsAtr);

            long registros = 1036 / longAtributos;//1048 - desbordamiento - indice = 1036

            long direccionCajon = guardar.Length;
            bw.Seek((int)direccionCajon, SeekOrigin.Begin);
            int i;
            int indiceCajon = ind;
            bw.Write(indiceCajon);//Indice entero
            for(i = 0; i < registros; i++)
            {
                foreach(Atributo atr in lsAtr)//Registro de datos
                {
                    switch(atr.tipoDato)
                    {
                        case 'E':
                            int menos = -1;
                            bw.Write(menos);
                            break;

                        case 'C':
                            string menosC = "-1".PadRight(atr.longDato - 1);
                            bw.Write(menosC);
                            break;
                    }
                }
            }
            long desborda = -1;//Bloque de desbordamiento
            bw.Write(desborda);
            guardar.Close();
            bw.Close();
            return direccionCajon;
        }

        /*Método que guarda los apuntadores de los cajones de hash dinamico en la caja principla del indice*/
        private void escribeHashDinamico(int indice, List<long> direcciones)
        {
            BinaryWriter bw;
            FileStream guardar = new FileStream(nombreEntidad + ".idx", FileMode.Open, FileAccess.ReadWrite);
            bw = new BinaryWriter(guardar);
            bw.Seek((int)atr.dirIndice, SeekOrigin.Begin);
            bw.Write(indice);
            for (int i = 0; i < direcciones.Count; i++)
            {
                bw.Write(direcciones[i]);
                if(i == 128)
                {
                    BinaryReader reader;
                    reader = new BinaryReader(guardar);
                    long checa = guardar.Position;
                    reader.BaseStream.Position = checa;
                    if(reader.ReadInt64() == -1)
                    {
                        bw.BaseStream.Position = checa;
                        long posSig = creaIndiceHashDinamico(guardar);
                        bw.BaseStream.Position = checa;
                        bw.Write(posSig);
                        bw.BaseStream.Position = posSig + 4;
                        //bw.Seek((int)posSig + 4, SeekOrigin.Begin);
                    }
                    //bw.Seek((int)posSig + 4, SeekOrigin.Begin);
                    //guardar.Position = 
                    //reader.Close();
                }
            }
            //bw.Write(desborda);
            guardar.Close();
            
        }

        /*Método que escribe los registros en el archivo idx*/
        private int escribeCajonesHash(int indice, long direccionCajon, long direccionRegistrodeDatos, List<Atributo> lsAtr)
        {
            int insertado = 0;

            BinaryReader reader;
            FileStream abre = new FileStream(nombreEntidad + ".idx", FileMode.OpenOrCreate, FileAccess.Read);
            abre.Seek(direccionCajon + 4, SeekOrigin.Begin);//+4 porque queremos tener acceso en donde empiezan los registros
            reader = new BinaryReader(abre);
            int res = 0;
            string resStr = "0";
            string menos = "-1";
            int registros = 0;
            int avanza = regresaLongituddeAtributos(lsAtr);
            //int regTotal = 1036 / avanza;

            int regTotal = 3;

            if (lsAtr[0].tipoDato == 'E')//El primer atributo en la lista de atributos.
            {
                res = 0;
                while (res != -1)
                {
                    res = reader.ReadInt32();
                    if (res != -1)
                    {
                        registros++;
                        reader.BaseStream.Position = reader.BaseStream.Position + (avanza - 4);
                    }
                }
                if (registros != regTotal)//Si ya no cabe un registro en el cajon
                {
                    long posicionaInsertar = reader.BaseStream.Position - lsAtr[0].longDato;//Posicion a insertar en el archivo idx.
                    abre.Close();
                    reader.Close();

                    FileStream guarda = new FileStream(nombreEntidad + ".idx", FileMode.OpenOrCreate, FileAccess.Write);
                    BinaryWriter bw;
                    bw = new BinaryWriter(guarda);
                    bw.Seek((int)posicionaInsertar, SeekOrigin.Begin);//posicion a insertar en el idx

                    FileStream abreReg = new FileStream(nombreEntidad + ".dat", FileMode.OpenOrCreate, FileAccess.Read);
                    abreReg.Seek(direccionRegistrodeDatos + 8, SeekOrigin.Begin);
                    reader = new BinaryReader(abreReg);

                    foreach (Atributo atr in lsAtr)
                    {
                        switch (atr.tipoDato)
                        {
                            case 'E':
                                int valor = reader.ReadInt32();
                                bw.Write(valor);
                                break;

                            case 'C':
                                string valorStr = reader.ReadString();
                                bw.Write(valorStr);
                                break;
                        }
                    }
                    bw.Close();
                    guarda.Close();
                    abreReg.Close();
                    reader.Close();
                    insertado = 1;
                }
                else
                    insertado = 0;
            }

            else
            {
                resStr = "0";
                menos = "-1".PadRight(lsAtr[0].longDato - 1);
                while (resStr != menos && registros != regTotal)
                {
                    resStr = reader.ReadString();
                    if (resStr != menos)
                    {
                        registros++;
                        reader.BaseStream.Position = reader.BaseStream.Position + (avanza - (lsAtr[0].longDato));
                    }
                }

                if (registros != regTotal)//Si todavia cabe un registro en el cajon
                {
                    long posicionaInsertar = reader.BaseStream.Position - lsAtr[0].longDato;//Posicion a insertar en el archivo idx.
                    abre.Close();
                    reader.Close();

                    //escribeRegistrosenidx(posicionaInsertar, lsAtr);
                    FileStream guarda = new FileStream(nombreEntidad + ".idx", FileMode.OpenOrCreate, FileAccess.Write);
                    BinaryWriter bw;
                    bw = new BinaryWriter(guarda);
                    bw.Seek((int)posicionaInsertar, SeekOrigin.Begin);//posicion a insertar en el idx

                    FileStream abreReg = new FileStream(nombreEntidad + ".dat", FileMode.OpenOrCreate, FileAccess.Read);
                    abreReg.Seek(direccionRegistrodeDatos + 8, SeekOrigin.Begin);
                    reader = new BinaryReader(abreReg);

                    foreach (Atributo atr in lsAtr)
                    {
                        switch (atr.tipoDato)
                        {
                            case 'E':
                                int valor = reader.ReadInt32();
                                bw.Write(valor);
                                break;

                            case 'C':
                                string valorStr = reader.ReadString();
                                bw.Write(valorStr);
                                break;
                        }
                    }
                    bw.Close();
                    guarda.Close();
                    abreReg.Close();
                    reader.Close();
                    insertado = 1;
                }
                else
                    insertado = 0;
            }
            abre.Close();
            reader.Close();
            return insertado;
        }

        /*Método que escribe los registros en el archivo idx pasandole el archivo a editar como parametro*/
        private int escribeCajonesHash(FileStream archivoidx, long direccionCajon, long direccionRegistrodeDatos, List<Atributo> lsAtr)
        {
            int insertado = 0;

            BinaryReader reader;
            //FileStream abre = new FileStream(nombreEntidad + ".idx", FileMode.OpenOrCreate, FileAccess.Read);
            archivoidx.Seek(direccionCajon + 4, SeekOrigin.Begin);//+4 porque queremos tener acceso en donde empiezan los registros
            reader = new BinaryReader(archivoidx);
            int res = 0;
            string resStr = "0";
            string menos = "-1";
            int registros = 0;

            int avanza = regresaLongituddeAtributos(lsAtr);
            //int regTotal = 1036 / avanza;
            int regTotal = 3;

            if (lsAtr[0].tipoDato == 'E')//El primer atributo en la lista de atributos.
            {
                res = 0;
                while (res != -1)
                {
                    res = reader.ReadInt32();
                    if (res != -1)
                    {
                        registros++;
                        reader.BaseStream.Position = reader.BaseStream.Position + (avanza - 4);
                    }
                }

                if (registros != regTotal)//Si ya no cabe un registro en el cajon
                {
                    long posicionaInsertar = reader.BaseStream.Position - lsAtr[0].longDato;//Posicion a insertar en el archivo idx.

                    BinaryWriter bw;
                    bw = new BinaryWriter(archivoidx);
                    bw.Seek((int)posicionaInsertar, SeekOrigin.Begin);//posicion a insertar en el idx

                    FileStream abreReg = new FileStream(nombreEntidad + ".dat", FileMode.OpenOrCreate, FileAccess.Read);
                    abreReg.Seek(direccionRegistrodeDatos, SeekOrigin.Begin);
                    BinaryReader lee = new BinaryReader(abreReg);

                    foreach (Atributo atr in lsAtr)
                    {
                        switch (atr.tipoDato)
                        {
                            case 'E':
                                int valor = lee.ReadInt32();
                                bw.Write(valor);
                                break;

                            case 'C':
                                string valorStr = lee.ReadString();
                                bw.Write(valorStr);
                                break;
                        }
                    }
                    abreReg.Close();
                    lee.Close();
                    insertado = 1;
                }
            }
            else
            {
                resStr = "0";
                menos = "-1".PadRight(lsAtr[0].longDato - 1);
                while (resStr != menos && registros != regTotal)
                {
                    resStr = reader.ReadString();
                    if (resStr != menos)
                    {
                        registros++;
                        reader.BaseStream.Position = reader.BaseStream.Position + (avanza - (lsAtr[0].longDato));
                    }
                }

                if (registros != regTotal)//Si todavia cabe un registro en el cajon
                {
                    long posicionaInsertar = reader.BaseStream.Position - lsAtr[0].longDato;//Posicion a insertar en el archivo idx.

                    BinaryWriter bw;
                    bw = new BinaryWriter(archivoidx);
                    bw.Seek((int)posicionaInsertar, SeekOrigin.Begin);//posicion a insertar en el idx

                    FileStream abreReg = new FileStream(nombreEntidad + ".dat", FileMode.OpenOrCreate, FileAccess.Read);
                    abreReg.Seek(direccionRegistrodeDatos, SeekOrigin.Begin);
                    BinaryReader lee = new BinaryReader(abreReg);

                    foreach (Atributo atr in lsAtr)
                    {
                        switch (atr.tipoDato)
                        {
                            case 'E':
                                int valor = lee.ReadInt32();
                                bw.Write(valor);
                                break;

                            case 'C':
                                string valorStr = lee.ReadString();
                                bw.Write(valorStr);
                                break;
                        }
                    }
                    abreReg.Close();
                    lee.Close();
                    insertado = 1;
                }
                else
                    insertado = 0;
            }
            return insertado;
        }

        /*Método que regresa un valor con la longitud de los atributos*/
        private int regresaLongituddeAtributos(List<Atributo> lsAtr)
        {
            int longAtributos = 0;
            foreach(Atributo atr in lsAtr)
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

        /*Método que regresa una cadena con el valor binario de una clave*/
        public string numerosBinarios(int valor)
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

        /*Método que organiza el reacomodo de los registrsos al duplicarse la tabla*/
        private int reacomodaRegistros(long dirCajonLleno, List<Atributo> atributos, List<string> posicionBinaria, List<long>dirCajones, int indTabla)
        {
            int exito = 0;
            BinaryReader leer;
            FileStream abre = new FileStream(nombreEntidad + ".idx", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            abre.Position = dirCajonLleno;
            leer = new BinaryReader(abre);

            //int reg = 1036 / regresaLongituddeAtributos(atributos);
            int contReg = 0;
            int reg = 3;
            int indiceCajon = leer.ReadInt32();

            int posTabla = dirCajones.IndexOf(dirCajonLleno);
            //int post = dirCajones.IndexOf();
            long dirActualidx = 0;

            while(contReg != reg)
            {
                dirActualidx = leer.BaseStream.Position;
                foreach (Atributo atr in atributos)
                {
                    switch (atr.tipoDato)
                    {
                        case 'E':
                            int valor;
                            valor = leer.ReadInt32();
                            if (atr.tipoIndice == 6)
                            {
                                string bin = numerosBinarios(valor);
                                string cadenaRecortada = bin.Substring(0, indTabla);//obtiene los primeros n valores de la cadena
                                if (cadenaRecortada != posicionBinaria[posTabla])//Si la clave binaria del registro no es igual
                                                                                 //al valor en binario de la tabla, entonces hay que moverla de lugar
                                {
                                    long act = leer.BaseStream.Position;
                                    //ELIMINARLO DE ESE CAJON Y DESPUES INSERTARLO EN EL NUEVO CAJON
                                    eliminaRegistro(abre, dirActualidx, atributos);
                                    for(int i = 0; i < posicionBinaria.Count; i++)
                                    {
                                        if(cadenaRecortada == posicionBinaria[i])
                                        {
                                            Registro registro = new Registro(atributos, nombreEntidad);
                                            long dirDatos = registro.regresaDirecciondat(valor, entidadActual.dirDatos);
                                            escribeCajonesHash(abre, dirCajones[i], dirDatos, atributos);
                                            break;
                                        }
                                    }
                                    leer.BaseStream.Position = act;
                                }
                            }
                            break;

                        case 'C':
                            string str = leer.ReadString();
                            break;
                    }
                }
                contReg++;
            }
            leer.Close();
            abre.Close();
            return exito;
        }

        /*Método que llena de -1's una direccion especifica de un registro en el indice idx*/
        public void eliminaRegistro(FileStream file, long posicionElimina, List<Atributo> lsAtr)
        {
            //FileStream guarda = new FileStream(nombreEntidad + ".idx", FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter bw;
            bw = new BinaryWriter(file);
            bw.Seek((int)posicionElimina, SeekOrigin.Begin);

            int menos = -1;
            foreach (Atributo atr in lsAtr)
            {
                switch(atr.tipoDato)
                {
                    case 'E':
                        bw.Write(menos);
                        break;

                    case 'C':
                        string menosStr = "-1".PadRight(atr.longDato - 1);
                        bw.Write(menosStr);
                        break;
                }
            }
            //bw.Close();
        }

        /*Método que encuentra el registro a eliminar en el archivo idx*/
        public void encuentraRegistroaEliminar(int valorElimina, long dirTablaHash, List<Atributo> atributos)
        {
            BinaryReader leer;
            FileStream abre = new FileStream(nombreEntidad + ".idx", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            abre.Position = dirTablaHash;
            leer = new BinaryReader(abre);

            int indiceTabla = leer.ReadInt32();
            List<string> lsBin = new List<string>();
            List<long> direcciones = new List<long>();

            double nBits = Math.Pow(2, indiceTabla);

            for (int i = 0; i < nBits; i++)//GENERA LOS NUMEROS BINARIOS CON LOS QUE SE VA A COMPARAR
            {
                lsBin.Add(numerosBinarios(i, indiceTabla));
            }

            string strElimina = numerosBinarios(valorElimina);

            for (int i = 0; i < lsBin.Count; i++)
            {
                direcciones.Add(leer.ReadInt64());

                if (lsBin[i] == strElimina.Substring(0, indiceTabla))
                {
                    long dirCajonHash = direcciones[i];
                    abre.Position = dirCajonHash;
                    break;
                }
            }

            int reg = 1036 / regresaLongituddeAtributos(atributos);
            int contReg = 0;

            int indiceCajon = leer.ReadInt32();
            
            long dirActualidx = 0;
            int elimina = 0;
            long posicion = 0;

            while (contReg != reg && elimina == 0)
            {
                dirActualidx = leer.BaseStream.Position;
                foreach (Atributo atr in atributos)
                {
                    switch (atr.tipoDato)
                    {
                        case 'E':
                            int valor;
                            valor = leer.ReadInt32();
                            if (atr.tipoIndice == 6 && valorElimina == valor)
                            {
                                posicion = leer.BaseStream.Position;
                                eliminaRegistro(abre, dirActualidx, atributos);
                                elimina = 1;
                                leer.BaseStream.Position = posicion;
                            }
                            break;

                        case 'C':
                            string str = leer.ReadString();
                            break;
                    }
                }
                contReg++;
            }
            leer.Close();
            abre.Close();
        }


        public int regresaIndiceCajonHash(long dirCajon)
        {
            int ind = 0;
            FileStream abrir;
            abrir = File.Open(nombreEntidad + ".idx", FileMode.Open, FileAccess.Read);
            abrir.Seek(dirCajon, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(abrir);

            ind = reader.ReadInt32();

            abrir.Close();
            reader.Close();
            return ind;
        }
    }
}
