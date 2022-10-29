using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class GameParameters : MonoBehaviour
{
    public bool useInitialSeed;
    public int seed;
    public int numLevels;
    public LabyrinthParameters initialLabyrinthParameters;
    public LabyrinthSize initialLabyrinthSizes;

    private System.Random random;

    private LabyrinthParameters labyrinthParameters;
    private LabyrinthSize labyrinthSizes;

    private void Awake()
    {
        if (useInitialSeed)
            random = new System.Random(seed);
        else
        {
            seed = new System.Random().Next();
            random = new System.Random(seed);
        }
        initialLabyrinthParameters.random = random;
    }

    public void ResetParameters()
    {
        labyrinthParameters = (LabyrinthParameters)initialLabyrinthParameters.Clone();
        labyrinthSizes = (LabyrinthSize)initialLabyrinthSizes.Clone();
    }

    public void UpdateParameters(int level)
    {
        labyrinthParameters = (LabyrinthParameters)labyrinthParameters.Clone();

        labyrinthParameters.numSections++;
        labyrinthParameters.isFinalLevel = level == numLevels;
        if (labyrinthParameters.isFinalLevel)
            labyrinthParameters.numSections = 2;
        if ((level + 1) % 2 == 0)
            labyrinthParameters.enemyDensity++;

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

        EditorGUI.BeginChangeCheck();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Use Inspector Seed");
        component.useInitialSeed = EditorGUILayout.Toggle(component.useInitialSeed);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Randomize Seed"))
        {
            component.seed = new System.Random().Next();
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Seed");
        component.seed = EditorGUILayout.IntField(component.seed);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Num Levels");
        component.numLevels = EditorGUILayout.IntField(component.numLevels);
        EditorGUILayout.EndHorizontal();

        component.initialLabyrinthParameters = (LabyrinthParameters)EditorGUILayout.ObjectField("Initial Labyrinth Parameters", component.initialLabyrinthParameters, typeof(LabyrinthParameters), true);
        component.initialLabyrinthSizes = (LabyrinthSize)EditorGUILayout.ObjectField("Initial Labyrinth Sizes", component.initialLabyrinthSizes, typeof(LabyrinthSize), true);

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }
    }

}
#endif
