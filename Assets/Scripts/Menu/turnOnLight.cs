using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOnLight : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera;
    private Light torchLight;

    void Start()
    {
        torchLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - camera.transform.position).magnitude <= 5)
        {
            torchLight.enabled = true;
        }
    }
}
