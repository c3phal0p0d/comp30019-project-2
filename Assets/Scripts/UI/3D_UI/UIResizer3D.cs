using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResizer3D : MonoBehaviour
{
    [SerializeField]
    private Camera uiCamera;
    [SerializeField]
    private GameObject[] uiObjects;

    float fov;
    float aspect;
    private Vector3[] screenPositions;
    private Vector3[] screenScales;

    void Start()
    {
        screenPositions = new Vector3[uiObjects.Length];
        screenScales = new Vector3[uiObjects.Length];
        for (int i = 0; i < uiObjects.Length; i++)
        {
            screenPositions[i] = uiObjects[i].transform.localPosition;
            screenScales[i] = uiObjects[i].transform.localScale;
        }
        fov = uiCamera.fieldOfView;
        aspect = uiCamera.aspect;
        resize();
    }

    void Update()
    {
        if (fov != uiCamera.fieldOfView || aspect != uiCamera.aspect)
        {
            fov = uiCamera.fieldOfView;
            aspect = uiCamera.aspect;
            resize();
        }
    }

    private void resize()
    {
        float height = Mathf.Tan(uiCamera.fieldOfView * Mathf.PI / 360);
        float width = height * uiCamera.aspect;
        Vector3 scale = new Vector3(width, height, 1);
        for (int i = 0; i < uiObjects.Length; i++)
        {
            uiObjects[i].transform.localPosition = Vector3.Scale(screenPositions[i], scale);
            //uiObjects[i].transform.localScale = screenScales[i] * uiCamera.aspect; // This sucks
        }
    }
}
