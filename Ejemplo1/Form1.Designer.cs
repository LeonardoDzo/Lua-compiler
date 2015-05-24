namespace Ejemplo1
{
    partial class CompiladorLua
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.rtxtboxCodigo = new System.Windows.Forms.RichTextBox();
            this.listbxTokens = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCompilar = new System.Windows.Forms.Button();
            this.lbxErrores = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // rtxtboxCodigo
            // 
            this.rtxtboxCodigo.BackColor = System.Drawing.Color.DimGray;
            this.rtxtboxCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtxtboxCodigo.ForeColor = System.Drawing.Color.White;
            this.rtxtboxCodigo.Location = new System.Drawing.Point(0, 24);
            this.rtxtboxCodigo.Name = "rtxtboxCodigo";
            this.rtxtboxCodigo.Size = new System.Drawing.Size(907, 335);
            this.rtxtboxCodigo.TabIndex = 2;
            this.rtxtboxCodigo.Text = "";
            this.rtxtboxCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyUp);
            // 
            // listbxTokens
            // 
            this.listbxTokens.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listbxTokens.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listbxTokens.ForeColor = System.Drawing.Color.White;
            this.listbxTokens.FormattingEnabled = true;
            this.listbxTokens.Location = new System.Drawing.Point(913, 25);
            this.listbxTokens.Name = "listbxTokens";
            this.listbxTokens.Size = new System.Drawing.Size(168, 444);
            this.listbxTokens.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1093, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.openToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.saveToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.exitToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(477, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(111, 24);
            this.panel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Archivo1.txt";
            // 
            // btnCompilar
            // 
            this.btnCompilar.Location = new System.Drawing.Point(749, 0);
            this.btnCompilar.Name = "btnCompilar";
            this.btnCompilar.Size = new System.Drawing.Size(75, 23);
            this.btnCompilar.TabIndex = 7;
            this.btnCompilar.Text = "Compilar";
            this.btnCompilar.UseVisualStyleBackColor = true;
            this.btnCompilar.Click += new System.EventHandler(this.btnCompilar_Click);
            // 
            // lbxErrores
            // 
            this.lbxErrores.FormattingEnabled = true;
            this.lbxErrores.Location = new System.Drawing.Point(0, 365);
            this.lbxErrores.Name = "lbxErrores";
            this.lbxErrores.Size = new System.Drawing.Size(907, 108);
            this.lbxErrores.TabIndex = 8;
            // 
            // CompiladorLua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1093, 479);
            this.Controls.Add(this.lbxErrores);
            this.Controls.Add(this.btnCompilar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listbxTokens);
            this.Controls.Add(this.rtxtboxCodigo);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CompiladorLua";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comilador de Lua";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox rtxtboxCodigo;
        private System.Windows.Forms.ListBox listbxTokens;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCompilar;
        private System.Windows.Forms.ListBox lbxErrores;
    }
}

