
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualizationScheduling
{
    public class ReadyQueueElement2
    {
        public int processID;
        public int burstTime;
        public int waitingTime;

        public ReadyQueueElement2(int processID, int burstTime, int waitingTime)
        {
            this.processID = processID;
            this.burstTime = burstTime;
            this.waitingTime = waitingTime;
        }
    }

        public class SRT //Shortest Joob First
        {
            public static List<Result> Run(List<Process> jobList, List<Result> resultList)
            {
                int currentProcess = 0;//탑재된 프로세스
                int runTime = 0;
                int i;

                List<ReadyQueueElement2> readyQueue = new List<ReadyQueueElement2>();

                do
                {
                    //-------------------------------------------------------------------------------------------//
                    //jobList는 arrivetime별로 소팅된 상태
                    //도착된 것들 다 넣기
                    while (jobList.Count != 0)
                    {
                        Process frontJob = jobList.ElementAt(0);

                        if (frontJob.ArriveTime == runTime)
                        {
                            readyQueue.Add(new ReadyQueueElement2(frontJob.ProcessID, frontJob.BurstTime, 0));
                            jobList.RemoveAt(0);

                            readyQueue.Sort(delegate(ReadyQueueElement2 rqe1, ReadyQueueElement2 rqe2)
                            {
                                return rqe1.burstTime.CompareTo(rqe2.burstTime);
                            });
                        }
                        else
                            break;
                    }
                    //------------------------------------------------------------------------------------------//
                    readyQueue.ElementAt(0).burstTime = readyQueue.ElementAt(0).burstTime - 1;
                    //<<<< 리스트의 인자값 수정이 불가능, 그래서 기존값 지우고 새로운 인자를 생성해서 넣어야함>>>>>//
                    //http://www.devpia.com/Maeul/Contents/Detail.aspx?BoardID=17&MaeulNo=8&no=131529&ref=131527

                    resultList.Add(new Result(readyQueue.ElementAt(0).processID, runTime, 1, readyQueue.ElementAt(0).waitingTime,1));




                    resultList.ElementAt(runTime).processID = readyQueue.ElementAt(0).processID;
                    resultList.ElementAt(runTime).startP = runTime;
                    resultList.ElementAt(runTime).burstTime = 1;
                    resultList.ElementAt(runTime).waitingTime = readyQueue.ElementAt(0).waitingTime;

                    for (i = 1; i < readyQueue.Count; i++)
                    {
                        readyQueue.ElementAt(i).waitingTime++;
                    }

                    //다하면 삭제
                    if (readyQueue.ElementAt(0).burstTime == 0)
                        readyQueue.RemoveAt(0);
                    else
                    {
                        ReadyQueueElement2 rq = readyQueue.ElementAt(0);
                        readyQueue.RemoveAt(0);
                        readyQueue.Add(new ReadyQueueElement2(rq.processID, rq.burstTime - 1, rq.waitingTime));

                        readyQueue.Sort(delegate(ReadyQueueElement2 rqe1, ReadyQueueElement2 rqe2)
                        {
                            return rqe1.burstTime.CompareTo(rqe2.burstTime);
                        });
                    }

                    runTime++;


                } while (jobList.Count != 0 || readyQueue.Count != 0 || currentProcess != 0);

                return resultList;
            }
        }
    
}
