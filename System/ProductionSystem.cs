using System;
using System.Timers;

public class ProductionSystem
{
    private Player player;
    private Timer productionTimer;

    public ProductionSystem(Player p)
    {
        player = p;
        productionTimer = new Timer(1000); // every 1 second
        productionTimer.Elapsed += ProduceChocolate;
        productionTimer.AutoReset = true;
        productionTimer.Enabled = true;
    }

    private void ProduceChocolate(Object source, ElapsedEventArgs e)
    {
        int totalProduction = 0;

        foreach (Factory f in player.Factories)
        {
            totalProduction += f.GetTotalProduction();
        }

        player.Chocolate += totalProduction;

        if (totalProduction > 0)
        {
            Console.WriteLine($"[AUTO] Produced {totalProduction} chocolate! Total: {player.Chocolate}");
        }
    }

    public void Stop()
    {
        productionTimer.Stop();
    }
}