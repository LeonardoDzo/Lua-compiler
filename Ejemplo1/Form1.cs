using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Ejemplo1
{
    public partial class CompiladorLua : Form
    {
        // DELEGADOS
        internal delegate void Delegado(ListViewItem listView);
        static StreamWriter escribir;
        Lexico lx = new Lexico();
        static string[] palReservadas = new string[] { "and", "break", "else", "end", "false", "for", "if", "nil", "not", "or", "return", "then", "true", "Print","~", "=", ">", "<" };
        List<string> letras = new List<string>();

        public CompiladorLua()
        {
            InitializeComponent();
        }


        static bool mu = false;
        public void AnalisisLexico()
        {
           
            string cadena;
            string[] lineCount = rtxtboxCodigo.Lines;
           
            lx.Strucs.Clear();
            lbxErrores.Items.Clear();
            lx.ErroresList().Clear();
            
            
            if (rtxtboxCodigo.Text == "")
            {
                lx.Strucs.Clear();
                listView1.Clear();
            } 
            for (int i = 0; i < lineCount.Length; i++)
            {
                cadena = lineCount[i];
                //Envio linea escrita
                lx.Cadena = cadena;
                lx.Inicializa();
                //Envio el numero de linea en el que me encuentro
                lx.Linea = i;
                //Aquí hago referencia para siestoy en comentario multilinea
                lx.Multi1 = mu;
                //Mando analisar el renglon
                lx.AnalisisContenedor();
                mu = lx.Multi();
                //Limpia el listview de los tokens y lexemas
                listView1.Columns.Clear();
                listView1.Clear();
                listView1.Columns.Add("Token");
                listView1.Columns.Add("Lexema");
                string[] slitem = new String[3];
                ListViewItem lvi;
                foreach (var item in lx.Strucs)
                {
                    slitem[0] = Convert.ToString(item.token);
                    slitem[1] = item.lexema;
                    lvi = new ListViewItem(slitem);
                    listView1.Items.Add(lvi);

                }


                foreach (string a in lx.ErroresList())
                {
                    lbxErrores.Items.Add(a);
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
            btn_Lexico_Click(sender,e);
            Sintaxis sx = new Sintaxis();
            sx.Strucs = lx.Strucs;
            //sx.Program = program;
            sx.Inicializa();
            BuscarCoincidencia();
            listView1.Columns.Clear();
            listView1.Clear();

            //Llenar lista de variables
            lviewVariables.Columns.Clear();
            lviewVariables.Clear();
            lviewVariables.Columns.Add("Tipo");
            lviewVariables.Columns.Add("Lex");
            lviewVariables.Columns.Add("Mascara");
            string[] sitem = new String[5];
            ListViewItem lvi1;
            foreach (var item in sx.Identificadoreses)
            {
                sitem[0] = Convert.ToString(item.tipo);
                sitem[1] = item.lexema;
                sitem[2] = item.mascara;
                lvi1 = new ListViewItem(sitem);
                lviewVariables.Items.Add(lvi1);

            }

            listView2.Columns.Clear();
            listView2.Clear();
            listView2.Columns.Add("OP");
            listView2.Columns.Add("OP1");
            listView2.Columns.Add("Op2");
            listView2.Columns.Add("R");
            listView2.Columns.Add("Apuntador");
            foreach (var item in sx.Polish)
            {
                sitem[0] = item.operador;
                sitem[1] = item.operador1;
                sitem[2] = item.operador2;
                sitem[3] = item.resultado;
                sitem[4] = item.apuntador;
                lvi1 = new ListViewItem(sitem);
                listView2.Items.Add(lvi1);

            }

            //lineas = sx.Lineas();
            foreach (var item in sx.Errores())
            {
                lbxErrores.Items.Add(item);
            }
            GeneradorCodigoObjeto go = new GeneradorCodigoObjeto();
            go.main();
            if(lbxErrores.Text == null)
            {
                go.main();
            }
            MessageBox.Show("Compilacin terminada");
        }
      
        private void lbxErrores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxErrores.Focus())
            {
                
            }
            else
            {
                rtxtboxCodigo.ClearUndo();
            }

        }

        private void rtxtboxCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            AnalisisLexico();
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
                    rtxtboxCodigo.SelectionColor = Color.Tomato;
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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
