using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCanvas : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject difficultyCamera;
    private Canvas difficultySelection;


    void Start()
    {
        difficultySelection = GetComponent<Canvas>();

    }
    void Update()
    {
        if (difficultyCamera.transform.position.x <= 10)
        {
            difficultySelection.enabled = true;
        }
    }
}
