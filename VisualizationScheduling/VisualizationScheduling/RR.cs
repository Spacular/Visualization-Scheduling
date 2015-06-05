using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace VisualizationScheduling
{
    public class rr_quere
    {
        public int processID;
        public int burstTime;
        public double waitingTime;
        public int priorty;
        public int asame;

        public rr_quere(int processID, int burstTime, double waitingTime, int asame, int priorty)
        {
            this.processID = processID;
            this.burstTime = burstTime;
            this.waitingTime = waitingTime;
            this.asame = asame;
            this.priorty = priorty;
        }
    }

    public class RR
    {
        public static List<Result_double> Run(List<Process> jobList, int Quantum)
        {
            int timequntam = Quantum;
            int exetime = 0;
            int runTime = 0;
            if (jobList.ElementAt(0).ArriveTime > runTime)
            {
                runTime = jobList.ElementAt(0).ArriveTime;
            }
            List<Result_double> resultList = new List<Result_double>();
            List<rr_quere> readyQueue = new List<rr_quere>();
            do
            {
                while (jobList.Count != 0)
                {
                    Process frontJob = jobList.ElementAt(0);
                    if (frontJob.ArriveTime == runTime)
                    {
                        readyQueue.Add(new rr_quere(frontJob.ProcessID, frontJob.BurstTime, 0, frontJob.same, frontJob.Priority));
                        jobList.RemoveAt(0);
                    }
                    else if (frontJob.ArriveTime <= runTime)
                    {
                        readyQueue.Add(new rr_quere(frontJob.ProcessID, frontJob.BurstTime, runTime - frontJob.ArriveTime, frontJob.same, frontJob.Priority));
                        jobList.RemoveAt(0);
                    }
                    else 
                        break;
                }
           
                if (readyQueue.Count != 0)
                {
                    if (readyQueue.ElementAt(0).burstTime >= timequntam)
                    {
                        readyQueue.ElementAt(0).burstTime -= timequntam;
                        exetime = timequntam;
                    }
                    else
                    {
                        exetime = readyQueue.ElementAt(0).burstTime;
                        readyQueue.ElementAt(0).burstTime = 0;
                    }
                    resultList.Add(new Result_double(readyQueue.ElementAt(0).processID, runTime, exetime, readyQueue.ElementAt(0).waitingTime, readyQueue.ElementAt(0).priorty,0));
                    if (readyQueue.ElementAt(0).burstTime != 0)
                    {
                        readyQueue.Add(new rr_quere(readyQueue.ElementAt(0).processID, readyQueue.ElementAt(0).burstTime, readyQueue.ElementAt(0).waitingTime, readyQueue.ElementAt(0).asame, readyQueue.ElementAt(0).priorty));
                       
                    }
                    readyQueue.RemoveAt(0);
                    runTime = runTime + exetime;
                }
                else
                {
                    runTime++;
                }
                for (int i = 0; i < readyQueue.Count - 1; i++)
                {
                    readyQueue.ElementAt(i).waitingTime += exetime;
                    readyQueue.ElementAt(i).waitingTime += 0.2;
                }

            } while (jobList.Count != 0 || readyQueue.Count != 0);

            return resultList;
        }
    }
}