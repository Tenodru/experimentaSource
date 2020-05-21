using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class SpawnObjects2 : MonoBehaviour
{
    //Hook variables used as references in script
    [Header("Food Objects")]
    [SerializeField] Transform blueFood;
    [SerializeField] Transform greenFood;
    [SerializeField] Transform redFood;

    [Header("Other References")]
    [SerializeField] Camera playerCam;
    [SerializeField] Light spotlight;
    [SerializeField] GameObject creature;
    [SerializeField] Transform barrier;
    List<Transform> labLights = new List<Transform>();

    [Header("Sound")]
    [SerializeField] AudioClip labStartSound;
    [SerializeField] AudioClip barrierDrop;
    [SerializeField] AudioClip buttonPress;
    AudioSource audioSource;

    private int spawnedCount;
    private int fedCount;
    private int curedCount;
    private bool labStart;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        labStart = false;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Light"))
        {
            labLights.Add(obj.GetComponent<Transform>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Spawns appropriate object based on return of ClickedButton
        if (Input.GetButtonDown("Fire1") && ClickedButton() == 1)
        {
            Debug.Log("Clicked M1 Button");
            audioSource.PlayOneShot(buttonPress);
            Instantiate(blueFood, new Vector3(Random.Range(40f, 48f), 10.0f, Random.Range(-37.5f, -31.5f)), new Quaternion(0, 0, 0, 0));
        }
        if (Input.GetButtonDown("Fire1") && ClickedButton() == 2)
        {
            Debug.Log("Clicked M2 Button");
            audioSource.PlayOneShot(buttonPress);
            Instantiate(greenFood, new Vector3(Random.Range(40f, 48f), 10.0f, Random.Range(-48f, -42f)), new Quaternion(0, 0, 0, 0));
        }
        if (Input.GetButtonDown("Fire1") && ClickedButton() == 3)
        {
            Debug.Log("Clicked M3 Button");
            audioSource.PlayOneShot(buttonPress);
            Instantiate(redFood, new Vector3(Random.Range(40f, 48f), 10.0f, Random.Range(-58.5f, -52.5f)), new Quaternion(0, 0, 0, 0));
        }
        if (Input.GetButtonDown("Fire1") && ClickedButton() == 4)
        {
            Debug.Log("Clicked start Button");
            audioSource.PlayOneShot(buttonPress);
            SpawnWave();
        }
        if (Input.GetButtonDown("Fire1") && ClickedButton() == 5)
        {
            Debug.Log("Clicked barrier Button");
            audioSource.PlayOneShot(buttonPress);
            DropBarrier();
        }
    }

    /// <summary>
    /// Detects which machine button was pressed via raycast.
    /// </summary>
    /// <returns></returns>
    public int ClickedButton()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.distance < 3.0f)
        {
            if (hit.transform.name == "M1 Button")
                return 1;
            else if (hit.transform.name == "M2 Button")
                return 2;
            else if (hit.transform.name == "M3 Button")
                return 3;
            else if (hit.transform.name == "Lab Button")
                return 4;
            else if (hit.transform.name == "Barrier Button")
                return 5;
        }
        return 0;
    }

    /// <summary>
    /// Spawns creatures.
    /// </summary>
    public void SpawnWave()
    {
        if (!labStart)
        {
            for (int i = 0; i < labLights.Count; i++)
            {
                labLights[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 255, 255));
            }
            spotlight.color = new Color(0, 255, 255);
            spotlight.range = 10.0f;
            spotlight.intensity = 0.05f;
            labStart = true;
            audioSource.PlayOneShot(labStartSound);
        }

        Instantiate(creature, new Vector3(27, 1, 20), new Quaternion(0, 0, 0, 0));
        Instantiate(creature, new Vector3(5, 1, 20), new Quaternion(0, 0, 0, 0));
        Instantiate(creature, new Vector3(38, 1, 20), new Quaternion(0, 0, 0, 0));
    }

    /// <summary>
    /// Drops the lab barrier.
    /// </summary>
    public void DropBarrier()
    {
        audioSource.PlayOneShot(barrierDrop);
        LeanTween.moveLocalY(barrier.gameObject, -12.7f, 5).setEaseInOutBounce();
    }

    public bool IsLabStart()
    {
        return labStart;
    }
}
