using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainComponent : MonoBehaviour
{
    [SerializeField]
    private GameParameters gameParameters;
    [SerializeField]
    private GameObject levelObject;

    public static MainComponent instance;

    private int levelNumber;

    private void Start()
    {
        instance = this;
        ResetGame();
        NextLevel();
    }

    public void ResetGame()
    {
        levelNumber = 1;
        gameParameters.ResetParameters();
    }

    public void NextLevel()
    {
        DeleteLevel();
        LabyrinthCreator lc = new LabyrinthCreator(gameParameters.LevelSizes);
        lc.CreateLabyrinth(gameParameters.LevelParameters);

        levelNumber++;
        gameParameters.UpdateValues(levelNumber);
    }

    private void DeleteLevel()
    {
        foreach (Transform child in levelObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
