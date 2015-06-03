using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualizationScheduling
{
    public class ReadyQueueElement3
    {
        public int PID;
        public int BurstTime;
        public int WaitingTime;
        public int asame;

        public ReadyQueueElement3(int PID, int BurstTime, int WaitingTime, int asame)
        {
            this.PID = PID;
            this.BurstTime = BurstTime;
            this.WaitingTime = WaitingTime;
            this.asame = asame;
        }
    }

    public class SJF
    {
        public static List<Result> Run(List<Process> JobList, List<Result> ResultList)
        {
            // JobList는 oList로 넘겨받은 인자, ResultList는 반환해 줄 Result 배열

            int currentProcess = 0;     // 현재 탑재된 PID
            int cpuTime = 0;
            int cpuDone = 0;
            int runTime = 0;            // 전체 실행시간
            int first = 0;
            int min = 0;

            List<ReadyQueueElement3> ReadyQueue = new List<ReadyQueueElement3>();               // 받은 프로세스별 내용을 분류할 리스트 배열.
            List<ReadyQueueElement3> SelectQueue = new List<ReadyQueueElement3>();

            do
            {
                if (JobList.Count != 0)         // 할 작업이 아직 남아있다면
                {
                    SelectQueue.RemoveRange(0, SelectQueue.Count);   // 선별 큐 비우고
                    min = 10000000;                                  // 작업 시간은 일단 최대한으로 정해놓자
                    for (int i = 0; i < JobList.Count; i++)     // 일단 지금 들어갈 수 있는 새키들 전부 집어넣기!
                    {
                        if (JobList.ElementAt(i).ArriveTime == runTime)
                        {
                            SelectQueue.Add(new ReadyQueueElement3(JobList.ElementAt(i).ProcessID, JobList.ElementAt(i).BurstTime, 0, JobList.ElementAt(i).same));
                            JobList.RemoveAt(i);
                            i--;                        // 지워버렸으니까 당겨지므로 지워진 위치를 다시 한 번 더 확인해야 함!
                        }
                    }

                    if (SelectQueue.Count != 0)                                  // 선별된 놈들이 있다면!
                    {
                        while (SelectQueue.Count > 0)
                        {
                            for (int i = 0; i < SelectQueue.Count; i++)
                            {
                                if (SelectQueue.ElementAt(i).BurstTime < min)
                                {
                                    min = SelectQueue.ElementAt(0).BurstTime;
                                    first = i;
                                }
                            }
                            ReadyQueue.Add(SelectQueue.ElementAt(first));
                            SelectQueue.RemoveAt(first);
                            if (SelectQueue.Count != 0)
                            {
                                min = SelectQueue.ElementAt(0).BurstTime;
                                first = 0;
                            }
                        }
                    }
                }

                if (currentProcess == 0)        // 현재 실행중인 작업이 없다면
                {
                    if (ReadyQueue.Count != 0)                  // 레디큐에 할 작업이 있다면!
                    {
                        min = 10000;
                        first = 0;
                        for (int i = 0; i < ReadyQueue.Count; i++)
                        {
                            if (ReadyQueue.ElementAt(i).BurstTime < min)
                            {
                                min = ReadyQueue.ElementAt(i).BurstTime;
                                first = i;
                            }
                            else if (ReadyQueue.ElementAt(i).BurstTime == min)
                            {
                                if (ReadyQueue.ElementAt(i).PID < ReadyQueue.ElementAt(first).PID)
                                {
                                    min = ReadyQueue.ElementAt(i).BurstTime;
                                    first = i;
                                }
                            }
                        }
                        if (ReadyQueue.ElementAt(first).BurstTime != 0)
                        {
                            ReadyQueueElement3 rq = ReadyQueue.ElementAt(first);    // 레디큐의 제일 첫 작업을 rq에 넣어준다.
                            ResultList.Add(new Result(rq.PID, runTime, rq.BurstTime, rq.WaitingTime, 0));   // 처리한 결과를 ResultList에 넣어준다.
                            cpuDone = rq.BurstTime;             // 이 작업의 소요작업시간을 cpuDone에다가 집어넣는다
                            cpuTime = 0;                        // 이 작업이 cpu가 얼마만큼 실행했는지 나타낸다.
                            currentProcess = rq.PID;            // 현재 몇 번 프로세스가 실행되고 있는지 나타낸다.
                            ReadyQueue.RemoveAt(first);             // 레디큐에서 이제 빠져나갔으므로 지워준다!
                        }
                        else
                        {
                            ReadyQueue.RemoveAt(first);
                            continue;
                        }
                    }
                }

                else                            // 현재 실행중인 작업이 있다면
                {
                    if (cpuTime == cpuDone)     // 작업 완료했다면
                    {
                        currentProcess = 0;
                        continue;
                    }
                }
                if (currentProcess != 0)
                    cpuTime++;
                runTime++;
                for (int i = 0; i < ReadyQueue.Count; i++)
                {
                    ReadyQueue.ElementAt(i).WaitingTime++;          // 레디큐에 들어있는 놈들의 대기시간 증가!
                }
            }

            while (JobList.Count != 0 || ReadyQueue.Count != 0 || currentProcess != 0);
            // JobList의 갯수가 0이 아니거나, ReadyQueue에 아직 작업이 남아있거나 혹은 현재 실행중인 Process의 수가 0이 아닌동안 계속 반복!

            return ResultList;
        }
    }
}