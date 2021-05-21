using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * Abstract class with main Enemy props and abilities
 */

public abstract class Enemy : MonoBehaviour
{
    public Player targetPlayer { get; set; }
    public float speed { get; set; }
    public float health { get; set; }
    public float damage { get; set; }
    public float damageDelay { get; set; }
    public Transform usedSpawnPlace { get; set; }  //remember spawn place that should be re-used after ai death

    public abstract void InitData(Player _player, Transform _place);    //called on init

    private NavMeshAgent navMeshAgent;
    private Coroutine damagingCor { get; set; }

    //called by ai in update cycle
    protected void MoveEnemy(Transform _target, float _speed)  
    {
        navMeshAgent.destination = _target.position;
        navMeshAgent.speed = _speed;
    }

    //sends dmg from enemy to player
    protected void SendDamage(Player _player, float _damage)
    {
        _player.GetDamage(_damage);
    }

    //void stops shooting cor for ai
    protected void TryToStopCor()
    {
        if (damagingCor != null) StopCoroutine(damagingCor);
    }

    protected void StartDamagingCycle(Player _player, float _damage)
    {
        TryToStopCor();
        damagingCor = StartCoroutine(DamagingCor(_player, _damage));
    }

    protected IEnumerator DamagingCor(Player _player, float _damage)
    {      
        SendDamage(_player, _damage);
        yield return new WaitForSeconds(damageDelay);
        damagingCor = StartCoroutine(DamagingCor(_player, _damage));
    }

    //called to destroy ai
    private void Die()
    {
        Player.Instance.UpdateKillsCount();
        EnemySpawner.Instance.RestoreSpawnPlace(usedSpawnPlace);
        EnemySpawner.Instance.SpawnNewEnemy();
        ItemsSpawner.Instance.SpawnNewItem(transform.position + Vector3.up*3f);

        Destroy(gameObject);
    }

    //called when player clicks over ai
    public void GetDamage(float _dmg)
    {
        health -= _dmg;
        if (health <= 0)
            Die();
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();    //assign navmesh
    }

    private void FixedUpdate()
    {
        if (!targetPlayer)
            return;

        MoveEnemy(targetPlayer.transform, speed);
    }
}

/**
 * Couple of void for enemies that can take damage by collision
 */

public interface ICollidable
{
    public void OnCollisionEnter(Collision collision);
    public void OnCollisionExit(Collision collision);
}

/**
 * Voids for enemies that can shoot 
 */

public interface IShootable
{
    public void SendRaycast();
}

public interface IClickable
{
    public void OnMouseDown();
}