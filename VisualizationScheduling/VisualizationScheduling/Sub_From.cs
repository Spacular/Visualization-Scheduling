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
        public List<Result_double> rr, rr_dataview;
        public Visualization_Scheduling main = new Visualization_Scheduling();
        int j = 0;
        int a = 0;
        public int prex;
        public Boolean flag = false;
        string[] strvalue;
        int[] value;
        string[] strvalue_rr;
        double[] value_rr;
        Boolean[] Schedul = new Boolean[5];
        enum Calor { Red = 1, Yellow = 2, Blue = 3 };
        public Sub_From()
        {
            InitializeComponent();
            oList = new List<Process>();
            this.MouseWheel += new MouseEventHandler(Sub_From_MouseMove);
        }
        private void Sub_From_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                Console.Out.WriteLine(e.Delta);
            }
        }

        public Sub_From(Visualization_Scheduling _form)
        {
            InitializeComponent();
            oList = new List<Process>();
            fcfs = new List<Result>();
            srt = new List<Result>();
            sjf = new List<Result>();
            rr = new List<Result_double>();
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

                ////////////////////////////
                int[] wT = new int[oList.Count];

                for (int i = 0; i < srt.Count; i++)
                {
                    if (wT[srt.ElementAt(i).processID] < srt.ElementAt(i).waitingTime)
                    {
                        wT[srt.ElementAt(i).processID] = srt.ElementAt(i).waitingTime;
                    }
                }
                ////////////////////////////

                j = 0;
                foreach (Result e in srt)
                {
                    srow[0] = e.processID.ToString();
                    srow[1] = e.burstTime.ToString();
                    srow[2] = e.waitingTime.ToString();
                    srow[3] = e.Priority.ToString();
                    strvalue2[j] = srow[0];
                    value2[j] = wT[j];//////
                    watingTime += wT[j];////
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
                for (int i = 0; i < srt.Count; i++)
                {
                    j += srt.ElementAt(i).burstTime;
                }
                SRT_label.Text = "전체 실행시간: " + (j).ToString();
                SRT_label2.Text = "평균 대기시간: " + (watingTime / srt.Count).ToString();
               // prex = srt[srt.Count - 1].startP + srt[srt.Count - 1].burstTime;
                SRT_panel.AutoScrollMinSize = new Size((prex * 11), SRT_panel.Size.Height + 1);

            }
            if (main.Schedul[2] == true)//SJF
            {
                for (int i = 0; i < main.pList.Count; i++)
                {
                    Process p = new Process(main.pList.ElementAt(i).ProcessID, main.pList.ElementAt(i).ArriveTime, main.pList.ElementAt(i).BurstTime, main.pList.ElementAt(i).Priority);
                    oList.Add(p);
                }
                sjf = SJF.Run(oList, sjf); //SRT클래스의 Run메소드 호출 //SRT는 Result클래스로 이루어진 리스트
                string[] strvalue3 = new string[sjf.Count];
                int[] value3 = new int[sjf.Count];
                string[] srow = { "", "", "", "" };
                double watingTime = 0.0;
                j = 0;
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
            if (main.Schedul[3] == true)//RR
            {
                int time = main.TimeQuntam;
                oList = new List<Process>();
                for (int i = 0; i < main.pList.Count; i++)
                {
                    Process p = new Process(main.pList.ElementAt(i).ProcessID, main.pList.ElementAt(i).ArriveTime, main.pList.ElementAt(i).BurstTime, main.pList.ElementAt(i).Priority);
                    oList.Add(p);
                }
                rr = RR.Run(oList, rr, time); //fcfs클래스의 Run메소드 호출 //fcfs는 Result클래스로 이루어진 리스트
                bool[] flag = new bool[oList.Count];
                strvalue_rr = new string[rr.Count];
                value_rr = new double[rr.Count];
                dataGridView1.Rows.Clear();
                string[] row = new string[4];
                double watingTime = 0.0;
                double busrtime = 0;
                j = 0;

                foreach (Result_double r in rr)
                {

                    row[0] = r.processID.ToString();
                    row[1] = r.burstTime.ToString();
                    row[2] = r.waitingTime.ToString();
                    row[3] = r.Priority.ToString();
                    strvalue_rr[j] = row[0];
                    value_rr[j] = r.waitingTime;
                    busrtime += r.burstTime;
                    watingTime += r.waitingTime;
                    dataGridView6.Rows.Add(row);
                    j++;
                }
                int prex = rr[rr.Count - 1].startP + rr[rr.Count - 1].burstTime;
                rr_panel.AutoScrollMinSize = new Size((prex * 11), rr_panel.Size.Height + 1);
                rr_label.Text = "RR전체 실행시간: " + busrtime.ToString();
                rr_label2.Text = "평균 대기시간: " + watingTime.ToString();
                rr_label3.Text = "TimeQuantum: " + main.TimeQuntam.ToString();
            }
            if (main.Schedul[4] == true)//Priorty 선점
            {

            }
            if (main.Schedul[5] == true)//Priorty 비선점
            {

            }
        }
        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
               
            SaveFileDialog savePanel = new SaveFileDialog();
            savePanel.InitialDirectory = @"d:\";
            savePanel.Filter = "CustomFile (*.cus)|*.cus|txt (*.txt)|*.txt | All files (*.*)|(*.*)";
            if (savePanel.ShowDialog() == DialogResult.OK)
            {
               
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

        }
        private void SRT_panel_Paint(object sender, PaintEventArgs e)
        {
            if (main.Schedul[1] == false)
            {
                return;
            }
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
        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            if (main.Schedul[3] == false)
            {
                return;
            }
            int startPosition = 10;
            double waitingTime = 0.0;
            int resultListPosition = 0;
            if (main.Schedul[3] == true)
            {
                foreach (Result_double r in rr)
                {
                    e.Graphics.DrawString("p" + r.processID.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - a, resultListPosition);
                    e.Graphics.DrawRectangle(Pens.Red, startPosition + (r.startP * 10) - a, resultListPosition + 20, r.burstTime * 10, 30);
                    e.Graphics.DrawString(r.burstTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - a, resultListPosition + 60);
                    waitingTime += (double)r.waitingTime;
                }
            }
        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            a = e.NewValue;
            this.Invalidate();
        }

        private void SJF_panel_Paint_1(object sender, PaintEventArgs e)
        {
            if (main.Schedul[2] == false)
            {
                return;
            }
            int startPosition = 10;
            double waitingTime = 0.0;
            Invalidate();
            int resultListPosition = 0;
            foreach (Result r in sjf)
            {
                e.Graphics.DrawString("p" + r.processID.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - a, resultListPosition);
                e.Graphics.DrawRectangle(Pens.Red, startPosition + (r.startP * 10) - a, resultListPosition + 20, r.burstTime * 10, 30);
                e.Graphics.DrawString(r.burstTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - a, resultListPosition + 60);
                e.Graphics.DrawString(r.waitingTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - a, resultListPosition + 80);
                waitingTime += (double)r.waitingTime;
            }
        }

        private void SJF_panel_Scroll_1(object sender, ScrollEventArgs e)
        {
            a = e.NewValue;
            this.Invalidate();
        }

    }
}
