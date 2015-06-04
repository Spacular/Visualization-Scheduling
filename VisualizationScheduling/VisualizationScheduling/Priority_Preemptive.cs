using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizationScheduling
{
    class Priority_Preemptive
    {
        public static List<Result> Run(List<Process> jobList)
        {
            int currentProcess = 0;
            int cpuTime = 0;
            int cpuDone = 0;
            int runTime = 0;
            int min,current=-1;

            List<Result> resultList = new List<Result>();
            List<Result> readyQueue = new List<Result>();
            /*for (int i = 0; i < jobList.Count; ) //Sorting
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
                    for (int i = 0; jobList.ElementAt(i).ArriveTime == runTime; i++)
                    {
                        readyQueue.Add(new Result(jobList.ElementAt(i).ProcessID, jobList.ElementAt(i).BurstTime, 0, jobList.ElementAt(i).same, jobList.ElementAt(i).Priority));
                        jobList.RemoveAt(i);
                    }
                }
                if (currentProcess == 0)
                {
                    if (readyQueue.Count != 0)
                    {
                        min=0;
                        for (int i = 1; i < readyQueue.Count; i++)
                            if (readyQueue.ElementAt(i).Priority <= readyQueue.ElementAt(min).Priority)
                            {
                                min = i;
                            }
                        if(min != current)
                        {
                            if (current != -1)
                                readyQueue.ElementAt(current).burstTime -= cpuTime;
                            current = min;
                            cpuDone = readyQueue.ElementAt(current).burstTime;
                            cpuTime = 0;
                            currentProcess = readyQueue.ElementAt(current).processID;
                        }
                    }
                }
                else
                {
                    if (cpuTime == cpuDone)
                    {
                        currentProcess = 0;
                        resultList.Add(new Result(readyQueue.ElementAt(current).processID, runTime, readyQueue.ElementAt(current).waitingTime, readyQueue.ElementAt(current).burstTime, readyQueue.ElementAt(current).Priority));
                        readyQueue.RemoveAt(current);
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
