﻿using System;
using System.Collections.Generic;

namespace TestMapGenerator;
public class Program
{
    const int MAP_WIDTH = 50;
    const int MAP_HEIGHT = 50;

    static void ConnectRooms(char[,] map, Room room1, Room room2)
    {
        var (x1, y1) = room1.Center();
        var (x2, y2) = room2.Center();

        if (room2.isWall(x1, y2))
            x1++;
        if (room2.isWall(x2, y1))
            y1++;
        MapRenderer.AddSymbolicCorridorOnMap(x1, y1, x2, y2, map);
    }


    static char[,] GenerateLevel()
    {
        char[,] map = new char[MAP_HEIGHT, MAP_WIDTH];
        for (int i = 0; i < MAP_HEIGHT; i++)
            for (int j = 0; j < MAP_WIDTH; j++)
                map[i, j] = ' ';

        Node root = new Node(0, 0, MAP_WIDTH, MAP_HEIGHT);
        Queue<Node> nodes = new Queue<Node>();
        nodes.Enqueue(root);

        while (nodes.Count > 0)
        {
            Node current = nodes.Dequeue();
            if (current.Split())
            {
                nodes.Enqueue(current.LeftChild);
                nodes.Enqueue(current.RightChild);
            }
        }

        root.CreateRoom();
        List<Room> rooms = root.GetRooms();
       
        foreach (Room room in rooms)
            MapRenderer.AddSymbolicRoomOnMap(room, map);

        for (int i = 0; i < rooms.Count - 1; i++)
            ConnectRooms(map, rooms[i], rooms[i + 1]);
            

        return map;
    }

   

    static void PrintMap(char[,] map)
    {
        for (int i = 0; i < MAP_HEIGHT; i++)
        {
            for (int j = 0; j < MAP_WIDTH; j++)
                Console.Write(map[i, j]);
            Console.WriteLine();
        }
    }

    static void Main(string[] args)
    {
        char[,] level = GenerateLevel();
        PrintMap(level);
    }
}
