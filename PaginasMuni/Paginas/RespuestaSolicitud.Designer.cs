namespace PaginasMuni
{
    partial class RespuestaSolicitud
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
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewJustificaciones = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJustificaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(344, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Respuesta de solicitud";
            // 
            // dataGridViewJustificaciones
            // 
            this.dataGridViewJustificaciones.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dataGridViewJustificaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewJustificaciones.Location = new System.Drawing.Point(80, 118);
            this.dataGridViewJustificaciones.Name = "dataGridViewJustificaciones";
            this.dataGridViewJustificaciones.Size = new System.Drawing.Size(627, 226);
            this.dataGridViewJustificaciones.TabIndex = 9;
            this.dataGridViewJustificaciones.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewJustificaciones_CellContentClick);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.Location = new System.Drawing.Point(311, 364);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 30);
            this.button1.TabIndex = 10;
            this.button1.Text = "Descargar Como Excel ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RespuestaSolicitud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridViewJustificaciones);
            this.Controls.Add(this.label3);
            this.Name = "RespuestaSolicitud";
            this.Text = "RespuestaSolicitud";
            this.Load += new System.EventHandler(this.RespuestaSolicitud_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJustificaciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridViewJustificaciones;
        private System.Windows.Forms.Button button1;
    }
}