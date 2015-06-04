using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            do
            {
                if (jobList.Count != 0)
                {
                    if (jobList.ElementAt(0).ArriveTime == runTime)
                    {
                        for (int i = 0; i < jobList.Count; i++)
                        {
                            if (jobList.ElementAt(0).ArriveTime != jobList.ElementAt(i).ArriveTime)
                                break;
                            readyQueue.Add(new Result(jobList.ElementAt(i).ProcessID, 0, jobList.ElementAt(i).BurstTime, 0, jobList.ElementAt(i).Priority));
                            jobList.RemoveAt(i);
                        }
                    }
                }
                if (currentProcess == 0)
                {
                    if (readyQueue.Count != 0)
                    {
                        min=0;
                        for (int i = 1; i < readyQueue.Count; i++)
                            if (readyQueue.ElementAt(i).Priority < readyQueue.ElementAt(min).Priority)
                            {
                                min = i;
                            }
                        if(current == -1)
                        {
                            current = min;
                            resultList.Add(new Result(readyQueue.ElementAt(current).processID, runTime, 
                                readyQueue.ElementAt(current).burstTime, readyQueue.ElementAt(current).waitingTime, readyQueue.ElementAt(current).Priority));
                            cpuDone = readyQueue.ElementAt(current).burstTime;
                            cpuTime = 0;
                            currentProcess = readyQueue.ElementAt(current).processID;
                        }
                        if(min != current)
                        {
                            readyQueue[current].burstTime -= cpuTime;
                            resultList.Add(new Result(readyQueue.ElementAt(current).processID, runTime, 
                                readyQueue.ElementAt(current).burstTime, readyQueue.ElementAt(current).waitingTime, readyQueue.ElementAt(current).Priority));
                            
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
                        resultList.Add(new Result(readyQueue.ElementAt(current).processID, runTime, 
                            readyQueue.ElementAt(current).burstTime, readyQueue.ElementAt(current).waitingTime, readyQueue.ElementAt(current).Priority));
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
