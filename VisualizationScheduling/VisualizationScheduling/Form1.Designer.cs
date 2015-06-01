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
            this.ID_FCFS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BurstTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WatingTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Priorty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FCFS_label2 = new System.Windows.Forms.Label();
            this.FCFS_label = new System.Windows.Forms.Label();
            this.RR_panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID_FCFS,
            this.BurstTime,
            this.WatingTime,
            this.Priorty});
            this.dataGridView1.Location = new System.Drawing.Point(13, 12);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(471, 248);
            this.dataGridView1.TabIndex = 5;
            // 
            // ID_FCFS
            // 
            this.ID_FCFS.Name = "ID_RR";
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
            // FCFS_label2
            // 
            this.FCFS_label2.AutoSize = true;
            this.FCFS_label2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FCFS_label2.Location = new System.Drawing.Point(25, 492);
            this.FCFS_label2.Name = "RR_label2";
            this.FCFS_label2.Size = new System.Drawing.Size(159, 20);
            this.FCFS_label2.TabIndex = 8;
            this.FCFS_label2.Text = "평균대기시간 : ";
            // 
            // FCFS_label
            // 
            this.FCFS_label.AutoSize = true;
            this.FCFS_label.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FCFS_label.Location = new System.Drawing.Point(25, 453);
            this.FCFS_label.Name = "RR_label";
            this.FCFS_label.Size = new System.Drawing.Size(138, 20);
            this.FCFS_label.TabIndex = 7;
            this.FCFS_label.Text = "출력데이터 : ";
            // 
            // RR_panel
            // 
            this.RR_panel.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.RR_panel.AutoScroll = true;
            this.RR_panel.AutoScrollMinSize = new System.Drawing.Size(603, 164);
            this.RR_panel.BackColor = System.Drawing.Color.White;
            this.RR_panel.Location = new System.Drawing.Point(12, 266);
            this.RR_panel.Name = "RR_panel";
            this.RR_panel.Size = new System.Drawing.Size(472, 164);
            this.RR_panel.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 565);
            this.Controls.Add(this.FCFS_label2);
            this.Controls.Add(this.FCFS_label);
            this.Controls.Add(this.RR_panel);
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
        private System.Windows.Forms.Label FCFS_label2;
        private System.Windows.Forms.Label FCFS_label;
        private System.Windows.Forms.Panel RR_panel;
    }
}