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
                    if (jobList.ElementAt(0).ArriveTime == runTime) //Runtime 이 도착시간과 같을때
                    {
                        SameValue = 1;
                        for (int i = 1; i < jobList.Count; i++, SameValue++)    //jobList의 모든 Element와 비교
                        {
                            if (jobList.ElementAt(0).ArriveTime != jobList.ElementAt(i).ArriveTime) //만약 현재 Element ArriveTime이 runtime과 다를 때 break;
                                break;
                        }
                        for (int i = 0; i < SameValue; i++) //같은 ArriveTime을 가진 Process로 우선순위 비교
                        {
                            readyQueue.Add(new Result(jobList.ElementAt(0).ProcessID, 0, jobList.ElementAt(0).BurstTime, 0, jobList.ElementAt(0).Priority));
                            jobList.RemoveAt(0);
                        }
                    }
                }
                if (currentProcess == 0)
                {
                    if (readyQueue.Count != 0)
                    {
                        min = 0;
                        for (int i = 1; i < readyQueue.Count; i++)
                            if (readyQueue.ElementAt(min).Priority > readyQueue.ElementAt(i).Priority)//우선순위 결정
                                min = i;
                        resultList.Add(new Result(readyQueue.ElementAt(min).processID, runTime,
                        readyQueue.ElementAt(min).burstTime, readyQueue.ElementAt(min).waitingTime, readyQueue.ElementAt(min).Priority));
                        cpuDone = readyQueue.ElementAt(min).burstTime;
                        cpuTime = 0;
                        currentProcess = readyQueue.ElementAt(min).processID;
                        readyQueue.RemoveAt(min);
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
