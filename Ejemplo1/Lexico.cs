using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ejemplo1
{

    
    class Lexico 
    {
            
        static string cadena,palabra= "";
        static char[] contenedor; 
        static List<string> letras = new List<string>();
        static List<int> program = new List<int>();
        static List<int> estados = new List<int>();
        static int linea;

        public int Linea
        {
            get { return linea; }
            set { linea = value; }
        }
        static int Qcoluma, Tfila = 0;
        bool comillas= false;
        bool multi;

        public bool Multi1
        {
            get { return multi; }
            set { multi = value; }
        }
       

        public void Inicializa()
        {
            Qcoluma = 0;
            Tfila = 0;
            palabra = "";
            letras.Clear();
            estados.Clear();
            comillas = false;
            //espaciosenblanco();
           
        }
        static string[] palReservadas = new string[] { "and", "break", "do", "else", "end", "false", "for", "if", "in", "nil", "not", "or", "return", "then","true" };
        static string[] Errores = new string[] { "Caracter invalido", "Numero mal escrito", "Falta un igual", "Falta una comilla" };

        static int[,] MatrizT = new int[,] {      /*  d	       L	  _       "	      '       -   	  .  	  ;  	 +	     *	     %   	 {   	 }	     (	     )	    <	      >     =	     ~	     [   	]	  Demas, WitheSpace,       ,,   Enter*/

                          /*0*/                    {  1,	  4,	  4,	  5,	  6,	  7,	  9,	107,	108,	109,	110,	113,	112,	114,	115,	11,	     12,	13,	     14,	123,	124,	500,          0,     15,  0},
                          /*1*/                    {  1,	101,	101,	101,	101,	101,	  2,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,        101,     101,  101},
                          /*2*/                    {  3,	501,	501,	501,	501,	501,	501,	501,	501,	501,	501,	501,	501,	501,	501,	501,	501,	501,	501,	501,	501,	501,        501,     501,  501},
                          /*3*/                    {  3,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,	101,        101,     101,  101},
                          /*4*/                    {  4,	  4,	  4,	102,	102,	102,	102,	102,	102,	102,	102,	102,	102,	102,	102,	102,	102,	102,	102,	102,	102,	102,        102,     102,  102},
                          /*5*/                    {  5,	  5,	  5,	103,	  5,	  5,	  5,	  5,	  5,	  5,	  5,	  5,	  5,	  5,	  5,	  5,	  5,	  5,	  5,	  5,	  5,	5,            5,       5,  503},
                          /*6*/                    {  6,      6,	  6,	  6,	103,	  6,	  6,	  6,	  6,	  6,	  6,	  6,	  6,	  6,	  6,	  6,	  6,	  6,	  6,      6,      6,	6,            6,       6,  503},
                          /*7*/                    {104,	104,	104,	104,	104,	  8,	104,	104,	104,	104,	104,	104,	104,	104,	104,	104,	104,	104,	104,	104,	104,	104,        104,     104,  104},
                          /*8*/                    {  8,	  8,	  8,	  8,	  8,	  8,	  8,	  8,	  8,	  8,	  8,	  8,	  8,	  8,	  8,	  8,	  8,	  8,	  8,	  8,	  8,	  0,          8,       8,    0},
                          /*9*/                    {  3,	106,	106,	106,	106,	106,	 10,	106,	106,	106,	106,	106,	106,	106,	106,	106,	106,	106,	106,	106,	106,	106,        106,     106,  106},
                          /*10*/                   {105,	105,	105,	105,	105,	105,	105,	105,	105,	105,	105,	105,	105,	105,	105,	105,	105,	105,	105,	105,	105,	105,        105,     105,  105},
                          /*11*/                   {116,	116,	116,	116,	116,	116,	116,	116,	116,	116,	116,	116,	116,	116,	116,	116,	116,	117,	116,	116,	116,	116,        116,     116,  116},
                          /*12*/                   {118,	118,	118,	118,	118,	118,	118,	118,	118,	118,	118,	118,	118,	118,	118,	118,	118,	119,	118,	118,	118,	118,        118,     118,  118},
                          /*13*/                   {120,	120,	120,	120,	120,	120,	120,	120,	120,	120,	120,	120,	120,	120,	120,	120,	120,	121,	120,	120,	120,	120,        120,     120,  120},
                          /*14*/                   {502,	502,	502,	502,	502,	502,	502,	502,	502,	502,	502,	502,	502,	502,	502,	502,	502,	122,    502,	502,    502,	502,        502,     502,  502}};
       
        public string Cadena
        {
          get { return cadena; }
          set { cadena = value; }
        }

          
        public void AnalisisContenedor(){
            
            contenedor = cadena.ToCharArray();
            letras.Clear();
            
             foreach (char VARIABLE in contenedor)
            {

                if (char.IsWhiteSpace(VARIABLE))
                {
                   
                       Tfila = 22;
                    
                }
                else{
                
                    
                    if (char.IsDigit(VARIABLE))//verifica si es un numero
                    {

                        Tfila = 0;
                        palabra += VARIABLE;
                     


                    }
                    else
                    {
                        if (char.IsLetter(VARIABLE))//verifica si es una letra
                        {
                            palabra += VARIABLE;
                            Tfila = 1;
                         

                        }
                        else
                        {
                            if (char.IsSymbol(VARIABLE))//verifica si es símbolo
                            {
                                switch (VARIABLE)
                                {
                                    case '+':
                                        palabra += VARIABLE;
                                        Tfila = 8;
                                    

                                        break;
                                    case '<':
                                        palabra += VARIABLE;
                                        Tfila = 15;
                                       

                                        break;
                                    case '>':
                                        palabra += VARIABLE;
                                        Tfila = 16;
                                       
                                        break;
                                    case '=':
                                        palabra += VARIABLE;
                                        Tfila = 17;
                                      

                                        break;
                                    case '~':
                                        palabra += VARIABLE;
                                        Tfila = 18;
                                     

                                        break;
                                    default:
                                        Tfila = 21;
                                        break;

                                }


                            }
                            else
                            {
                                if (char.IsPunctuation(VARIABLE))//verifica si es algún signo de puntuación
                                {
                                    switch (VARIABLE)
                                    {
                                        case '-':
                                            palabra += VARIABLE;
                                            Tfila = 5;
                                            
                                            break;
                                        case '_':
                                            palabra += VARIABLE;
                                            Tfila = 2;
                                         
                                            break;
                                        case '"':
                                           comillas = true; 
                                            palabra += VARIABLE;
                                            Tfila = 3;
                                         
                                            break;
                                        case '\'':
                                          comillas = true;
                                            palabra += VARIABLE;
                                            Tfila = 4;
                                         
                                            break;
                                        case '*':
                                            palabra += VARIABLE;
                                            Tfila = 9;
                                         
                                            break;
                                        case '.':
                                            palabra += VARIABLE;
                                            Tfila = 6;
                                         
                                            break;
                                        case ';':
                                            palabra += VARIABLE;
                                            Tfila = 7;
                                           
                                            break;
                                        case ',':
                                            palabra += VARIABLE;
                                            Tfila = 23;

                                            break;
                                        case '[':
                                            palabra += VARIABLE;
                                            Tfila = 19;
                                          
                                            break;
                                        case ']':
                                            palabra += VARIABLE;
                                            Tfila = 20;
                                         
                                            break;
                                        case '{':
                                            palabra += VARIABLE;
                                            Tfila = 11;
                                       
                                            break;
                                        case '}':
                                            palabra += VARIABLE;
                                            Tfila = 12;
                                            
                                            break;
                                        case '(':
                                            palabra += VARIABLE;
                                            Tfila = 13;
                                        
                                            break;
                                        case ')':
                                            palabra += VARIABLE;
                                            Tfila = 14;
                                           
                                            break;
                                        case '%':
                                            palabra += VARIABLE;
                                            Tfila = 10;
                                            
                                            break;
                                        default:
                                            Tfila = 21;
                                            break;

                                    }

                                }
                            }
                        }
                }
                
            }
                if (Qcoluma >= 0)
                {
                    estados.Add(Tfila);
                }
            

               
             
                
              
            }
            estados.Add(24);
           
            Verifica();
        }

        public List<string> Lista()
        {
            return letras;
        }

        public List<int> Program()
        {
            return program;
        }
        //public void espaciosenblanco()
        //{
        //    string aux = "";
        //    contenedor = cadena.ToCharArray();
        //    for (int i = 0; i < contenedor.Length-1; i++)
        //    {
        //        bool encuentra = false;
        //        if (encuentra || char.IsWhiteSpace(contenedor[i])==false)
        //        {
        //            aux += contenedor[i];
        //            encuentra = true;
        //        }
        //    }
        //    cadena = aux;
        //}
        public bool Multi()
        {
            return multi;
        }
        public void Verifica()
        {
            palabra = "";
            int token = 0;
            for (int i = 0; i < estados.Count; i++)
            {
               
                   if (estados[i] < 24)
                   {
                       #region: Multilinea
                       if (multi)
                        {
                            if (contenedor[i] == ']')
                            {
                                try
                                {
                                    if (contenedor[i + 1] == ']')
                                    {
                                        multi = false;
                                        i++;
                                        
                                    }
                                }
                                catch
                                { }
                            }
                        }
                       #endregion
                       else
                        {
                            if (comillas == false && estados[i] == 22) continue;
                            palabra += contenedor[i];
                            token = MatrizT[Qcoluma, estados[i]];
                            Qcoluma = MatrizT[Qcoluma, estados[i]];
                            if (Qcoluma > 100) Qcoluma = 0;
                            #region: Multilinea
                            if (token == 8)
                            {
                                if (contenedor[i] == '[')
                                {
                                    try
                                    {
                                        if (palabra== "--["&&contenedor[i + 1] == '[')
                                        {
                                            multi = true;
                                            
                                        }
                                    }
                                    catch
                                    {

                                    }
                                    
                                }
                            }
                            #endregion

                            if (MatrizT[Qcoluma, estados[(i + 1)]] > 100 || token > 100)
                            {
                                try
                                {

                                    if (MatrizT[Qcoluma, estados[i + 1]] == 117 || MatrizT[Qcoluma, estados[i + 1]] == 119 || MatrizT[Qcoluma, estados[i + 1]] == 103 || MatrizT[Qcoluma, estados[i + 1]] == 121 || MatrizT[Qcoluma, estados[i + 1]] == 122)
                                    {

                                        palabra += contenedor[i + 1];
                                        token = MatrizT[Qcoluma, estados[i + 1]];
                                        i++;

                                    }
                                    else
                                    {
                                        if (token < 100)
                                        {
                                            token = MatrizT[Qcoluma, estados[i + 1]];
                                        }

                                    }


                                }
                                catch { }

                                // if (token > 100) token = MatrizT[Qcoluma, estados[i]]; else token = MatrizT[Qcoluma, estados[i+1]];
                                #region: PalabradasReservadas
                                if (token == 102)
                                {
                                    for (int j = 0; j < palReservadas.Length; j++)
                                    {
                                        if (palabra == palReservadas[j])
                                        {
                                            token = 200 + j + 1;
                                            break;
                                        }
                                    }
                                }
                                #endregion
                                #region:Errores
                                if (token >= 500)
                                {
                                    for (int k = 0; k < Errores.Length; k++)
                                    {
                                        if (token == (k + 500))
                                        {
                                            letras.Add("Error: " + token + " " + Errores[k] + " Linea: " + (linea + 1) + "Posicion: " + (i + 1));
                                            break;
                                        }
                                    }
                                }
                                #endregion
                                else
                                {
                                    letras.Add("" + token + " TOKEN " + palabra);
                                    program.Add(token);
                                }

                                palabra = "";
                                Qcoluma = 0;

                            }
                        }
                    }
                }

            }

        
    }
}
