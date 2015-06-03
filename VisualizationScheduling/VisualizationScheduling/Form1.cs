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
    public partial class Form1 : Form
    {
        public List<Process> oList, pView;
        public List<Result_double> rr, rr_dataview;
        public Visualization_Scheduling main = new Visualization_Scheduling();
        int j = 0;
        int a = 0;
        public Boolean flag = false;
        string[] strvalue;
        double[] value;
        enum Calor { Red = 1, Yellow = 2, Blue = 3 };

        public Form1()
        {
            InitializeComponent();
        }
        public Form1(Visualization_Scheduling _form)
        {
            InitializeComponent();
            rr = new List<Result_double>();
            rr_dataview = new List<Result_double>();
            main = _form;
            double context_Swich;
            int time = main.TimeQuntam;
            oList = new List<Process>();
            for (int i = 0; i < main.pList.Count; i++)
            {
                Process p = new Process(main.pList.ElementAt(i).ProcessID, main.pList.ElementAt(i).ArriveTime, main.pList.ElementAt(i).BurstTime, main.pList.ElementAt(i).Priority);
                oList.Add(p);
            }
            rr = RR.Run(oList, rr, time); //fcfs클래스의 Run메소드 호출 //fcfs는 Result클래스로 이루어진 리스트
            bool[] flag = new bool[oList.Count];
            strvalue = new string[rr.Count];
            value = new double[rr.Count];

            for (int i = 0; i < rr.Count; i++)
            {
                for (j = i + 1; j < rr.Count; j++)
                {
                    if (rr.ElementAt(i).processID == rr.ElementAt(j).processID)
                    {
                        rr.ElementAt(i).burstTime = rr.ElementAt(i).burstTime + rr.ElementAt(j).burstTime;
                        if (rr.ElementAt(i).waitingTime < rr.ElementAt(j).waitingTime)
                        {
                            rr.ElementAt(i).waitingTime = rr.ElementAt(j).waitingTime;
                        }
                    }
                }
            }
            context_Swich = 0.1 * rr.Count - 1;

            for (int i = 0; i < rr.Count; i++)
            {
                if (flag[rr.ElementAt(i).processID - 1] == false)
                {
                    Result_double p = new Result_double(rr.ElementAt(i).processID, rr.ElementAt(i).startP, rr.ElementAt(i).burstTime, rr.ElementAt(i).waitingTime, rr.ElementAt(i).Priority, rr.ElementAt(i).same);
                    rr_dataview.Add(p);
                    flag[rr.ElementAt(i).processID - 1] = true;
                }
                else
                {
                    continue;
                }
            }
        }
        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            int startPosition = 10;
            double waitingTime = 0.0;
            int resultListPosition = 0;
            foreach (Result_double r in rr)
            {
                e.Graphics.DrawString("p" + r.processID.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - a, resultListPosition);
                e.Graphics.DrawRectangle(Pens.Red, startPosition + (r.startP * 10) - a, resultListPosition + 20, r.burstTime * 10, 30);
                e.Graphics.DrawString(r.burstTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10) - a, resultListPosition + 60);
                waitingTime += (double)r.waitingTime;
            }

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
                strvalue[j] = row[0];
                value[j] = r.waitingTime;
                busrtime += r.burstTime;
                watingTime += r.waitingTime;
                dataGridView1.Rows.Add(row);
                j++;
            }
            int prex = rr[rr.Count - 1].startP + rr[rr.Count - 1].burstTime;
            panel1.AutoScrollMinSize = new Size((prex * 11), panel1.Size.Height + 1);
            RR_label.Text = "RR전체 실행시간: " + busrtime.ToString();
            RR_label2.Text = "평균 대기시간: " + (watingTime / rr_dataview.Count).ToString();
            label1.Text = "TimeQuantum: " + main.TimeQuntam.ToString();
        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            a = e.NewValue;
            this.Invalidate();
        }
    }
}
