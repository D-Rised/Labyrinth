using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item
{
    StatModifier healthUp;

    private void Start()
    {
        healthUp = new StatModifier(10, StatModifierTimeType.Permanent, StatModifierValueType.Current);
    }

    public override void PickUp(PlayerController player)
    {
        if (player != null)
        {
            player.ApplyEffect(healthUp, player.Health);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider entity)
    {
        if (entity.tag == "Player" && entity.GetComponent<PlayerController>() != null)
        {
            PickUp(entity.GetComponent<PlayerController>());
        }
    }
}
