using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualizationScheduling
{
    public class Result_double
    {
        public int processID;
        public int startP;
        public int same;
        public int burstTime;
        public double waitingTime;
        public int Priority;

        public Result_double(int processID, int startP, int burstTime, double waitingTime, int Priority,int same)
        {
            this.processID = processID;
            this.startP = startP;
            this.burstTime = burstTime;
            this.waitingTime = waitingTime;
            this.Priority = Priority;
            this.same = same;
        }
    }
}
