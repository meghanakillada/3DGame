using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum State { Patrol, Chase } // initial states

    [Header("State")]
    public State state = State.Patrol;

    [Header("Movement")]
    public float patrolSpeed = 2.5f;
    public float chaseSpeed = 4.5f;

    [Header("Detection")]
    public float detectRange = 12f;   // how close the player must be to start chasing
    public float catchDistance = 2f;  // how close to "catch" the player

    [Header("Patrol")]
    public Transform[] waypoints;
    public float waypointStopDistance = 0.5f;
    public float waitAtWaypoint = 1.5f;

    NavMeshAgent agent;
    Transform player;
    int currentWaypointIndex;
    float waitTimer;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        FirstPersonController pc = FindObjectOfType<FirstPersonController>();
        player = pc.transform;
        agent.speed = patrolSpeed;
    }

    void Start()
    {
        currentWaypointIndex = 0;
        if (agent.isOnNavMesh) agent.destination = waypoints[currentWaypointIndex].position;
    }

    void Update()
    {
        if (player == null || !agent.isOnNavMesh) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (state)
        {
            case State.Patrol:
                // switch to chase if player is close enough
                if (distanceToPlayer <= detectRange)
                {
                    state = State.Chase;
                    agent.speed = chaseSpeed;
                    Debug.Log("Enemy: Started chasing player");
                }
                else
                {
                    UpdatePatrol();
                }
                break;

            case State.Chase:
                UpdateChase(distanceToPlayer);
                break;
        }
    }

    void UpdatePatrol()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance <= waypointStopDistance)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitAtWaypoint)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                agent.destination = waypoints[currentWaypointIndex].position;
                waitTimer = 0f;
            }
        }
    }

    void UpdateChase(float distanceToPlayer)
    {
        // constantly chase the player's position
        agent.destination = player.position;

        // close enough to catch?
        if (distanceToPlayer <= catchDistance)
        {
            Debug.Log($"Enemy caught player at distance {distanceToPlayer}");

            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerCaught();
            }
            else
            {
                Debug.LogError("EnemyAI: GameManager.Instance is NULL");
            }
        }
    }
}