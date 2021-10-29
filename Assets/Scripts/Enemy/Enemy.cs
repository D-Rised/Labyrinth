using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public string Name;

    public Stats EnemyStats;
    public Stat Health;
    public Stat Damage;

    private Enemy enemy;
    public bool isAttacked;
    public bool isAttackedCooldown;

    public GameManager gameManager;
    public NavMeshAgent enemyNavMeshAgent;
    public GameObject player;
    public PlayerController playerController;

    public GameObject test;
    public float speed;
    public float walkRadius;
    public float triggerPlayerDistance;
    public float attackDistance;
    public float currentPlayerDistance;
    public float currentTargetDistance;
    public Vector3 currentDestination;
    public Vector3 atackBackwardPosition;
    public bool isTriggered;

    void Start()
    {
        gameManager = GameManager.instance;
        var enemy = GetComponent<Enemy>();
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        speed = 5;
        walkRadius = 5;
        triggerPlayerDistance = 5;
        attackDistance = 2;
    }

    void Update()
    {
        EnemyMove();
    }

    public virtual void EnemyMove()
    {
        if (player == null) { return; }
        enemyNavMeshAgent.speed = speed;
        
        var enemy = GetComponent<Enemy>();
        if (gameManager.GetGameState() == GameState.Stop)
        {
            enemyNavMeshAgent.isStopped = true; 
            return;
        }
        else
        {
            enemyNavMeshAgent.isStopped = false;
        }

        if (playerController.Damage.GetValue() > 0)
        {
            Vector3 playerEnemyVector = transform.position - player.transform.position;
            Vector3 backwardDirection = playerEnemyVector.normalized;

            currentDestination = transform.position + backwardDirection * 10;
            SetNewDestination(currentDestination);
        }
        else
        {
            if (enemy.isAttacked)
            {
                NavMeshHit edge;
                NavMesh.FindClosestEdge(transform.position, out edge, NavMesh.AllAreas);

                enemyNavMeshAgent.isStopped = true;
                if (!enemy.isAttackedCooldown)
                {
                    enemy.isAttackedCooldown = true;
                    atackBackwardPosition = transform.position + transform.forward * -attackDistance;
                }
                else if (enemy.isAttackedCooldown && (Vector3.Distance(transform.position, edge.position) <= 0f || Vector3.Distance(transform.position, atackBackwardPosition) <= 1))
                {
                    enemy.isAttacked = false;
                    enemy.isAttackedCooldown = false;
                    enemyNavMeshAgent.isStopped = false;
                }
                else
                {
                    transform.position += transform.forward * Time.deltaTime * -5;
                }
            }
            else
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
    }

    public void SetNewDestination(Vector3 destination)
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
