using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthCreatorTest : MonoBehaviour
{
    [SerializeField]
    private int numSections = 4;
    [SerializeField]
    private GameObject origin;
    [SerializeField]
    private Material brickMaterial;
    [SerializeField]
    private GameObject wallTorchPrefab;

    [SerializeField]
    private LabyrinthSize sizes;

    void Start()
    {
        System.Random random = new System.Random();
        LabyrinthCreator lc = new LabyrinthCreator(sizes);
        LabyrinthParameters labyrinthParameters = new LabyrinthParameters();
        labyrinthParameters.numSections = numSections;
        labyrinthParameters.origin = origin;
        labyrinthParameters.random = random;
        labyrinthParameters.brickMaterial = brickMaterial;
        labyrinthParameters.wallTorchPrefab = wallTorchPrefab;
        lc.CreateLabyrinth(labyrinthParameters);
    }
}
