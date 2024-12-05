namespace ConnexionSQL
{
    partial class FormularioPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConexion = new System.Windows.Forms.Button();
            this.btnDesconexion = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnAbrirTrabajos = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConexion
            // 
            this.btnConexion.Location = new System.Drawing.Point(42, 129);
            this.btnConexion.Name = "btnConexion";
            this.btnConexion.Size = new System.Drawing.Size(162, 38);
            this.btnConexion.TabIndex = 0;
            this.btnConexion.Text = "Conectar";
            this.btnConexion.UseVisualStyleBackColor = true;
            this.btnConexion.Click += new System.EventHandler(this.btnConexion_Click);
            // 
            // btnDesconexion
            // 
            this.btnDesconexion.Location = new System.Drawing.Point(223, 129);
            this.btnDesconexion.Name = "btnDesconexion";
            this.btnDesconexion.Size = new System.Drawing.Size(162, 38);
            this.btnDesconexion.TabIndex = 1;
            this.btnDesconexion.Text = "Desconectar";
            this.btnDesconexion.UseVisualStyleBackColor = true;
            this.btnDesconexion.Click += new System.EventHandler(this.btnDesconexion_Click);
            // 
            // lblEstado
            // 
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.Location = new System.Drawing.Point(126, 52);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(193, 39);
            this.lblEstado.TabIndex = 2;
            this.lblEstado.Text = "ESTADO";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAbrirTrabajos
            // 
            this.btnAbrirTrabajos.Location = new System.Drawing.Point(257, 292);
            this.btnAbrirTrabajos.Name = "btnAbrirTrabajos";
            this.btnAbrirTrabajos.Size = new System.Drawing.Size(128, 23);
            this.btnAbrirTrabajos.TabIndex = 3;
            this.btnAbrirTrabajos.Text = "Añadir Trabajo";
            this.btnAbrirTrabajos.UseVisualStyleBackColor = true;
            this.btnAbrirTrabajos.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(257, 321);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Añadir Empleado";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // FormularioPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAbrirTrabajos);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.btnDesconexion);
            this.Controls.Add(this.btnConexion);
            this.Name = "FormularioPrincipal";
            this.Text = "Conexion SQL";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConexion;
        private System.Windows.Forms.Button btnDesconexion;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Button btnAbrirTrabajos;
        private System.Windows.Forms.Button button1;
    }
}

