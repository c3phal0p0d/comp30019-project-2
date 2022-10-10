using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torchLightCamera : MonoBehaviour
{
    [SerializeField] private float lightRange;
    public GameObject torch;
    public Light torchLight;

    void Start()
    {
        torchLight = GetComponent<Light>();
    }

    void Update()
    {
        if ((Camera.main.transform.position.x - torch.transform.position.x) <= lightRange)
        {
            torchLight.enabled = true;
        }
    }
}
