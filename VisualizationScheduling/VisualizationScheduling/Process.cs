using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizationScheduling
{
    public class Process
    {
        public int ProcessID; 
        public int ArriveTime; 
        public int BurstTime;   
        public int Priority;
        public int same = 0;

        public Process(int ProcessID, int ArriveTime, int BurstTime, int Priority)
        {
            this.ProcessID = ProcessID;
            this.ArriveTime = ArriveTime;
            this.BurstTime = BurstTime;
            this.Priority = Priority;
        } 
    }
}
