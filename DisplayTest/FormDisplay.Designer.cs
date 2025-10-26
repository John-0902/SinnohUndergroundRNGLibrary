using System.Windows.Forms;

namespace DisplayTest
{
    partial class FormDisplay
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbWall = new System.Windows.Forms.PictureBox();
            this.pbCover = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbWall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCover)).BeginInit();
            this.SuspendLayout();
            // 
            // pbWall
            // 
            this.pbWall.Location = new System.Drawing.Point(0, 0);
            this.pbWall.Name = "pbWall";
            this.pbWall.Size = new System.Drawing.Size(416, 320);
            this.pbWall.TabIndex = 0;
            this.pbWall.TabStop = false;
            // 
            // pbCover
            // 
            this.pbCover.Location = new System.Drawing.Point(416, 0);
            this.pbCover.Name = "pbCover";
            this.pbCover.Size = new System.Drawing.Size(416, 320);
            this.pbCover.TabIndex = 1;
            this.pbCover.TabStop = false;
            // 
            // FormDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 320);
            this.Controls.Add(this.pbCover);
            this.Controls.Add(this.pbWall);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormDisplay";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormDisplay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbWall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCover)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbWall;
        private System.Windows.Forms.PictureBox pbCover;
    }
}

