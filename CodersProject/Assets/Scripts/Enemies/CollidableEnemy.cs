using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollidableEnemy : Enemy, ICollidable
{
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
}
