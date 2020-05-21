using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Management utility for dialogue statements.
/// </summary>
public class WinConditionManager2 : MonoBehaviour
{
    [SerializeField] SpawnObjects2 spawnObjects;
    [SerializeField] UIReferences uireference;
    [SerializeField] Transform dialogue;

    int reqMet;
    bool wasFed;
    bool wasMadeHostile;
    bool wasCured;
    bool wasMadeFriendly;

    bool wasAlreadyFed;
    bool wasAlreadyHostile;
    bool wasAlreadyCured;
    bool wasAlreadyFriendly;

    bool labStarted;

    bool metFedCount;
    bool metReq3;
    bool metReq4;


    // Start is called before the first frame update
    void Start()
    {
        wasFed = false;
        wasMadeHostile = false;
        wasCured = false;
        wasMadeFriendly = false;

        wasAlreadyFed = false;
        wasAlreadyHostile = false;
        wasAlreadyCured = false;
        wasAlreadyFriendly = false;

        labStarted = false;

        metFedCount = false;
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
        if (spawnObjects.IsLabStart() && labStarted == false)
        {
            dialogue.transform.GetComponent<TextMeshProUGUI>().text = "These are one of Experimenta's lab species.";
            StartCoroutine(DelayMessage(5, "They are relatively friendly...unless you feed them the wrong food!"));
            StartCoroutine(DelayMessage(10, "When you are ready, press the button in front of the barrier to lower it."));
            StartCoroutine(StopPassMessage(15));
            labStarted = true;
        }
        if (wasFed == true && wasAlreadyFed == false)
        {
            dialogue.transform.GetComponent<TextMeshProUGUI>().text = "Well done, you've fed a creature!";
            StartCoroutine(DelayMessage(5, "Keep experimenting with the food!"));
            StartCoroutine(StopPassMessage(10));
            wasAlreadyFed = true;
            reqMet++;
        }
        if (wasMadeFriendly == true && wasAlreadyFriendly == false)
        {
            dialogue.transform.GetComponent<TextMeshProUGUI>().text = "Ah, looks like you've made a friend!";
            StartCoroutine(DelayMessage(5, "Feeding a neutral creature enough times will make them friendly towards you."));
            StartCoroutine(DelayMessage(10, "Friendly creatures will turn green and will follow you around!"));
            StartCoroutine(StopPassMessage(15));
            wasAlreadyFriendly = true;
            reqMet++;
        }
        if (wasMadeHostile == true && wasAlreadyHostile == false)
        {
            dialogue.transform.GetComponent<TextMeshProUGUI>().text = "Uh oh, you've angered someone!";
            StartCoroutine(DelayMessage(5, "Feeding creature too much of the wrong food will make them hostile."));
            StartCoroutine(DelayMessage(10, "Hostile creatures will try to attack you!"));
            StartCoroutine(DelayMessage(15, "Fortunately, they can be pacified with the right food."));
            StartCoroutine(StopPassMessage(20));
            wasAlreadyHostile = true;
            reqMet++;
        }
        if (wasCured == true && wasAlreadyCured == false)
        {
            dialogue.transform.GetComponent<TextMeshProUGUI>().text = "There you go, you've pacified a hostile creature!";
            StartCoroutine(DelayMessage(5, "Pacifying a creature will turn its hide back to blue."));
            StartCoroutine(DelayMessage(10, "You can then feed them as normal!"));
            StartCoroutine(StopPassMessage(15));
            wasAlreadyCured = true;
            reqMet++;
        }
        if (reqMet == 4 && metReq4 == false)
        {
            StartCoroutine(DelayMessage(18, "You seem to have familiarized yourself with these creatures!"));
            StartCoroutine(DelayMessage(24, "Your test is nearly done. Feel free to keep playing with the creatures!"));
            StartCoroutine(DelayMessage(30, "We will head over and explain the final phase of your test."));
            StartCoroutine(StopPassMessage(36));
            metReq4 = true;
        }
    }

    /// <summary>
    /// Displays the start dialogue.
    /// </summary>
    public void StartDialogue()
    {
        StartCoroutine(DelayMessage(0, "Welcome to Wing Eight-B of the Experimenta facility."));
        StartCoroutine(DelayMessage(5, "Your test is not yet complete. We need to see how you work with living creatures."));
        StartCoroutine(DelayMessage(10, "Press the button in the center of the room when you are ready."));
        StartCoroutine(StopPassMessage(15));
    }

    public void IsFed(bool state)
    {
        wasFed = state;
    }
    public void IsMadeHostile(bool state)
    {
        wasMadeHostile = state;
    }
    public void IsCured(bool state)
    {
        wasCured = state;
    }
    public void IsMadeFriendly(bool state)
    {
        wasMadeFriendly = state;
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
}
