using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * AI realisation of Enemy logic for Cube prefab
 */

public class CubeEnemy : Enemy, ICollidable, IClickable
{
    public override void InitData(Player _player, Transform _place)
    {
        speed = 2;
        health = 100;
        damage = 100;
        damageDelay = 3;
        targetPlayer = _player;
        usedSpawnPlace = _place;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform == targetPlayer.transform)
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
