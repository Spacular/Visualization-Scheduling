using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualizationScheduling
{
    public class Result
    {
        public int processID;
        public int startP;
        public int burstTime;
        public int waitingTime;
        public int Priority;

        public Result(int processID, int startP, int burstTime, int waitingTime, int Priority)
        {
            this.processID = processID;
            this.startP = startP;
            this.burstTime = burstTime;
            this.waitingTime = waitingTime;
            this.Priority = Priority;
        }
    }
}
