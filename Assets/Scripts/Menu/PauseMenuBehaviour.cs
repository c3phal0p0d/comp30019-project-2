using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{
    public GameObject canvas;

    [SerializeField]
    private Canvas gameplayInstructions;
    [SerializeField]
    private Canvas controlsInfo;
    [SerializeField]
    private Canvas pauseMenu;

    void Start()
    {
        Cursor.visible = true;
    }

    public void PlayButtonHoverSound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonHover");
    }

    public void PlayButtonClickSound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void DisplayInstructions()
    {
        gameplayInstructions.enabled = true;
        controlsInfo.enabled = false;
        pauseMenu.enabled = false;
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
        pauseMenu.enabled = true;
    }

    public void ExitToMainMenu(){
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1;
    }

    public void ResumeGame(){
        Cursor.visible = false;
        pauseMenu.enabled = false;
        Time.timeScale = 1;
        FindObjectOfType<AudioManager>().Play("BackgroundMusic");
        FindObjectOfType<PlayerAttack>().isPaused = false;
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
