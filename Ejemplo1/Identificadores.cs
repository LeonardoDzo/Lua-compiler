using System.Collections.Generic;

namespace Ejemplo1
{
    public class Identificadores
    {
        public string lexema;
        public int tipo;
        public string mascara;

        public string Lexema
        {
            get { return lexema; }
            set { lexema = value; }
        }

        public void setTipo(int tipo)
        {
            this.tipo = tipo;
        }
    }
}