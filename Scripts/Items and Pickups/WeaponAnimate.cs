using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.rotateAround(this.gameObject, Vector3.up, 360f, 2f).setLoopClamp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Calls when weapon is clicked on by player.
    /// </summary>
    public void Pickup()
    {
        LeanTween.cancel(this.gameObject);
        Destroy(this.gameObject);
    }
}
