using System;

public class Upgrade
{
    public string Name;
    public int Level = 1;
    public int BaseCost;
    public double Multiplier;
    
    // Upgrade Function
    public Upgrade(string name, int baseCost, double multiplier)
    {
        Name = name;
        BaseCost = baseCost;
        Multiplier = multiplier;
    }

    public int GetCost()
    {
        return (int)(BaseCost * Math.Pow(1.5, Level - 1));
    }

    public void LevelUp()
    {
        Level++;
    }
}