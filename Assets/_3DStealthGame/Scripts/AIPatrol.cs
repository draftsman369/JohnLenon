using UnityEngine;
using UnityEngine.AI;

public class AIPatrol : MonoBehaviour
{

    public float wanderRadius = 3f;
    public float wanderTimer = 5f;  
    public float timer;
    private NavMeshAgent agent;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        timer = wanderTimer;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPosition = RandomNavPosition(transform.position, wanderRadius, NavMesh.AllAreas);
            agent.SetDestination(newPosition);
            timer = 0;

        }
    }

    private Vector3 RandomNavPosition(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection += origin;

        if(NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, dist, layerMask))
        {
            return navHit.position;
        }
        else
        {
            return origin;
        }
    }
}
