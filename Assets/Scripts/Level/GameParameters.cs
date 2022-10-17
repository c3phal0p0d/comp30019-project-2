using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class GameParameters : MonoBehaviour
{
    private int seed;
    public int numLevels;
    public GameObject[] statIncreases = new GameObject[Enum.GetNames(typeof(PlayerStats.StatType)).Length];
    public LabyrinthParameters initialLabyrinthParameters;
    public LabyrinthSize initialLabyrinthSizes;

    private System.Random random;

    private LabyrinthParameters labyrinthParameters;
    private LabyrinthSize labyrinthSizes;

    private void Awake()
    {
        random = new System.Random(seed);
        initialLabyrinthParameters.random = random;
    }

    public void ResetParameters()
    {
        labyrinthParameters = (LabyrinthParameters)initialLabyrinthParameters.Clone();
        labyrinthSizes = (LabyrinthSize)initialLabyrinthSizes.Clone();
    }

    public int RandomSeed
    {
        get => seed;
        set
        {
            if (seed != value)
                random = new System.Random(seed);
            seed = value;
        }
    }

    public void UpdateParameters(int level)
    {
        labyrinthParameters = (LabyrinthParameters)labyrinthParameters.Clone();

        labyrinthParameters.numSections++;
        labyrinthParameters.isFinalLevel = level == numLevels;
        if (labyrinthParameters.isFinalLevel)
            labyrinthParameters.numSections = 1;
        labyrinthParameters.enemyDensity = 1 + level / 2;

        labyrinthSizes = (LabyrinthSize)labyrinthSizes.Clone();
        if ((level+1) % 2 == 0)
        {
            labyrinthSizes.mazeWidth++;
            labyrinthSizes.mazeHeight++;
        }
        labyrinthSizes.wallHeight += 0.1f;
        labyrinthSizes.tubeHeight += 1f;
    }

    public System.Random Random => random;
    public LabyrinthParameters LevelParameters => labyrinthParameters;
    public LabyrinthSize LevelSizes => labyrinthSizes;
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameParameters))]
class GameParametersEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GameParameters component = (GameParameters)target;
        if (component == null)
            return;

        if (GUILayout.Button("Randomize Seed"))
        {
            component.RandomSeed = component.Random.Next();
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Seed");
        component.RandomSeed = EditorGUILayout.IntField(component.RandomSeed);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Num Levels");
        component.numLevels = EditorGUILayout.IntField(component.numLevels);
        EditorGUILayout.EndHorizontal();

        string[] statNames = Enum.GetNames(typeof(PlayerStats.StatType));

        GUILayout.Label("Stat increase prefabs");
        for (int i = 0; i < statNames.Length; i++)
        {
            component.statIncreases[i] = (GameObject)EditorGUILayout.ObjectField(statNames[i], component.statIncreases[i], typeof(GameObject), false);
        }

        component.initialLabyrinthParameters = (LabyrinthParameters)EditorGUILayout.ObjectField("Initial Labyrinth Parameters", component.initialLabyrinthParameters, typeof(LabyrinthParameters), true);
        component.initialLabyrinthSizes = (LabyrinthSize)EditorGUILayout.ObjectField("Initial Labyrinth Sizes", component.initialLabyrinthSizes, typeof(LabyrinthSize), true);
    }

}
#endif
