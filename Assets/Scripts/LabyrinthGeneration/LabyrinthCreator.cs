using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthCreator
{
    private int numSections = 4;
    private int mazeWidth = 10;
    private int mazeHeight = 10;
    private float cellWidth = 1f;
    private float wallDepth = 0.1f;
    private float wallHeight = 1.5f;
    private GameObject origin;
    private Material brickMaterial;
    private GameObject wallTorchPrefab;
    private GameObject statuePrefab;

    private float timer;

    private System.Random random;

    public LabyrinthCreator(int numSections, int mazeWidth, int mazeHeight, float cellWidth, float wallHeight, float wallDepth, GameObject origin, Material brickMaterial, GameObject wallTorchPrefab, GameObject statuePrefab, System.Random random)
    {
        this.numSections = numSections;
        this.mazeWidth = mazeWidth;
        this.mazeHeight = mazeHeight;
        this.cellWidth = cellWidth;
        this.wallHeight = wallHeight;
        this.wallDepth = wallDepth;
        this.origin = origin;
        this.random = random;
        this.brickMaterial = brickMaterial;
        this.wallTorchPrefab = wallTorchPrefab;
        this.statuePrefab = statuePrefab;

        CreateLabyrinth();
    }
    
    private void CreateMaze(Maze maze, GameObject mazeOrigin)
    {   
        // Floor
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = "Floor";
        floor.transform.SetParent(mazeOrigin.transform);
        floor.transform.localScale = new Vector3(maze.Width * cellWidth + wallDepth, 0.5f, maze.Height * cellWidth + wallDepth);
        floor.transform.localPosition = new Vector3(maze.Width * cellWidth / 2, 0, maze.Height * cellWidth / 2 );
        floor.GetComponent<Renderer>().material = brickMaterial;

        // Roof
        GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ceiling.name = "Ceiling";
        ceiling.transform.SetParent(mazeOrigin.transform);
        ceiling.transform.localScale = new Vector3(maze.Width * cellWidth + wallDepth, 0.5f, maze.Height * cellWidth + wallDepth);
        ceiling.transform.localPosition = new Vector3(maze.Width * cellWidth / 2, wallHeight, maze.Height * cellWidth / 2 );
        ceiling.GetComponent<Renderer>().material = brickMaterial;
        
        // Starting position
        GameObject startPos = new GameObject();
        startPos.name = "StartPos";
        startPos.transform.SetParent(mazeOrigin.transform);
        startPos.transform.localScale = new Vector3(cellWidth, 3 * wallDepth, cellWidth);
        startPos.transform.localPosition = new Vector3(cellWidth * (maze.StartX + 0.5f), 0, cellWidth * (maze.StartY + 0.5f));

        int i = 0;
        foreach (Maze.Wall wall in maze.Walls)
        {
            if (wall.isHorizontal)
            {
                GameObject wallObj = CreateInnerWall(i, new Vector3(cellWidth * (wall.x + 0.5f), wallHeight / 2, cellWidth * (wall.y + 1)), 
                    new Vector3(cellWidth + wallDepth, wallHeight, wallDepth), mazeOrigin, true);

                Physics.SyncTransforms();

                // Check for overlaps between parts of the wall with neighbouring walls
                bool overlapLeft = CheckOverlap(wallObj.transform.position - new Vector3((cellWidth + wallDepth)/3, 0, 0), new Vector3(wallObj.transform.localScale.x/12, wallObj.transform.localScale.y/8, wallObj.transform.localScale.z/4));
                bool overlapRight = CheckOverlap(wallObj.transform.position + new Vector3((cellWidth + wallDepth)/3, 0, 0), new Vector3(wallObj.transform.localScale.x/12, wallObj.transform.localScale.y/8, wallObj.transform.localScale.z/4));
                bool overlapMiddle = CheckOverlap(wallObj.transform.position, new Vector3(wallObj.transform.localScale.x/12, wallObj.transform.localScale.y/8, wallObj.transform.localScale.z/4));

                if (overlapLeft&&!overlapRight&&!overlapMiddle){     // Only left third of wall overlaps
                    // Remove current wall
                    GameObject.Destroy(wallObj);

                    // Recreate wall without left third
                    GameObject newWallObj = CreateInnerWall(i, new Vector3(cellWidth * (wall.x + 0.5f) + (cellWidth + wallDepth)/6, wallHeight / 2, cellWidth * (wall.y + 1)), 
                        new Vector3((cellWidth + wallDepth)*2/3, wallHeight, wallDepth), mazeOrigin, true);
                }

                else if (!overlapLeft&&overlapRight&&!overlapMiddle){     // Only right third of wall overlaps
                    // Remove current wall
                    GameObject.Destroy(wallObj);

                    // Recreate wall without right third
                    GameObject newWallObj = CreateInnerWall(i, new Vector3(cellWidth * (wall.x + 0.5f) - (cellWidth + wallDepth)/6, wallHeight / 2, cellWidth * (wall.y + 1)), 
                        new Vector3((cellWidth + wallDepth)*2/3, wallHeight, wallDepth), mazeOrigin, true);
                }

                else if (overlapLeft&&overlapRight&&!overlapMiddle){     // Left and right thirds of wall overlap
                    // Remove current wall
                    GameObject.Destroy(wallObj);

                    // Recreate middle third of wall
                    GameObject newWallObj = CreateInnerWall(i, new Vector3(cellWidth * (wall.x + 0.5f), wallHeight / 2, cellWidth * (wall.y + 1)), 
                        new Vector3((cellWidth + wallDepth)*1/3, wallHeight, wallDepth), mazeOrigin, true);

                    //Physics.SyncTransforms();

                    //SpawnStatue(wallObj.transform.position, true);
                }

                else if (overlapMiddle){     // Middle third of wall overlaps
                    // Remove current wall
                    GameObject.Destroy(wallObj);

                    if (!overlapLeft){    // Recreate left third of wall if it does not overlap
                        GameObject newWallObj = CreateInnerWall(i, new Vector3(cellWidth * (wall.x + 0.5f) - (cellWidth + wallDepth)/3, wallHeight / 2, cellWidth * (wall.y + 1)), 
                            new Vector3((cellWidth + wallDepth)*1/3, wallHeight, wallDepth), mazeOrigin, true);
                        
                        //Physics.SyncTransforms();

                        //SpawnStatue(newWallObj.transform.position, true);
                    }

                    if (!overlapRight){    // Recreate right third of wall if it does not overlap
                        GameObject newWallObj2 = CreateInnerWall(i, new Vector3(cellWidth * (wall.x + 0.5f) + (cellWidth + wallDepth)/3, wallHeight / 2, cellWidth * (wall.y + 1)), 
                            new Vector3((cellWidth + wallDepth)*1/3, wallHeight, wallDepth), mazeOrigin, true);

                        //Physics.SyncTransforms();

                        //SpawnStatue(newWallObj2.transform.position, true);
                    }
                }

            }
            else
            {
                GameObject wallObj = CreateInnerWall(i, new Vector3(cellWidth * (wall.x + 1), wallHeight / 2, cellWidth * (wall.y + 0.5f)), 
                    new Vector3(wallDepth, wallHeight, cellWidth + wallDepth), mazeOrigin);

                Physics.SyncTransforms();

                // Check for overlaps between parts of the wall with neighbouring walls
                bool overlapLeft = CheckOverlap(wallObj.transform.position - new Vector3(0, 0, (cellWidth + wallDepth)/3), new Vector3(wallObj.transform.localScale.x/4, wallObj.transform.localScale.y/8, wallObj.transform.localScale.z/12));
                bool overlapRight = CheckOverlap(wallObj.transform.position + new Vector3(0, 0, (cellWidth + wallDepth)/3), new Vector3(wallObj.transform.localScale.x/4, wallObj.transform.localScale.y/8, wallObj.transform.localScale.z/12));
                bool overlapMiddle = CheckOverlap(wallObj.transform.position, new Vector3(wallObj.transform.localScale.x/4, wallObj.transform.localScale.y/8, wallObj.transform.localScale.z/12));

                if (overlapLeft&&!overlapRight&&!overlapMiddle){     // Only left third of wall overlaps
                    // Remove current wall
                    GameObject.Destroy(wallObj);

                    // Recreate wall without left third
                    GameObject newWallObj = CreateInnerWall(i, new Vector3(cellWidth * (wall.x + 1), wallHeight / 2, cellWidth * (wall.y + 0.5f) + (cellWidth + wallDepth)/6), 
                        new Vector3(wallDepth, wallHeight, (cellWidth + wallDepth)*2/3), mazeOrigin);
                }

                else if (!overlapLeft&&overlapRight&&!overlapMiddle){     // Only right third of wall overlaps
                    // Remove current wall
                    GameObject.Destroy(wallObj);

                    // Recreate wall without right third
                    GameObject newWallObj = CreateInnerWall(i, new Vector3(cellWidth * (wall.x + 1), wallHeight / 2, cellWidth * (wall.y + 0.5f) - (cellWidth + wallDepth)/6), 
                        new Vector3(wallDepth, wallHeight, (cellWidth + wallDepth)*2/3), mazeOrigin);
                }

                else if (overlapLeft&&overlapRight&&!overlapMiddle){     // Left and right thirds of wall overlap
                    // Remove current wall
                    GameObject.Destroy(wallObj);

                    // Recreate middle third of wall
                    GameObject newWallObj = CreateInnerWall(i, new Vector3(cellWidth * (wall.x + 1), wallHeight / 2, cellWidth * (wall.y + 0.5f)), 
                        new Vector3(wallDepth, wallHeight, (cellWidth + wallDepth)*1/3), mazeOrigin);

                    //Physics.SyncTransforms();

                    //SpawnStatue(wallObj.transform.position);
                }

                else if (overlapMiddle){     // Middle third of wall overlaps
                    // Remove current wall
                    GameObject.Destroy(wallObj);

                    if (!overlapLeft){    // Recreate left third of wall if it does not overlap
                        GameObject newWallObj = CreateInnerWall(i, new Vector3(cellWidth * (wall.x + 1), wallHeight / 2, cellWidth * (wall.y + 0.5f) - (cellWidth + wallDepth)/3), 
                            new Vector3(wallDepth, wallHeight, (cellWidth + wallDepth)*1/3), mazeOrigin);

                        //Physics.SyncTransforms();

                         //SpawnStatue(newWallObj.transform.position);
                    }


                    if (!overlapRight){    // Recreate right third of wall if it does not overlap
                        GameObject newWallObj2 = CreateInnerWall(i, new Vector3(cellWidth * (wall.x + 1), wallHeight / 2, cellWidth * (wall.y + 0.5f) + (cellWidth + wallDepth)/3), 
                            new Vector3(wallDepth, wallHeight, (cellWidth + wallDepth)*1/3), mazeOrigin);
                        
                        Physics.SyncTransforms();

                        //SpawnStatue(newWallObj2.transform.position);
                    }
                }
                
            }

            i++;
        }
    }

    private GameObject CreateInnerWall(int i, Vector3 wallPosition, Vector3 wallScale, GameObject mazeOrigin, bool isHorizontal = false){
        GameObject wallObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wallObj.name = "InnerWall" + i;
        wallObj.transform.SetParent(mazeOrigin.transform);
        wallObj.transform.localScale = wallScale;
        wallObj.transform.localPosition = wallPosition;
        wallObj.GetComponent<Renderer>().material = brickMaterial;
        
        int torchRotation;
        Vector3 torchPositionOffset;

        // Add torch to wall
        int randomInt = Random.Range(1,10);
        int factor = 1;     // increase this integer to reduce the number of torches spawned
        if (randomInt%factor==-1){
            GameObject wallTorch;
            if (i%2==0){    // alternate between sides of walls
                if (isHorizontal){
                    torchRotation = 90;
                    torchPositionOffset = new Vector3(0, wallHeight/10, wallDepth/2);
                } else {
                    torchRotation = 180;
                    torchPositionOffset = new Vector3(wallDepth/2, wallHeight/10, 0);
                }
            }
            else {
                if (isHorizontal){
                    torchRotation = -90;
                    torchPositionOffset = new Vector3(0, wallHeight/10, -wallDepth/2);
                } else {
                    torchRotation = 0;
                    torchPositionOffset = new Vector3(-wallDepth/2, wallHeight/10, 0);
                }
            }
            wallTorch = GameObject.Instantiate(wallTorchPrefab, wallObj.transform.position + torchPositionOffset, Quaternion.identity);

            if (CheckOverlap(wallTorch.transform.position, wallTorch.transform.localScale/100)){   // Check if wall torch has been spawned in position where it overlaps with a wall
                GameObject.Destroy(wallTorch);
            } else {
                GameObject wallTorchObject = new GameObject();
                wallTorch.transform.rotation = Quaternion.AngleAxis(torchRotation, Vector3.up);
                wallTorchObject.transform.SetParent(wallObj.transform);
                wallTorch.transform.SetParent(wallTorchObject.transform);
            }
        }
        
        return wallObj;
    }

    private bool CheckOverlap(Vector3 overlapPosition, Vector3 overlapRadius){
        //Physics.SyncTransforms();

        // Check if object overlaps with other objects
        Collider[] overlappingObjects = Physics.OverlapBox(overlapPosition, overlapRadius, Quaternion.identity);
        
        if (overlappingObjects.Length > 1){
            return true;
        }
        return false;
    }

    /*
    private void SpawnStatue(Vector3 wallPosition, bool isHorizontal = false){
        Vector3 statuePositionOffset = new Vector3(0, 0, 0);
        int statueRotation;
        float yOffset = -0.98f;
        GameObject statue;
        bool existsWallToLeft;
        bool existsWallToRight;
        // Check if wall forms part of a dead end, and if it is, spawn statue in front of it (this only works if wall is either 1 unit or 3 units long)
        if (isHorizontal){
            // Check one side of wall
            statuePositionOffset = new Vector3(0, yOffset, wallDepth*2/3);
            statueRotation = 90;
            existsWallToLeft = CheckOverlap(wallPosition + statuePositionOffset - new Vector3(cellWidth/2, 0, 0), new Vector3(cellWidth/4, wallHeight/100, wallDepth/100));
            existsWallToRight = CheckOverlap(wallPosition + statuePositionOffset + new Vector3(cellWidth/2, 0, 0), new Vector3(cellWidth/4, wallHeight/100, wallDepth/100));
            if (existsWallToLeft&&existsWallToRight){
                statue = GameObject.Instantiate(statuePrefab, wallPosition + statuePositionOffset, Quaternion.identity);
                statue.transform.rotation = Quaternion.AngleAxis(statueRotation, Vector3.up);
            }

            // Check opposite side of wall
            statuePositionOffset = new Vector3(0, yOffset, -wallDepth*2/3);
            statueRotation = -90;
            existsWallToLeft = CheckOverlap(wallPosition + statuePositionOffset + new Vector3(cellWidth/2, 0, 0), new Vector3(cellWidth/4, wallHeight/100, wallDepth/100));
            existsWallToRight = CheckOverlap(wallPosition + statuePositionOffset - new Vector3(cellWidth/2, 0, 0), new Vector3(cellWidth/4, wallHeight/100, wallDepth/100));
            if (existsWallToLeft&&existsWallToRight){
                statue = GameObject.Instantiate(statuePrefab, wallPosition + statuePositionOffset, Quaternion.identity);
                statue.transform.rotation = Quaternion.AngleAxis(statueRotation, Vector3.up);
            }
            
        } else {
            // Check one side of wall
            statuePositionOffset = new Vector3(wallDepth*2/3, yOffset, 0);
            statueRotation = 180;
            existsWallToLeft = CheckOverlap(wallPosition + statuePositionOffset - new Vector3(0, 0, cellWidth/2), new Vector3(wallDepth/100, wallHeight/100, cellWidth/4));
            existsWallToRight = CheckOverlap(wallPosition + statuePositionOffset + new Vector3(0, 0, cellWidth/2), new Vector3(wallDepth/100, wallHeight/100, cellWidth/4));
            if (existsWallToLeft&&existsWallToRight){
                statue = GameObject.Instantiate(statuePrefab, wallPosition + statuePositionOffset, Quaternion.identity);
                statue.transform.rotation = Quaternion.AngleAxis(statueRotation, Vector3.up);
            }

            // Check opposite side of wall
            statuePositionOffset = new Vector3(-wallDepth*2/3, yOffset, 0);
            statueRotation = 0;
            existsWallToLeft = CheckOverlap(wallPosition + statuePositionOffset - new Vector3(0, 0, cellWidth/2), new Vector3(wallDepth/100, wallHeight/100, cellWidth/4));
            existsWallToRight = CheckOverlap(wallPosition + statuePositionOffset + new Vector3(0, 0, cellWidth/2), new Vector3(wallDepth/100, wallHeight/100, cellWidth/4));
            if (existsWallToLeft&&existsWallToRight){
                statue = GameObject.Instantiate(statuePrefab, wallPosition + statuePositionOffset, Quaternion.identity);
                statue.transform.rotation = Quaternion.AngleAxis(statueRotation, Vector3.up);
            }
        }

    }
    */

    private void CreateLabyrinth()
    {
        int x = 0;
        int y = 0;
        int dxPrev = 0;
        int dyPrev = 0;
        int dx = 0;
        int dy = 0;
        //int holePos;
        for (int i = 0; i < numSections; i++)
        {
            // Position of the maze
            GameObject mazeOrigin = new GameObject("MazeOrigin" + i);
            mazeOrigin.transform.SetParent(origin.transform);
            mazeOrigin.transform.localPosition = new Vector3(x * (mazeWidth * cellWidth + wallDepth), 0, y * (mazeHeight * cellWidth + wallDepth));

            (dx, dy) = NewDirection(dxPrev, dyPrev);
            x += dx;
            y += dy;

            // Outer Walls
            if (dxPrev != 1 && (i == numSections-1 || dx != -1))
                CreateOuterWall(0, false, mazeOrigin, "OuterWallLeft");
            if (dxPrev != -1 && (i == numSections - 1 || dx != 1))
                CreateOuterWall(1, false, mazeOrigin, "OuterWallRight");
            if (dyPrev != 1 && (i == numSections - 1 || dy != -1))
                CreateOuterWall(0, true, mazeOrigin, "OuterWallDown");
            if (dyPrev != -1 && (i == numSections - 1 || dy != 1))
                CreateOuterWall(1, true, mazeOrigin, "OuterWallUp");

            // Generate inner walls
            Maze maze = new Maze(mazeWidth, mazeHeight, new System.Tuple<int, int>(random.Next() % mazeWidth, random.Next() % mazeHeight), random);
            CreateMaze(maze, mazeOrigin);

            dxPrev = dx;
            dyPrev = dy;
        }
    }

    private (int, int) NewDirection(int dxPrev, int dyPrev)
    {
        if (dyPrev != 0)
        {
            int x = random.Next() % 2;
            return (x, (1-x) * dyPrev); // x=0 -> (0,dyPrev)   x=1 -> (1,0)
        }
        else
        {
            int x = random.Next() % 3;
            return (x % 2, 1 - x); // x=0 -> (0,1)   x=1 -> (1,0)   x=2 -> (0,-1)
        }
    }

    private void CreateOuterWall(int x, bool isHorizontal, GameObject mazeOrigin, string name)
    {
        GameObject outerWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        outerWall.name = name;
        outerWall.transform.SetParent(mazeOrigin.transform);
        outerWall.GetComponent<Renderer>().material = brickMaterial;
        
        if (isHorizontal)
        {
            outerWall.transform.localScale = new Vector3(mazeWidth * cellWidth + wallDepth, wallHeight, wallDepth);
            outerWall.transform.localPosition = new Vector3(mazeWidth * cellWidth / 2, wallHeight / 2, x * mazeHeight * cellWidth);
        }
        else
        {
            outerWall.transform.localScale = new Vector3(wallDepth, wallHeight, mazeHeight * cellWidth + wallDepth);
            outerWall.transform.localPosition = new Vector3(x * mazeWidth * cellWidth, wallHeight / 2, mazeHeight * cellWidth / 2);
        }
    }
}
