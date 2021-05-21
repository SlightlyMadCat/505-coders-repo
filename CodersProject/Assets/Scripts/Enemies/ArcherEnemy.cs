using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * AI realisation of Enemy logic for Archer prefab
 */

public class ArcherEnemy : Enemy, IShootable, IClickable
{
    public override void InitData(Player _player, Transform _place)
    {
        speed = 1;
        health = 50;
        damage = 70;
        damageDelay = 7f;
        targetPlayer = _player;
        usedSpawnPlace = _place;
    }

    public void OnMouseDown()
    {
        targetPlayer.SendDamage(this);
    }

    public void SendRaycast()
    {
        //TODO
    }
}
