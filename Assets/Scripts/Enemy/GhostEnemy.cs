using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostEnemy : Enemy
{
    public Stat Speed;
    public bool canWalk;

    void Awake()
    {
        List<Stat> stats = new List<Stat>();
        Health = new Stat(StatType.Health, 10, 10);
        Damage = new Stat(StatType.Damage, 25, 25);
        Speed = new Stat(StatType.Speed, 1, 1);

        stats.Add(Health);
        stats.Add(Damage);
        stats.Add(Speed);

        EnemyStats = new Stats(stats);
    }

    public override void EnemyMove()
    {
        if (player == null) { return; }

        var enemy = GetComponent<Enemy>();
        if (gameManager.GetGameState() == GameState.Stop)
        {
            canWalk = false;
            return;
        }
        else
        {
            canWalk = true;
        }

        if (playerController.Damage.GetValue() > 0)
        {
            Vector3 playerEnemyVector = transform.position - player.transform.position;
            Vector3 backwardDirection = playerEnemyVector.normalized;

            currentDestination = transform.position + backwardDirection * 10;
            transform.position = Vector3.MoveTowards(transform.position, currentDestination, Time.deltaTime * enemy.EnemyStats.GetStatsByType(StatType.Speed).GetValue());
        }
        else
        {
            if (enemy.isAttacked)
            {
                if (!enemy.isAttackedCooldown)
                {
                    enemy.isAttackedCooldown = true;
                    atackBackwardPosition = transform.position + transform.forward * -attackDistance;
                }
                else if (enemy.isAttackedCooldown && Vector3.Distance(transform.position, atackBackwardPosition) <= 1)
                {
                    enemy.isAttacked = false;
                    enemy.isAttackedCooldown = false;
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
                }

                if (!isTriggered)
                {
                    isTriggered = true;
                    Vector3 randomPosition = Random.insideUnitSphere;
                    currentDestination = new Vector3(randomPosition.x, 0, randomPosition.z)  * 2 +  new Vector3(player.transform.position.x, 0, player.transform.position.y);
                }

                if (currentTargetDistance <= 1)
                {
                    isTriggered = false;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentDestination, Time.deltaTime * enemy.EnemyStats.GetStatsByType(StatType.Speed).GetValue());
                }
            }
        }
    }
}
