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
        public List<Result> fcfs, srt, sjf, priorty, priority_preemptive;
        public List<Result_double> rr, rr_dataview;
        public Visualization_Scheduling main = new Visualization_Scheduling();
        int j = 0;
        int x = 0;
        public int prex;
        Boolean[] Schedul = new Boolean[6];
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
            priorty = new List<Result>();
            priority_preemptive = new List<Result>();
            rr = new List<Result_double>();
           
            main = _form;

            if (main.Schedul[0] == true)//fCFS
            {
                for (int i = 0; i < main.pList.Count; i++)
                {
                    Process p = new Process(main.pList.ElementAt(i).ProcessID, main.pList.ElementAt(i).ArriveTime, main.pList.ElementAt(i).BurstTime, main.pList.ElementAt(i).Priority);
                    oList.Add(p);
                }
                fcfs = FCFS.Run(oList);
                string[] row = new string[4];
                double watingTime = 0.0;
                j = 0;
                dataGridView1.Rows.Clear();
                FCFC_Chart.Series.Clear();
                int burstime = 0;
                foreach (Result r in fcfs)
                {
                    row[0] = r.processID.ToString();
                    row[1] = r.burstTime.ToString();
                    row[2] = r.waitingTime.ToString();
                    row[3] = r.Priority.ToString();
                    burstime += r.burstTime;
                    watingTime += r.waitingTime;
                    dataGridView1.Rows.Add(row);
                    j++;
                    if (r.waitingTime != 0)
                    {
                        System.Windows.Forms.DataVisualization.Charting.Series se = new System.Windows.Forms.DataVisualization.Charting.Series();
                        se.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                        se.Name = ("process" + r.processID.ToString()); 
                        se.Points.Add(r.waitingTime);
                        FCFC_Chart.Series.Add(se);
                        FCFS_Chart2.Series[0].Points.Add(r.waitingTime);
                        FCFS_Chart2.Series[0].Points.ElementAt(FCFS_Chart2.Series[0].Points.Count - 1).LegendText = ("process" + r.processID.ToString());
                    }
                }
                FCFS_Chart2.Series[0]["PieDrawingStyle"] = "Concave";
                FCFS_Chart2.Series[0]["DounutRadius"] = "40";
                FCFS_label.Text = "전체 실행시간: " + burstime.ToString();
                FCFS_label2.Text = "평균 대기시간: " + (watingTime / fcfs.Count).ToString();
                prex = fcfs[fcfs.Count - 1].startP + fcfs[fcfs.Count - 1].burstTime;
                FCFS_panel.AutoScrollMinSize = new Size((prex * 11), FCFS_panel.Size.Height + 1);

            }
            if (main.Schedul[1] == true)
            {
                for (int i = 0; i < main.pList.Count; i++)
                {
                    Process p = new Process(main.pList.ElementAt(i).ProcessID, main.pList.ElementAt(i).ArriveTime, main.pList.ElementAt(i).BurstTime, main.pList.ElementAt(i).Priority);
                    oList.Add(p);
                }
                int[] wT = new int[main.pList.Count];
                srt = SRT.Run(oList); 
                double watingTime = 0.0; int[] value2 = new int[srt.Count];
                string[] strvalue2 = new string[srt.Count];
                string[] srow = { "", "", "", "" };


                for (int i = 0; i < srt.Count; i++)
                {
                    if (wT[srt.ElementAt(i).processID - 1] < srt.ElementAt(i).waitingTime)
                    {
                        wT[srt.ElementAt(i).processID - 1] = srt.ElementAt(i).waitingTime;
                    }
                }

                j = 0;
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
                }
                SRT_Chart.Series.Clear();
                for (int i = 0; i < main.pList.Count; i++)
                {
                    if (wT[i] <= 0)
                    {
                        continue;
                    }
                    System.Windows.Forms.DataVisualization.Charting.Series se = new System.Windows.Forms.DataVisualization.Charting.Series();
                    se.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    se.Name = ("process" + (i+1).ToString());
                    se.Points.Add(wT[i]);
                    SRT_Chart.Series.Add(se);
                    SRT_Chart2.Series[0].Points.Add(wT[i]);
                    SRT_Chart2.Series[0].Points.ElementAt(SRT_Chart2.Series[0].Points.Count - 1).LegendText = ("process" + (i + 1)).ToString();
                }
                SRT_Chart2.Series[0]["PieDrawingStyle"] = "Concave";
                SRT_Chart2.Series[0]["DounutRadius"] = "40";
                j = 0;
                for (int i = 0; i < srt.Count; i++)
                {
                    j += srt.ElementAt(i).burstTime;
                }
                SRT_label.Text = "전체 실행시간: " + (j).ToString();
                SRT_label2.Text = "평균 대기시간: " + (watingTime / main.pList.Count).ToString();
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
                sjf = SJF.Run(oList); //SRT클래스의 Run메소드 호출 //SRT는 Result클래스로 이루어진 리스트
                string[] srow = { "", "", "", "" };
                double watingTime = 0.0;
                int burstime = 0;
                SJF_Chart.Series.Clear();
                foreach (Result sjff in sjf)
                {
                    srow[0] = sjff.processID.ToString();
                    srow[1] = sjff.burstTime.ToString();
                    srow[2] = sjff.waitingTime.ToString();
                    srow[3] = sjff.Priority.ToString();
                    watingTime += sjff.waitingTime;
                    burstime += sjff.burstTime;
                    dataGridView3.Rows.Add(srow);
                    if (sjff.waitingTime != 0)
                    {
                        System.Windows.Forms.DataVisualization.Charting.Series se = new System.Windows.Forms.DataVisualization.Charting.Series();
                        se.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                        se.Name = ("process" + sjff.processID.ToString());
                        se.Points.Add(sjff.waitingTime);
                        SJF_Chart.Series.Add(se);
                        SJF_Chart2.Series[0].Points.Add(sjff.waitingTime);
                        SJF_Chart2.Series[0].Points.ElementAt(SJF_Chart2.Series[0].Points.Count - 1).LegendText = ("process" + sjff.processID.ToString());
                    }

                }
                SJF_Chart2.Series[0]["PieDrawingStyle"] = "Concave";
                SJF_Chart2.Series[0]["DounutRadius"] = "40";
                SJF_label.Text = "전체 실행시간: " + burstime.ToString();
                SJF_label2.Text = "평균 대기시간: " + (watingTime / sjf.Count).ToString();
                prex = sjf[sjf.Count - 1].startP + sjf[sjf.Count - 1].burstTime;
                SJF_panel.AutoScrollMinSize = new Size((prex * 11), SJF_panel.Size.Height + 1);


            }
            if (main.Schedul[3] == true)//RR
            {
                int time = main.TimeQuntam;
                oList = new List<Process>();
                double[] wT = new double[main.pList.Count];
                for (int i = 0; i < main.pList.Count; i++)
                {
                    Process p = new Process(main.pList.ElementAt(i).ProcessID, main.pList.ElementAt(i).ArriveTime, main.pList.ElementAt(i).BurstTime, main.pList.ElementAt(i).Priority);
                    oList.Add(p);
                }
                rr = RR.Run(oList, time); //fcfs클래스의 Run메소드 호출 //fcfs는 Result클래스로 이루어진 리스트
                string[] row = new string[4];
                double watingTime = 0.0;
                double busrtime = 0;
                for (int i = 0; i < rr.Count; i++)
                {
                    if (wT[rr.ElementAt(i).processID - 1] < rr.ElementAt(i).waitingTime)
                    {
                        wT[rr.ElementAt(i).processID - 1] = rr.ElementAt(i).waitingTime;
                    }
                }
                j = 0;
                dataGridView6.Rows.Clear();
                foreach (Result_double r in rr)
                {
                    row[0] = r.processID.ToString();
                    row[1] = r.burstTime.ToString();
                    row[2] = r.waitingTime.ToString();
                    row[3] = r.Priority.ToString();
                    busrtime += r.burstTime;
                    watingTime += r.waitingTime;
                    dataGridView6.Rows.Add(row);
                }
                rr_chart.Series.Clear();
                for (int i = 0; i < main.pList.Count; i++)
                {
                    if (wT[i] == 0)
                    {
                        continue;
                    }
                    System.Windows.Forms.DataVisualization.Charting.Series se = new System.Windows.Forms.DataVisualization.Charting.Series();
                    se.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    se.Name = ("process" + (i + 1).ToString());
                    se.Points.Add(wT[i]);
                    rr_chart.Series.Add(se);
                    rr_chart2.Series[0].Points.Add(wT[i]);
                    rr_chart2.Series[0].Points.ElementAt(rr_chart2.Series[0].Points.Count - 1).LegendText = ("process" + (i + 1)).ToString();
                }
                rr_chart2.Series[0]["PieDrawingStyle"] = "Concave";
                rr_chart2.Series[0]["DounutRadius"] = "40";
                rr_label.Text = "전체 실행시간: " + busrtime.ToString();
                rr_label2.Text = "평균 대기시간: " + (watingTime / main.pList.Count).ToString();
                rr_label3.Text = "TimeQuantam: " + time.ToString();
                prex = rr[rr.Count - 1].startP + rr[rr.Count - 1].burstTime;
                rr_panel.AutoScrollMinSize = new Size((prex * 11), rr_panel.Size.Height + 1);
            }
            if (main.Schedul[4] == true)//Priorty 선점
            {
                oList = new List<Process>();
                for (int i = 0; i < main.pList.Count; i++)
                {
                    Process p = new Process(main.pList.ElementAt(i).ProcessID, main.pList.ElementAt(i).ArriveTime, main.pList.ElementAt(i).BurstTime, main.pList.ElementAt(i).Priority);
                    oList.Add(p);
                }
                priority_preemptive = Priority_Preemptive.Run(oList);
            }
            if (main.Schedul[5] == true)//Priorty 비선점
            {
                oList = new List<Process>();
                for (int i = 0; i < main.pList.Count; i++)
                {
                    Process p = new Process(main.pList.ElementAt(i).ProcessID, main.pList.ElementAt(i).ArriveTime, main.pList.ElementAt(i).BurstTime, main.pList.ElementAt(i).Priority);
                    oList.Add(p);
                }
                priorty = Priority.Run(oList);
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
                e.Graphics.DrawString("p" + r.processID.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10)-x, resultListPosition);
                e.Graphics.DrawRectangle(Pens.Red, startPosition + (r.startP * 10)-x, resultListPosition + 20, r.burstTime * 10, 30);
                e.Graphics.DrawString(r.burstTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10)-x, resultListPosition + 60);
                e.Graphics.DrawString(r.waitingTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10)-x, resultListPosition + 80);
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
                    e.Graphics.DrawString("p" + r.processID.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - x, resultListPosition);
                    e.Graphics.DrawRectangle(Pens.Red, startPosition + (r.startP * 10) - x, resultListPosition + 20, r.burstTime * 10, 30);
                    e.Graphics.DrawString(r.burstTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - x, resultListPosition + 60);
                    e.Graphics.DrawString(r.waitingTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - x, resultListPosition + 80);
                    waitingTime += (double)r.waitingTime;
                }
        }
        private void FCFS_panel_Scroll(object sender, ScrollEventArgs e)
        {
            x = e.NewValue;
            this.Invalidate();
        }
        private void rr_panel1_Paint(object sender, PaintEventArgs e)
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
                    e.Graphics.DrawString("p" + r.processID.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - x, resultListPosition);
                    e.Graphics.DrawRectangle(Pens.Red, startPosition + (r.startP * 10) - x, resultListPosition + 20, r.burstTime * 10, 30);
                    e.Graphics.DrawString(r.burstTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - x, resultListPosition + 60);
                    waitingTime += (double)r.waitingTime;
                }
            }
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
                e.Graphics.DrawString("p" + r.processID.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - x, resultListPosition);
                e.Graphics.DrawRectangle(Pens.Red, startPosition + (r.startP * 10) - x, resultListPosition + 20, r.burstTime * 10, 30);
                e.Graphics.DrawString(r.burstTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - x, resultListPosition + 60);
                e.Graphics.DrawString(r.waitingTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - x, resultListPosition + 80);
                waitingTime += (double)r.waitingTime;
            }
        }

    }
}
