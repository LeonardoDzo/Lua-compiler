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
        static int[] expre = new int[]  {101,103,206,210,215};
        static int[] opBinario = new int[] {104,108,109,110,116,117,118,119,122,201,212};
        static string msg;
        
        static int pos = 0;
        static bool go= false;
        public string MSG()
        {
            return msg;
        }
        public Sintaxis() {
            pos = 0;
        }
        public List<int> Program
        {
            get { return program; }
            set { program = value; }
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
                switch(program[pos]){
                    case 102:
                        varlist();
                        if (program[pos] == 120)
                        {
                            pos++;
                            explist();
                            if (go) msg = "Compilacion terminada"; else msg="Compilacion Fallida";
                            pos++;
                        }
                        break;
                    case 203:
                        pos++;
                        if (program[pos] == 24) pos++; 
                        block();
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
                go = true;
            }
            else
            {
                prefiexp();
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
