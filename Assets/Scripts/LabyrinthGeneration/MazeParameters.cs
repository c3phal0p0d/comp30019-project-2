using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MazeParameters : MonoBehaviour
{
    public int[] numberOfPickups = new int[Enum.GetNames(typeof(PlayerStats.StatType)).Length];
    public int numberOfEnemies;
    private bool isFinalBoss;
    private bool isExit;

    public bool IsFinalBoss
    {
        get => isFinalBoss;
        set
        {
            isFinalBoss = value;
            if (isFinalBoss)
                isExit = false;
        }
    }

    public bool IsExit
    {
        get => isExit;
        set
        {
            isExit = value;
            if (isExit)
                isFinalBoss = false;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MazeParameters))]
class MazeParametersEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MazeParameters component = (MazeParameters)target;
        if (component == null) return;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Num Enemies");
        component.numberOfEnemies = EditorGUILayout.IntField(component.numberOfEnemies);
        EditorGUILayout.EndHorizontal();
        
        GUILayout.Label("Num Pickups:");
        string[] pickupNames = Enum.GetNames(typeof(PlayerStats.StatType));
        for (int i = 0; i < component.numberOfPickups.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(pickupNames[i]);
            component.numberOfPickups[i] = EditorGUILayout.IntField(component.numberOfPickups[i]);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Is Final Boss");
        component.IsFinalBoss = EditorGUILayout.Toggle(component.IsFinalBoss);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Is Exit");
        component.IsExit = EditorGUILayout.Toggle(component.IsExit);
        EditorGUILayout.EndHorizontal();
    }
}
#endif
