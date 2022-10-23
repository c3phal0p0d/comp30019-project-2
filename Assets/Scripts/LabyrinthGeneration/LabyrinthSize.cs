using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LabyrinthSize : MonoBehaviour, ICloneable
{
    public int mazeWidth = 10;
    public int mazeHeight = 10;
    public float cellWidth = 1f;
    public float wallDepth = 0.1f;
    public float wallHeight = 1.5f;
    public float tubeHeight = 10f;

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
