using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 * Player life cycle logic
 */

[RequireComponent(typeof(UnityStandardAssets.Characters.FirstPerson.FirstPersonController))]

public class Player : MonoBehaviour
{
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI damageText;

    [Space]
    public float health;
    public int killsCounter;
    public float damage = 50;

    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPerson;

    #region
    public static Player Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private void Start()
    {
        firstPerson = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();

        //set start text
        UpdateSomeUiText(healthText, health);
        UpdateSomeUiText(killsText, killsCounter);
        UpdateSomeUiText(damageText, damage);
    }

    //get damage from enemy
    public void GetDamage(float _dmg)
    {
        health -= _dmg;
        UpdateSomeUiText(healthText, health);
    }

    //send damage to enemy
    public void SendDamage(Enemy _enemy)
    {
        _enemy.GetDamage(damage);
    }

    //called when some enemy dies
    public void UpdateKillsCount()
    {
        killsCounter++;
        UpdateSomeUiText(killsText, killsCounter);
    }

    //ui updater
    private void UpdateSomeUiText(TextMeshProUGUI _textField, float _val)
    {
        _textField.text = _val.ToString();
    }

    #region DamageBoost
    //called when player takes item
    public void StartDamageBoost(float _boostScaler, float _time)
    {
        StartCoroutine(DamageBoostingCor(_boostScaler, _time));
    }

    IEnumerator DamageBoostingCor(float _boostScaler, float _time)
    {
        damage *= _boostScaler;
        UpdateSomeUiText(damageText, damage);
        yield return new WaitForSeconds(_time);
        damage /= _boostScaler;
        UpdateSomeUiText(damageText, damage);
    }
    #endregion

    #region SpeedBoost
    private Coroutine boostCor;

    public void StartSpeedBoost(float _boostFactor, float _time)
    {
        if (boostCor != null) StopCoroutine(boostCor);
        boostCor = StartCoroutine(SpeedBoost(_boostFactor, _time));
    }

    IEnumerator SpeedBoost(float _boostFactor, float _time)
    {
        firstPerson.UpdateSpeedFactor(_boostFactor);
        yield return new WaitForSeconds(_time);
        firstPerson.UpdateSpeedFactor(1);
    }
    #endregion
}
