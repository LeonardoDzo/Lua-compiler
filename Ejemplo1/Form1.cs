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

       
    }
}
