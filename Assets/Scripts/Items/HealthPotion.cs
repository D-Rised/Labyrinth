using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item
{
    StatModifier healthUp;

    private void Start()
    {
        healthUp = new StatModifier(10, StatModifierType.Permanent);
    }

    public override void PickUp(PlayerController player)
    {
        player.Health.ModifierAddCurrentValue(healthUp);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider entity)
    {
        if (entity.tag == "Player" && entity.GetComponent<PlayerController>() != null)
        {
            PickUp(entity.GetComponent<PlayerController>());
        }
    }
}
