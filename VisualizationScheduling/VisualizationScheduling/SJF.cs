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
            // JobList�� oList�� �Ѱܹ��� ����, ResultList�� ��ȯ�� �� Result �迭

            int currentProcess = 0;     // ���� ž��� PID
            int cpuTime = 0;
            int cpuDone = 0;
            int runTime = 0;            // ��ü ����ð�
            int first = 0;
            int min = 0;

            List<ReadyQueueElement3> ReadyQueue = new List<ReadyQueueElement3>();               // ���� ���μ����� ������ �з��� ����Ʈ �迭.
            List<ReadyQueueElement3> SelectQueue = new List<ReadyQueueElement3>();

            do
            {
                if (JobList.Count != 0)         // �� �۾��� ���� �����ִٸ�
                {
                    SelectQueue.RemoveRange(0, SelectQueue.Count);   // ���� ť ����
                    min = 10000000;                                  // �۾� �ð��� �ϴ� �ִ������� ���س���
                    for (int i = 0; i < JobList.Count; i++)     // �ϴ� ���� �� �� �ִ� ��Ű�� ���� ����ֱ�!
                    {
                        if (JobList.ElementAt(i).ArriveTime == runTime)
                        {
                            SelectQueue.Add(new ReadyQueueElement3(JobList.ElementAt(i).ProcessID, JobList.ElementAt(i).BurstTime, 0, JobList.ElementAt(i).same));
                            JobList.RemoveAt(i);
                            i--;                        // �����������ϱ� ������Ƿ� ������ ��ġ�� �ٽ� �� �� �� Ȯ���ؾ� ��!
                        }
                    }

                    if (SelectQueue.Count != 0)                                  // ������ ����� �ִٸ�!
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

                if (currentProcess == 0)        // ���� �������� �۾��� ���ٸ�
                {
                    if (ReadyQueue.Count != 0)                  // ����ť�� �� �۾��� �ִٸ�!
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
                            ReadyQueueElement3 rq = ReadyQueue.ElementAt(first);    // ����ť�� ���� ù �۾��� rq�� �־��ش�.
                            ResultList.Add(new Result(rq.PID, runTime, rq.BurstTime, rq.WaitingTime, 0));   // ó���� ����� ResultList�� �־��ش�.
                            cpuDone = rq.BurstTime;             // �� �۾��� �ҿ��۾��ð��� cpuDone���ٰ� ����ִ´�
                            cpuTime = 0;                        // �� �۾��� cpu�� �󸶸�ŭ �����ߴ��� ��Ÿ����.
                            currentProcess = rq.PID;            // ���� �� �� ���μ����� ����ǰ� �ִ��� ��Ÿ����.
                            ReadyQueue.RemoveAt(first);             // ����ť���� ���� �����������Ƿ� �����ش�!
                        }
                        else
                        {
                            ReadyQueue.RemoveAt(first);
                            continue;
                        }
                    }
                }

                else                            // ���� �������� �۾��� �ִٸ�
                {
                    if (cpuTime == cpuDone)     // �۾� �Ϸ��ߴٸ�
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
                    ReadyQueue.ElementAt(i).WaitingTime++;          // ����ť�� ����ִ� ����� ���ð� ����!
                }
            }

            while (JobList.Count != 0 || ReadyQueue.Count != 0 || currentProcess != 0);
            // JobList�� ������ 0�� �ƴϰų�, ReadyQueue�� ���� �۾��� �����ְų� Ȥ�� ���� �������� Process�� ���� 0�� �ƴѵ��� ��� �ݺ�!

            return ResultList;
        }
    }
}