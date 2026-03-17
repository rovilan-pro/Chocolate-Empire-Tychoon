using System;

public class OfflineSystem
{
    public static void ApplyOfflineEarnings(Player player)
    {
        TimeSpan offlineTime = DateTime.Now - player.LastPlayed;
        int seconds = (int)offlineTime.TotalSeconds;

        int productionPerSecond = 0;
        foreach (Factory f in player.Factories)
            productionPerSecond += f.GetTotalProduction(player.WorkerUpgrade.Level);

        // Offline limiter
        int maxSeconds = 3600; // 1 hour
        seconds = Math.Min(seconds, maxSeconds);

        int earnings = productionPerSecond * seconds;
        player.Chocolate += earnings;

        Console.WriteLine($"\nYou earned {earnings} chocolate while offline!");
    }
}