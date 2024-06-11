using UnityEngine;
using UnityEngine.AI;

namespace Unity.AI.Navigation.Samples
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ClickToMove : MonoBehaviour
    {
        NavMeshAgent m_Agent;
        RaycastHit m_HitInfo = new RaycastHit();
        public float scareRadius = 10f;
        private AudioSource audioSource;
        public AudioClip scareSound;
        private float barkCooldown = 7f;
        private float nextBarkTime = 0f;
        private Animator animator; // Add this line

        void Start()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>(); // Add this line
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextBarkTime)
            {
                ScareWolves();
                nextBarkTime = Time.time + barkCooldown;
                animator.SetBool("isMoving", false); // Add this line
            }
            else if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
                {
                    m_Agent.destination = m_HitInfo.point;
                    animator.SetBool("isMoving", true); // Add this line
                }
            }

            // Add these lines
            if (!m_Agent.pathPending && m_Agent.remainingDistance <= m_Agent.stoppingDistance)
            {
                animator.SetBool("isMoving", false);
            }
        }

        void ScareWolves()
        {
            audioSource.clip = scareSound;
            audioSource.time = 1.8f;
            audioSource.Play();

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, scareRadius);
            foreach (var hitCollider in hitColliders)
            {
                var wolf = hitCollider.GetComponent<EnemyScript>();
                if (wolf != null)
                {
                    wolf.isScared = true;
                    wolf.scareTimer = 0f;
                }
            }
        }
    }
}