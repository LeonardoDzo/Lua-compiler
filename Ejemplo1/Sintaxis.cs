using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejemplo1
{
    class Sintaxis
    {
        static List<int> program = new List<int>();
        static int i = 1;
        public List<int> Program
        {
            get { return Sintaxis.program; }
            set { Sintaxis.program = value; }
        }
        static List<string> errores = new List<string>();
       

        public List<string> Errores()
        {
            return errores;
        }

        static int p= 0;
        

        public void inicializa()
        {
            p = 0;
            while (p < program.Count)
            {
                
                sentencia();
               
                
            }
        }
        private void chunk()
        {
            sentencia();
        }
        private void bloque()
        {
            chunk();
        }
 
 
        public void sentencia(){


            if (p < program.Count)
            {
                if (esIf(program[p]))
                {
                    p++;
                   
                    if (Metodoexp())
                    {
                        if (esThen(program[p]))
                        {

                            p++;
                            
                            if (p < program.Count && esElse(program[p]))
                            {
                                p++;
                                
                            }
                            if (p < program.Count && esEnd(program[p]))
                            {
                                program.RemoveAt(p);

                            }
                            else
                            {
                                
                                bloque();
                                if (p < program.Count && esElse(program[p]))
                                {
                                    p++;
                                   
                                }
                                int aux = p;
                                while (p <= program.Count)
                                {
                                    if (p < program.Count && esEnd(program[p]))
                                    {
                                        program.RemoveAt(p);
                                        break;
                                    }
                                    else 
                                    {
                                        
                                        if (p == program.Count)
                                        {
                                            errores.Add("Se esperaba un End");
                                            break;
                                        }
                                    }
                                    p++;
                                    
                                }
                                p = aux;

                            }
                           
                            
                        }
                        else
                        {
                            errores.Add("Se esperaba la palabra reservada Then");
                            
                        }
                    }
                    

                }
                else if (esFor(program[p]))
                {
                     p++;
                     
                     if (esIden(program[p]))
                     {
                         p++;
                       
                         if (esIgual(program[p]))
                         {
                             p++;
                            
                             Metodoexp();
                             if (esComa(program[p]))
                             {
                                 p++;
                               
                                 Metodoexp();
                                 if (esDo(program[p]))
                                 {
                                     p++;
                                     
                                     
                                     
                                     if (p < program.Count && esEnd(program[p]))
                                     {
                                         program.RemoveAt(p);

                                     }
                                     else
                                     {
                                         bloque();
                                         int aux = p;
                                         while (p <= program.Count)
                                         {
                                             if (p < program.Count && esEnd(program[p]))
                                             {
                                                 program.RemoveAt(p);
                                                 break;
                                             }
                                             else
                                             {
                                                 if (p == program.Count)
                                                 {
                                                     errores.Add("Se esperaba un end");
                                                     break;
                                                 }
                                             }
                                             p++;

                                         }
                                         p = aux;

                                     }
                                     
                                     }
                                     else
                                     {
                                         errores.Add("Se esperaba la palabra reservada do");

                                     }

                                 }

                         }
                         else
                         {
                             errores.Add("Se esperaba un =");
                         }

                     }
                     else
                     {
                         errores.Add("No se declara el identificador");
                     }
                }
                else if (esIden(program[p]))
                {
                        p++;
                        if (varList())
                        {

                            if (p < program.Count && esIgual(program[p]))
                            {
                                p++;
                                Metodoexp();

                            }
                            else
                            {
                                errores.Add("Se esperaba un igual");
                            }
                        }
                        else {
                            errores.Add("Se esperaba un Identificador");

                        }

                   



                }
                else if (esPrint(program[p]))
                {
                    p++;
                    if (espaIzq(program[p]))
                    {
                        do
                        {
                            p++;
                           
                            Metodoexp();

                        } while (esComa(program[p]));
                        if (p<program[p] && espaDer(program[p]))
                        {
                            p++;
                            
                        }
                        else
                        {
                            errores.Add("Se esperaba un parentesis");
                        }
                    }
                    else
                    {
                        errores.Add("Se esperaba un parentesis");
                    }

                }
                else
                {
                  
                    p++;
                 
                }


            }
           
            
        }

        private bool esPrint(int token)
        {
            if (token == 216)
            {
                return true;
            }
            return false;
        }

        private bool esElse(int token)
        {
            if (token == 204)
            {
                return true;
            }
            return false;
        }
        
        private bool esComa(int token)
        {
            if (token == 125)
            {
                return true;
            }
            return false;
            
        }
        private bool esDo(int token)
        {
            if (token == 203)
            {
                return true;
            }
            return false;

        }
        private bool espaDer(int token)
        {
            if (token == 115)
            {
                return true;
            }
            return false;

        }
        private bool espaIzq(int token)
        {
            if (token == 114)
            {
                return true;
            }
            return false;
        }
    public bool varList()
    {
        bool h=true;
        while (p < program.Count && esComa(program[p]))
        {
            p++;
            if (p< program.Count && esIden(program[p]))
            {
                h = true;
                p++;
            }
            else
            {
                h = false;
                break;
            }


        } 

        return h;
            
            
    }
              
    private bool Metodoexp(){
        if (p< program.Count && exp(program[p]))
        {
            p++;
           
            if (p < program.Count && operadorBin(program[p]))
            {
                p++;
                
                Metodoexp();

            }else{
                
                return true;
            }
        }
        else
        {
            errores.Add("Falta una expresion");
            return false;
        }
        return true;
        
    }
    private bool esIf(int token)
    {
        if (token == 208)
        {
            return true;
        }
        return false;
    }

    private bool esThen(int token)
    {
        if (token == 214)
        {
            return true;
        }
        return false;
    }
    private bool esEnd(int token)
    {
        if (token == 205)
        {
            return true;
        }
        return false;
    }

    private bool esFor(int token)
    {
        if (token == 207)
        {
            return true;
        }
        return false;
    }

    private bool esIden(int token)
    {
        if (token == 102)
        {
            return true;
        }
        return false;
    }
    private bool esIgual(int token)
    {
        if (token == 120)
        {
            return true;

        }
        return false;
    }
    private bool exp(int token){
        switch (token)
        {
            case 215:
                return true;
            case 206:
                return true;
            case 101:
                return true;
            case 102:
                return true;
            case 103:
                return true;
            default:
                return false;
        }
    }
    private bool operadorBin(int token){
        switch(token){
            case 108:
                return true;
            case 109:
                return true;
            case 104:
                return true;
            case 116:
                return true;
            case 118:
                return true;
            case 112:
                return true;
            case 117:
                return true;
            case 119:
                return true;
            case 120:
                return true;
            case 121:
                return true;
            case 122:
                return true;  
            default:
                return false;      
        }
    }
}
}
