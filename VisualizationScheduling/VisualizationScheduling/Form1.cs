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
        public List<Result> rr;
        public Visualization_Scheduling main = new Visualization_Scheduling();
        int j = 0;
        int k = 0;
        public Boolean flag = false;
        string[] strvalue = new string[100];
        int[] value = new int[100];
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
            rr = new List<Result>();
            main = _form;
            oList = main.pList;
            rr = RR.Run(oList, rr); //fcfs클래스의 Run메소드 호출 //fcfs는 Result클래스로 이루어진 리스트
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
                            rr.ElementAt(i).waitingTime = rr.ElementAt(i).waitingTime + rr.ElementAt(j).burstTime;
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
            string[] row = { "", "", "", "" };
            double watingTime = 0.0;
            foreach (Result r in rr)
            {
                row[0] = r.processID.ToString();
                row[1] = r.burstTime.ToString();
                row[2] = r.waitingTime.ToString();
                row[3] = r.Priority.ToString();
                strvalue[j] = row[0];
                value[j] = Convert.ToInt32(row[2]);
                watingTime += r.waitingTime;
                dataGridView1.Rows.Add(row);
                j++;
            }
        }
    }
}
