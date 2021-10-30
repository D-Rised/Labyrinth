using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    private GameManager gameManager;
    private NavMeshAgent enemyNavMeshAgent;
    private GameObject player;
    private PlayerController playerController;
    
    public float walkRadius;
    public float triggerPlayerDistance;
    public float attackDistance;
    private float currentPlayerDistance;
    private float currentTargetDistance;
    private Vector3 currentDestination;
    private Vector3 atackBackwardPosition;
    private bool isTriggered;

    void Start()
    {
        gameManager = GameManager.instance;

        var enemy = GetComponent<Enemy>();

        enemyNavMeshAgent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        enemyNavMeshAgent.speed = 5;
        Stat speed = enemy.EnemyStats.GetStats().ToList().Find(x => x.GetStatType() == StatType.Speed);
        if (speed != null)
        {
            enemyNavMeshAgent.speed = speed.GetValue();
        }
    }
}
