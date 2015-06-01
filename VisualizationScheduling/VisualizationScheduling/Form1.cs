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
        public List<Result_double> rr;
        public Visualization_Scheduling main = new Visualization_Scheduling();
        int j = 0;
        int k = 0;
        public Boolean flag = false;
        string[] strvalue;
        double[] value;
        Boolean[] Schedul = new Boolean[5];
        enum Calor { Red = 1, Yellow = 2, Blue = 3 };

        public Form1()
        {
            InitializeComponent();
        }
        public Form1(Visualization_Scheduling _form)
        {
            InitializeComponent();
            int count = 0;
            rr = new List<Result_double>();
            main = _form;
            int time = main.TimeQuntam;
            int runtime = 0;
            oList = new List<Process>();
            for (int i = 0; i < main.pList.Count; i++)
                {
                    Process p = new Process(main.pList.ElementAt(i).ProcessID, main.pList.ElementAt(i).ArriveTime, main.pList.ElementAt(i).BurstTime, main.pList.ElementAt(i).Priority);
                    oList.Add(p);
                }
            strvalue = new string[oList.Count];
            value = new double[oList.Count];
            bool[] flag = new bool[oList.Count];
            rr = RR.Run(oList, rr,time); //fcfs클래스의 Run메소드 호출 //fcfs는 Result클래스로 이루어진 리스트
            for (int i = 0; i < rr.Count; i++)
            {
                for (j = i + 1; j < rr.Count; j++)
                {
                    if (rr.ElementAt(i).processID == rr.ElementAt(j).processID)
                    {
                        rr.ElementAt(i).startP++;
                    }
                }
            }

            for (int i = 0; i < rr.Count; i++)
            {
                for (j = 0; j < rr.Count; j++)
                {
                    if (rr.ElementAt(i).processID != rr.ElementAt(j).processID)
                    {
                        if (rr.ElementAt(i).startP < 0)
                        {
                            continue;
                        }
                        else
                        {
                            rr.ElementAt(i).waitingTime = rr.ElementAt(i).waitingTime + rr.ElementAt(j).burstTime + 0.1;
                        }
                    }
                    else
                    {
                        if (rr.ElementAt(i).startP == 0)
                        {
                            break;
                        }
                        rr.ElementAt(i).startP--;
                    }
                }

            }
            for (int i = 0; i < rr.Count; i++)
            {
                for (j = i + 1; j < rr.Count; j++)
                {
                    if (rr.ElementAt(i).processID == rr.ElementAt(j).processID)
                    {
                        rr.ElementAt(i).burstTime = rr.ElementAt(i).burstTime + rr.ElementAt(j).burstTime;
                    }
                }
            }

            count = rr.Count;
            int k = oList.Count;
            for (j = k; j < count; j++)
            {
                rr.RemoveAt(k);
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
            RR_label.Text = "RR전체 실행시간: " + busrtime.ToString();
            RR_label2.Text = "평균 대기시간: " + (watingTime / rr.Count).ToString();
        }

    }
}
