using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * AI realisation of Enemy logic for Capsule prefab
 */

public class CapsuleEnemy : Enemy, ICollidable, IClickable
{
    public override void InitData(Player _player, Transform _place)
    {
        speed = 1;
        health = 50;
        damage = 40;
        damageDelay = 5f;
        targetPlayer = _player;
        usedSpawnPlace = _place;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == targetPlayer.transform)
        {
            base.StartDamagingCycle(targetPlayer, damage);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.transform == targetPlayer.transform)
        {
            base.TryToStopCor();
        }
    }

    public void OnMouseDown()
    {
        targetPlayer.SendDamage(this);
    }
}
