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

        static string[] palReservadas = new string[] { "and", "break", "do", "else", "end", "false", "for", "if", "in", "nil", "not", "or", "return", "then", "true" };
        List<string> letras = new List<string>();
        List<string> Errores = new List<string>();
        List<int> lineas = new List<int>();
        List<int> program = new List<int>();
        
        public CompiladorLua()
        {
            InitializeComponent();

            
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        
        {
            
            AnalisisLexico();
            ColorTxt();
      
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

                if (cadena == "") { program.Add(24); continue; }
                Lexico lx = new Lexico();
                
                lx.Cadena = cadena;
                lx.Inicializa();
                
                lx.Linea = i;
                
                lx.Multi1 = mu;
                lx.AnalisisContenedor();
                mu = lx.Multi();
                letras = lx.Lista();
                program = lx.Program();
                program.Add(24);
                 
                
                
                foreach (string a in letras)
                {
                    listbxTokens.Items.Add(a);
                }
                
            }
            //this.rtxtboxCodigo.TextChanged += (ob, ev) =>
            //{
            //    var palabras = this.rtxtboxCodigo.Text.Split(new char[] { ' ' },
            //    StringSplitOptions.RemoveEmptyEntries);
            //    var resultado = from b in palReservadas
            //                    from c in palabras

            //                    where c == b
            //                    select b;

            //    int inicio = 0;
            //    foreach (var item in resultado)
            //    {
            //        inicio = this.rtxtboxCodigo.Text.IndexOf(item, inicio);
            //        this.rtxtboxCodigo.Select(inicio, item.Length);
            //        this.rtxtboxCodigo.SelectionColor = Color.Aquamarine;
            //        this.rtxtboxCodigo.SelectionStart = this.rtxtboxCodigo.Text.Length;
            //        inicio++;
            //    }

            //    this.rtxtboxCodigo.SelectionColor = Color.White;
            //    this.rtxtboxCodigo.SelectionStart = this.rtxtboxCodigo.Text.Length;

            //};
            letras.Clear();
            
        }
        private void ColorTxt()
        {
          
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                rtxtboxCodigo.Text = File.ReadAllText(openFileDialog1.FileName);
                ColorTxt();
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
            Errores.Clear();
            Sintaxis sx = new Sintaxis();
            sx.Program = program;
            sx.inicializa();
           
            Errores = sx.Errores();
            lineas = sx.Lineas();
            foreach (var item in Errores)
            {
                lbxErrores.Items.Add(item);
            }
            MessageBox.Show("Compilacin terminada");
        }

        private void lbxErrores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxErrores.Focus())
            {
                int i = 0;
                i = lbxErrores.SelectedIndex;

               
                //int position = this.rtxtboxCodigo.GetCharIndexFromPosition(p);

                //int Linea = this.rtxtboxCodigo.GetLineFromCharIndex(position);
                int start = this.rtxtboxCodigo.GetFirstCharIndexFromLine(lineas[i]);

                int end = this.rtxtboxCodigo.Lines[(lineas[i] - 1)].Length;
                this.rtxtboxCodigo.Select(Math.Abs(lineas[i]), end + 1);
                this.rtxtboxCodigo.SelectionBackColor = Color.Blue;
            }
            else
            {
                rtxtboxCodigo.ClearUndo();
            }
            
            
            

        }

       
    }
}
