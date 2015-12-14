using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejemplo1
{
    class GeneradorCodigoObjeto
    {
        List<string> _ensamblador = new List<string>();
        List<string> variables = new List<string>(); 
        Sintaxis sx = new Sintaxis();
        public void main()
        {
            _ensamblador.Add("INCLUDE macros.mac");
            _ensamblador.Add(".MODEL SMALL");
            _ensamblador.Add(".DATA");
            buscaVariables();
            _ensamblador.Add(".CODE");
            _ensamblador.Add("BEGIN:");
            _ensamblador.Add(" MOV AX,@DATA");
            _ensamblador.Add(" MOV DS,AX");
            _ensamblador.Add("CALL COMPI");
            _ensamblador.Add("MOV AX, 4C00H");
            _ensamblador.Add("INT 21H");
            _ensamblador.Add("COMPI PROC");
            Codigo();
            _ensamblador.Add("ret");
            _ensamblador.Add("COMPI ENDP");
            _ensamblador.Add("END BEGIN");

            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Leonardo Durazo\Documents\Escuela\Lenguajes y Automatas II\test.asm");
            foreach (var item in _ensamblador)
            {
                file.WriteLine(item);
            }

            file.Close();
        }

        private void Codigo()
        {
            foreach (var item in sx.Polish)
            {
                switch (item.operador)
                {
                    case "*":
                        Multiplica(item);
                        break;
                    case "/":
                        Divide(item);
                        break;
                    case "+":
                        Suma(item);
                        break;
                    case "-":
                        Resta(item);
                        break;
                    case "..":
                        Concatenacion();
                        break;
                    case ">":
                        
                        break;
                    case ">=":
                     
                        break;
                    case "<":
                      
                        break;
                    case "<=":
                       
                        break;
                    case "==":
                     
                        break;
                    case "and":       
                        break;
                    case "or":
                        break;
                    case "=":
                        Asigna(item);
                        break;
                    case "print":
                        Imprime(item);
                        break;
                    default:
                        break;
                }
            }
            
        }

        private void Imprime(ObjPolish item)
        {
            _ensamblador.Add("WRITE " + item.operador1);
        }

        private void Asigna(ObjPolish item)
        {
            _ensamblador.Add("mov ax," + item.operador1);
            _ensamblador.Add("add ax, 30h");
            _ensamblador.Add("mov " + item.resultado + " ,ax");
        }

        private void Concatenacion()
        {
            throw new NotImplementedException();
        }

        private void Resta(ObjPolish item)
        {
            _ensamblador.Add("RESTA " + item.operador1 + "," + item.operador2 + "," + item.resultado);
        
        }

        private void Suma(ObjPolish item)
        {
            _ensamblador.Add("SUMAR "+item.operador1+","+item.operador2+","+item.resultado);
           
            
        }

        private void Divide(ObjPolish item)
        {
            _ensamblador.Add("DIVIDE " + item.operador1 + "," + item.operador2 + "," + item.resultado);
            
        }

        private void Multiplica(ObjPolish item)
        {
            _ensamblador.Add("MULTI " + item.operador1 + "," + item.operador2 + "," + item.resultado);
            
        }

        private void buscaVariables()
        {
            foreach (var item in sx.Polish)
            {
                if(item.resultado != null )
                {
                    if (buscarListaVarcreadas(item.resultado))
                    {
                        _ensamblador.Add(item.resultado + " dw ? , '$'");
                        variables.Add(item.resultado);
                    }
                    
                }
                
            }
        }

        private bool buscarListaVarcreadas(string resultado)
        {
            bool noencontro = true;
            foreach (var item in variables)
            {
                if(item == resultado)
                {
                    noencontro = false;
                }
            }
            return noencontro;
        }
    }
}
