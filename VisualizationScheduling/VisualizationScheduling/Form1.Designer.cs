namespace VisualizationScheduling
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID_RR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BurstTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WatingTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Priorty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RR_label2 = new System.Windows.Forms.Label();
            this.RR_label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID_RR,
            this.BurstTime,
            this.WatingTime,
            this.Priorty});
            this.dataGridView1.Location = new System.Drawing.Point(13, 12);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(471, 248);
            this.dataGridView1.TabIndex = 5;
            // 
            // ID_RR
            // 
            this.ID_RR.Name = "ID_RR";
            // 
            // BurstTime
            // 
            this.BurstTime.Name = "BurstTime";
            // 
            // WatingTime
            // 
            this.WatingTime.Name = "WatingTime";
            // 
            // Priorty
            // 
            this.Priorty.Name = "Priorty";
            // 
            // RR_label2
            // 
            this.RR_label2.AutoSize = true;
            this.RR_label2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.RR_label2.Location = new System.Drawing.Point(25, 492);
            this.RR_label2.Name = "RR_label2";
            this.RR_label2.Size = new System.Drawing.Size(159, 20);
            this.RR_label2.TabIndex = 8;
            this.RR_label2.Text = "평균대기시간 : ";
            // 
            // RR_label
            // 
            this.RR_label.AutoSize = true;
            this.RR_label.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.RR_label.Location = new System.Drawing.Point(25, 453);
            this.RR_label.Name = "RR_label";
            this.RR_label.Size = new System.Drawing.Size(138, 20);
            this.RR_label.TabIndex = 7;
            this.RR_label.Text = "출력데이터 : ";
            // 
            // panel1
            // 
            this.panel1.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(471, 176);
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(13, 266);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(471, 175);
            this.panel1.TabIndex = 9;
            this.panel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel1_Scroll);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(26, 530);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "TimeQuantum : ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 565);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RR_label2);
            this.Controls.Add(this.RR_label);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_FCFS;
        private System.Windows.Forms.DataGridViewTextBoxColumn BurstTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn WatingTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Priorty;
        private System.Windows.Forms.Label RR_label2;
        private System.Windows.Forms.Label RR_label;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_RR;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}