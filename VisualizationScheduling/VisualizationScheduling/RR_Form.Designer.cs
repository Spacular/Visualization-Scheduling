namespace VisualizationScheduling
{
    partial class RR_Form
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
            this.RR_panel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // RR_panel
            // 
            this.RR_panel.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.RR_panel.AutoScroll = true;
            this.RR_panel.BackColor = System.Drawing.Color.White;
            this.RR_panel.Location = new System.Drawing.Point(13, 194);
            this.RR_panel.Name = "RR_panel";
            this.RR_panel.Size = new System.Drawing.Size(603, 164);
            this.RR_panel.TabIndex = 26;
            // 
            // RR_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 552);
            this.Controls.Add(this.RR_panel);
            this.Name = "RR_Form";
            this.Text = "RR_Form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel RR_panel;
    }
}