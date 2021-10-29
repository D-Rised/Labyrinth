using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    private GameManager gameManager;
    private NavMeshAgent enemyNavMeshAgent;
    private GameObject player;
    
    public float walkRadius;
    public float triggerPlayerDistance;
    public float enemySpeed;
    private float currentPlayerDistance;
    private float currentTargetDistance;
    private Vector3 currentDestination;
    private bool isTriggered;

    void Start()
    {
        gameManager = GameManager.instance;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        enemyNavMeshAgent.speed = enemySpeed;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        if (gameManager.GetGameState() == GameState.Stop)
        {
            enemyNavMeshAgent.isStopped = true;
            return;
        }
        else
        {
            enemyNavMeshAgent.isStopped = false;
        }

        if (player != null)
        {
            currentPlayerDistance = Vector3.Distance(transform.position, player.transform.position);
            currentTargetDistance = FlatDistanceTo(transform.position, currentDestination);

            if (triggerPlayerDistance > currentPlayerDistance)
            {
                isTriggered = true;
                currentDestination = player.transform.position;
                SetNewDestination(currentDestination);
            }

            if (!isTriggered)
            {
                isTriggered = true;
                currentDestination = Random.insideUnitSphere * walkRadius + transform.position;
                SetNewDestination(currentDestination);
            }
            
            if (currentTargetDistance <= 2)
            {
                isTriggered = false;
            }
        }
    }

    private void SetNewDestination(Vector3 destination)
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(destination, out hit, walkRadius, 3);
        enemyNavMeshAgent.SetDestination(hit.position);
        currentDestination = hit.position;
    }

    public Vector2 vectorXZ(Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }
    public float FlatDistanceTo(Vector3 vectorFrom, Vector3 vectorTo)
    {
        Vector2 a = vectorXZ(vectorFrom);
        Vector2 b = vectorXZ(vectorTo);
        return Vector2.Distance(a, b);
    }
}
