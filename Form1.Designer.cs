namespace PRY2_Analisis_CCSS
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.consultoriosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AgregarConsultorio = new System.Windows.Forms.ToolStripMenuItem();
            this.EliminarConsultorio = new System.Windows.Forms.ToolStripMenuItem();
            this.especialidadesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AgregarEspecialidad = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pacientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AgregarPaciente = new System.Windows.Forms.ToolStripMenuItem();
            this.VerPacientes = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.PanelEspera = new System.Windows.Forms.Panel();
            this.panelTiempo = new System.Windows.Forms.Panel();
            this.lblHora = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.botonRepartir = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelTiempo.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(135)))));
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.consultoriosToolStripMenuItem,
            this.especialidadesToolStripMenuItem,
            this.pacientesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1370, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // consultoriosToolStripMenuItem
            // 
            this.consultoriosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AgregarConsultorio,
            this.EliminarConsultorio});
            this.consultoriosToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.consultoriosToolStripMenuItem.Name = "consultoriosToolStripMenuItem";
            this.consultoriosToolStripMenuItem.Size = new System.Drawing.Size(128, 29);
            this.consultoriosToolStripMenuItem.Text = "Consultorios";
            // 
            // AgregarConsultorio
            // 
            this.AgregarConsultorio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(135)))));
            this.AgregarConsultorio.DoubleClickEnabled = true;
            this.AgregarConsultorio.ForeColor = System.Drawing.Color.White;
            this.AgregarConsultorio.Name = "AgregarConsultorio";
            this.AgregarConsultorio.Size = new System.Drawing.Size(178, 34);
            this.AgregarConsultorio.Text = "Agregar";
            this.AgregarConsultorio.Click += new System.EventHandler(this.AgregarConsultorio_Click);
            // 
            // EliminarConsultorio
            // 
            this.EliminarConsultorio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(135)))));
            this.EliminarConsultorio.ForeColor = System.Drawing.Color.White;
            this.EliminarConsultorio.Name = "EliminarConsultorio";
            this.EliminarConsultorio.Size = new System.Drawing.Size(178, 34);
            this.EliminarConsultorio.Text = "Eliminar";
            this.EliminarConsultorio.Click += new System.EventHandler(this.EliminarConsultorio_Click);
            // 
            // especialidadesToolStripMenuItem
            // 
            this.especialidadesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AgregarEspecialidad,
            this.editarToolStripMenuItem});
            this.especialidadesToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.especialidadesToolStripMenuItem.Name = "especialidadesToolStripMenuItem";
            this.especialidadesToolStripMenuItem.Size = new System.Drawing.Size(142, 29);
            this.especialidadesToolStripMenuItem.Text = "Especialidades";
            this.especialidadesToolStripMenuItem.Click += new System.EventHandler(this.especialidadesToolStripMenuItem_Click);
            // 
            // AgregarEspecialidad
            // 
            this.AgregarEspecialidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(135)))));
            this.AgregarEspecialidad.ForeColor = System.Drawing.Color.White;
            this.AgregarEspecialidad.Name = "AgregarEspecialidad";
            this.AgregarEspecialidad.Size = new System.Drawing.Size(178, 34);
            this.AgregarEspecialidad.Text = "Agregar";
            this.AgregarEspecialidad.Click += new System.EventHandler(this.agregarToolStripMenuItem1_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(135)))));
            this.editarToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(178, 34);
            this.editarToolStripMenuItem.Text = "Editar";
            // 
            // pacientesToolStripMenuItem
            // 
            this.pacientesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AgregarPaciente,
            this.VerPacientes});
            this.pacientesToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.pacientesToolStripMenuItem.Name = "pacientesToolStripMenuItem";
            this.pacientesToolStripMenuItem.Size = new System.Drawing.Size(100, 29);
            this.pacientesToolStripMenuItem.Text = "Pacientes";
            // 
            // AgregarPaciente
            // 
            this.AgregarPaciente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(135)))));
            this.AgregarPaciente.ForeColor = System.Drawing.Color.White;
            this.AgregarPaciente.Name = "AgregarPaciente";
            this.AgregarPaciente.Size = new System.Drawing.Size(218, 34);
            this.AgregarPaciente.Text = "Agregar";
            this.AgregarPaciente.Click += new System.EventHandler(this.AgregarPaciente_Click);
            // 
            // VerPacientes
            // 
            this.VerPacientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(135)))));
            this.VerPacientes.ForeColor = System.Drawing.Color.White;
            this.VerPacientes.Name = "VerPacientes";
            this.VerPacientes.Size = new System.Drawing.Size(218, 34);
            this.VerPacientes.Text = "Ver pacientes";
            this.VerPacientes.Click += new System.EventHandler(this.VerPacientes_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 524);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label1.Size = new System.Drawing.Size(205, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "Registros";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // PanelEspera
            // 
            this.PanelEspera.AutoScroll = true;
            this.PanelEspera.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelEspera.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelEspera.Location = new System.Drawing.Point(0, 592);
            this.PanelEspera.Name = "PanelEspera";
            this.PanelEspera.Size = new System.Drawing.Size(1370, 157);
            this.PanelEspera.TabIndex = 2;
            // 
            // panelTiempo
            // 
            this.panelTiempo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(130)))), ((int)(((byte)(188)))));
            this.panelTiempo.Controls.Add(this.lblHora);
            this.panelTiempo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTiempo.ForeColor = System.Drawing.Color.White;
            this.panelTiempo.Location = new System.Drawing.Point(0, 0);
            this.panelTiempo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelTiempo.Name = "panelTiempo";
            this.panelTiempo.Size = new System.Drawing.Size(207, 35);
            this.panelTiempo.TabIndex = 3;
            this.panelTiempo.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTiempo_Paint);
            // 
            // lblHora
            // 
            this.lblHora.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHora.AutoSize = true;
            this.lblHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHora.Location = new System.Drawing.Point(80, 9);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(0, 20);
            this.lblHora.TabIndex = 0;
            this.lblHora.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHora.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.panelTiempo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1163, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(207, 559);
            this.panel2.TabIndex = 4;
            // 
            // botonRepartir
            // 
            this.botonRepartir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.botonRepartir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(130)))), ((int)(((byte)(188)))));
            this.botonRepartir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonRepartir.ForeColor = System.Drawing.SystemColors.Control;
            this.botonRepartir.Location = new System.Drawing.Point(0, 545);
            this.botonRepartir.Name = "botonRepartir";
            this.botonRepartir.Size = new System.Drawing.Size(108, 47);
            this.botonRepartir.TabIndex = 0;
            this.botonRepartir.Text = "Repartir";
            this.botonRepartir.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(218)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.botonRepartir);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PanelEspera);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Sistema CCSS";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panelTiempo.ResumeLayout(false);
            this.panelTiempo.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem consultoriosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AgregarConsultorio;
        private System.Windows.Forms.ToolStripMenuItem EliminarConsultorio;
        private System.Windows.Forms.ToolStripMenuItem especialidadesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AgregarEspecialidad;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pacientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AgregarPaciente;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem VerPacientes;
        private System.Windows.Forms.Panel PanelEspera;
        private System.Windows.Forms.Panel panelTiempo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Button botonRepartir;
    }
}

