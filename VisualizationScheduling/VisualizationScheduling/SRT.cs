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
    class SRT //Shortest Joob First
    {
        public static List<Result> Run(List<Process> jobList, List<Result> resultList)
        {
            int runTime = 0;
            int i;

            List<ReadyQueueElement> readyQueue = new List<ReadyQueueElement>();

            do
            {
                if (jobList.Count != 0)
                {
                    Process frontJob = jobList.ElementAt(0);

                    if (frontJob.ArriveTime == runTime)
                    {
                        readyQueue.Add(new ReadyQueueElement(frontJob.ProcessID, frontJob.BurstTime, 0, 0, frontJob.Priority));
                        jobList.RemoveAt(0);

                        readyQueue.Sort(delegate(ReadyQueueElement rqe1, ReadyQueueElement rqe2)
                        {
                            return rqe1.burstTime.CompareTo(rqe2.burstTime);
                        });
                    }
                }


                resultList.Add(new Result(readyQueue.ElementAt(0).processID, runTime, 1, readyQueue.ElementAt(0).waitingTime, readyQueue.ElementAt(0).priorty));

                if (readyQueue.ElementAt(0).burstTime == 1)
                    readyQueue.RemoveAt(0);
                else
                {
                    ReadyQueueElement rq = readyQueue.ElementAt(0);
                    readyQueue.RemoveAt(0);
                    readyQueue.Add(new ReadyQueueElement(rq.processID, rq.burstTime - 1, rq.waitingTime, rq.asame, rq.priorty));

                    readyQueue.Sort(delegate(ReadyQueueElement rqe1, ReadyQueueElement rqe2)
                    {
                        return rqe1.burstTime.CompareTo(rqe2.burstTime);
                    });
                }

                runTime++;


            } while (jobList.Count != 0 || readyQueue.Count != 0);

            return resultList;
        }
    }
}
