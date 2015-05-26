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
        static int[] expre = new int[]  {101,103,206,210,215};
        static int[] opBinario = new int[] {104,108,109,110,116,117,118,119,122,201,212};
        static string msg;
        
        static int p= 0;
        static bool go= false;

        public void inicializa()
        {
            p = 0;
            for (int i = 0; i < program.Count; i++)
            {
                
                sentencia();
                i = p;
                
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
                    bloqueif();

                }
                else if (esWhile(program[p]))
                {

                }
                else if (esIden(program[p]))
                {
                    if (varList())
                    {
                        if (esIgual(program[p]))
                        {
                            p++;
                            Metodoexp();
                        }
                        else
                        {
                            errores.Add("Se esperaba un igual");
                        }
                    }
                    else
                    {
                        errores.Add("Se esperaba un identificador");
                    }



                }
                else
                {
                    errores.Add("Error de sintaxis");
                }

            }
            
        }

        private bool esComa(int token)
        {
            if (token == 125)
            {
                return true;
            }
            return false;
            
        } 
        
       
   

    private bool esIf(int token){
        if (token == 208)
        {
            return true;
        }
        return false;
    }
    
    private bool esThen(int token){
        if (token == 214){
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
        
    private bool esWhile(int token){
        if (token == 211){
            return true;
        }
        return false;
    }
    
    private bool esIden(int token){
        if (token == 102) {
            return true;
        }
        return false;
    }
    private bool esIgual(int token){
        if (token == 120) {
            return true;
          
        }
        return false;
    }

    public bool varList()
    {
            if (esIden(program[p]))
            {
                p++;
                if (esComa(program[p]))
                {
                    p++;
                    varList();
                }
                else
                {
                    return true;
                }
                
            }
            else
            {
                return false ;
            }
           
          
               
         
            return true;

     
    }


    private bool bloqueif(){
        if (Metodoexp())
        {
            if (esThen(program[p]))
            {
                p+=1;
                bloque();
                if (p< program.Count && esEnd(program[p]))
                {
                    p++;
                }
                else
                {
                    errores.Add("Se esperaba un end");
                }
                 
                
            }else
            {
                errores.Add("Se esperaba la palabra reservada Then"); 
            }
        }
        else
        {
            return false;
        }

        
        return true;
    }
   
                 
    private bool Metodoexp(){
        if (p< program.Count && exp(program[p]))
        {
            p+=1;
            if (p < program.Count && operadorBin(program[p]))
            {
                p+=1;
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
