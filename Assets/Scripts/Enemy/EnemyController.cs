using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void ApplyDamage(int damage)
    {
        var enemy = GetComponent<Enemy>();
        enemy.Health.Value -= damage;
        if (enemy.Health.GetValue() <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider entity)
    {
        if (entity.tag == "Player" && entity.GetComponent<PlayerController>() != null)
        {
            PlayerController player = entity.GetComponent<PlayerController>();
            var enemy = GetComponent<Enemy>();
            if (!enemy.isAttacked && player.Damage.GetValue() <= 0)
            {
                enemy.isAttacked = true;
                player.ApplyDamage(enemy.Damage.Value);
            }
            else if (player.Damage.GetValue() > 0)
            {
                ApplyDamage(player.Damage.GetValue());
            }
        }
    }
}
