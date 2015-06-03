using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualizationScheduling
{
    public class SRT_ReadyQueueElement
    {
        public int processID;
        public int startP;
        public int burstTime;
        public int waitingTime;
        public int Priority;

        public SRT_ReadyQueueElement(int processID, int startP, int burstTime, int waitingTime, int Priority)
        {
            this.processID = processID;
            this.startP = startP;
            this.burstTime = burstTime;
            this.waitingTime = waitingTime;
            this.Priority = Priority;
        }
    }
    class SRT //Shortest Joob First
    {
        public static List<Result> Run(List<Process> jobList, List<Result> resultList)
        {
            int runTime = 0;

            List<SRT_ReadyQueueElement> readyQueue = new List<SRT_ReadyQueueElement>();

            do
            {
                while (jobList.Count != 0)
                {
                    Process frontJob = jobList.ElementAt(0);

                    if (frontJob.ArriveTime == runTime)
                    {
                        readyQueue.Add(new SRT_ReadyQueueElement(frontJob.ProcessID, runTime, frontJob.BurstTime, 0, frontJob.Priority));
                        jobList.RemoveAt(0);

                        readyQueue.Sort(delegate(SRT_ReadyQueueElement rqe1, SRT_ReadyQueueElement rqe2)
                        {
                            return rqe1.burstTime.CompareTo(rqe2.burstTime);
                        });
                    }
                    else
                        break;
                }


                for (int i = 1; i < readyQueue.Count; i++)
                {
                    readyQueue.ElementAt(i).waitingTime++;
                }


                if (readyQueue.ElementAt(0).processID == resultList.ElementAt(resultList.Count - 1).processID)
                {
                    resultList.ElementAt(resultList.Count - 1).burstTime++;
                }
                else
                    resultList.Add(new Result(readyQueue.ElementAt(0).processID, runTime, 1, readyQueue.ElementAt(0).waitingTime, readyQueue.ElementAt(0).Priority));



                if (readyQueue.ElementAt(0).burstTime <= 1)
                    readyQueue.RemoveAt(0);
                else
                {
                    SRT_ReadyQueueElement rq = readyQueue.ElementAt(0);
                    readyQueue.RemoveAt(0);
                    readyQueue.Add(new SRT_ReadyQueueElement(rq.processID, runTime, rq.burstTime - 1, rq.waitingTime, rq.Priority));

                    readyQueue.Sort(delegate(SRT_ReadyQueueElement rqe1, SRT_ReadyQueueElement rqe2)
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
