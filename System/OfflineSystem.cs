using System;
using ChocolateEmpireTyhcoon;

namespace ChocolateEmpireTyhcoon.Systems
{
    public static class OfflineEarnings
    {
        public static void ApplyOfflineEarnings(Player player)
        {
            TimeSpan offlineTime = DateTime.Now - player.LastPlayed;
            int seconds = (int)offlineTime.TotalSeconds;

            int productionPerSecond = 0;
            foreach (var f in player.Factories)
                productionPerSecond += f.GetTotalProduction(player.WorkerUpgrade.Level);

            int maxSeconds = 3600; // limit offline earnings to 1 hour
            seconds = Math.Min(seconds, maxSeconds);

            int earnings = productionPerSecond * seconds;
            player.Chocolate += earnings;

            Console.WriteLine($"\nYou earned {earnings} chocolate while offline!");
        }
    }
}