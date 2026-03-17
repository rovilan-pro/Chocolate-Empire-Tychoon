using System;
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace ChocolateEmpireTyhcoon.Systems
{
    public static class SaveSystem
    {
        private static string saveFile = "save.json";

        public static void Save(Player player)
        {
            var data = new SaveData
            {
                Chocolate = player.Chocolate,
                WorkerUpgradeLevel = player.WorkerUpgrade.Level,
                LastPlayed = player.LastPlayed,
                Factories = new List<FactorySave>()
            };

            foreach (var f in player.Factories)
            {
                var fSave = new FactorySave { Name = f.Name, Rooms = new List<RoomSave>() };
                foreach (var r in f.Rooms)
                {
                    fSave.Rooms.Add(new RoomSave
                    {
                        Name = r.Name,
                        Level = r.Level,
                        MaxWorkers = r.MaxWorkers,
                        ProductionBonus = r.ProductionBonus,
                        WorkerCount = r.Workers.Count
                    });
                }
                data.Factories.Add(fSave);
            }

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(saveFile, json);
        }

        public static void Load(Player player)
        {
            if (!File.Exists(saveFile)) return;

            string json = File.ReadAllText(saveFile);
            var data = JsonSerializer.Deserialize<SaveData>(json);
            if (data == null) return;

            player.Chocolate = data.Chocolate;
            player.WorkerUpgrade.Level = data.WorkerUpgradeLevel;
            player.LastPlayed = data.LastPlayed;

            player.Factories.Clear();
            foreach (var fSave in data.Factories)
            {
                var factory = new Factory(fSave.Name);
                foreach (var rSave in fSave.Rooms)
                {
                    var room = new Room(rSave.Name, rSave.MaxWorkers)
                    {
                        Level = rSave.Level,
                        ProductionBonus = rSave.ProductionBonus
                    };
                    for (int i = 0; i < rSave.WorkerCount; i++)
                        room.AddWorker(new Worker());
                    factory.AddRoom(room);
                }
                player.AddFactory(factory);
            }
        }
    }

    // Helper classes for serialization
    public class SaveData
    {
        public int Chocolate;
        public int WorkerUpgradeLevel;
        public DateTime LastPlayed;
        public List<FactorySave> Factories = new List<FactorySave>();
    }

    public class FactorySave
    {
        public string Name = "";
        public List<RoomSave> Rooms = new List<RoomSave>();
    }

    public class RoomSave
    {
        public string Name = "";
        public int Level;
        public int MaxWorkers;
        public int ProductionBonus;
        public int WorkerCount;
    }
}