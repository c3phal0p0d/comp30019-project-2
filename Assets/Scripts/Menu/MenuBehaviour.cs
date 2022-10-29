using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    public string difficultySelectionScene;
    public GameObject canvas;
    public Camera mainCamera;

    [SerializeField]
    private Canvas gameplayInstructions;
    [SerializeField]
    private Canvas controlsInfo;
    [SerializeField]
    private Canvas mainMenu;

    void Start()
    {
        Cursor.visible = true;
        mainCamera.GetComponent<MoveCamera>().move = false;
    }

    public void PlayButtonHoverSound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonHover");
    }

    public void PlayButtonClickSound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void GoToDifficultySelection()
    {
        canvas.SetActive(false);
        mainCamera.GetComponent<MoveCamera>().move = true;
        MoveCamera.setCameraSpeed(10);
    }

    public void EasyNewGame()
    {
        SceneManager.LoadScene("Scenes/Easy");
    }

    public void MediumNewGame()
    {
        SceneManager.LoadScene("Scenes/Medium");
    }
    public void HardNewGame()
    {
        SceneManager.LoadScene("Scenes/Hard");
    }

    public void DisplayInstructions()
    {
        gameplayInstructions.enabled = true;
        controlsInfo.enabled = false;
        mainMenu.enabled = false;
    }

    public void DisplayControls()
    {
        gameplayInstructions.enabled = false;
        controlsInfo.enabled = true;
    }

    public void ExitToMenu()
    {
        gameplayInstructions.enabled = false;
        controlsInfo.enabled = false;
        mainMenu.enabled = true;
    }

    public void QuitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
