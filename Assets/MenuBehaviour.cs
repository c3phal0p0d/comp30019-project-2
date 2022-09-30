using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public string difficultySelectionScene;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void newGame()
    {

        SceneManager.LoadScene(difficultySelectionScene);
    }

    public void options()
    {

    }

    public void quitGame()
    {

        Application.Quit();
    }

}
