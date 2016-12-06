using System;
using System.Collections.Generic;
using System.Linq;


public class Schedulers{

    public Queue<int> Queue {get;set;} = new Queue<int>();
    public List<int> Processtime {get;set;} = new List<int>();
    public List<int> Process {get;set;} = new List<int>();
    public List<int> Priority {get;set;} = new List<int>();
    public int responsetime {get; set;}
    public int turnaroundtime {get; set;}
    public int Ticks {get; set;}
    //Round Robin
    public Schedulers(int ticks, Queue<int> queue){
        Ticks = ticks; //timeslice
        Queue = queue; //burst time
    }
    //Priority
    public Schedulers(Queue<int> queue, List<int> priority){
        Queue = queue; //burst time
        Priority = priority;
    }
    //run Round Robin algorithm
    public void RunRoundRobin()
    {
            int length = Queue.Count;
            bool done = false;
            int temp = 0;
            while(!done)
            {
                for(int i = 0; i < length; i++)
                {
                    temp = Queue.Dequeue(); //take first process burst time
                    if((temp == Ticks || temp < Ticks) && temp != 0) //check if burst time <= ticks 
                    {
                        Processtime.Add(temp); //store time
                        Process.Add(i+1); //store process number
                        temp = 0; 
                        Queue.Enqueue(0); //process is done. Burst time = 0
                        done = true;
                    }
                    else if(temp > Ticks) //check if burst time > ticks
                    {
                        temp -= Ticks; //update burst time
                        Processtime.Add(Ticks); //store time
                        Process.Add(i+1); //store process number
                        Queue.Enqueue(temp); //add process at the end of the queue
                        done = false;
                    }
                    else{
                        Queue.Enqueue(0);
                    }
                }
            }
    }
    //run priority algorithm
    public void RunPriority()
    {
        int temp = 0;
        int count = 0;
        int count1 = 0;
        int count2 = Queue.Count;
        while(count2 != 0) //terminate when all the processes finished
        {   
            count1++;
            for(int i = 0; i < Queue.Count; i++)
            {
                temp = Queue.Dequeue(); //take first process burst time
                if(count1 == 1) 
                {
                    if(Priority.Max() == Priority[i]) //check if current process has the higher priority
                    {
                        Process.Add(i+1);
                        count = count + temp; 
                        Processtime.Add(count); //store time
                        temp = 0;
                        Queue.Enqueue(temp); //process is done. Burst time = 0
                        Priority[i] = 0; // priority = 0
                        count2--; // update number of processes left
                    }
                    else if(((i+1) < Queue.Count) && (Priority[i] <= Priority[i+1])) //check if the current process priority is less than or equal to the next process priority
                    {
                        temp--; //update burst time
                        Process.Add(i+1); //store process number
                        Processtime.Add(++count); //store time
                        Queue.Enqueue(temp);
                    }
                    else if(i != Queue.Count) //current process priority is greater than the next process priority
                    {
                        count++; //update time
                        Process.Add(i+1); //store process number
                        Processtime.Add(++count); //store time
                        i = i + 1; 
                        temp = temp - 2;
                        Queue.Enqueue(temp); //skip next process
                        temp = Queue.Dequeue();
                        Queue.Enqueue(temp);
                    }
                    else{
                        Queue.Enqueue(temp);
                    }
                }
                else
                {
                    if((Priority.Max() == Priority[i]) && Priority[i] != 0) //check if the current process has the highest priority
                    {
                        Process.Add(i+1); //store process number
                        count = count + temp; //update time
                        Processtime.Add(count); //store time
                        temp = 0; //process is done. Burst time = 0
                        Priority[i] = 0; // priority = 0
                        count2--; //update number of processes left
                    }
                    Queue.Enqueue(temp);
                }
                
                
            }
       }
   
    }
    //Round Robin algorithm only. Add times.
    public void AddProcessTime()
    {
         for(int i = 0; i < Processtime.Count; i++)
            {
                if(i - 1 >= 0)
                {
                    Processtime[i] = Processtime[i] + Processtime[i-1];
                }
            }
    }
    //calculate response time
    public void ResponseTime()
    {
        double totalresponsetime = 0;
        int j = 0;
        for(int i = 0; i < Queue.Count; i++)
        {
            j = Process.IndexOf(i+1); //look for the first element in the list and store the index
            responsetime = responsetime + (Processtime[j]-Ticks);
        }
        totalresponsetime = Convert.ToDouble(responsetime) / Convert.ToDouble(Queue.Count);
        Console.WriteLine("Total response time: " + totalresponsetime);
    }
    //calculate turnaround time
    public void Turnaround()
    {
        double totalturnaroundtime = 0;
        int j = 0;
        for(int i = 0; i < Queue.Count; i++)
        {
            j = Process.LastIndexOf(i+1); //look for the last element in the list and store the index
            turnaroundtime = turnaroundtime + (Processtime[j]);
        }
        totalturnaroundtime = Convert.ToDouble(turnaroundtime) / Convert.ToDouble(Queue.Count);
        Console.WriteLine("Total turnaround time: " + totalturnaroundtime);
    }
    //calculate average time
    public void AverageTime()
    {
        int[] waittime = new int[Queue.Count+1]; //list to store waiting time for each process
        double totaltime = 0;
        int sum = 0;
        int count = 0;
        int p = 0;
        int prev = 0;
        for(int i = 0; i < waittime.Length; i++)
        {
            while(Process.Contains(i))
            {
                    p = Process.IndexOf(i); //look for the first element in the list and store the index
                    if(count == 0)
                    {
                        if(p != 0)
                        {
                            waittime[i] = Processtime[p-1]; //store time
                        }
                        Process[p] = 0;
                        count++;
                    }
                    prev = p; //keep previous time
                    Process[p] = 0;
                    if(Process.Contains(i))
                    {
                        p = Process.IndexOf(i);
                        waittime[i] = waittime[i] + (Processtime[p-1] - Processtime[prev]); //add wait time for each process
                    }
            }
            count = 0;
            sum = sum + waittime[i]; //add total waitime
        }
        totaltime = Convert.ToDouble(sum) / Convert.ToDouble(Queue.Count);
        Console.WriteLine("Total time: " + totaltime);
    }
}