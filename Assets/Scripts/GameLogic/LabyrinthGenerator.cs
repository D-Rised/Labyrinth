using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum WallType
{
    Left = 1, // 0001
    Right = 2, // 0010
    Up = 4, // 0100
    Down = 8, // 1000

    Checked = 128
}

public struct Position
{
    public int X;
    public int Y;
}

public struct NeighbourCell
{
    public Position Position;
    public WallType SharedWall;
}

public static class LabyrinthGenerator
{
    private static WallType GetOppositeWall(WallType type)
    {
        switch(type)
        {
            case WallType.Right: return WallType.Left;
            case WallType.Left: return WallType.Right;
            case WallType.Up: return WallType.Down;
            case WallType.Down: return WallType.Up;
            default: return WallType.Left;
        }
    }

    private static WallType[,] InitializeRecursiveCheck(WallType[,] labyrinth, int width, int height)
    {
        var random = new System.Random();
        var positionStack = new Stack<Position>();
        var position = new Position
        {
            X = random.Next(0, width),
            Y = random.Next(0, height)
        };
        
        labyrinth[position.X, position.Y] |= WallType.Checked;
        positionStack.Push(position);

        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbours = GetUncheckedNeighbours(current, labyrinth, width, height);

            if (neighbours.Count > 0)
            {
                positionStack.Push(current);
                var randomIndex = random.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randomIndex];

                var neighbourPosition = randomNeighbour.Position;
                labyrinth[current.X, current.Y] &= ~randomNeighbour.SharedWall;
                labyrinth[neighbourPosition.X, neighbourPosition.Y] &= ~GetOppositeWall(randomNeighbour.SharedWall);

                labyrinth[neighbourPosition.X, neighbourPosition.Y] |= WallType.Checked;

                positionStack.Push(neighbourPosition);
            }
        }

        return labyrinth;
    }

    private static List<NeighbourCell> GetUncheckedNeighbours(Position position, WallType[,] labyrinth, int width, int height)
    {
        var list = new List<NeighbourCell>();

        // Left wall check
        if (position.X > 0)
        {
            if (!labyrinth[position.X - 1, position.Y].HasFlag(WallType.Checked))
            {
                list.Add(new NeighbourCell
                {
                    Position = new Position
                    {
                        X = position.X - 1,
                        Y = position.Y
                    },
                    SharedWall = WallType.Left
                });
            }
        }

        // Right wall check
        if (position.X < width - 1)
        {
            if (!labyrinth[position.X + 1, position.Y].HasFlag(WallType.Checked))
            {
                list.Add(new NeighbourCell
                {
                    Position = new Position
                    {
                        X = position.X + 1,
                        Y = position.Y
                    },
                    SharedWall = WallType.Right
                });
            }
        }

        // Up wall check
        if (position.Y < width - 1)
        {
            if (!labyrinth[position.X, position.Y + 1].HasFlag(WallType.Checked))
            {
                list.Add(new NeighbourCell
                {
                    Position = new Position
                    {
                        X = position.X,
                        Y = position.Y + 1
                    },
                    SharedWall = WallType.Up
                });
            }
        }

        // Down wall check
        if (position.Y > 0)
        {
            if (!labyrinth[position.X, position.Y - 1].HasFlag(WallType.Checked))
            {
                list.Add(new NeighbourCell
                {
                    Position = new Position
                    {
                        X = position.X,
                        Y = position.Y - 1
                    },
                    SharedWall = WallType.Down
                });
            }
        }

        return list;
    }

    public static WallType[,] Generate(int width, int height)
    {
        WallType[,] labyrinth = new WallType[width, height];
        WallType initial = WallType.Left | WallType.Right | WallType.Up | WallType.Down;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                labyrinth[i, j] = initial;
            }
        }
        
        return InitializeRecursiveCheck(labyrinth, width, height);
    }
}
