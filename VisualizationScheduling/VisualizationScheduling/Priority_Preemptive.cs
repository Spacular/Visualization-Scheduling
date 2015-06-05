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
            int currentProcess = -1;
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
                if (readyQueue.Count != 0)
                {
                    min = 0;
                    for (int i = 1; i < readyQueue.Count; i++) //readyQueue의 모든 Element와 비교
                        if (readyQueue.ElementAt(i).Priority < readyQueue.ElementAt(min).Priority) //현재 우선순위가 기존의 우선순위보다 높을 경우
                        {
                            min = i; //변경
                        }
                    if (currentProcess == -1) //현재 값이 없을 경우 init
                    {
                        currentProcess = min;
                        cpuDone = readyQueue.ElementAt(currentProcess).burstTime;
                        cpuTime = 0;
                    }
                    else if (min != currentProcess) //기존과 다른 프로세스의 경우
                    {

                        readyQueue[currentProcess].burstTime -= cpuTime; //현재 Burst저장
                        resultList.Add(new Result(readyQueue.ElementAt(currentProcess).processID, runTime - cpuTime,
                            cpuTime, readyQueue.ElementAt(currentProcess).waitingTime, readyQueue.ElementAt(currentProcess).Priority));

                        currentProcess = min;  //우선순위로 기존 프로세스를 미러냄
                        cpuDone = readyQueue.ElementAt(currentProcess).burstTime;
                        cpuTime = 0;
                    }
                    else
                    {
                        if (cpuTime == cpuDone)//선점한 상태에서 Burst Time이 끝난 경우
                        {
                            resultList.Add(new Result(readyQueue.ElementAt(currentProcess).processID, runTime - readyQueue.ElementAt(currentProcess).burstTime,
                            readyQueue.ElementAt(currentProcess).burstTime, readyQueue.ElementAt(currentProcess).waitingTime, readyQueue.ElementAt(currentProcess).Priority));
                            readyQueue.RemoveAt(currentProcess); //Remove
                            currentProcess = -1;
                            continue;
                        }
                    }
                }

                cpuTime++;
                runTime++;
                for (int i = 0; i < readyQueue.Count; i++)
                {
                    if (i != currentProcess)
                        readyQueue.ElementAt(i).waitingTime++;
                }

            } while (jobList.Count != 0 || readyQueue.Count != 0 || currentProcess != -1);

            return resultList;
        }
    }
}
