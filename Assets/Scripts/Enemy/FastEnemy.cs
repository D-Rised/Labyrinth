using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FastEnemy : Enemy
{   
    public Stat Speed;

    void Awake()
    {
        List<Stat> stats = new List<Stat>();
        Health = new Stat(StatType.Health, 5, 5);
        Damage = new Stat(StatType.Damage, 2, 2);
        Speed = new Stat(StatType.Speed, 15, 15);

        stats.Add(Health);
        stats.Add(Damage);
        stats.Add(Speed);

        EnemyStats = new Stats(stats);
    }

    public override void EnemyMove()
    {
        if (player == null) { return; }

        enemyNavMeshAgent.speed = Speed.GetValue();

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
}
