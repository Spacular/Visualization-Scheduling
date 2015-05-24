using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizationScheduling
{
    class Process
    {
        public int ProcessID; //Process ID
        public int ArriveTime; //Arriving Time
        public int BurstTime; // Burst Time
        public int Priority; //Priority each Process

        public Process(int ProcessID, int ArriveTime, int BurstTime, int Priority)
        {
            this.ProcessID = ProcessID;
            this.ArriveTime = ArriveTime;
            this.BurstTime = BurstTime;
            this.Priority = Priority;
        } 
    }
}
