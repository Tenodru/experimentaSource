using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Management utility for in-game UI elements.
/// </summary>
public class UIManager : MonoBehaviour
{
    static bool paused = false;
    public UIReferences uireference;

    void Start()
    {
        Time.timeScale = 1.0f;
        HidePaused();
    }
    
    void Update()
    {
        Escape();
    }

    /// <summary>
    /// Uses the escape button to pause and unpause the game.
    /// </summary>
    public void Escape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            if (paused == false)
            {
                Time.timeScale = 0;
                paused = true;
                ShowPaused();
            }
            else if (paused == true)
            {
                Resume();
            }
        }
    }

    /// <summary>
    /// Resumes the game.
    /// </summary>
    public void Resume()
    {
        Debug.Log("Resumed.");
        Time.timeScale = 1.0f;
        paused = false;
        HidePaused();
    }

    /// <summary>
    /// Shows the pause menu.
    /// </summary>
    public void ShowPaused()
    {
        uireference.pauseMenu.SetActive(true);
        uireference.pauseMainMenu.SetActive(false);
    }

    /// <summary>
    /// Hides the pause menu.
    /// </summary>
    public void HidePaused()
    {
        Cursor.visible = false;
        uireference.pauseMenu.SetActive(false);
        uireference.pauseMainMenu.SetActive(false);
        Debug.Log("Hidden.");
    }

    /// <summary>
    /// Returns whether or not the game is paused.
    /// </summary>
    /// <returns></returns>
    public bool IsPaused()
    {
        return paused;
    }

    /// <summary>
    /// Closes the game.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Shows the main menu confirmation screen.
    /// </summary>
    public void MainMenu()
    {
        uireference.pauseMenu.SetActive(false);
        uireference.pauseMainMenu.SetActive(true);
    }

    /// <summary>
    /// Goes back to the main menu.
    /// </summary>
    public void MainMenuConfirm()
    {
        SceneManager.LoadScene(0);
    }
}
