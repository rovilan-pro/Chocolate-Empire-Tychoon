using System;
using System.Collections.Generic;

public class SaveData
{
    public int Chocolate;
    public int WorkerUpgradeLevel;
    public DateTime LastPlayed;
    public List<Factory> Factories = new List<Factory>();

    // Optional property to match previous references
    public int FactoryCount => Factories?.Count ?? 0;
}