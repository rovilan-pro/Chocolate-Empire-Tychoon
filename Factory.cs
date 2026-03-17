using System.Collections.Generic;

public class Factory
{
    public string Name { get; private set; }
    private List<Room> rooms = new List<Room>();
    public IReadOnlyList<Room> Rooms => rooms.AsReadOnly();

    public Factory(string name)
    {
        Name = name;
    }

    public void AddRoom(Room room)
    {
        rooms.Add(room);
    }

    public int GetTotalProduction(int workerUpgradeLevel)
    {
        int total = 0;
        foreach (Room r in rooms)
        {
            total += r.GetProduction(workerUpgradeLevel);
        }
        return total;
    }
}