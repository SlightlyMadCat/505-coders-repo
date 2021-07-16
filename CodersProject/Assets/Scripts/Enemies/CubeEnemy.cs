using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * AI realisation of Enemy logic for Cube prefab
 */

public class CubeEnemy : CollidableEnemy, ICollidable
{
    public override void KillCreature()
    {
        Die();
    }
}
