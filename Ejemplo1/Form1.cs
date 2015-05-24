using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Ejemplo1
{
    public partial class CompiladorLua : Form
    {
        static StreamWriter escribir;


        List<string> letras = new List<string>();
        List<string> Errores = new List<string>();
        List<int> program = new List<int>();
        public CompiladorLua()
        {
            InitializeComponent();
            
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        
        {
            AnalisisLexico();
      
        }
        static bool mu = false;
        public void AnalisisLexico()
        {
            program.Clear();
            string cadena;
            string[] lineCount = rtxtboxCodigo.Lines;
           
            listbxTokens.Items.Clear();
            for (int i = 0; i < lineCount.Length; i++)
            {
                cadena = lineCount[i];
               
                if (cadena=="" ) continue ;
                Lexico lx = new Lexico();
                
                lx.Cadena = cadena;
                lx.Inicializa();
                
                lx.Linea = i;
                
                lx.Multi1 = mu;
                lx.AnalisisContenedor();
                mu = lx.Multi();
                letras = lx.Lista();
                program = lx.Program();
                 if (lineCount.Length == (i + 1)) program.Add(1000); else program.Add(24);
                
                
                foreach (string a in letras)
                {
                    listbxTokens.Items.Add(a);
                }
                
            }
           
            letras.Clear();

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                rtxtboxCodigo.Text = File.ReadAllText(openFileDialog1.FileName);
            }
            AnalisisLexico();
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    escribir = new StreamWriter(saveFileDialog1.FileName);
                    escribir.Write(rtxtboxCodigo.Text);
                    escribir.Close();
                    
                }
            }
            catch
            {

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCompilar_Click(object sender, EventArgs e)
        {
            lbxErrores.Items.Clear();
            Sintaxis sx = new Sintaxis();
            sx.Program = program; 
            sx.chunk();
            Errores = sx.Errores();
            foreach (var item in Errores)
            {
                lbxErrores.Items.Add(item);
            }
            MessageBox.Show("Compilacin terminada");
        }

       
    }
}
