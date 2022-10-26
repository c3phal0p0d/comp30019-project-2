using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private static float cameraSpeed;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= 10)
        {
            var xPos = transform.position.x;
            var newX = xPos - (cameraSpeed * Time.deltaTime);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        else
        {
            // Camera.main.transform.position = new Vector3((transform.position.x * cameraSpeed * Time.deltaTime) - transform.position.x, transform.position.y, transform.position.z);
        }

    }

    public static void setCameraSpeed(float x)
    {
        cameraSpeed = x;
    }
}
