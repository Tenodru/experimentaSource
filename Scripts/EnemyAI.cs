using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// State of the creature: Idle, Wander, SeekFood
    /// </summary>
    public enum CreatureState { Idle, Waiting, Wander, SeekFood, SeekEnemy, Attacking };

    /// <summary>
    /// Creature's allegiance: Neutral, Friendly, Hostile
    /// </summary>
    public enum CreatureAllegiance { Neutral, Friendly, Hostile };

    [Header("AI Attributes")]
    [SerializeField] float speed = 3f;
    [SerializeField] float wanderRadius = 10f;
    [SerializeField] float wanderTimer = 5f;
    [SerializeField] float stopDistance = 4f;
    [SerializeField] int requiredFeedCount = 5;
    float fedCount = 0;

    NavMeshAgent navMesh;
    Vector3 startPosition;
    float timer;
    bool wandering = false;
    bool hasTurned = false;

    Animator anim;
    Rigidbody rb;
    CreatureState state;
    CreatureAllegiance allegiance;

    [Header("Other References")]
    [SerializeField] PlayerController player;
    [SerializeField] WinConditionManager2 winConditionManager;

    [Header("Apperances")]
    [SerializeField] Material friendlyColor;
    [SerializeField] Material neutralColor;
    [SerializeField] Material hostileColor;

    [Header("Sound")]
    [SerializeField] AudioClip eat;
    [SerializeField] AudioClip attack;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        state = CreatureState.Idle;
        navMesh.speed = speed;
        state = CreatureState.Idle;
        allegiance = CreatureAllegiance.Neutral;

        player = FindObjectOfType<PlayerController>();
        winConditionManager = FindObjectOfType<WinConditionManager2>();
    }

    //Update is called once per frame
    void Update()
    {
        Animate();

        timer += Time.deltaTime;
        if (player.IsFoodHeld())
        {
            SeekFood();
        }
        //If time passed since creature last wandered >= wanderTimer and creature is currently idle, creature will wander again.
        else if (!player.IsFoodHeld())
        {
            if (timer >= wanderTimer)
            {
                if (state == CreatureState.Idle || state == CreatureState.Waiting || state == CreatureState.SeekFood)
                {
                    Wander(transform.position, wanderRadius, -1);
                }
            }
        }

        if (allegiance == CreatureAllegiance.Friendly)
        {
            Debug.Log("Creature is now friendly!");
            SeekFood();
            GetComponentInChildren<SkinnedMeshRenderer>().material = null;
            GetComponentInChildren<SkinnedMeshRenderer>().material = friendlyColor;
            winConditionManager.IsMadeFriendly(true);
        }
        else if (allegiance == CreatureAllegiance.Neutral && hasTurned == true)
        {
            Debug.Log("Creature is now neutral!");
            GetComponentInChildren<SkinnedMeshRenderer>().material = null;
            GetComponentInChildren<SkinnedMeshRenderer>().material = neutralColor;
            winConditionManager.IsCured(true);
        }
        else if (allegiance == CreatureAllegiance.Hostile)
        {
            winConditionManager.IsMadeHostile(true);
            Debug.Log("Creature is now hostile!");
            GetComponentInChildren<SkinnedMeshRenderer>().material = null;
            GetComponentInChildren<SkinnedMeshRenderer>().material = hostileColor;
            Attack();
        }

        Debug.Log("Creature state: " + state);
    }

    /// <summary>
    /// Uses RandomNavSphere to determine where this creature can roam.
    /// </summary>
    /// <param name="origin">Starting position.</param>
    /// <param name="dist">Maximum distance to wander.</param>
    /// <param name="navMask">NavMesh mask to read.</param>
    void Wander(Vector3 origin, float dist, int navMask)
    {
        Vector3 newPos = RandomNavSphere(origin, dist, navMask);
        navMesh.SetDestination(newPos);
        Debug.Log(transform.name + " is roaming.");
        timer = 0;
        state = CreatureState.Wander;

        //Calculates the distance between the creature's current and new positions.
        float distance = Vector3.Distance(transform.position, newPos);

        //Calculates the time it will take for the creature to reach its destination.
        float timeTillDestination = distance / speed;

        //Sets the creature's state to Idle once it has reached its destination.
        StartCoroutine(ChangeState(timeTillDestination, CreatureState.Idle));
    }

    /// <summary>
    /// Draws a sphere around this object and returns a point within that sphere.
    /// </summary>
    /// <param name="origin">Starting position.</param>
    /// <param name="dist">Radius of sphere.</param>
    /// <param name="navMask">NavMesh mask to read.</param>
    /// <returns></returns>
    Vector3 RandomNavSphere(Vector3 origin, float dist, int navMask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, navMask);

        Debug.Log("New Position: " + navHit.position);
        return navHit.position;
    }

    /// <summary>
    /// Creature seeks out player and waits for food.
    /// </summary>
    void SeekFood()
    {
        state = CreatureState.SeekFood;
        navMesh.SetDestination(player.transform.position);
        if (Vector3.Distance(transform.position, player.transform.position) <= stopDistance)
        {
            navMesh.SetDestination(transform.position);
            state = CreatureState.Waiting;
            FaceTarget(player.transform);
        }
    }

    /// <summary>
    /// Creature seeks out player and attacks when it gets close.
    /// </summary>
    void Attack()
    {
        state = CreatureState.SeekEnemy;
        navMesh.SetDestination(player.transform.position);
        if (Vector3.Distance(transform.position, player.transform.position) <= stopDistance)
        {
            navMesh.SetDestination(transform.position);
            state = CreatureState.Attacking;
            FaceTarget(player.transform);
        }
    }

    public void Eat(int foodCode)
    {
        if (foodCode == 1) //Normal feed
        {
            audioSource.PlayOneShot(eat);
            winConditionManager.IsFed(true);
            fedCount++;
            Instantiate(Resources.Load("EatParticlesBlue"), transform.position + transform.forward + transform.up, transform.rotation);
            if (fedCount >= requiredFeedCount && allegiance == CreatureAllegiance.Neutral)
            {
                allegiance = CreatureAllegiance.Friendly;
                hasTurned = true;
            }
        }
        else if (foodCode == 2) //Bad feed
        {
            audioSource.PlayOneShot(eat);
            audioSource.PlayOneShot(attack);
            Instantiate(Resources.Load("EatParticlesRed"), transform.position + transform.forward + transform.up, transform.rotation);
            if (allegiance == CreatureAllegiance.Neutral)
            {
                allegiance = CreatureAllegiance.Hostile;
                hasTurned = true;
            }
        }
        else if (foodCode == 3) //Cure
        {
            audioSource.PlayOneShot(eat);
            Instantiate(Resources.Load("EatParticlesGreen"), transform.position + transform.forward + transform.up, transform.rotation);
            if (allegiance == CreatureAllegiance.Hostile)
            {
                allegiance = CreatureAllegiance.Neutral;
                state = CreatureState.Idle;
                hasTurned = true;
            }
        }
    }

    /// <summary>
    /// Rotates this gameObject to look at target gameObject.
    /// </summary>
    /// <param name="target"></param>
    private void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Animate()
    {
        if (state == CreatureState.Idle || state == CreatureState.Waiting)
        {
            anim.Play("Idle");
        }
        else if (state == CreatureState.Wander || state == CreatureState.SeekFood)
        {
            anim.Play("Walk");
        }
        else if (state == CreatureState.SeekEnemy)
        {
            anim.Play("Run");
        }
        else if (state == CreatureState.Attacking)
        {
            anim.Play("Basic Attack");
        }
    }

    public CreatureState GetState()
    {
        return state;
    }
    public CreatureAllegiance GetAllegiance()
    {
        return allegiance;
    }

    IEnumerator ChangeState(float time, CreatureState newState)
    {
        yield return new WaitForSeconds(time);
        state = newState;
    }
}
