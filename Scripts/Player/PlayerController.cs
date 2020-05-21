using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CameraController))]
public class PlayerController : MonoBehaviour
{
    enum HeldItemType { Empty, Ball, Cube, Weapon1, BlueFood, GreenFood, RedFood };

    //Mostly hook variables used as references in this script
    [Header("Object References")]
    public Transform playerBody;
    public Transform heldPos;
    public UIManager gameMenu;
    Transform heldItem = null;
    HeldItemType heldItemType;

    //Flat variables that can be changed in Unity Editor
    [Header("Player Stats")]
    [SerializeField] float defaultSpeed = 10.0f;
    [SerializeField] float jumpForce = 10.0f;
    [SerializeField] float interactDistance = 8;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float jumpMultiplier = 2f;

    Rigidbody rb;

    [Header("Weapon References")]
    [SerializeField] GameObject gun1;

    [Header("Held Item References")]
    [SerializeField] Transform ballHeld;
    [SerializeField] Transform cubeHeld;
    [SerializeField] Transform weapon1;
    [SerializeField] Transform blueFoodHeld;
    [SerializeField] Transform greenFoodHeld;
    [SerializeField] Transform redFoodHeld;

    private float speed;
    private float movementV;
    private float movementH;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        ballHeld.gameObject.SetActive(false);
        cubeHeld.gameObject.SetActive(false);
        weapon1.gameObject.SetActive(false);
        blueFoodHeld.gameObject.SetActive(false);
        greenFoodHeld.gameObject.SetActive(false);
        redFoodHeld.gameObject.SetActive(false);

        heldItemType = HeldItemType.Empty;
        speed = defaultSpeed;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Sets movement speed for each axis (should be the same)
        movementV = Input.GetAxis("Vertical") * speed; //Forward/backward
        movementH = Input.GetAxis("Horizontal") * speed; //Side to side

        //Stops ability to move when game is paused
        movementV *= Time.deltaTime;
        movementH *= Time.deltaTime;

        //Moves player
        transform.Translate(0, 0, movementV);
        transform.Translate(movementH, 0, 0);

        //Jump function
        if (Input.GetKeyDown("space") && CanJump())
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            rb.velocity += Physics.gravity * (fallMultiplier - 1) * Time.deltaTime;
            Debug.Log("Jumped.");
        }

        //Sprint function
        if (Input.GetKeyDown("left shift"))
        {
            speed = defaultSpeed + (defaultSpeed * 0.3f);
        }
        if (Input.GetKeyUp("left shift"))
        {
            speed = defaultSpeed;
        }
        PlayerPickUp();
        PlayerGive();

        PlayerReleaseItem();
    }


    /// <summary>
    /// Checks if player is/isn't in the air, if player is in the air then player cannot jump.
    /// </summary>
    /// <returns></returns>
    bool CanJump()
    {
        Ray ray = new Ray(transform.position, transform.up * -1);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, transform.localScale.y))
            return true;
        else
            return false;
    }


    /// <summary>
    /// Detects and picks up items.
    /// </summary>
    void PlayerPickUp()
    {
        RaycastHit hit;

        //Checks if ray collided with an object
        if (Input.GetButtonDown("Fire2") && Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactDistance)) //Mouse2 button
        {

            if (hit.transform.tag == "Pickup" && heldItem == null) //Checks if object hit has tag "Pickup" and no object is currently being held by player
            {
                //Checks what item the player is picking up
                if (hit.transform.name == "Ball(Clone)")
                {
                    heldItemType = HeldItemType.Ball;
                    heldItem = hit.transform;
                    hit.transform.gameObject.SetActive(false);
                    ballHeld.GetComponent<Renderer>().material.color = heldItem.transform.GetComponent<Renderer>().material.color;
                    ballHeld.gameObject.SetActive(true);
                }
                if (hit.transform.name == "Cube(Clone)")
                {
                    heldItemType = HeldItemType.Cube;
                    heldItem = hit.transform;
                    hit.transform.gameObject.SetActive(false);
                    cubeHeld.GetComponent<Renderer>().material.color = heldItem.transform.GetComponent<Renderer>().material.color;
                    cubeHeld.gameObject.SetActive(true);
                }

                if (hit.transform.name == "BlueFood(Clone)")
                {
                    heldItemType = HeldItemType.BlueFood;
                    heldItem = hit.transform;
                    hit.transform.gameObject.SetActive(false);
                    blueFoodHeld.GetComponent<Renderer>().material.color = heldItem.transform.GetComponent<Renderer>().material.color;
                    blueFoodHeld.gameObject.SetActive(true);
                }
                if (hit.transform.name == "GreenFood(Clone)")
                {
                    heldItemType = HeldItemType.GreenFood;
                    heldItem = hit.transform;
                    hit.transform.gameObject.SetActive(false);
                    greenFoodHeld.GetComponent<Renderer>().material.color = heldItem.transform.GetComponent<Renderer>().material.color;
                    greenFoodHeld.gameObject.SetActive(true);
                }
                if (hit.transform.name == "RedFood(Clone)")
                {
                    heldItemType = HeldItemType.RedFood;
                    heldItem = hit.transform;
                    hit.transform.gameObject.SetActive(false);
                    redFoodHeld.GetComponent<Renderer>().material.color = heldItem.transform.GetComponent<Renderer>().material.color;
                    redFoodHeld.gameObject.SetActive(true);
                }
            }

            if (hit.transform.tag == "Weapon" && heldItem == null) //Checks if object hit has tag "Pickup" and no object is currently being held by player
            {
                heldItemType = HeldItemType.Weapon1;
                heldItem = hit.transform;
                hit.transform.gameObject.SetActive(false);

                weapon1.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Player uses item.
    /// </summary>
    void PlayerGive()
    {
        RaycastHit hit;

        //Checks if ray collided with an object
        if (Input.GetButtonDown("Fire2") && Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactDistance)) //Mouse2 button
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red);
            Debug.Log("Detected " + hit.transform.name);
            if (heldItem != null && IsFoodHeld() && hit.transform.tag == "Creature")
            {
                if (hit.transform.GetComponent<EnemyAI>().GetAllegiance() == EnemyAI.CreatureAllegiance.Neutral)
                {
                    if (heldItemType == HeldItemType.BlueFood || heldItemType == HeldItemType.GreenFood)
                    {
                        Debug.Log("Fed " + hit.transform.name);
                        blueFoodHeld.gameObject.SetActive(false);
                        greenFoodHeld.gameObject.SetActive(false);
                        heldItem.gameObject.SetActive(true);
                        Destroy(heldItem.gameObject);

                        heldItemType = HeldItemType.Empty;
                        heldItem = null;

                        hit.transform.GetComponent<EnemyAI>().Eat(1);
                    }
                    if (heldItemType == HeldItemType.RedFood)
                    {
                        Debug.Log("Fed " + hit.transform.name);
                        redFoodHeld.gameObject.SetActive(false);
                        heldItem.gameObject.SetActive(true);
                        Destroy(heldItem.gameObject);

                        heldItemType = HeldItemType.Empty;
                        heldItem = null;

                        hit.transform.GetComponent<EnemyAI>().Eat(2);
                    }
                }
                else if (hit.transform.GetComponent<EnemyAI>().GetAllegiance() == EnemyAI.CreatureAllegiance.Hostile)
                {
                    if (heldItemType == HeldItemType.GreenFood)
                    {
                        Debug.Log("Fed " + hit.transform.name);
                        greenFoodHeld.gameObject.SetActive(false);
                        heldItem.gameObject.SetActive(true);
                        Destroy(heldItem.gameObject);

                        heldItemType = HeldItemType.Empty;
                        heldItem = null;

                        hit.transform.GetComponent<EnemyAI>().Eat(3);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Drops item if one is being held
    /// </summary>
    void PlayerReleaseItem()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //Q key
        {
            if (heldItemType == HeldItemType.Ball)
            {
                ballHeld.gameObject.SetActive(false);
                heldItem.gameObject.SetActive(true);
                heldItem.transform.position = transform.position + transform.forward;
                heldItemType = HeldItemType.Empty;
                heldItem = null;
            }
            if (heldItemType == HeldItemType.Cube)
            {
                cubeHeld.gameObject.SetActive(false);
                heldItem.gameObject.SetActive(true);
                heldItem.transform.position = transform.position + transform.forward;
                heldItemType = HeldItemType.Empty;
                heldItem = null;
            }
            if (heldItemType == HeldItemType.Weapon1)
            {
                weapon1.gameObject.SetActive(false);
                Instantiate(gun1, transform.position + transform.forward, transform.rotation);
                heldItemType = HeldItemType.Empty;
                Destroy(heldItem.gameObject);
            }
            if (heldItemType == HeldItemType.BlueFood)
            {
                blueFoodHeld.gameObject.SetActive(false);
                heldItem.gameObject.SetActive(true);
                heldItem.transform.position = transform.position + transform.forward;
                heldItemType = HeldItemType.Empty;
                heldItem = null;
            }
            if (heldItemType == HeldItemType.GreenFood)
            {
                greenFoodHeld.gameObject.SetActive(false);
                heldItem.gameObject.SetActive(true);
                heldItem.transform.position = transform.position + transform.forward;
                heldItemType = HeldItemType.Empty;
                heldItem = null;
            }
            if (heldItemType == HeldItemType.RedFood)
            {
                redFoodHeld.gameObject.SetActive(false);
                heldItem.gameObject.SetActive(true);
                heldItem.transform.position = transform.position + transform.forward;
                heldItemType = HeldItemType.Empty;
                heldItem = null;
            }
        }
    }

    /// <summary>
    /// Returns true if food is held by player.
    /// </summary>
    /// <returns>True if food is held.</returns>
    public bool IsFoodHeld()
    {
        if (heldItemType == HeldItemType.BlueFood || heldItemType == HeldItemType.GreenFood || heldItemType == HeldItemType.RedFood)
        {
            Debug.Log("Food is currently held.");
            return true;
        }
        return false;
    }

    public bool IsItemHeld()
    {
        if (heldItemType != HeldItemType.Empty)
            return true;
        else return false;
    }
}
