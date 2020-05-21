using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Management utility for main menu functions.
/// </summary>
public class MainMenuManager : MonoBehaviour
{ 

    [SerializeField] UIReferences uireference;

    private AudioSource audioSource;
    [SerializeField] AudioClip buttonSelectSound;

    private GameObject startMenu;
    private GameObject enterButton;

    private GameObject mainMenu;
    private GameObject lselButton;
    private GameObject setButton;
    private GameObject quitButton;

    private GameObject levelSelectMenu;
    private GameObject l1Button;
    private GameObject l2Button;
    private GameObject l3Button;
    private GameObject goBackButton;

    private GameObject quitMenu;
    private GameObject qYesButton;
    private GameObject qNoButton;

    // Start is called before the first frame update
    void Start()
    {
        //Audio Elements
        audioSource = GetComponent<AudioSource>();


        //UI Element references
        startMenu = uireference.startMenu;
        enterButton = uireference.enterButton;

        mainMenu = uireference.mainMenu;
        lselButton = uireference.levelSelectButton;
        setButton = uireference.settingsButton;
        quitButton = uireference.quitButton;

        levelSelectMenu = uireference.levelSelectMenu;
        l1Button = uireference.l1Button;
        l2Button = uireference.l2Button;
        l3Button = uireference.l3Button;
        goBackButton = uireference.goBackButton;

        quitMenu = uireference.quitMenu;
        qYesButton = uireference.quitYesButton;
        qNoButton = uireference.quitNoButton;

        SetButtonPos();
        startMenu.SetActive(true);
        LeanTween.moveLocalY(enterButton, -20f, 0.5f);
        mainMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        quitMenu.SetActive(false);
    }

    /// <summary>
    /// Shows main menu buttons and hides all others.
    /// </summary>
    public void MMEnter()
    {
        SetButtonPos();
        audioSource.PlayOneShot(buttonSelectSound);

        startMenu.SetActive(false);
        mainMenu.SetActive(true);
        LeanTween.moveLocalY(lselButton, 10f, 0.5f);
        LeanTween.moveLocalY(setButton, -40f, 0.5f);
        LeanTween.moveLocalY(quitButton, -90f, 0.5f);
        levelSelectMenu.SetActive(false);
        quitMenu.SetActive(false);
        Debug.Log("Pressed enter button.");
    }

    /// <summary>
    /// Shows quit menu buttons and hides all others.
    /// </summary>
    public void MMQuit()
    {
        SetButtonPos();
        audioSource.PlayOneShot(buttonSelectSound);

        startMenu.SetActive(false);
        mainMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        quitMenu.SetActive(true);
        LeanTween.moveLocalY(qYesButton, -70f, 0.5f);
        LeanTween.moveLocalY(qNoButton, -20f, 0.5f);
    }

    /// <summary>
    /// Closes the game.
    /// </summary>
    public void MMQuitYes()
    {
        audioSource.PlayOneShot(buttonSelectSound);
        Application.Quit();
    }

    /// <summary>
    /// Shows level select menu buttons and hides all other ones.
    /// </summary>
    public void MMLevelSelect()
    {
        SetButtonPos();
        audioSource.PlayOneShot(buttonSelectSound);

        startMenu.SetActive(false);
        mainMenu.SetActive(false);
        levelSelectMenu.SetActive(true);
        LeanTween.moveLocalY(l1Button, -20f, 0.5f);
        LeanTween.moveLocalY(l2Button, -20f, 0.5f);
        LeanTween.moveLocalY(goBackButton, -70f, 0.5f);
        quitMenu.SetActive(false);
    }

    /// <summary>
    /// Loads level 1.
    /// </summary>
    public void Level1Select()
    {
        audioSource.PlayOneShot(buttonSelectSound);
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Loads level 2.
    /// </summary>
    public void Level2Select()
    {
        audioSource.PlayOneShot(buttonSelectSound);
        SceneManager.LoadScene(2);
    }

    public void SetButtonPos()
    {
        enterButton.transform.localPosition = new Vector3(0, 150, 0);
        lselButton.transform.localPosition = new Vector3(0, 150, 0);
        setButton.transform.localPosition = new Vector3(0, 150, 0);
        quitButton.transform.localPosition = new Vector3(0, 150, 0);
        l1Button.transform.localPosition = new Vector3(-90, 150, 0);
        l2Button.transform.localPosition = new Vector3(90, 150, 0);
        //l3Button.transform.localPosition = new Vector3(0, 150, 0);
        goBackButton.transform.localPosition = new Vector3(0, 150, 0);
        qYesButton.transform.localPosition = new Vector3(0, 150, 0);
        qNoButton.transform.localPosition = new Vector3(0, 150, 0);
        Debug.Log("Reset button positions to default.");
    }
}
