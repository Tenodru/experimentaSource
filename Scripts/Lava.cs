using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour
{
    //Hook variable used as reference for whether an object has been destroyed by lava. Do not change in Unity editor.
    public bool firstDestroy = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Pickup")
        {
            Destroy(c.gameObject);
            firstDestroy = true;
        }
        if(c.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
