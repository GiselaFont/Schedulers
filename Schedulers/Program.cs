using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>();
            Queue<int> queue1 = new Queue<int>();
            List<int> priority = new List<int>();
            int[] qtmp = new int[queue.Count];
            Random num = new Random();
            int count = 0;
            int time = 0;
            int p = 0;
            while(count <= 10000)
            {
                time = num.Next(1,11);
                p = num.Next(1,11);
                queue.Enqueue(time);
                priority.Add(p);
                count += time;
            }
            qtmp = queue.ToArray();

            Schedulers s = new Schedulers(4, queue = new Queue<int>(qtmp)); //Round-Robin 4 ticks
            Schedulers s1 = new Schedulers(10, queue = new Queue<int>(qtmp)); //Round-Robin 10 ticks
            Schedulers s2 = new Schedulers(queue = new Queue<int>(qtmp), priority);
          
            //CPU usage
            /*PerformanceCounter cpuCounter;
            PerformanceCounter ramCounter;

            cpuCounter = new PerformanceCounter();

            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            ramCounter = new PerformanceCounter("Memory", "Available MBytes");


            public string getCurrentCpuUsage(){
                    return cpuCounter.NextValue()+"%";
            }

            public string getAvailableRAM(){
                    return ramCounter.NextValue()+"MB";
            }   */  

            Console.WriteLine("Round-Robin timeslice of 4 ticks");
            s.RunRoundRobin(); //execute
            s.AddProcessTime(); //add processtime list
            s.ResponseTime();  //calculate average response time
            s.Turnaround(); //calculate turnaround time
            s.AverageTime(); //calculate average wait time
            Console.WriteLine();

            Console.WriteLine("Round-Robin timeslice of 10 ticks");
            s1.RunRoundRobin(); //execute
            s1.AddProcessTime(); //add processtime list
            s1.ResponseTime();  //calculate average response time
            s1.Turnaround(); //calculate turnaround time
            s1.AverageTime(); //calculate average wait time*/

            Console.WriteLine();
            Console.WriteLine("Priority");
            s2.RunPriority();
            s2.ResponseTime();
            s2.Turnaround();
            s2.AverageTime();
            
        }
    }
}
