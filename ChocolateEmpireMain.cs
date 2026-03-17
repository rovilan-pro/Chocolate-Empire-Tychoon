using System;
using System.Collections.Generic;

public class ChocolateEmpireMain
{
    static Player player;
    static bool gameRunning = true;
    static ProductionSystem autoProd;

    public static void Main(string[] args)
    {
        Console.WriteLine("=== CHOCOLATE MANIA EMPIRE ===");
        Console.WriteLine("By AGames+ Studio\n");

        StartGame();
        GameLoop();
    }

    static void StartGame()
    {
        // Create or load player
        Console.Write("Enter your Boss Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Factory Name: ");
        string factoryName = Console.ReadLine();

        player = new Player(name, factoryName);

        // Load save
        SaveSystem.Load(player);

        // Apply offline earnings
        OfflineSystem.Apply(player);

        // If no factories exist, create first factory
        if (player.Factories.Count == 0)
        {
            Factory factory1 = new Factory(player.FactoryName);
            Room chocolateRoom = new Room("Chocolate Room", 5);
            factory1.AddRoom(chocolateRoom);
            player.Factories.Add(factory1);
        }

        Console.WriteLine($"\nWelcome Boss {player.Name}!");
        Console.WriteLine("Your chocolate empire begins now!\n");

        // Start automatic production
        autoProd = new ProductionSystem(player);
    }

    static void GameLoop()
    {   
        //Game Loop/Menu System
        while (gameRunning)
        {
            ShowMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": 
                ManualProduction(); 
                break;

                case "2": 
                player.ShowStatus(); 
                break;

                case "3": 
                BuyWorker(); 
                break;

                case "4": 
                AssignWorkerToRoom(); 
                break;

                case "5": 
                UpgradeWorkers(); 
                break;

                case "6": 
                UpgradeRoom(); 
                break;

                case "7": 
                UnlockFactory(); 
                break;

                case "8":
                    ExitGame();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("\n==== MENU ====");
        Console.WriteLine("1. Produce Chocolate");
        Console.WriteLine("2. Player Status");
        Console.WriteLine("3. Hire Worker");
        Console.WriteLine("4. Assign Worker to Room");
        Console.WriteLine("5. Upgrade Workers");
        Console.WriteLine("6. Upgrade Room");
        Console.WriteLine("7. Unlock Factory");
        Console.WriteLine("8. Exit");
        Console.Write("Select: ");
    }
    // Manual Function
    static void ManualProduction()
    {
        player.Chocolate += 1;
        Console.WriteLine("You produced 1 chocolate!");
    }
    // Adding Worker 
    static void BuyWorker()
    {
        int cost = 10;
        if (player.Chocolate >= cost)
        {
            player.Chocolate -= cost;
            player.Workers.Add(new Worker());
            Console.WriteLine("Worker hired!");
        }
        else Console.WriteLine("Not enough chocolate.");
    }
    // Assigning Worker to Room
    static void AssignWorkerToRoom()
    {
        if (player.Workers.Count == 0)
        {
            Console.WriteLine("You have no workers to assign!");
            return;
        }

        Console.WriteLine("\nSelect a Factory:");
        for (int i = 0; i < player.Factories.Count; i++)
            Console.WriteLine($"{i + 1}. {player.Factories[i].Name}");

        if (!int.TryParse(Console.ReadLine(), out int factoryIndex)) return;
        factoryIndex -= 1;
        if (factoryIndex < 0 || factoryIndex >= player.Factories.Count) return;

        Factory selectedFactory = player.Factories[factoryIndex];

        Console.WriteLine("\nSelect a Room:");
        for (int i = 0; i < selectedFactory.Rooms.Count; i++)
        {
            Room r = selectedFactory.Rooms[i];
            Console.WriteLine($"{i + 1}. {r.Name} (Workers: {r.Workers.Count}/{r.MaxWorkers})");
        }

        if (!int.TryParse(Console.ReadLine(), out int roomIndex)) return;
        roomIndex -= 1;
        if (roomIndex < 0 || roomIndex >= selectedFactory.Rooms.Count) return;

        Room selectedRoom = selectedFactory.Rooms[roomIndex];

        Worker workerToAssign = player.Workers[0];
        if (selectedRoom.AddWorker(workerToAssign))
        {
            player.Workers.Remove(workerToAssign);
            Console.WriteLine($"Worker assigned to {selectedRoom.Name}!");
        }
        else Console.WriteLine("This room is full!");
    }
    // Upgrade Worker 
    static void UpgradeWorkers()
    {
        int cost = player.WorkerUpgrade.GetCost();
        Console.WriteLine($"\nUpgrade Worker Efficiency");
        Console.WriteLine($"Current Level: {player.WorkerUpgrade.Level}");
        Console.WriteLine($"Cost: {cost} Chocolate");

        if (player.Chocolate >= cost)
        {
            player.Chocolate -= cost;
            player.WorkerUpgrade.LevelUp();
            Console.WriteLine("Worker upgraded!");
        }
        else Console.WriteLine("Not enough chocolate.");
    }
    // Upgrade Room
    static void UpgradeRoom()
    {
        Room room = player.Factories[0].Rooms[0];
        int cost = room.GetUpgradeCost();

        Console.WriteLine($"\nUpgrading {room.Name}");
        Console.WriteLine($"Level: {room.Level}");
        Console.WriteLine($"Cost: {cost}");

        if (player.Chocolate >= cost)
        {
            player.Chocolate -= cost;
            room.Upgrade();
            Console.WriteLine("Room upgraded!");
        }
        else Console.WriteLine("Not enough chocolate.");
    }
    // Unlock New Factory
    static void UnlockFactory()
    {
        int cost = player.Factories.Count * 200;

        Console.WriteLine($"\nUnlock New Factory");
        Console.WriteLine($"Cost: {cost}");

        if (player.Chocolate >= cost)
        {
            player.Chocolate -= cost;

            Factory newFactory = new Factory("Factory " + (player.Factories.Count + 1));
            newFactory.AddRoom(new Room("Chocolate Room", 5));

            player.Factories.Add(newFactory);

            Console.WriteLine("New factory unlocked!");
        }
        else Console.WriteLine("Not enough chocolate.");
    }

    // Exit Game Function
    static void ExitGame()
    {
        player.LastPlayed = DateTime.Now;
        SaveSystem.Save(player);
        autoProd.Stop();
        gameRunning = false;
        Console.WriteLine("Game saved! Goodbye!");
    }
}