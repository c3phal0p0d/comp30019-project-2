using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerHealth;
    [SerializeField]
    private Stamina playerStamina;
    [SerializeField]
    private PlayerAmmo playerAmmo;
    [SerializeField]
    private float[] initialValues = new float[Enum.GetNames(typeof(StatType)).Length];
    [SerializeField]
    private float[] maxValues = new float[Enum.GetNames(typeof(StatType)).Length];

    public class Stat
    {
        private float min, max, value;

        public Stat(float min, float max, float initialValue)
        {
            this.min = min;
            this.max = max;
            this.value = initialValue;
        }
        public void increment(float value)
        {
            this.value += value;
            this.value = (this.value < min) ? min : (this.value > max) ? max : this.value;
        }

        public float Value => value;
        public float Min => min;
        public float Max => max;
    }

    public enum StatType
    {
        MaxHealth,
        MaxStamina,
        Speed,
        Strength,
        MaxAmmo
    }

    private Stat[] stats;

    private void Awake()
    {
        stats = new Stat[Enum.GetNames(typeof(StatType)).Length];
        for (int i = 0; i < stats.Length; i++)
            stats[i] = new Stat(0, maxValues[i], initialValues[i]);

        playerHealth.SetStat(GetStat(StatType.MaxHealth));
        playerStamina.SetStat(GetStat(StatType.MaxStamina));
        playerAmmo.SetStat(GetStat(StatType.MaxAmmo));
    }

    public Stat GetStat(StatType statType)
    {
        return stats[(int)statType];
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PlayerStats))]
class PlayerStatsEditor : Editor
{
    SerializedProperty playerHealthProperty;
    SerializedProperty playerStaminaProperty;
    SerializedProperty playerAmmoProperty;
    SerializedProperty playerInitialStatsArray;
    SerializedProperty playerMaxStatsArray;

    private void OnEnable()
    {
        playerHealthProperty = serializedObject.FindProperty("playerHealth");
        playerStaminaProperty = serializedObject.FindProperty("playerStamina");
        playerAmmoProperty = serializedObject.FindProperty("playerAmmo");
        playerInitialStatsArray = serializedObject.FindProperty("initialValues");
        playerMaxStatsArray = serializedObject.FindProperty("maxValues");
    }

    public override void OnInspectorGUI()
    {
        PlayerStats component = (PlayerStats)target;
        if (target == null)
            return;

        serializedObject.Update();

        EditorGUILayout.PropertyField(playerHealthProperty);
        EditorGUILayout.PropertyField(playerStaminaProperty);
        EditorGUILayout.PropertyField(playerAmmoProperty);

        EditorGUILayout.LabelField("Player Stats (Initial, Max)");
        string[] statNames = Enum.GetNames(typeof(PlayerStats.StatType));
        for (int i = 0; i < statNames.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(statNames[i]);
            SerializedProperty initialValue = playerInitialStatsArray.GetArrayElementAtIndex(i);
            SerializedProperty maxValue = playerMaxStatsArray.GetArrayElementAtIndex(i);
            initialValue.floatValue = Mathf.Clamp(EditorGUILayout.FloatField(initialValue.floatValue), 0, maxValue.floatValue);
            maxValue.floatValue = Mathf.Max(0, EditorGUILayout.FloatField(maxValue.floatValue));
            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
