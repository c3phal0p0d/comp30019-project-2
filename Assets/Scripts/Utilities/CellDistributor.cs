using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CellDistributor
{
    private struct CellPriority
    {
        public int x;
        public int y;
        public int priority;
    }

    private List<CellPriority> cells;

    public CellDistributor(int width, int height)
    {
        cells = new List<CellPriority>(width * height);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                cells.Add(new CellPriority { x = i, y = j, priority = int.MaxValue / 4 });
            }
        }
    }

    public (int, int) RandomCell(System.Random random)
    {
        cells.Sort((cell1, cell2) => (cell2.priority - cell1.priority));

        // Choose random cell, prefering higher priority cells
        double randomDouble = random.NextDouble();
        int randomIndex = (int)(randomDouble * randomDouble * cells.Count);
        CellPriority cellSelected = cells[randomIndex];
        cells.RemoveAt(randomIndex);

        UpdatePriorities(cellSelected);

        return (cellSelected.x, cellSelected.y);
    }

    private void UpdatePriorities (CellPriority cellSelected)
    {
        for (int i = 0; i < cells.Count; i++)
        {
            CellPriority cell = cells[i];
            int newPriority = Math.Min(Math.Abs(cell.x - cellSelected.x), Math.Abs(cell.y - cellSelected.y));
            cell.priority = Math.Min(newPriority, cell.priority);
        }
    }
}
