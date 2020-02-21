using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_de_datos
{
    public class Entidad
    {
        public List<Atributo> lsAtributo = new List<Atributo>();
        public string nombre;
        public long dirEnt;
        public long dirAtr;
        public long dirDatos;
        public long dirSigEnt;

        //public List<ValueType> values = new List<ValueType>();

        public Entidad()
        {

        }

        public Entidad(string nom, long dE, long dA, long dD, long dSE)
        {
            nombre = nom;
            dirEnt = dE;
            dirAtr = dA;
            dirDatos = dD;
            dirSigEnt = dSE;
        }

        public long DireccionAtributo
        {
            get
            {
                return dirAtr;
            }
            set
            {
                dirAtr = value;
            }
        }

        //Método para agregar un atributo a una  entidad */
        public void agregaAtributo(Atributo nuevo)
        {
            lsAtributo.Add(nuevo);
        }
    }
}
