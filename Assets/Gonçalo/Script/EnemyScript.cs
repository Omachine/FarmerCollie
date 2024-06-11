using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    private Animator animator;

   public GameObject initialPositionObject; // The initial position object
    public float searchRadius = 10f; // The radius in which to search for sheep
    
    public float destroyRadius = 3f; // The radius within which to destroy the sheep
    public float randomRange = 20f; // The range for random movement
    [HideInInspector]
    public string sheepTag = "sheep"; // The tag of the sheep
    [HideInInspector]
    public float waitTimeAfterDestroy = 3f; // Time to wait after destroying a sheep
    public float scareTime = 5f; // Time to be scared for

    [HideInInspector]
    public NavMeshAgent agent;
    public GameObject player; // Reference to the player's GameObject
    private Vector3 randomDestination;
    private bool hasReachedInitialPosition = false;
    private bool isWaiting = false; // To check if the enemy is in waiting state
    public bool isScared = false; // To check if the enemy is scared
    [HideInInspector]
    public float scareTimer = 0f; // Timer for how long the enemy has been scared
    private float runDistance = 15.0f; // Distance the enemy should run away
    public float patrolSpeed = 4f; // The speed when patrolling
    public float chaseSpeed = 4.5f; // The speed when chasing sheep
    public float scaredSpeed = 6f; // The speed when scared

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(initialPositionObject.transform.position);
        animator = GetComponent<Animator>();

    }

  void Update()
{
    if (isWaiting) return; // If waiting, do nothing

    if (isScared)
    {
        agent.speed = scaredSpeed; // Set speed to scaredSpeed when scared
        animator.SetBool("IsScared", true); // Set the isScared parameter in the Animator
        RunAwayFromPlayer();
    }
    else
    {
        animator.SetBool("IsScared", false); // Set the isScared parameter in the Animator

        if (!hasReachedInitialPosition)
        {
            agent.speed = patrolSpeed; // Set speed to patrolSpeed when patrolling
            MoveToInitialPosition();
        }
         else
        {
            Collider closestSheep = FindClosestSheep();

            if (closestSheep != null)
            {
                agent.speed = chaseSpeed; // Set speed to chaseSpeed when chasing sheep
                animator.SetBool("IsChasing", true); // Set the isChasing parameter in the Animator
                ChaseSheep(closestSheep);
            }
            else
            {
                agent.speed = patrolSpeed; // Set speed to patrolSpeed when moving randomly
                animator.SetBool("IsChasing", false); // Set the isChasing parameter in the Animator
                MoveRandomly();
            }
        }
    }
}

void RunAwayFromPlayer()
{
    // Calculate the direction away from the player
    Vector3 runDirection = transform.position - player.transform.position;

    // Normalize the run direction
    runDirection.Normalize();

    // Calculate the run destination
    Vector3 destination = transform.position + runDirection * runDistance;

    // Clear the agent's path
    agent.ResetPath();

    // Set the agent's destination to the run destination
    agent.SetDestination(destination);

    scareTimer += Time.deltaTime;
    if (scareTimer >= scareTime)
    {
        isScared = false;
        scareTimer = 0f;
    }
}

void MoveToInitialPosition()
{
    if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
    {
        hasReachedInitialPosition = true;
    }
}

Collider FindClosestSheep()
{
    Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
    float minDistance = float.MaxValue;
    Collider closestSheep = null;

    foreach (var collider in colliders)
    {
        if (collider.CompareTag(sheepTag))
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestSheep = collider;
            }
        }
    }

    return closestSheep;
}

void ChaseSheep(Collider sheep)
{
    agent.SetDestination(sheep.transform.position);

    // Check if the enemy is within the destroy radius of the closest sheep
    if (Vector3.Distance(transform.position, sheep.transform.position) <= destroyRadius)
    {
        DestroySheep(sheep.gameObject);
    }
}

void MoveRandomly()
{
    if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
    {
        Vector2 randomDirection = Random.insideUnitCircle * randomRange;
        randomDestination = transform.position + new Vector3(randomDirection.x, 0, randomDirection.y);
        agent.SetDestination(randomDestination);
    }
}
    void DestroySheep(GameObject sheep)
    {
        ScoreManager.score -= 10; // Assuming ScoreManager has a static score variable
        Destroy(sheep);
        animator.SetBool("IsEating", true); // Start the eating animation

        StartCoroutine(WaitAfterDestroy());
    }

    IEnumerator WaitAfterDestroy()
    {
        isWaiting = true;
        agent.isStopped = true; // Stop the NavMeshAgent from moving

        yield return new WaitForSeconds(waitTimeAfterDestroy); // Wait for the specified time
        animator.SetBool("IsEating", false); // Start the eating animation

        agent.isStopped = false; // Resume the NavMeshAgent
        isWaiting = false;
    }
}
