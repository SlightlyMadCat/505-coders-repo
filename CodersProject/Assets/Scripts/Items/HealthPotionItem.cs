using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionItem : Item, ICollidable
{
    public float boostValue;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            Player.Instance.GetDamage(-boostValue);
            base.DestroyItem();
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //nothing
    }
}
