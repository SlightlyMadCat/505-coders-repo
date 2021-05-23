using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creatures : MonoBehaviour
{
    public float health;
    public float speed;
    public float damage;

    public void GetDamage(float _damage)
    {
        health -= _damage;
        if (health <= 0) KillCreature();
    }

    public void SendDamage(Creatures _target, float _damage)
    {
        print("send damage to "+_target.name);
        _target.GetDamage(_damage);
    }

    public abstract void KillCreature();
}
