using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VisualizationScheduling
{
    class Priority
    {
       
        public static List<Result> Run(List<Process> jobList)
        {
            int currentProcess = 0;
            int cpuTime = 0;
            int cpuDone = 0;
            int runTime = 0;
            //int min;

            List<Result> resultList = new List<Result>();
            List<Result> readyQueue = new List<Result>();
           /* for (int i = 0; i < jobList.Count;) //Sorting
            {
                min = i;
                for (int j = i + 1; j < jobList.Count; j++)
                    if (jobList.ElementAt(min).ArriveTime >= jobList.ElementAt(j).ArriveTime)
                    {
                        if (jobList.ElementAt(min).ArriveTime == jobList.ElementAt(j).ArriveTime && jobList.ElementAt(min).Priority > jobList.ElementAt(j).Priority)
                        {
                            min = j;
                        }
                        min = j;
                    }
                readyQueue.Add(new Result(jobList.ElementAt(min).ProcessID, jobList.ElementAt(min).BurstTime, 0, jobList.ElementAt(min).same, jobList.ElementAt(min).Priority));
                jobList.RemoveAt(min);
            }*/
            do
            {
                if (jobList.Count != 0)
                {
                    for (int i = 0; jobList.ElementAt(i).ArriveTime == runTime;i++ )
                    {
                        readyQueue.Add(new Result(jobList.ElementAt(i).ProcessID, jobList.ElementAt(i).BurstTime, 0, jobList.ElementAt(i).same, jobList.ElementAt(i).Priority));
                        jobList.RemoveAt(i);
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

            } while (readyQueue.Count != 0 || currentProcess != 0);

            return resultList;
        }
    }
}
