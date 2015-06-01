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
using System.Text.RegularExpressions;

namespace Ejemplo1
{
    public partial class CompiladorLua : Form
    {
        static StreamWriter escribir;
        static int posicion = 0;
        static string[] palReservadas = new string[] { "and", "break", "do", "else", "end", "false", "for", "if", "nil", "not", "or", "return", "then", "true", "Print" };
        List<string> letras = new List<string>();
        List<string> Errores = new List<string>();
        List<int> lineas = new List<int>();
        List<int> program = new List<int>();
        
        public CompiladorLua()
        {
            InitializeComponent();

            
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
            letras.Clear();
            
        }
    
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                rtxtboxCodigo.Text = File.ReadAllText(openFileDialog1.FileName);
               
            }
           
            AnalisisLexico();
            BuscarCoincidencia();
            
            
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
            AnalisisLexico();
            Errores.Clear();
            Sintaxis sx = new Sintaxis();
            sx.Program = program;
            sx.inicializa();
            BuscarCoincidencia();
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
        private void rtxtboxCodigo_MouseClick(object sender, MouseEventArgs e)
        {
            int firstcharindex = rtxtboxCodigo.GetFirstCharIndexOfCurrentLine();
            int currentline = rtxtboxCodigo.GetLineFromCharIndex(firstcharindex);
            string currentlinetext = rtxtboxCodigo.Lines[currentline];
            rtxtboxCodigo.Select(firstcharindex, currentlinetext.Length);
        }

        private void rtxtboxCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            AnalisisLexico();
            //string comparar = "";
            //int indice = 0;
            //if (Convert.ToInt32(e.KeyChar) == Convert.ToInt32(Keys.Space))
            //{

            //    comparar = rtxtboxCodigo.Text;
            //    char[] arreglo = rtxtboxCodigo.Text.ToCharArray();
            //    for (int i = 0; i < rtxtboxCodigo.TextLength; i++)
            //    {
            //        if (arreglo[i] == ' ')
            //        {
            //            indice = i;
            //        }
            //    }
            //    comparar = null;
            //    if (indice != 0)
            //    {
            //        indice++;
            //    }
            //    for (int i = indice; i < rtxtboxCodigo.Text.Length; i++)
            //    {
            //        comparar += arreglo[i];
            //    }
            //    ColorTxt(comparar, indice);
            //}

            
 
            //this.rtxtboxCodigo.SelectionStart = this.rtxtboxCodigo.Text.Length;
 
            //this.rtxtboxCodigo.TextChanged += (ob, ev) =>
            //    {
            //        posicion = rtxtboxCodigo.SelectionStart;
            //        ColorTxt();
            //    };
 
           
        }
        private void ColorTxt()
        {
            //for (int i = 0; i < palReservadas.Length; i++)
            //{
            //    if (palReservadas[i] == comparar)
            //    {
            //        rtxtboxCodigo.Select(indice, rtxtboxCodigo.Text.Length);
            //        rtxtboxCodigo.SelectionColor = Color.Aqua;
            //        rtxtboxCodigo.SelectionStart = this.rtxtboxCodigo.Text.Length;
            //        break;
            //    }
            //    else
            //    {
            //        rtxtboxCodigo.Select(indice, rtxtboxCodigo.Text.Length);
            //        rtxtboxCodigo.SelectionColor = Color.White;
            //        rtxtboxCodigo.SelectionStart = this.rtxtboxCodigo.Text.Length;
            //    }
            //}
            //this.rtxtboxCodigo.Select(0, rtxtboxCodigo.Text.Length);
            //this.rtxtboxCodigo.SelectionColor = Color.Black;
            //this.rtxtboxCodigo.Select(posicion, 0);

            //string[] texto = rtxtboxCodigo.Text.Trim().Split(' ');
            //int inicio = 0;

            //foreach (string x in texto)
            //{
            //    foreach (string y in palReservadas)
            //    {
            //        if (x.Length != 0)
            //        {
            //            if (x.Trim().Equals(y))
            //            {
            //                inicio = this.rtxtboxCodigo.Text.IndexOf(x, inicio);
            //                this.rtxtboxCodigo.Select(inicio, x.Length);
            //                rtxtboxCodigo.SelectionColor = Color.Red;
            //                this.rtxtboxCodigo.Select(posicion, 0);
            //                inicio = inicio + 1;
            //            }
            //        }
            //    }
            //}
        }

        private void BuscarCoincidencia()
        {
            MatchCollection Resultados;
		    Int32 cont =0;
            rtxtboxCodigo.SelectAll();
            rtxtboxCodigo.SelectionColor = Color.White;
            rtxtboxCodigo.SelectionBackColor = Color.DimGray;
            for (int i = 0; i < palReservadas.Length; i++)
            {
                string b = palReservadas[i];
                Regex busqueda = new Regex(b, RegexOptions.IgnoreCase);
                Resultados = busqueda.Matches(rtxtboxCodigo.Text);
                

                // En este punto recorres los resultados de la busqueda
                // aca si quieres los puedes reemplazar, com resaltarlos, que es lo que hago en este caso.
                foreach (Match Palabra in Resultados)
                {
                    rtxtboxCodigo.SelectionStart = Palabra.Index;
                    rtxtboxCodigo.SelectionLength = Palabra.Length;
                    rtxtboxCodigo.SelectionColor = Color.Aqua;
                    cont++;
                }
			
                
            }
           
			
}

        private void btn_Lexico_Click(object sender, EventArgs e)
        {
            AnalisisLexico();
            BuscarCoincidencia();
        }

        private void rtxtboxCodigo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            rtxtboxCodigo.SelectAll();
            rtxtboxCodigo.SelectionColor = Color.White;
            rtxtboxCodigo.SelectionBackColor = Color.DimGray;
            BuscarCoincidencia();
        }
       
    }
}
