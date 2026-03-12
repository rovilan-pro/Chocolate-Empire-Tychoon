using System;
using System.Collections.Generic;

public class ChocoEmpireMain
{
    static Player player;
    static bool gameRunning = true;

    public static void Main(string[] args)
    {
        Console.WriteLine("=== CHOCO MANIA EMPIRE ===");
        Console.WriteLine("By AGames+ Studio\n");

        StartGame();
        GameLoop();
    }

    static void StartGame()
    {   
        // Create Character 
        Console.Write("Enter your Boss Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Factory Name: ");
        string factoryName = Console.ReadLine();

        player = new Player(name, factoryName);

        Console.WriteLine("\nWelcome Boss " + player.Name + "!");
        Console.WriteLine("Your chocolate empire begins now!\n");

        // Create first factory and room
        Factory factory1 = new Factory(player.FactoryName);
        Room chocolateRoom = new Room("Chocolate Room", maxWorkers: 5);
        factory1.AddRoom(chocolateRoom);
        player.Factories.Add(factory1);

        // Start automatic production
        ProductionSystem autoProd = new ProductionSystem(player);
    }

    static void GameLoop()
    {
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
                    gameRunning = false;
                    Console.WriteLine("Saving game... Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void ShowMenu()
    {   
        // Game Menu
        Console.WriteLine("\n==== MENU ====");
        Console.WriteLine("1. Produce Chocolate");
        Console.WriteLine("2. Player Status");
        Console.WriteLine("3. Hire Worker");
        Console.WriteLine("4. Assign Worker to Room");
        Console.WriteLine("5. Exit");
        Console.Write("Select: ");
    }

    static void ManualProduction()
    {   
        // Manual Function for gaining points/chcolate
        player.Chocolate += 1;
        Console.WriteLine("You produced 1 chocolate!");
    }

    static void BuyWorker()
    {
        int cost = 10;

        if (player.Chocolate >= cost)
        {
            player.Chocolate -= cost;
            player.Workers.Add(new Worker());
            Console.WriteLine("Worker hired!");
        }
        else
        {
            Console.WriteLine("Not enough chocolate.");
        }
    }

    static void AssignWorkerToRoom()
{
    if (player.Workers.Count == 0)
    {
        Console.WriteLine("You have no workers to assign!");
        return;
    }

    // Show factories
    Console.WriteLine("\nSelect a Factory:");
    for (int i = 0; i < player.Factories.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {player.Factories[i].Name}");
    }

    int factoryIndex = int.Parse(Console.ReadLine()) - 1;
    if (factoryIndex < 0 || factoryIndex >= player.Factories.Count)
    {
        Console.WriteLine("Invalid factory selection.");
        return;
    }

    Factory selectedFactory = player.Factories[factoryIndex];

    // Show rooms
    Console.WriteLine("\nSelect a Room:");
    for (int i = 0; i < selectedFactory.Rooms.Count; i++)
    {
        Room r = selectedFactory.Rooms[i];
        Console.WriteLine($"{i + 1}. {r.Name} (Workers: {r.Workers.Count}/{r.MaxWorkers})");
    }

    int roomIndex = int.Parse(Console.ReadLine()) - 1;
    if (roomIndex < 0 || roomIndex >= selectedFactory.Rooms.Count)
    {
        Console.WriteLine("Invalid room selection.");
        return;
    }

    Room selectedRoom = selectedFactory.Rooms[roomIndex];

    // Assign first available worker
    Worker workerToAssign = player.Workers[0];
    if (selectedRoom.AddWorker(workerToAssign))
    {
        player.Workers.Remove(workerToAssign);
        Console.WriteLine($"Worker assigned to {selectedRoom.Name}!");
    }
    else
    {
        Console.WriteLine("This room is full!");
    }
}
}