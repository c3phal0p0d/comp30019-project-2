using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LabyrinthParameters : MonoBehaviour, ICloneable
{
    public int numSections;
    public bool isFinalLevel;
    public int enemyDensity;
    public int pickupDensity;
    public int healthDensity;
    public int ammoDensity;
    public GameObject origin;
    public System.Random random;
    public Material brickMaterial;
    public Material blackMaterial;
    public GameObject wallObject;
    public GameObject wallTorchPrefab;
    public GameObject levelEndPrefab;

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
