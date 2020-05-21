using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    //Hook variable used as reference in script
    public Transform player;

    //Hook variable, do not change in Unity editor.
    public bool colorChange = false;

    private int objectsChanged;

    // Start is called before the first frame update
    void Start()
    {
        objectsChanged = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// When object is placed on pressure plate, object changes to a random color.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pickup")
        {
            Color newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            var rend = other.GetComponent<Renderer>();
            rend.material.SetColor("_Color", newColor);
            colorChange = true;
            objectsChanged++;
        }
    }

    public int GetObjectsChanged()
    {
        return objectsChanged;
    }
}
