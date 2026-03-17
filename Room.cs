using System;
using System.Collections.Generic;

public class Room
{
    public string Name;
    public int Level = 1;
    public int MaxWorkers;
    public int ProductionBonus = 1;
    public List<Worker> Workers = new List<Worker>();

    public Room(string name, int maxWorkers)
    {
        Name = name;
        MaxWorkers = maxWorkers;
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

    public int GetProduction(int workerUpgradeLevel)
    {
        int total = 0;
        foreach (Worker w in Workers)
            total += w.GetProduction(workerUpgradeLevel);
        return total * ProductionBonus;
    }

    public int GetUpgradeCost() => (int)(50 * Math.Pow(1.6, Level - 1));

    public void Upgrade()
    {
        Level++;
        ProductionBonus++;
        MaxWorkers += 2;
    }
}