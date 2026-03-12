using System;
using System.Collections.Generic;

public class Room
{
    public string Name;
    public int Level = 1;
    public int MaxWorkers;
    public List<Worker> Workers = new List<Worker>();
    public int ProductionBonus = 1; // multiplier per room, can upgrade later

    public Room(string name, int maxWorkers)
    {
        Name = name;
        MaxWorkers = maxWorkers;
    }

    public int GetProduction()
    {
        int total = 0;
        foreach (Worker w in Workers)
        {
            total += w.Production;
        }
        return total * ProductionBonus;
    }

    public bool AddWorker(Worker w)
    {
        if (Workers.Count < MaxWorkers)
        {
            Workers.Add(w);
            return true;
        }
        return false;
    }
}