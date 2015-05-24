using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizationScheduling
{
    class FCFS //Frist Come First Served
    {

        ////////////////////////////////////////////////////////////////////
        namespace Schd
{
    class ReadyQueueElement
    {
        public int processID;
        public int burstTime;
        public int waitingTime;

        public ReadyQueueElement(int processID, int burstTime, int waitingTime)
        {
            this.processID = processID;
            this.burstTime = burstTime;
            this.waitingTime = waitingTime;
        }
    }

    
    class SchedulingAlgorithm
    {
        public static List<Result> Run(List<Process> jobList, List<Result> resultList)
        {
            int currentProcess = 0;//작업 리스트에 들어온 프로세스 수
            int cpuTime = 0;//cpu가 돌아간 시간(작업수 단위)
            int cpuDone = 0;//???

            int runTime = 0;//총 걸린 시간(작업수 단위)

            //레디큐 만들기
            List<ReadyQueueElement> readyQueue = new List<ReadyQueueElement>();

            do
            {
                //작업 리스트가 있다면
                if (jobList.Count != 0)
                {
                    //맨 앞의 작업을 끌어옴
                    Process frontJob = jobList.ElementAt(0);
                    //잔류시간이 실행시간과 같으면
                    if (frontJob.arriveTime == runTime)
                    {
                        //준비상태로 만듬
                        readyQueue.Add(new ReadyQueueElement(frontJob.processID, frontJob.burstTime, 0));
                        //그리고 가져온 작업을 작업리스트에서 없앰
                        jobList.RemoveAt(0);
                    }
                }

                //작업 리스트에 남은게 없을때
                if (currentProcess == 0)
                {
                    //레디 큐에 남은 작업 있으면 마무리
                    if (readyQueue.Count != 0)
                    {
                        //rq에 0번째 작업을 넣음
                        ReadyQueueElement rq = readyQueue.ElementAt(0);
                        //결과 리스트에 각 요소를 더함
                        //processID작업된 순서대로 아이디가 출력될꺼고
                        //runTIme : 총 진행시간
                        //burstTime : 해당작업의 작업시간(작업수 단위)
                        //waitingTime : 작업 하기까지 기다린 시간(작업수 단위)
                        resultList.Add(new Result(rq.processID, runTime, rq.burstTime, rq.waitingTime));
                        //밑에 두 작업은 왜하는거지??
                        cpuDone = rq.burstTime;
                        cpuTime = 0;
                        //아이디를 넣어서 뭐하는거지???
                        currentProcess = rq.processID;
                        //큐에서 0번째 작업 제거
                        readyQueue.RemoveAt(0);

                    }
                }
                //작업리스트에 할게 남아 있으면
                else
                {
                    //이것은 무엇일까??
                    if (cpuTime == cpuDone)
                    {
                        //남은 작업 리스트를 0으로 만들고 while문 다시시작
                        currentProcess = 0;
                        continue;
                    }
                }

                //작업시간 증가(작업수단위)
                cpuTime++;
                runTime++;

                //레디 큐에 있는 작업들의 대기 시간을 증가(작업수 단위)
                for(int i = 0; i < readyQueue.Count; i++)
                {
                    readyQueue.ElementAt(i).waitingTime++;
                }

            } while (jobList.Count != 0 || readyQueue.Count != 0 || currentProcess != 0);
            //하나라도 남으면 반복

            //결과 리스트를 출력
            return resultList;
        }
    }
}

        ////////////////////////////////////////////////////////////////////1
    }
}
