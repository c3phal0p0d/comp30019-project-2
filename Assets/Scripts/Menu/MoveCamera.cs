using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private static float cameraSpeed;
    public bool move = false;

    void Update()
    {
        if (move && (transform.position.x >= 10))
        {
            var xPos = transform.position.x;
            var newX = xPos - (cameraSpeed * Time.deltaTime);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

    }

    public static void setCameraSpeed(float x)
    {
        cameraSpeed = x;
    }
}
