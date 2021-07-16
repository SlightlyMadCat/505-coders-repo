using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creatures : MonoBehaviour
{
    public float health;
    public float speed;
    public float damage;

    public delegate void HealthChanging(float _newHealth);
    public HealthChanging OnHealthChanging;

    public void GetDamage(float _damage)
    {
        health -= _damage;
        if (health <= 0) KillCreature();
        OnHealthChanging?.Invoke(health);
    }

    public void SendDamage(Creatures _target, float _damage)
    {
        _target.GetDamage(_damage);
    }

    public abstract void KillCreature();
}
