using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reference storage script used by UI and MainMenu scripts.
/// </summary>
public class UIReferences : MonoBehaviour
{
    //References for main menu elements.
    [Header("Start Menu")]
    public GameObject startMenu;
    public GameObject enterButton;

    [Header("Main Menu")]
    public GameObject mainMenu;
    public GameObject levelSelectButton;
    public GameObject settingsButton;
    public GameObject quitButton;

    [Header("Level Select Menu")]
    public GameObject levelSelectMenu;
    public GameObject l1Button;
    public GameObject l2Button;
    public GameObject l3Button;
    public GameObject goBackButton;

    [Header("Quit Menu")]
    public GameObject quitMenu;
    public GameObject quitYesButton;
    public GameObject quitNoButton;

    [Header("In-Game UI")]
    //References for in-game UI elements.
    public GameObject pauseMenu;
    public GameObject pauseMainMenu;
    public GameObject passed;
}
