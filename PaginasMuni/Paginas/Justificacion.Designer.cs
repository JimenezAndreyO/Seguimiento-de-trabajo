namespace PaginasMuni
{
    partial class Justificacion
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
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtJustificacion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Aceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.Location = new System.Drawing.Point(582, 399);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 27);
            this.button1.TabIndex = 0;
            this.button1.Text = "Retornar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Justificación";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtJustificacion
            // 
            this.txtJustificacion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtJustificacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJustificacion.Location = new System.Drawing.Point(234, 136);
            this.txtJustificacion.MaximumSize = new System.Drawing.Size(300, 300);
            this.txtJustificacion.MinimumSize = new System.Drawing.Size(120, 120);
            this.txtJustificacion.Name = "txtJustificacion";
            this.txtJustificacion.Size = new System.Drawing.Size(300, 120);
            this.txtJustificacion.TabIndex = 4;
            this.txtJustificacion.TextChanged += new System.EventHandler(this.txtJustificacion_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label3.Location = new System.Drawing.Point(338, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Justificacion de reporte";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // Aceptar
            // 
            this.Aceptar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Aceptar.Location = new System.Drawing.Point(350, 255);
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(95, 28);
            this.Aceptar.TabIndex = 11;
            this.Aceptar.Text = "Aceptar";
            this.Aceptar.UseVisualStyleBackColor = true;
            this.Aceptar.Click += new System.EventHandler(this.Aceptar_Click);
            // 
            // Justificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Aceptar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtJustificacion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Name = "Justificacion";
            this.Text = "Justificacion";
            this.Load += new System.EventHandler(this.Justificacion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtJustificacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Aceptar;
    }
}