using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStats ownStats;

    void Start()
    {
        ownStats = GetComponent<CharacterStats>();
    }
    public void attack(CharacterStats targetStats)
    {
        targetStats.TakeDamage(ownStats.da)
    }
}
