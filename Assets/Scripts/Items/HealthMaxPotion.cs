using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMaxPotion : Item
{
    private StatModifier healthMaxUp;

    private void Start()
    {
        healthMaxUp = new StatModifier(50, StatModifierTimeType.Permanent, StatModifierValueType.Max);
    }

    public override void PickUp(PlayerController player)
    {
        if (player != null)
        {
            player.ApplyEffect(healthMaxUp, player.Health);
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
