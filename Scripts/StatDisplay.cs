using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Displays various in-game statistics.
/// </summary>
public class StatDisplay : MonoBehaviour
{
    [SerializeField] SpawnObjects spawnObjects;
    [SerializeField] PressurePlate pressurePlate;
    [SerializeField] UIReferences uireference;
    [SerializeField] int displayCode;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DisplayCount(displayCode);
    }

    /// <summary>
    /// Displays the appropriate statistic. | 1 = Total Spawned. 2 = Balls Spawned. 3 = Cubes Spawned. 4 = Objects Altered.
    /// </summary>
    /// <param name="code"></param>
    public void DisplayCount(int code)
    {
        if (code == 1)
        {
            this.transform.GetComponent<TextMeshProUGUI>().text = ("Objects Spawned: " + SpawnedCount() + "/20");
        }
        else if (code == 2)
        {
            this.transform.GetComponent<TextMeshProUGUI>().text = ("Balls Spawned: " + SpawnedBallCount() + "/10");
        }
        else if (code == 3)
        {
            this.transform.GetComponent<TextMeshProUGUI>().text = ("Cubes Spawned: " + SpawnedCubeCount() + "/10");
        }
        else if (code == 4)
        {
            this.transform.GetComponent<TextMeshProUGUI>().text = ("Objects Altered: " + ObjectsAltered() + "/5");
        }
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
