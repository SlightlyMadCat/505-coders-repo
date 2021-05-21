using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostItem : Item, ICollidable
{
    public float boostFactor;
    public float boostTime;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            Player.Instance.StartSpeedBoost(boostFactor, boostTime);
            base.DestroyItem();
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //nothing
    }
}
