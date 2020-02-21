using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Diccionario_de_datos
{
    class Registro
    {
        List<Atributo> atributos = new List<Atributo>();
        string nombreEntidad = "";

        public Registro(List<Atributo> lsAtr, string nomEntidad)
        {
            atributos = lsAtr;
            nombreEntidad = nomEntidad;
        }

        public long regresaDirecciondat(int valor, long dirRegDatos)
        {
            long dir = 0;
            BinaryReader reader;
            FileStream abre = new FileStream(nombreEntidad + ".dat", FileMode.OpenOrCreate, FileAccess.Read);
            reader = new BinaryReader(abre);
            int exito = 0;
            long dirSiguiente = dirRegDatos;
            //long posicion = 0;
            while (dirSiguiente != -1 && exito == 0)
            {
                abre.Seek(dirSiguiente, SeekOrigin.Begin);

                dir = reader.ReadInt64();
                foreach (Atributo atr in atributos)
                {
                    switch (atr.tipoDato)
                    {
                        case 'E':
                            int entero = reader.ReadInt32();
                            if (valor == entero)
                                if (atr.tipoIndice == 6)
                                    exito = 1;
                            break;

                        case 'C':
                            string cadena = reader.ReadString();
                            break;
                    }
                }
                dirSiguiente = reader.ReadInt64();
                
            }
            abre.Close();
            reader.Close();

            return dir + 8;
        }
    }
}
