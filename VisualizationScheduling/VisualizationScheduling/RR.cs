using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace VisualizationScheduling
{
    public class RR
    {
        public static List<Result_double> Run(List<Process> jobList, List<Result_double> resultList, int a)
        {
            int count = 0;
            int time = a;
            Boolean[] flag = new Boolean[jobList.Count];
            for (int i = 0; i < jobList.Count; i++)
            {
                flag[i] = true;
            }
                while (true)
                {
                    for (int i = 0; i < jobList.Count; i++)
                    {
                        if (jobList.ElementAt(i).BurstTime >= time)
                        {
                            jobList.ElementAt(i).BurstTime = jobList.ElementAt(i).BurstTime - time;
                            resultList.Add(new Result_double(jobList.ElementAt(i).ProcessID, 0, time, 0, jobList.ElementAt(i).Priority));
                        }
                        else
                        {
                            if (jobList.ElementAt(i).BurstTime == 0)
                            {
                                flag[i] = false;
                                continue;
                            }
                            else
                            {
                                resultList.Add(new Result_double(jobList.ElementAt(i).ProcessID, 0, jobList.ElementAt(i).BurstTime, 0, jobList.ElementAt(i).Priority));
                                jobList.ElementAt(i).BurstTime = 0;
                                flag[i] = false;
                            }
                        }
                    }
                    for (int j = 0; j < jobList.Count; j++)
                    {
                        if (flag[j] == false)
                        {
                            count++;
                        }
                    }
                    if (count == jobList.Count)
                    {
                        break;
                    }
                    else
                    {
                        count = 0;
                    }

                }

                return resultList;
               
        }
    }
}
