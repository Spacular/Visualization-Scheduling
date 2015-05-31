using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualizationScheduling
{
    public class Result_RR
    {
        public int processID;
        public int startP;
        public int burstTime;
        public int waitingTime;
        public int Priority;

        public Result_RR(int processID, int startP, int burstTime, int waitingTime, int Priority)
        {
            this.processID = processID;
            this.startP = startP;
            this.burstTime = burstTime;
            this.waitingTime = waitingTime;
            this.Priority = Priority;
        }
    }
}
