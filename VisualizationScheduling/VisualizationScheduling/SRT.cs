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
    class SRT //Shortest Remaining Time
    {
        public static List<Result> Run(List<Process> jobList, List<Result> resultList)
        {
            int currentProcess = 0;//탑재된 프로세스
            int cpuTime = 0;
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

                readyQueue.ElementAt(0).burstTime--;
                //다하면 삭제
                if (readyQueue.ElementAt(0).burstTime == 0)
                    readyQueue.RemoveAt(0);


                cpuTime++;
                runTime++;

                for (i = 1; i < readyQueue.Count; i++)
                {
                    readyQueue.ElementAt(i).waitingTime++;
                }
            } while (jobList.Count != 0 || readyQueue.Count != 0 || currentProcess != 0);

            return resultList;
        }
    }
}
