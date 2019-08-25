using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] float decisionDelay = 3f;
    [SerializeField] Transform objectToChase;
    [SerializeField] Transform[] destinations;
    int currentDes = 0;
    public GameObject Enemy1;
    public int EnemyHealth = 10;

    enum EnemyStates
    {
        Patrolling,
        Chasing
    }

    [SerializeField] EnemyStates currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("SetDestination", 0.5f, decisionDelay);
        if (currentState == EnemyStates.Patrolling)
            agent.SetDestination(destinations[currentDes].position);
    }

    void Update()
    {
        Movement();

        if (EnemyHealth <= 0)
        {
            Destroy(Enemy1);
        }
    }

    void SetDestination()
    {
        if (currentState == EnemyStates.Chasing)
            agent.SetDestination(objectToChase.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Hit player");
        }
    }

    public void DeductHealth(int DamageAmount)
    {
        if (Enemy1)
        {
            EnemyHealth -= DamageAmount;
        }
    }

    void Movement()
    {
        if (Vector3.Distance(transform.position, objectToChase.position) > 10f)
        {
            currentState = EnemyStates.Patrolling;
        }
        else
        {
            currentState = EnemyStates.Chasing;
        }
        if (currentState == EnemyStates.Patrolling)
        {
            if (Vector3.Distance(transform.position, destinations[currentDes].position) <= 0.6f)
            {
                currentDes++;
                if (currentDes == destinations.Length)
                {
                    currentDes = 0;
                }
            }
            agent.SetDestination(destinations[currentDes].position);
        }
    }
}