using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMazeTest : MonoBehaviour
{
    [SerializeField]
    private int width = 10;
    [SerializeField]
    private int height = 10;
    [SerializeField]
    private float cellWidth = 1f;
    [SerializeField]
    private float wallWidth = 0.1f;
    [SerializeField]
    private GameObject origin;

    private void Start()
    {
        Maze maze = new Maze(width, height, new System.Tuple<int, int>(3, 0));
        CreateMaze(maze, origin, cellWidth, wallWidth);
    }

    public static void CreateMaze(Maze maze, GameObject origin, float cellWidth, float wallWidth)
    {
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.transform.SetParent(origin.transform);
        floor.transform.localScale = new Vector3(maze.Width * cellWidth, wallWidth, maze.Height * cellWidth);
        floor.transform.localPosition = new Vector3(maze.Width * cellWidth / 2, 0, maze.Height * cellWidth / 2);

        GameObject startPos = GameObject.CreatePrimitive(PrimitiveType.Cube);
        startPos.transform.SetParent(origin.transform);
        startPos.transform.localScale = new Vector3(cellWidth, 1 + wallWidth, cellWidth);
        startPos.transform.localPosition = new Vector3(cellWidth * (maze.StartX + 0.5f), 0, cellWidth * (maze.StartY + 0.5f));

        foreach (Maze.Wall wall in maze.Walls)
        {
            if (wall.isHorizontal)
            {
                GameObject wallObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wallObj.transform.SetParent(origin.transform);
                wallObj.transform.localScale= new Vector3(cellWidth, cellWidth, wallWidth);
                wallObj.transform.localPosition = new Vector3(cellWidth * (wall.x + 0.5f), cellWidth / 2, cellWidth * (wall.y + 1));
            }
            else
            {
                GameObject wallObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wallObj.transform.SetParent(origin.transform);
                wallObj.transform.localScale = new Vector3(wallWidth, cellWidth, cellWidth);
                wallObj.transform.localPosition = new Vector3(cellWidth * (wall.x + 1), cellWidth / 2, cellWidth * (wall.y + 0.5f));
            }
        }
    }
}
