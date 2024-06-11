using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SheepWalk : MonoBehaviour
{
    public float m_Range = 25.0f;
    public GameObject chaser; // The chaser object
    public float dangerDistance = 10.0f; // The distance at which the agent starts running away
    public BoxCollider safeArea; // The safe area
     Animator animator;
    NavMeshAgent m_Agent;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }
    //make so when sheep is in safe area it will be destroyed
   bool isWaiting = false;

void Update()
{
    if (m_Agent.pathPending || !m_Agent.isOnNavMesh || isWaiting)
        return;

    float distanceToChaser = Vector3.Distance(transform.position, chaser.transform.position);

    if (distanceToChaser < dangerDistance)
    {
        // Run in the opposite direction of the chaser
        Vector3 directionToChaser = (chaser.transform.position - transform.position).normalized;
        m_Agent.destination = transform.position - directionToChaser * m_Range;
        animator.SetBool("IsWalking", true); // Start the walking animation

    }
    else if (!m_Agent.pathPending && m_Agent.remainingDistance <= 0.1f)
    {
        // Start the wait coroutine before setting a new destination
        StartCoroutine(WaitBeforeMoving());
    }
    else if (safeArea.bounds.Contains(transform.position))
    {
        Destroy(gameObject);
        ScoreManager.score += 10;
    }
}

IEnumerator WaitBeforeMoving()
{
    isWaiting = true;
    animator.SetBool("IsWalking", false); // Stop the walking animation
    yield return new WaitForSeconds(1.8f); // Wait 
    
    // Set a new destination after waiting
    m_Agent.destination = transform.position + m_Range * Random.insideUnitSphere;
    animator.SetBool("IsWalking", true); // Start the walking animation
    isWaiting = false;
}
}
