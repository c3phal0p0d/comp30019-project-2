using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MainComponent : MonoBehaviour
{
    [SerializeField]
    private GameParameters gameParameters;
    [SerializeField]
    private GameObject levelObject;

    public static MainComponent instance;

    private int levelNumber;

    private bool doSpawn;
    private Vector3 spawnPosition;

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
        foreach (Transform child0 in levelObject.transform)
        {
            foreach (Transform child1 in child0)
            {
                child1.gameObject.layer = (int)LayerMask.NameToLayer("ToDestroy");
            }
        }
        // Generate new maze
        DeleteLevel();
        LabyrinthCreator lc = new LabyrinthCreator(gameParameters.LevelSizes);
        spawnPosition = lc.CreateLabyrinth(gameParameters.LevelParameters);
        doSpawn = true;

        levelNumber++;
        gameParameters.UpdateParameters(levelNumber);
    }

    private void FixedUpdate()
    {
        if (doSpawn)
        {
            PlayerManager.instance.gameObject.transform.position = spawnPosition;
            doSpawn = false;
        }
    }

    private void DeleteLevel()
    {
        foreach (Transform child in levelObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public GameParameters Parameters => gameParameters;
}
