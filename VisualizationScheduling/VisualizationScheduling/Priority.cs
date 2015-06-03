using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizationScheduling
{
    class Priority
    {
       
        public static List<Result> Run(List<Process> jobList, List<Result> resultList)
        {
            int currentProcess = 0;
            int cpuTime = 0;
            int cpuDone = 0;
            int runTime = 0;

            List<Result> readyQueue = new List<Result>();
            for (int i = 0; i < jobList.Count; i++)
            {
                for (int j = i + 1; j < jobList.Count; j++)
                    if (jobList.ElementAt(i).ArriveTime == jobList.ElementAt(j).ArriveTime)
                    {
                        jobList.ElementAt(j).ArriveTime++;
                        jobList.ElementAt(j).same++;
                    }
            }
            do
            {
                if (jobList.Count != 0)
                {
                    Process frontJob = jobList.ElementAt(0);
                    if (frontJob.ArriveTime == runTime)
                    {
                        readyQueue.Add(new Result(frontJob.ProcessID, frontJob.BurstTime, 0, frontJob.same, frontJob.Priority));
                        jobList.RemoveAt(0);
                    }
                }
                if (currentProcess == 0)
                {

                    if (readyQueue.Count != 0)
                    {

                        Result rq = readyQueue.ElementAt(0);
                        resultList.Add(new Result(rq.processID, runTime, rq.waitingTime, rq.burstTime, rq.Priority));
                        cpuDone = rq.burstTime;
                        cpuTime = 0;
                        currentProcess = rq.processID;
                        readyQueue.RemoveAt(0);
                    }
                }
                else
                {
                    if (cpuTime == cpuDone)
                    {
                        currentProcess = 0;
                        continue;
                    }
                }
                cpuTime++;
                runTime++;
                for (int i = 0; i < readyQueue.Count; i++)
                {
                    readyQueue.ElementAt(i).waitingTime++;
                }

            } while (jobList.Count != 0 || readyQueue.Count != 0 || currentProcess != 0);

            return resultList;
        }
    }
}
