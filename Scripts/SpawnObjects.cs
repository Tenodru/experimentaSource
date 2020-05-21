using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpawnObjects : MonoBehaviour
{
    //Hook variables used as references in script
    public Transform ball;
    public Transform cube;
    public Camera playerCam;

    private int spawnedCount;
    private int spawnedBallCount;
    private int spawnedCubeCount;

    [Header("Sound")]
    [SerializeField] AudioClip buttonPress;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Spawns appropriate object based on return of ClickedButton
        if (Input.GetButtonDown("Fire1") && ClickedButton() == 1)
        {
            Debug.Log("Clicked M1 Button");
            audioSource.PlayOneShot(buttonPress);
            Instantiate(ball, new Vector3(Random.Range(-9.0f, -1.0f), 16.5f, Random.Range(6.5f, 12.5f)), new Quaternion(0,0,0,0));
            spawnedBallCount++;
            spawnedCount++;
        }
        if (Input.GetButtonDown("Fire1") && ClickedButton() == 2)
        {
            Debug.Log("Clicked M2 Button");
            audioSource.PlayOneShot(buttonPress);
            Instantiate(cube, new Vector3(Random.Range(10.5f, 18.5f), 16.5f, Random.Range(6.0f, 12.0f)), new Quaternion(0, 0, 0, 0));
            spawnedCubeCount++;
            spawnedCount++;
        }
    }

    /// <summary>
    /// Detects which machine button was pressed via raycast.
    /// </summary>
    /// <returns></returns>
    public int ClickedButton()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.distance < 3.0f)
        {
            if (hit.transform.name == "M1 Button")
                return 1;
            else if (hit.transform.name == "M2 Button")
                return 2;
            else if (hit.transform.name == "M3 Button")
                return 3;
        }
        return 0;
    }

    /// <summary>
    /// Gets the total number of objects spawned.
    /// </summary>
    /// <returns></returns>
    public int GetSpawnedCount()
    {
        return spawnedCount;
    }

    /// <summary>
    /// Gets the total number of balls spawned.
    /// </summary>
    /// <returns></returns>
    public int GetSpawnedBallCount()
    {
        return spawnedBallCount;
    }

    /// <summary>
    /// Gets the total number of cubes spawned.
    /// </summary>
    /// <returns></returns>
    public int GetSpawnedCubeCount()
    {
        return spawnedCubeCount;
    }
}
