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
    [SerializeField]
    private int seed;
    public int numLevels;
    public GameObject[] statIncreases = new GameObject[Enum.GetNames(typeof(PlayerStats.StatType)).Length];
    public float[] statInitialValues = new float[Enum.GetNames(typeof(PlayerStats.StatType)).Length];

    private System.Random random;

    private void Awake()
    {
        random = new System.Random(seed);
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
    public System.Random Random => random;
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
            EditorGUILayout.ObjectField(statNames[i], component.statIncreases[i], typeof(GameObject), false);
        }

        GUILayout.Label("Player initial stats:");
        for (int i = 0; i < statNames.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(statNames[i]);
            component.statInitialValues[i] = EditorGUILayout.FloatField(component.statInitialValues[i]);
            EditorGUILayout.EndHorizontal();
        }
    }
}
#endif
