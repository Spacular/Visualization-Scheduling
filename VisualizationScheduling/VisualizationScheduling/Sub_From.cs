using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VisualizationScheduling
{
    public partial class Sub_From : Form  {
        public List<Process> oList, pView;
        public List<Result> fcfs;
        public Visualization_Scheduling main = new Visualization_Scheduling();
        int j = 0;
        public Boolean flag = false;
        string[] strvalue = new string[100];
        int[] value = new int[100];
        Boolean[] Schedul = new Boolean[5];
        enum Calor { Red = 1, Yellow = 2, Blue = 3 };

        public Sub_From()
        {
            InitializeComponent();
        }
        public Sub_From(Visualization_Scheduling _form)
        {
            InitializeComponent();
            fcfs = new List<Result>();
            main = _form;
            oList = main.pList;
            fcfs = FCFS.Run(oList, fcfs);
            if (fcfs.Count == 0)
            {
                MessageBox.Show("Data가 없습니다.", "Error");
                return;
                
            }
            dataGridView1.Rows.Clear();
            string[] row = { "", "", ""};
            double watingTime = 0.0;
            foreach (Result r in fcfs)
            {
                row[0] = r.processID.ToString();
                row[1] = r.burstTime.ToString();
                row[2] = r.waitingTime.ToString();
                strvalue[j] = row[0];
                value[j] = Convert.ToInt32(row[2]);
                watingTime += r.waitingTime;
                dataGridView1.Rows.Add(row);
                j++;
            }

          /*  chart1.ChartAreas["Area"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["Area"].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas["Area"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["Area"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["Area"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["Area"].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas["Area"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["Area"].AxisY.ScrollBar.IsPositionedInside = true; */
        }
        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
               
            if (main.path == null)
            {
                MessageBox.Show("파일이 열려있지 않습니다.", "Save Error");
                return;
            }
        
        }


        private void saveAsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (main.path == null)
            {
                MessageBox.Show("파일이 열려있지 않습니다.", "Save Error");
                return;
            }
   
        }

        public void panel1_Paint(object sender, PaintEventArgs e)
        {
            int startPosition = 10;
            double waitingTime = 0.0;
            Random rand = new Random();
            byte[] colorBytes = new byte[3];
            rand.NextBytes(colorBytes);
            Color randomColor = Color.FromArgb(colorBytes[0], colorBytes[1], colorBytes[2]);
            
            SolidBrush brushBack = new SolidBrush(randomColor);
            
            int resultListPosition = 0;
            
            foreach (Result r in fcfs)
            {
                brushBack = new SolidBrush(randomColor);
                e.Graphics.DrawString("proc" + r.processID.ToString(), Font, Brushes.Black, startPosition + (r.startP * 14), resultListPosition);
                e.Graphics.FillRectangle(brushBack, startPosition + (r.startP * 14), resultListPosition + 20, r.burstTime * 14, 60);
                e.Graphics.DrawRectangle(Pens.Black, startPosition + (r.startP * 14), resultListPosition + 20, r.burstTime * 14, 60);
                e.Graphics.DrawString(r.burstTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 14), resultListPosition + 90);
                e.Graphics.DrawString(r.waitingTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 14), resultListPosition + 110);
                waitingTime += (double)r.waitingTime;

            }
            label1.Text = "전체 실행시간: " + (fcfs[fcfs.Count - 1].startP + fcfs[fcfs.Count - 1].burstTime).ToString();
            label2.Text = "평균 대기시간: " + (waitingTime / fcfs.Count).ToString();
            if (flag == true)
            {
                return;
            }
            Page_Load(sender, e);
            chart3_Load(sender, e);
            chart1_Load(sender, e);
            flag = true;
        }
        public void Page_Load(object sender, EventArgs e)
        {
                for (int i = 0; i < j; i++)
                {
                    if (value[i] == 0)
                    {
                        continue;
                    }
                    chart2.Series[0].Points.Add(value[i]);
                }
                chart2.Series[0]["PieDrawingStyle"] = "Concave";
                chart2.Series[0]["DounutRadius"] = "40";
        }
        public void chart3_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < j; i++)
            {
                if (value[i] == 0)
                {
                    continue;
                }
                chart3.Series[0].Points.Add(value[i]);
            }
            chart3.Series[0]["LineDrawingStyle"] = "Concave";
        }
        public void chart1_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            for (int i = 0; i < j; i++)
            {
                System.Windows.Forms.DataVisualization.Charting.Series se = new
                System.Windows.Forms.DataVisualization.Charting.Series();
                se.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                se.Name = strvalue[i];
                se.Points.Add(value[i]);
                chart1.Series.Add(se);
            }
        }
    }
}
