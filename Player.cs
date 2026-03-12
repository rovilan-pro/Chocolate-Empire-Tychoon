using System;
using System.Collections.Generic;

public class Player
{
    public string Name;
    public string FactoryName;
    public int Chocolate = 0;
    public List<Worker> Workers = new List<Worker>();

    public List<Factory> Factories = new List<Factory>();

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
    }
}