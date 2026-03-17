using System;

public class ProductionSystem
{
    private Player player;
    private System.Timers.Timer? productionTimer; // Nullable to allow null assignment

    public ProductionSystem(Player player)
    {
        this.player = player;
        productionTimer = new System.Timers.Timer(1000); // Trigger every 1 second
        productionTimer.Elapsed += ProduceChocolate;
        productionTimer.AutoReset = true;
        productionTimer.Enabled = true;
    }

    // Nullability fixed: sender can be null
    private void ProduceChocolate(object? sender, System.Timers.ElapsedEventArgs e)
    {
        int totalProduction = 0;

        foreach (var factory in player.Factories)
        {
            totalProduction += factory.GetTotalProduction(player.WorkerUpgrade.Level);
        }

        if (totalProduction > 0)
        {
            // Thread-safe update
            lock (player)
            {
                player.Chocolate += totalProduction;
            }

            Console.WriteLine($"[AUTO] Produced {totalProduction} chocolate! Total: {player.Chocolate}");
        }
    }

    public void Stop()
    {
        if (productionTimer != null)
        {
            productionTimer.Stop();
            productionTimer.Dispose();
            productionTimer = null; // Now allowed because timer is nullable
        }
    }
}