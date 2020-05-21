using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    //Used to identify what text object this script is attached to
    public int textCode = 1;

    public SpawnObjects spawnObjectsRoot;
    public Lava lava;
    public PlayerController player;
    public PressurePlate plate;

    private bool wPressed = false;
    private bool aPressed = false;
    private bool sPressed = false;
    private bool dPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (textCode)
        {
            case 1:
                if (caseOne() == true)
                {
                    Destroy(this.gameObject);
                }
                break;
            case 2:
                if (caseTwo() == 1 || caseTwo() == 2)
                {
                    Destroy(this.gameObject);
                }
                break;
            case 3:
                if (caseThree() == true)
                {
                    Destroy(this.gameObject);
                }
                break;
            case 4:
                if (caseFour() == true)
                {
                    Destroy(this.gameObject);
                }
                break;
            case 5:
                if (caseFive() == true)
                {
                    Destroy(this.gameObject);
                }
                break;
        }

    }

    /// <summary>
    /// Detects if WASD keys have been pressed.
    /// </summary>
    /// <returns></returns>
    bool caseOne()
    {
        if (Input.GetKeyDown("w"))
            wPressed = true;
        if (Input.GetKeyDown("a"))
            aPressed = true;
        if (Input.GetKeyDown("s"))
            sPressed = true;
        if (Input.GetKeyDown("d"))
            dPressed = true;
        if (wPressed == true && aPressed == true && sPressed == true && dPressed == true)
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Detects which machine buttons have been pressed and returns the index of the button pressed.
    /// </summary>
    /// <returns></returns>
    int caseTwo()
    {
        //Machine 1 (ball machine) button
        if (spawnObjectsRoot.ClickedButton() == 1)
        {
            return 1;
        }
        //Machine 2 (cube machine) button
        if (spawnObjectsRoot.ClickedButton() == 2)
        {
            return 2;
        }
        //Machine 3 (disabled machine) button
        if (spawnObjectsRoot.ClickedButton() == 3)
        {
            return 3;
        }
        else return 0;
    }

    /// <summary>
    /// Detects if an object was picked up
    /// </summary>
    /// <returns></returns>
    bool caseThree()
    {
        if (player.IsItemHeld() == true)
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Detects if an object was destroyed by lava
    /// </summary>
    /// <returns></returns>
    bool caseFour()
    {
        if (lava.firstDestroy == true)
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Detects if an object was placed on pressure plate
    /// </summary>
    /// <returns></returns>
    bool caseFive()
    {
        if (plate.colorChange == true)
        {
            return true;
        }
        else return false;
    }
}
