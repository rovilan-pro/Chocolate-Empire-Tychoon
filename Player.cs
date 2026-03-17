using System;
using System.Collections.Generic;

public class Player
{   
    public DateTime LastPlayed;
    public string Name;
    public string FactoryName;
    public int Chocolate = 0;
    public List<Worker> Workers = new List<Worker>();

    public Upgrade WorkerUpgrade = new Upgrade("Worker Efficiency", 20, 1.5);

    public List<Factory> Factories = new List<Factory>();

    public int MaxFactories = 1;

    public Player(string name, string factory)
    {
        Name = name;
        FactoryName = factory;
    }

    public void ShowStatus()
    {   
        // Status Menu 
        Console.WriteLine("\n=== PLAYER STATUS ===");
        Console.WriteLine($"Boss: {Name}");
        Console.WriteLine($"Factory: {FactoryName}");
        Console.WriteLine($"Chocolate: {Chocolate}");
        Console.WriteLine($"Workers: {Workers.Count}");
        Console.WriteLine($"Factories: {Factories.Count}");
    }

    // ===== Helper Methods Needed by Main =====

    public void AddWorker(Worker worker)
    {
        Workers.Add(worker);
    }

    public void RemoveWorker(Worker worker)
    {
        Workers.Remove(worker);
    }

    public void AddFactory(Factory factory)
    {
        Factories.Add(factory);
    }
}