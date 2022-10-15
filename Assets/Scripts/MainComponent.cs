using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainComponent : MonoBehaviour
{
    [SerializeField]
    private GameParameters gameParameters;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject labyrinthOrigin;

    private int level;

    private void Start()
    {
        level = 0;
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        level++;
        LabyrinthParameters labyrinthParameters = new LabyrinthParameters();
        labyrinthParameters.numSections = 3 + level;
        labyrinthParameters.isFinalLevel = level == gameParameters.numLevels;
        labyrinthParameters.origin = labyrinthOrigin;
        labyrinthParameters.random = gameParameters.Random;

        LabyrinthCreator lc = new LabyrinthCreator(5 + level, 5 + level, 2, 4, 0.1f);
        lc.CreateLabyrinth(labyrinthParameters);
    }

}
