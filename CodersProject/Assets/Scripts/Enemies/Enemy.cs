using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * Abstract class with main Enemy props and abilities
 */

public abstract class Enemy : Creatures
{
    public Player targetPlayer { get; set; }
    public float damageDelay;
    public Transform usedSpawnPlace { get; set; }  //remember spawn place that should be re-used after ai death

    public void InitData(Player _player, Transform _place)  //called on init
    {
        targetPlayer = _player;
        usedSpawnPlace = _place;
    }    

    private NavMeshAgent navMeshAgent;
    private Coroutine damagingCor { get; set; }

    //called by ai in update cycle
    protected void MoveEnemy(Transform _target, float _speed)  
    {
        navMeshAgent.destination = _target.position;
        navMeshAgent.speed = _speed;
    }

    protected void Die()
    {
        Player.Instance.UpdateKillsCount();
        EnemySpawner.Instance.RestoreSpawnPlace(usedSpawnPlace);
        EnemySpawner.Instance.SpawnNewEnemy();
        ItemsSpawner.Instance.SpawnNewItem(transform.position + Vector3.up * 3f);

        Destroy(gameObject);
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

public interface IRaycastable
{
    public void SendRaycast();
}

/**
 *  Static physics class 
 */

public static class ShootingLogic
{
    //Raycasting logic
    public static Transform CalculateRaycast()
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
        {
            return _hit.transform;
        }
        else
            return null;
    }

    //Linecasting logic
    public static Transform CalculateLineCast(Transform _startPoint, Transform _endPoint)
    {
        if (Physics.Linecast(_startPoint.position, _endPoint.position, out RaycastHit _info))
        {
            return _info.transform;
        }
        else
            return null;
    }
}