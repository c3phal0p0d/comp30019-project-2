using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PrefabRepository : MonoBehaviour
{
    public static PrefabRepository instance;

    [SerializeField]
    private GameObject[] statIncreases = new GameObject[Enum.GetNames(typeof(PlayerStats.StatType)).Length];
    [SerializeField]
    private GameObject[] healingItems;
    [SerializeField]
    private GameObject[] ammoItems;
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private GameObject finalBoss;

    private void Awake()
    {
        instance = this;
    }

    public GameObject[] StatIncreases => statIncreases;
    public GameObject[] HealingItems => healingItems;
    public GameObject[] AmmoItems => ammoItems;
    public GameObject[] Enemies => enemies;
    public GameObject FinalBoss => finalBoss;
}

#if UNITY_EDITOR
[CustomEditor(typeof(PrefabRepository))]
class PrefabRepositoryEditor : Editor
{
    private SerializedProperty statIncreaseArray;
    private SerializedProperty healingItemArray;
    private SerializedProperty ammoItemArray;
    private SerializedProperty enemyArray;
    private SerializedProperty finalBossObject;

    private void OnEnable()
    {
        statIncreaseArray = serializedObject.FindProperty("statIncreases");
        healingItemArray = serializedObject.FindProperty("healingItems");
        ammoItemArray = serializedObject.FindProperty("ammoItems");
        enemyArray = serializedObject.FindProperty("enemies");
        finalBossObject = serializedObject.FindProperty("finalBoss");
    }

    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        EditorGUILayout.LabelField("Stat increases");
        string[] statNames = Enum.GetNames(typeof(PlayerStats.StatType));
        for (int i = 0; i < statNames.Length; i++)
        {
            SerializedProperty statIncreaseObject = statIncreaseArray.GetArrayElementAtIndex(i);
            statIncreaseObject.objectReferenceValue = EditorGUILayout.ObjectField(statNames[i], statIncreaseObject.objectReferenceValue, typeof(GameObject), true);
        }

        EditorGUILayout.PropertyField(healingItemArray);
        EditorGUILayout.PropertyField(ammoItemArray);
        EditorGUILayout.PropertyField(enemyArray);
        EditorGUILayout.PropertyField(finalBossObject);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
