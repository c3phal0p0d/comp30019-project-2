using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //use singleton pattern
    #region Singleton

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;

    }
    #endregion

    //game object that references player
    public GameObject PlayerObject;
}
