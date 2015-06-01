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
        public List<Result> fcfs, srt,sjf;
        public Visualization_Scheduling main = new Visualization_Scheduling();
        int j = 0;
        public static int preY;
        public Boolean flag = false;
        string[] strvalue;
        int[] value;
        Boolean[] Schedul = new Boolean[5];
        enum Calor { Red = 1, Yellow = 2, Blue = 3 };
        

        public Sub_From()
        {
            InitializeComponent();
            oList = new List<Process>();
        }

        public Sub_From(Visualization_Scheduling _form)
        {
            InitializeComponent();
            oList = new List<Process>();
            fcfs = new List<Result>();
            srt = new List<Result>();
            sjf = new List<Result>();
            main = _form;

            //변수 정의
            if (main.Schedul[0] == true)//fCFS
            {
                for (int i = 0; i < main.pList.Count; i++)
                {
                    Process p = new Process(main.pList.ElementAt(i).ProcessID, main.pList.ElementAt(i).ArriveTime, main.pList.ElementAt(i).BurstTime, main.pList.ElementAt(i).Priority);
                    oList.Add(p);
                }
                strvalue = new string[oList.Count];
                value = new int[oList.Count];
                fcfs = FCFS.Run(oList, fcfs); //fcfs클래스의 Run메소드 호출 //fcfs는 Result클래스로 이루어진 리스트
                dataGridView1.Rows.Clear();
                string[] row = new string[4];
                double watingTime = 0.0;
                j = 0;
                foreach (Result r in fcfs)
                {
                    row[0] = r.processID.ToString();
                    row[1] = r.burstTime.ToString();
                    row[2] = r.waitingTime.ToString();
                    row[3] = r.Priority.ToString();
                    strvalue[j] = r.processID.ToString();
                    value[j] = r.waitingTime;
                    watingTime += r.waitingTime;
                    dataGridView1.Rows.Add(row);
                    j++;
                }
                j = 0;
            }
            if (main.Schedul[1] == true)//SRT
            {
                for (int i = 0; i < main.pList.Count; i++)
                {
                    Process p = new Process(main.pList.ElementAt(i).ProcessID, main.pList.ElementAt(i).ArriveTime, main.pList.ElementAt(i).BurstTime, main.pList.ElementAt(i).Priority);
                    oList.Add(p);
                } 
                
                srt = SRT.Run(oList, srt); //SRT클래스의 Run메소드 호출 //SRT는 Result클래스로 이루어진 리스트
                string[] srow = { "", "", "", "" };
                double watingTime = 0.0; int[] value2 = new int[oList.Count];
                string[] strvalue2 = new string[oList.Count];
                foreach (Result e in srt)
                {
                    srow[0] = e.processID.ToString();
                    srow[1] = e.burstTime.ToString();
                    srow[2] = e.waitingTime.ToString();
                    srow[3] = e.Priority.ToString();
                    strvalue2[j] = srow[0];
                    value2[j] = e.waitingTime;
                    watingTime += e.waitingTime;
                    dataGridView2.Rows.Add(srow);
                    j++;
                }
                j = 0;
                
            }
            if (main.Schedul[2] == true)//SJF
            {
                for (int i = 0; i < main.pList.Count; i++)
                {
                    Process p = new Process(main.pList.ElementAt(i).ProcessID, main.pList.ElementAt(i).ArriveTime, main.pList.ElementAt(i).BurstTime, main.pList.ElementAt(i).Priority);
                    oList.Add(p);
                }
                sjf = SJF.Run(oList, sjf); //SRT클래스의 Run메소드 호출 //SRT는 Result클래스로 이루어진 리스트
                string[] srow = { "", "", "", "" };
                string[] strvalue3 = new string[oList.Count];
                int[] value3 = new int[oList.Count];
                double watingTime = 0.0;
                foreach (Result sjff in sjf)
                {
                    srow[0] = sjff.processID.ToString();
                    srow[1] = sjff.burstTime.ToString();
                    srow[2] = sjff.waitingTime.ToString();
                    srow[3] = sjff.Priority.ToString();
                    watingTime += sjff.waitingTime;
                    value[j] = sjff.waitingTime;
                    dataGridView3.Rows.Add(srow);
                    j++;
                }
                j = 0;
                SJF_label.Text = "전체 실행시간: " + (sjf[fcfs.Count - 1].startP + sjf[fcfs.Count - 1].burstTime).ToString();
                SJF_label2.Text = "평균 대기시간: " + (watingTime / sjf.Count).ToString();
            }
            if (main.Schedul[4] == true)//Priorty
            {

            }
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
            if (main.Schedul[0] == false)
            {
                return;
            }
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
                e.Graphics.DrawString("P" + r.processID.ToString(), Font, Brushes.Black, startPosition + (r.startP * 5), resultListPosition);
                e.Graphics.FillRectangle(brushBack, startPosition + (r.startP * 5), resultListPosition + 20, r.burstTime * 5, 60);
                e.Graphics.DrawRectangle(Pens.Black, startPosition + (r.startP * 5), resultListPosition + 20, r.burstTime * 5, 60);
                e.Graphics.DrawString(r.burstTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 5), resultListPosition + 90);
                e.Graphics.DrawString(r.waitingTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 5), resultListPosition + 110);
                waitingTime += (double)r.waitingTime;

            }
            FCFS_label.Text = "전체 실행시간: " + (fcfs[fcfs.Count - 1].startP + fcfs[fcfs.Count - 1].burstTime).ToString();
            FCFS_label2.Text = "평균 대기시간: " + (waitingTime / fcfs.Count).ToString();
            Page_Load(sender, e);
            chart1_Load(sender, e);
            main.Schedul[0] = false;
        }

        public void Page_Load(object sender, EventArgs e)
        {
                for (int i = 0; i < j; i++)
                {
                    if (value[i] == 0)
                    {
                        continue;
                    }
                    FCFS_Chart2.Series[0].Points.Add(value[i]);
                }
                FCFS_Chart2.Series[0]["PieDrawingStyle"] = "Concave";
                FCFS_Chart2.Series[0]["DounutRadius"] = "40";
        }
        public void chart1_Load(object sender, EventArgs e)
        {
            FCFS_Chart2.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            FCFC_Chart.Series.Clear();
            for (int i = 0; i < j; i++)
            {
                System.Windows.Forms.DataVisualization.Charting.Series se = new
                System.Windows.Forms.DataVisualization.Charting.Series();
                se.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                se.Name = strvalue[i];
                se.Points.Add(value[i]);
                FCFC_Chart.Series.Add(se);
            }
        }
        private void FCFS_Chart2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    FCFS_Chart2.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    FCFS_Chart2.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                }

                if (e.Delta > 0)
                {
                    double xMin = FCFS_Chart2.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    double xMax = FCFS_Chart2.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    double yMin = FCFS_Chart2.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    double yMax = FCFS_Chart2.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

                    double posXStart = FCFS_Chart2.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    double posXFinish = FCFS_Chart2.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    double posYStart = FCFS_Chart2.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    double posYFinish = FCFS_Chart2.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                    FCFS_Chart2.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                    FCFS_Chart2.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { } 
        }
    }
}
