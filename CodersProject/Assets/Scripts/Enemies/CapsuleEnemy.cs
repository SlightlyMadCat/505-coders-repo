using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * AI realisation of Enemy logic for Capsule prefab
 */

public class CapsuleEnemy : CollidableEnemy, ICollidable
{
    public override void KillCreature()
    {
        Die();
    }
}
