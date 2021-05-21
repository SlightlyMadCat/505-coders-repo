using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Item provides temp damage boost
 */

public class DamageBoostItem : Item, ICollidable
{
    public float damageBoostScale = 1f;
    public float boostTime = 5f;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            Player.Instance.StartDamageBoost(damageBoostScale, boostTime);
            base.DestroyItem();
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //nothing
    }
}
