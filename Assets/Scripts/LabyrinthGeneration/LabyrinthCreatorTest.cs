using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthCreatorTest : MonoBehaviour
{
    [SerializeField]
    private int numSections = 4;
    [SerializeField]
    private int mazeWidth = 10;
    [SerializeField]
    private int mazeHeight = 10;
    [SerializeField]
    private float cellWidth = 1f;
    [SerializeField]
    private float wallDepth = 0.1f;
    [SerializeField]
    private float wallHeight = 1.5f;
    [SerializeField]
    private GameObject origin;

    void Start()
    {
        System.Random random = new System.Random();
        new LabyrinthCreator(numSections, mazeWidth, mazeHeight, cellWidth, wallHeight, wallDepth, origin, random);
    }
}
