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
        int a = 0;
        public int prex;
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
                FCFS_label.Text = "전체 실행시간: " + (fcfs[fcfs.Count - 1].startP + fcfs[fcfs.Count - 1].burstTime).ToString();
                FCFS_label2.Text = "평균 대기시간: " + (watingTime / fcfs.Count).ToString();
                prex = fcfs[fcfs.Count - 1].startP + fcfs[fcfs.Count - 1].burstTime;
                FCFS_panel.AutoScrollMinSize = new Size((prex * 11), FCFS_panel.Size.Height + 1);
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
            if (main.Schedul[1] == true)//SRT
            {
                for (int i = 0; i < main.pList.Count; i++)
                {
                    Process p = new Process(main.pList.ElementAt(i).ProcessID, main.pList.ElementAt(i).ArriveTime, main.pList.ElementAt(i).BurstTime, main.pList.ElementAt(i).Priority);
                    oList.Add(p);
                }
                srt = SRT.Run(oList, srt); //SRT클래스의 Run메소드 호출 //SRT는 Result클래스로 이루어진 리스트
                double watingTime = 0.0; int[] value2 = new int[srt.Count];
                string[] strvalue2 = new string[srt.Count];
                string[] srow = { "", "", "", "" };

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
                for (int i = 0; i < j; i++)
                {
                    System.Windows.Forms.DataVisualization.Charting.Series se = new
                    System.Windows.Forms.DataVisualization.Charting.Series();
                    se.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    se.Name = strvalue2[i];
                    se.Points.Add(value2[i]);
                    SRT_Chart.Series.Add(se);
                }
                for (int i = 0; i < j; i++)
                {
                    if (value2[i] == 0)
                    {
                        continue;
                    }
                    SRT_Chart2.Series[0].Points.Add(value2[i]);
                }
                SJF_Chart2.Series[0]["PieDrawingStyle"] = "Concave";
                SJF_Chart2.Series[0]["DounutRadius"] = "40";
                j = 0;
                SRT_label.Text = "전체 실행시간: " + (srt[sjf.Count - 1].startP + srt[sjf.Count - 1].burstTime).ToString();
                SRT_label2.Text = "평균 대기시간: " + (watingTime / sjf.Count).ToString();
                prex = srt[srt.Count - 1].startP + srt[srt.Count - 1].burstTime;
                SRT_panel.AutoScrollMinSize = new Size((prex * 11), SRT_panel.Size.Height + 1);

            }
            if (main.Schedul[2] == true)//SJF
            {
                for (int i = 0; i < main.pList.Count; i++)
                {
                    Process p = new Process(main.pList.ElementAt(i).ProcessID, main.pList.ElementAt(i).ArriveTime, main.pList.ElementAt(i).BurstTime, main.pList.ElementAt(i).Priority);
                    oList.Add(p);
                }
                string[] strvalue3 = new string[oList.Count];
                int[] value3 = new int[oList.Count];
                sjf = SJF.Run(oList, sjf); //SRT클래스의 Run메소드 호출 //SRT는 Result클래스로 이루어진 리스트
                string[] srow = { "", "", "", "" };
                double watingTime = 0.0;
                foreach (Result sjff in sjf)
                {
                    srow[0] = sjff.processID.ToString();
                    srow[1] = sjff.burstTime.ToString();
                    srow[2] = sjff.waitingTime.ToString();
                    srow[3] = sjff.Priority.ToString();
                    strvalue3[j] = sjff.processID.ToString();
                    watingTime += sjff.waitingTime;
                    value3[j] = sjff.waitingTime;
                    dataGridView3.Rows.Add(srow);
                    j++;
                }
                for (int i = 0; i < j; i++)
                {
                    System.Windows.Forms.DataVisualization.Charting.Series se = new
                    System.Windows.Forms.DataVisualization.Charting.Series();
                    se.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    se.Name = strvalue3[i];
                    se.Points.Add(value3[i]);
                    SJF_Chart.Series.Add(se);
                }
                for (int i = 0; i < j; i++)
                {
                    if (value3[i] == 0)
                    {
                        continue;
                    }
                    SJF_Chart2.Series[0].Points.Add(value3[i]);
                }
                SJF_Chart2.Series[0]["PieDrawingStyle"] = "Concave";
                SJF_Chart2.Series[0]["DounutRadius"] = "40";
                j = 0;
                SJF_label.Text = "전체 실행시간: " + (sjf[sjf.Count - 1].startP + sjf[sjf.Count - 1].burstTime).ToString();
                SJF_label2.Text = "평균 대기시간: " + (watingTime / sjf.Count).ToString();
                prex = sjf[sjf.Count - 1].startP + sjf[sjf.Count - 1].burstTime;
                SJF_panel.AutoScrollMinSize = new Size((prex * 11), SJF_panel.Size.Height + 1);


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
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (main.Schedul[0] == false)
            {
                return;
            }
            int startPosition = 10;
            double waitingTime = 0.0;
            Invalidate();
            int resultListPosition = 0;
            foreach (Result r in fcfs)
            {
                e.Graphics.DrawString("p" + r.processID.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10)-a, resultListPosition);
                e.Graphics.DrawRectangle(Pens.Red, startPosition + (r.startP * 10)-a, resultListPosition + 20, r.burstTime * 10, 30);
                e.Graphics.DrawString(r.burstTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10)-a, resultListPosition + 60);
                e.Graphics.DrawString(r.waitingTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10)-a, resultListPosition + 80);
                waitingTime += (double)r.waitingTime;
            }
            main.Schedul[0] = true;
        }
        private void SRT_panel_Paint(object sender, PaintEventArgs e)
        {
            if (main.Schedul[1] == true)
            {
                int startPosition = 10;
                double waitingTime = 0.0;
                Invalidate();
                int resultListPosition = 0;
                foreach (Result r in srt)
                {
                    e.Graphics.DrawString("p" + r.processID.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - a, resultListPosition);
                    e.Graphics.DrawRectangle(Pens.Red, startPosition + (r.startP * 10) - a, resultListPosition + 20, r.burstTime * 10, 30);
                    e.Graphics.DrawString(r.burstTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - a, resultListPosition + 60);
                    e.Graphics.DrawString(r.waitingTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - a, resultListPosition + 80);
                    waitingTime += (double)r.waitingTime;
                }
                main.Schedul[1] = false;
            }
        }
        private void SJF_panel_Paint(object sender, PaintEventArgs e)
        {
            if (main.Schedul[2] == true)
            {
                int startPosition = 10;
                double waitingTime = 0.0;
                Invalidate();
                int resultListPosition = 0;
                foreach (Result r in sjf)
                {
                    e.Graphics.DrawString("p" + r.processID.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10)-a, resultListPosition);
                    e.Graphics.DrawRectangle(Pens.Red, startPosition + (r.startP * 10) - a, resultListPosition + 20, r.burstTime * 10, 30);
                    e.Graphics.DrawString(r.burstTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - a, resultListPosition + 60);
                    e.Graphics.DrawString(r.waitingTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - a, resultListPosition + 80);
                    waitingTime += (double)r.waitingTime;
                }
                main.Schedul[2] = false;
            }
        }

        private void FCFS_panel_Scroll(object sender, ScrollEventArgs e)
        {
            a = e.NewValue;
            this.Invalidate();
        }

        private void SRT_panel_Scroll(object sender, ScrollEventArgs e)
        {
            a = e.NewValue;
            this.Invalidate();
        }

        private void SJF_panel_Scroll(object sender, ScrollEventArgs e)
        {
            a = e.NewValue;
            this.Invalidate();
        }
    }
}
