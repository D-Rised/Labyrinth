using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthPotion : Item
{
    private StatModifier damageUp;

    private void Start()
    {
        damageUp = new StatModifier(100, StatModifierTimeType.Temporal, StatModifierValueType.Both, 5);
    }

    public override void PickUp(PlayerController player)
    {
        if (player != null)
        {
            player.ApplyEffect(damageUp, player.Damage);
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
