using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Management utility for dialogue statements.
/// </summary>
public class WinConditionManager : MonoBehaviour
{
    [SerializeField] SpawnObjects spawnObjects;
    [SerializeField] UIReferences uireference;
    [SerializeField] PressurePlate pressurePlate;
    [SerializeField] Transform dialogue;

    private int reqMet;
    private bool metSpawnedCount;
    private bool metSpawnedBallCount;
    private bool metSpawnedCubeCount;
    private bool metAlteredCount;
    private bool metReq3;
    private bool metReq4;


    // Start is called before the first frame update
    void Start()
    {
        metSpawnedCount = false;
        metSpawnedBallCount = false;
        metSpawnedCubeCount = false;
        metAlteredCount = false;
        metReq3 = false;
        metReq4 = false;
        reqMet = 0;
        Debug.Log("Working.");
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        Passed();
    }

    /// <summary>
    /// Determines whether or not the player has passed the test by meeting all of the stat requirements.
    /// </summary>
    public void Passed()
    {
        if (SpawnedCount() == 20 && metSpawnedCount == false)
        {
            dialogue.transform.GetComponent<TextMeshProUGUI>().text = "Nice, you've created 20 fabrications!";
            StartCoroutine(StopPassMessage(5));
            reqMet++;
            metSpawnedCount = true;
        }
        if (SpawnedBallCount() == 10 && metSpawnedBallCount == false)
        {
            dialogue.transform.GetComponent<TextMeshProUGUI>().text = "Ok, you've created 10 balls!";
            StartCoroutine(StopPassMessage(5));
            reqMet++;
            metSpawnedBallCount = true;
        }
        if (SpawnedCubeCount() == 10 && metSpawnedCubeCount == false)
        {
            dialogue.transform.GetComponent<TextMeshProUGUI>().text = "Alright, you've created 10 cubes!";
            StartCoroutine(StopPassMessage(5));
            reqMet++;
            metSpawnedCubeCount = true;
        }
        if (ObjectsAltered() == 5 && metAlteredCount == false)
        {
            dialogue.transform.GetComponent<TextMeshProUGUI>().text = "Great, you've altered objects 5 times!";
            StartCoroutine(StopPassMessage(5));
            reqMet++;
            metAlteredCount = true;
        }
        if (reqMet == 3 && metReq3 == false)
        {
            dialogue.transform.GetComponent<TextMeshProUGUI>().text = "You've nearly met all of the requirements!";
            StartCoroutine(StopPassMessage(5));
            metReq3 = true;
        }
        if (reqMet == 4 && metReq4 == false)
        {
            dialogue.transform.GetComponent<TextMeshProUGUI>().text = "Perfect, you've met all of my requirements! I think you'll make a fine trainee here at Experimenta.";
            StartCoroutine(DelayMessage(8, "Feel free to keep exploring the wing!"));
            StartCoroutine(StopPassMessage(13));
            metReq4 = true;
        }
    }

    /// <summary>
    /// Displays the start dialogue.
    /// </summary>
    public void StartDialogue()
    {
        StartCoroutine(DelayMessage(0, "Welcome to Wing Eight of the Experimenta facility."));
        StartCoroutine(DelayMessage(5, "You are one in a long line of potential trainees here - in order to become a trainee, you must first pass a simple test."));
        StartCoroutine(DelayMessage(10, "Let's see if you can learn how the machines in this wing function."));
        StartCoroutine(StopPassMessage(15));
    }

    /// <summary>
    /// Hides the passed message after a given time.
    /// </summary>
    /// <returns></returns>
    IEnumerator StopPassMessage(int time)
    {
        yield return new WaitForSeconds(time);
        dialogue.transform.GetComponent<TextMeshProUGUI>().text = " ";
    }

    /// <summary>
    /// Displays dialogue on the screen, after a delay.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="line"></param>
    /// <returns></returns>
    IEnumerator DelayMessage(int time, string line)
    {
        yield return new WaitForSeconds(time);
        dialogue.transform.GetComponent<TextMeshProUGUI>().text = line;
    }

    private int SpawnedCount()
    {
        return spawnObjects.GetSpawnedCount();
    }

    private int SpawnedBallCount()
    {
        return spawnObjects.GetSpawnedBallCount();
    }

    private int SpawnedCubeCount()
    {
        return spawnObjects.GetSpawnedCubeCount();
    }

    private int ObjectsAltered()
    {
        return pressurePlate.GetObjectsChanged();
    }
}
