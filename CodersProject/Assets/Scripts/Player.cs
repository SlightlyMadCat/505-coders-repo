using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 * Player life cycle logic
 */

[RequireComponent(typeof(UnityStandardAssets.Characters.FirstPerson.FirstPersonController))]

public class Player : Creatures, IRaycastable
{
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI damageText;

    [Space]
    public int killsCounter;

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

        OnHealthChanging += UpdateHealthBar;
    }

    //called for player to update ui health bar on damage recieving
    private void UpdateHealthBar(float _newHealth)
    {
        UpdateSomeUiText(healthText, _newHealth);   //some another text
        gameObject.SetActive(true);
    }

    public void KillerFeatureVoid()
    {
        //transform tor selected
        transform.rotation = transform.rotation;
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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) SendRaycast(); //detect each LMB click
    }

    public void SendRaycast()
    {
        Transform _target = ShootingLogic.CalculateRaycast();
        if (_target != null && _target.GetComponent<Enemy>() != null)
        {
            SendDamage(_target.GetComponent<Enemy>(), damage);
        }
    }

    public override void KillCreature()
    {
        Debug.LogError("GAME OVER!");
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
