namespace Buscaminas
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelTablero = new Panel();
            label1 = new Label();
            cmbxDifficult = new ComboBox();
            temporizador = new Label();
            SuspendLayout();
            // 
            // panelTablero
            // 
            panelTablero.Location = new Point(0, 42);
            panelTablero.Name = "panelTablero";
            panelTablero.Size = new Size(686, 540);
            panelTablero.TabIndex = 0;
            panelTablero.Paint += panelTablero_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(68, 16);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 2;
            label1.Text = "Dificultad";
            // 
            // cmbxDifficult
            // 
            cmbxDifficult.FormattingEnabled = true;
            cmbxDifficult.Items.AddRange(new object[] { "Fácil", "Medio", "Difícil" });
            cmbxDifficult.Location = new Point(132, 13);
            cmbxDifficult.Name = "cmbxDifficult";
            cmbxDifficult.Size = new Size(157, 23);
            cmbxDifficult.TabIndex = 3;
            cmbxDifficult.Text = "Seleccionar dificultad";
            cmbxDifficult.SelectedIndexChanged += cmbxDifficult_SelectedIndexChanged;
            // 
            // temporizador
            // 
            temporizador.AutoSize = true;
            temporizador.Location = new Point(371, 16);
            temporizador.Name = "temporizador";
            temporizador.Size = new Size(25, 15);
            temporizador.TabIndex = 4;
            temporizador.Text = "000";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(701, 594);
            Controls.Add(temporizador);
            Controls.Add(cmbxDifficult);
            Controls.Add(label1);
            Controls.Add(panelTablero);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelTablero;
        private Label label1;
        private ComboBox cmbxDifficult;
        private Label temporizador;
    }
}
