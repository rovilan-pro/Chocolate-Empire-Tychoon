using System;
using System.Collections.Generic;

public class Factory
{
    public string Name;
    public List<Room> Rooms = new List<Room>();

    public Factory(string name)
    {
        Name = name;
    }

    public int GetTotalProduction()
    {
        int total = 0;
        foreach (Room r in Rooms)
        {
            total += r.GetProduction();
        }
        return total;
    }

    public void AddRoom(Room room)
    {
        Rooms.Add(room);
    }
}