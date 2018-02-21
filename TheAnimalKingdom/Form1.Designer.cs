namespace TheAnimalKingdom
{
    partial class Form1
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
            this._dbPanel1 = new TheAnimalKingdom.DBPanel();
            this.SuspendLayout();
            // 
            // _dbPanel1
            // 
            this._dbPanel1.BackColor = System.Drawing.Color.White;
            this._dbPanel1.Location = new System.Drawing.Point(8, 6);
            this._dbPanel1.Name = "_dbPanel1";
            this._dbPanel1.Size = new System.Drawing.Size(800, 600);
            this._dbPanel1.TabIndex = 0;
            this._dbPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this._dbPanel1_Paint);
            this._dbPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this._dbPanel1_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(817, 613);
            this.Controls.Add(this._dbPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Name = "Form1";
            this.Text = "The animal kingdom";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private DBPanel _dbPanel1;
    }
}

