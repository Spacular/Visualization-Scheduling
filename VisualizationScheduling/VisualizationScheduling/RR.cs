using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace VisualizationScheduling
{
    public class RRQueue
    {
        public int processID;
        public int burstTime;
        public int waitingTime;
        public int priorty;

        public RRQueue(int processID, int burstTime, int waitingTime, int asame, int priorty)
        {
            this.processID = processID;
            this.burstTime = burstTime;
            this.waitingTime = waitingTime;
            this.priorty = priorty;
        }
        
    }

    public class RR
    {
        public static List<Result> Run(List<Process> jobList, List<Result> resultList)
        {
            int currentProcess = 0;
            int cpuTime = 0;
            int cpuDone = 0;
            int runTime = 0;
            Visualization_Scheduling main = new Visualization_Scheduling();
            List<RRQueue> readyQueue = new List<RRQueue>();
            while (jobList.Count != 0)
            { 
                for (int i = 0; i < jobList.Count; i++)
                {
                    if (jobList.ElementAt(i).BurstTime >= (int)main.TimeQuntam)
                    {
                        jobList.ElementAt(i).BurstTime = jobList.ElementAt(i).BurstTime - (int)main.TimeQuntam;
                        runTime = (int)main.TimeQuntam;
                        if (i == 0)
                        {

                        }
                        else
                        {

                        }
                    }   
                    else
                    {

                    } 
                }
            }
                return resultList;
        }
    }
}
