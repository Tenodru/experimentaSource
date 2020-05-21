using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public UIManager gameMenu;
    public float lookSpeed = 2.0f;
    private Vector2 rotation = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Sets camera rotation axes to mouse movement axes
        rotation.y += Input.GetAxis("Mouse Y") * -1.0f;
        rotation.y = Mathf.Clamp(rotation.y, -30f, 30f); //Limits how far player can look up or down
        rotation.x += Input.GetAxis("Mouse X");

        //Checks if game is paused and disables camera movement if so
        if(gameMenu.IsPaused() == true)
        {
            transform.eulerAngles = new Vector2(0, rotation.x) * lookSpeed * Time.deltaTime;
            Camera.main.transform.localRotation = Quaternion.Euler(rotation.y * lookSpeed * Time.deltaTime, 0, 0);
        }
        else if (gameMenu.IsPaused() == false)
        {
            transform.eulerAngles = new Vector2(0, rotation.x) * lookSpeed;
            Camera.main.transform.localRotation = Quaternion.Euler(rotation.y * lookSpeed, 0, 0);
        }
    }
}
