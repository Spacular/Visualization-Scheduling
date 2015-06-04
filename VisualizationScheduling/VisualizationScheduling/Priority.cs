using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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
            int min;
            int SameValue;
            List<Result> resultList = new List<Result>();
            List<Result> readyQueue = new List<Result>();
            do
            {
                if (jobList.Count != 0)
                {
                    if (jobList.ElementAt(0).ArriveTime == runTime)
                    {
                        SameValue = 1;
                        for (int i = 1; i<jobList.Count; i++,SameValue++)
                        {
                            if (jobList.ElementAt(0).ArriveTime != jobList.ElementAt(i).ArriveTime)
                                break;
                           
                        }
                        for (int i = 0; i < SameValue; SameValue--)
                        {
                            min = 0;
                            for(int j = 1; j < SameValue;j++)
                                if (jobList.ElementAt(min).Priority > jobList.ElementAt(j).Priority)
                                    min = j;
                            readyQueue.Add(new Result(jobList.ElementAt(min).ProcessID, 0,jobList.ElementAt(min).BurstTime, 0, jobList.ElementAt(min).Priority));
                            jobList.RemoveAt(min);
                        }
                    }
                
                }
                if (currentProcess == 0)
                {
                    if (readyQueue.Count != 0)
                    {
                        Result rq = readyQueue.ElementAt(0);
                        resultList.Add(new Result(rq.processID, runTime, rq.burstTime, rq.waitingTime, rq.Priority));
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
