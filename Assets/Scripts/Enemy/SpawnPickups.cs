using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnPickups : MonoBehaviour
{
    [SerializeField]
    int minPickupToSpawn;
    [SerializeField]
    int maxPickupToSpawn;
    [SerializeField]
    float chanceForHealingItem;
    [SerializeField]
    float horizontalSpawnScale;

    public void DoSpawn(Vector3 position)
    {
        System.Random random = new System.Random();
        int randNum = random.Next() % (maxPickupToSpawn - minPickupToSpawn) + minPickupToSpawn;
        for (int i = 0; i < randNum; i++)
        {
            InstantiatePrefab(RandomStatIncrease(), position);
        }

        if ((float)random.NextDouble() < chanceForHealingItem)
        {
            InstantiatePrefab(PrefabRepository.instance.HealingItems[0], position);
        }
    }

    private void InstantiatePrefab(GameObject prefab, Vector3 position)
    {
        prefab = GameObject.Instantiate(prefab);
        prefab.transform.SetParent(transform.parent);
        System.Random random = new System.Random();
        float x = (float)random.NextDouble() * horizontalSpawnScale;
        float y = (float)random.NextDouble() * horizontalSpawnScale;
        prefab.transform.position = position + new Vector3(x, 0.5f, y);
    }

    private GameObject RandomStatIncrease()
    {
        int n = (new System.Random()).Next() % Enum.GetNames(typeof(PlayerStats.StatType)).Length;
        return PrefabRepository.instance.StatIncreases[n];
    }
}