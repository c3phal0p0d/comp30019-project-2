using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public string difficultySelectionScene;
    public GameObject canvas;
    public Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void newGame()
    {
        canvas.SetActive(false);
        MoveCamera.setCameraSpeed(10);


    }

    public float setCameraSpeed()
    {
        return 10;
    }

    public void options()
    {

    }

    public void quitGame()
    {

        Application.Quit();
    }

}
