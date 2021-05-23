using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * AI realisation of Enemy logic for Archer prefab
 */

public class ArcherEnemy : Enemy, IRaycastable
{
    void Update()
    {
        //from here try to send raycasts
        SendRaycast();
    }

    public void SendRaycast()
    {
        if (isReloading)
            return;

        Transform _target = ShootingLogic.CalculateLineCast(transform, Player.Instance.transform);
        if (_target != null && _target.GetComponent<Player>() != null)
        {
            SendDamage(_target.GetComponent<Player>(), damage);
            StartCoroutine(ShootDelay());
        }
    }

    public bool isReloading = false;

    IEnumerator ShootDelay()
    {
        isReloading = true;
        yield return new WaitForSeconds(damageDelay);
        isReloading = false;
    }

    public override void KillCreature()
    {
        Die();
    }
}
