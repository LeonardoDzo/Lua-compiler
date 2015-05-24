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
        static List<string> errores = new List<string>();
        static int[] expre = new int[]  {101,103,206,210,215};
        static int[] opBinario = new int[] {104,108,109,110,116,117,118,119,122,201,212};
        static int bdo = 0;
        static int bfor = 0;
        static int bif = 0;
        static string msg;
        
        static int pos = 0;
        static bool go= false;

        public Sintaxis() {
            pos = 0;
        }
        public List<int> Program
        {
            get { return program; }
            set { program = value; }
        }
        public List<string> Errores()
        {
            return errores;
        }
        public void chunk()
        {
            stat();
        }
        public static void block()
        {
            stat();
        }
        public static void stat()
        {
            while (pos < program.Count && program[pos] < 1000)
            {
                if (program[pos] == 24) pos++;

                switch(program[pos]){
                    case 102:
                        varlist();
                        if (program[pos] == 120)
                        {
                            pos++;
                            explist();         
                            pos++;
                        }
                        else
                        {
                            errores.Add("Error Sintaxis: Se esperaba un  \"=\"");
                        }
                        break;
                    case 203:
                        pos++;
                        if (program[pos] == 24) pos++;
                        bdo++;
                        block();
                        break;
                    case 207:
                        break;
                    case 208:
                        break;
                    default:
                        while (bdo > 0)
                        {
                            if (program[pos] == 205)
                            {
                                bdo--;
                                if (program[pos] == 24) pos++;
                                pos++;
                            }
                            else
                            {
                                errores.Add("Error de sintaxis: Se esperaba la palabra reservada end");
                                break;
                            }
                        }
                        break;

                }
                
            }
            
        }

        public static void varlist()
        {
            var();
            while (program[pos] == 125)
            {
                var();
            }
        }
        public static void var()
        {
            
            if (program[pos] == 102)
            {
                pos++;
                if (program[pos] == 114)
                {
                    exp();
                    if (program[pos] == 115)
                    {
                        pos++;
                        go = true;
                    }
                    else
                    {
                        errores.Add("Error Sintaxis: Falta un ]");
                    }
                }
                else
                {
                    prefiexp();
                }
                
                go = true;
            }
            
        }
        public static void explist(){
            exp();
            
            while (pos < program.Count && program[pos] == 125)
            {
                exp();
            }
        }
        public static void exp()
        {
            for (int i = 0; i < expre.Length; i++)
            {
                if (expre[i] == program[pos])
                {
                    pos++;
                    for (int j = 0; j < opBinario.Length; j++)
                    {
                        if (pos >= program.Count) break;
                        if (program[pos] == opBinario[j])
                        {
                            pos++;
                            exp();
                        }
                    }
                    
                    go = true;
                    break;
                }
                else
                {
                    errores.Add("Error de Sintaxis se esperaba una Expresión");
                    go = false;
                }
            }
        }

        public static void prefiexp()
        {
            if (program[pos] == 102)
            {
                var();
            }
            else
            {
                if (program[pos] == 114)
                {
                    exp();
                    if (program[pos] == 115)
                    {
                        pos++;
                        go = true;
                    }
                }
            }
        }
        public static void ConstructorTabla()
        {
            if (program[pos] == 114)
            {
                pos++;
                if (program[pos] == 115)
                {
                    go = true;
                }
                else
                {
                    listadeCampos();
                    ConstructorTabla();
                }
            }
        }
        public static void listadeCampos(){
            campo();
            if (program[pos] == 107 || program[pos] == 125)
            {
                pos++;
                campo();
            }
        }
        public static void campo()
        {
            
            if (program[pos] == 102)
            {
                pos++;
                if (program[pos] == 120)
                {
                    pos++;
                    exp();
                }
               
            }
            if (program[pos] == 123)
            {
                exp();
                if (program[pos] == 124)
                {
                    go = true;
                }
            }
            else
            {
                exp();
            }
            
        }
        
      
        
    }
}
