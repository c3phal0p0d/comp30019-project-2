using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBar3D : MonoBehaviour
{
    [SerializeField]
    private GameObject origin;
    [SerializeField]
    private Material underBarMaterial;
    [SerializeField]
    private Material overBarMaterial;
    [SerializeField]
    private Material horizontalBorderMaterial;
    [SerializeField]
    private Material verticalBorderMaterial;
    [SerializeField]
    private Material backMaterial;

    private float barWidth;
    private float overBarWidth;
    private float underBarWidth;

    private GameObject back;
    private GameObject left;
    private GameObject right;
    private GameObject up;
    private GameObject down;
    private GameObject underBarWrapper;
    private GameObject underBar;
    private GameObject overBarWrapper;
    private GameObject overBar;

    private void Awake()
    {
        Create(1, origin);
    }

    public void Create(float barWidth, GameObject origin)
    {
        int ui_camera_layer = LayerMask.NameToLayer("UICamera");
        back = GameObject.CreatePrimitive(PrimitiveType.Cube);
        back.name = "Back";
        back.layer = ui_camera_layer;
        back.transform.SetParent(origin.transform);
        back.transform.localScale = new Vector3(barWidth + 1, 2, 0.01f);
        back.transform.localPosition = new Vector3((barWidth+1)/2, -1, 0.25f);
        back.GetComponent<Renderer>().material = backMaterial;

        left = GameObject.CreatePrimitive(PrimitiveType.Cube);
        left.name = "Left";
        left.layer = ui_camera_layer;
        left.transform.SetParent(origin.transform);
        left.transform.localScale = new Vector3(0.5f, 2, 0.5f);
        left.transform.localPosition = new Vector3(0.25f, -1, -0.25f);
        left.GetComponent<Renderer>().material = verticalBorderMaterial;

        right = GameObject.CreatePrimitive(PrimitiveType.Cube);
        right.name = "Right";
        right.layer = ui_camera_layer;
        right.transform.SetParent(origin.transform);
        right.transform.localScale = new Vector3(0.5f, 2, 0.5f);
        right.transform.localPosition = new Vector3(barWidth + 0.75f, -1, -0.25f);
        right.GetComponent<Renderer>().material = verticalBorderMaterial;

        up = GameObject.CreatePrimitive(PrimitiveType.Cube);
        up.name = "Up";
        up.layer = ui_camera_layer;
        up.transform.SetParent(origin.transform);
        up.transform.localScale = new Vector3(barWidth + 1, 0.5f, 0.5f);
        up.transform.localPosition = new Vector3((barWidth + 1) / 2, -0.25f, -0.25f);
        up.GetComponent<Renderer>().material = horizontalBorderMaterial;

        down = GameObject.CreatePrimitive(PrimitiveType.Cube);
        down.name = "Down";
        down.layer = ui_camera_layer;
        down.transform.SetParent(origin.transform);
        down.transform.localScale = new Vector3(barWidth + 1, 0.5f, 0.5f);
        down.transform.localPosition = new Vector3((barWidth + 1) / 2, -1.75f, -0.25f);
        down.GetComponent<Renderer>().material = horizontalBorderMaterial;

        underBarWrapper = new GameObject("UnderBarWrapper");
        underBarWrapper.layer = ui_camera_layer;
        underBarWrapper.transform.SetParent(origin.transform);
        underBarWrapper.transform.localScale = Vector3.one;
        underBarWrapper.transform.localPosition = new Vector3(0.5f, -1, 0);

        underBar = GameObject.CreatePrimitive(PrimitiveType.Cube);
        underBar.name = "UnderBar";
        underBar.layer = ui_camera_layer;
        underBar.transform.SetParent(underBarWrapper.transform);
        underBar.transform.localScale = new Vector3(barWidth, 1, 0.25f);
        underBar.transform.localPosition = new Vector3(barWidth / 2, 0, 0);
        underBar.GetComponent<MeshRenderer>().material = underBarMaterial;

        overBarWrapper = new GameObject("OverBarWrapper");
        overBarWrapper.layer = ui_camera_layer;
        overBarWrapper.transform.SetParent(origin.transform);
        overBarWrapper.transform.localScale = Vector3.one;
        overBarWrapper.transform.localPosition = new Vector3(0.5f, -1, 0);

        overBar = GameObject.CreatePrimitive(PrimitiveType.Cube);
        overBar.name = "OverBar";
        overBar.layer = ui_camera_layer;
        overBar.transform.SetParent(overBarWrapper.transform);
        overBar.transform.localScale = new Vector3(barWidth, 1, 0.5f);
        overBar.transform.localPosition = new Vector3(barWidth / 2, 0, 0);
        overBar.GetComponent<MeshRenderer>().material = overBarMaterial;
    }

    public float BarWidth
    {
        private get => barWidth;
        set {
            barWidth = value;
            resetBarLength();
        }
    }

    public float UnderBarWidth
    {
        private get => underBarWidth;
        set
        {
            underBarWidth = value;
            underBarWrapper.transform.localScale = new Vector3(underBarWidth, 1, 1);
        }
    }

    public float OverBarWidth
    {
        private get => overBarWidth;
        set
        {
            overBarWidth = value;
            overBarWrapper.transform.localScale = new Vector3(overBarWidth, 1, 1);
        }
    }


    private void resetBarLength()
    {
        back.transform.localScale = new Vector3(barWidth + 1, 2, 0.01f);
        back.transform.localPosition = new Vector3((barWidth + 1) / 2, -1, 0.25f);

        right.transform.localPosition = new Vector3(barWidth + 0.75f, -1, -0.25f);

        up.transform.localScale = new Vector3(barWidth + 1, 0.5f, 0.5f);
        up.transform.localPosition = new Vector3((barWidth + 1) / 2, -0.25f, -0.25f);

        down.transform.localScale = new Vector3(barWidth + 1, 0.5f, 0.5f);
        down.transform.localPosition = new Vector3((barWidth + 1) / 2, -1.75f, -0.25f);
    }
}
