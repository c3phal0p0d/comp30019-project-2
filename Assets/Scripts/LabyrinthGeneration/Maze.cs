using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Maze
{
    public struct Wall
    {
        public readonly int x, y;
        public readonly bool isHorizontal;

        public Wall(int x, int y, bool isHorizontal)
        {
            this.x = x;
            this.y = y;
            this.isHorizontal = isHorizontal;
        }
    }

    private readonly int width;
    private readonly int height;
    private readonly Tuple<int, int> startPosition;
    private readonly List<Wall> walls;

    private System.Random random;

    public Maze(int width, int height, Tuple<int,int> start, System.Random random)
    {
        this.width = width;
        this.height = height;
        this.startPosition = start;
        walls = new List<Wall>();

        this.random = random;

        GenerateWalls();
    }

    private void GenerateWalls()
    {
        Stack<Tuple<int, int>> path = new Stack<Tuple<int, int>>();
        bool[,] traversedSquares = new bool[width, height];
        bool[,] horizontalGaps = new bool[width, height - 1];
        bool[,] verticalGaps = new bool[width - 1, height];

        path.Push(startPosition);
        RecursiveGenerateGaps(path, traversedSquares, horizontalGaps, verticalGaps);

        for (int j = 0; j < height - 1; j++)
            for (int i = 0; i < width; i++)
                if (!horizontalGaps[i, j])
                    walls.Add(new Wall(i, j, true));


        for (int j = 0; j < height; j++)
            for (int i = 0; i < width-1; i++)
                if (!verticalGaps[i, j])
                    walls.Add(new Wall(i, j, false));
    }

    private void RecursiveGenerateGaps(Stack<Tuple<int,int>> path, bool[,] traversedSquares, bool[,] horizontalGaps, bool[,] verticalGaps)
    {
        //Debug.Log(path.Peek());
        traversedSquares[path.Peek().Item1, path.Peek().Item2] = true;
        int x = path.Peek().Item1, y = path.Peek().Item2;

        while (true)
        {
            bool[] canGo = {
                x > 0 && !traversedSquares[x-1,y],
                y < height - 1 && !traversedSquares[x,y+1],
                x < width - 1 && !traversedSquares[x+1,y],
                y > 0 && !traversedSquares[x,y-1]
            }; // left, up, right, down

            Tuple<int, int> randomDir = RandomDir(canGo);
            if (randomDir == null)
            {
                path.Pop();
                return;
            }

            switch (randomDir.Item1)
            {
                case 0:
                    break;
                case -1:
                    verticalGaps[x - 1, y] = true;
                    break;
                case 1:
                    verticalGaps[x, y] = true;
                    break;
            }

            switch (randomDir.Item2)
            {
                case 0:
                    break;
                case 1:
                    horizontalGaps[x, y] = true;
                    break;
                case -1:
                    horizontalGaps[x, y-1] = true;
                    break;
            }

            Tuple<int, int> nextCell = new Tuple<int, int>(x + randomDir.Item1, y + randomDir.Item2);
            path.Push(nextCell);
            RecursiveGenerateGaps(path, traversedSquares, horizontalGaps, verticalGaps);
        }
    }

    private int CountTrues(bool[] array)
    {
        int count = 0;
        foreach (bool b in array)
            if (b) count++;
        return count;
    }

    private int indexOfNthTrue(bool[] array, int n)
    {
        int count = -1;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i])
                count++;
            if (count == n)
                return i;
        }
        // Shouldn't happen
        return -1;
    }

    private Tuple<int, int> RandomDir(bool[] canGo)
    {
        int numWays = CountTrues(canGo);
        if (numWays == 0)
            return null;
        int dirToChoose = random.Next(0, numWays);

        int dir = indexOfNthTrue(canGo, dirToChoose);
        int dx = (dir % 2 == 0) ? dir - 1 : 0; // 0 -> -1 (left)   2 -> 1 (right)
        int dy = (dir % 2 == 1) ? 2 - dir : 0; // 1 -> 1 (up)   3 -> -1 (down)

        return new Tuple<int, int>(dx, dy);
    }

    public int Width => width;
    public int Height => height;
    public int StartX => startPosition.Item1;
    public int StartY => startPosition.Item2;
    public List<Wall> Walls => walls;
}
