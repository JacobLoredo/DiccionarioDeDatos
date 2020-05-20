using System.Collections.Generic;

namespace Diccionario_de_datos
{
    public class Atributo
    {
        public string nomAtributo;
        public char tipoDato;
        public int longDato;
        public long dirAtributo;
        public int tipoIndice;
        public long dirIndice;
        public long dirSigAtributo;

        public List<int> listaInt = new List<int>();
        public List<string> listaChar = new List<string>();

        public Atributo(string nom, char tipo, int longitud, long dirA, int tInd, long dirInd, long dirSigA)
        {
            nomAtributo = nom;
            tipoDato = tipo;
            longDato = longitud;
            dirAtributo = dirA;
            tipoIndice = tInd;
            dirIndice = dirInd;
            dirSigAtributo = dirSigA;
        }

        /*Método que crea la lista de registros de este atributo*/
        public void creaLista(string texto)
        {
            if (listaInt.Count == 0 && listaChar.Count == 0)
            {
                switch (this.tipoDato)
                {
                    case 'E':
                        listaInt = new List<int>();
                        break;

                    case 'C':
                        listaChar = new List<string>();
                        break;
                }
            }
            agregaAListaRegistro(texto);
        }

        /*Método que agrega la lista de registros de este atributo*/
        public void agregaAListaRegistro(string texto)
        {
            switch (this.tipoDato)
            {
                case 'E':
                    listaInt.Add(int.Parse(texto));
                    break;

                case 'C':
                    listaChar.Add(texto.PadRight(this.longDato - 1));
                    break;
            }

        }
    }
}
