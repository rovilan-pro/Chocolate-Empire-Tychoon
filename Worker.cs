using System;

public class Worker
{
    public int Production = 1; // Chocolate per second (base production)

    public int GetProduction(int upgradeLevel)
    {
        return Production * upgradeLevel;
    }
}