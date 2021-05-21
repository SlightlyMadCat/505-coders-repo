using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Main logic for ai spawn
 */

public class EnemySpawner : MonoBehaviour
{
    public Player player;
    [Header("Start ai count")]
    public float startAiCount = 3;

    [Space]
    public List<Enemy> enemiesToSpawn = new List<Enemy>();

    public List<Transform> spawnPlaces = new List<Transform>();
    private List<Transform> usedSpawnPlaces = new List<Transform>();

    #region Singleton
    public static EnemySpawner Instance; 

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private void Start()
    {
        StartSpawn();
    }

    private void StartSpawn()
    {
        for (int _i = 0; _i < startAiCount; _i++)
        {
            SpawnNewEnemy();
        }
    }

    public void SpawnNewEnemy()
    {
        if (spawnPlaces.Count == 0) //prevent spawn of new ai if there is no places!
            return;

        Transform _ai = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)].gameObject).transform;

        //remember new spawn place as used
        Transform _spawnPlace = spawnPlaces[Random.Range(0, spawnPlaces.Count)];
        spawnPlaces.Remove(_spawnPlace);
        usedSpawnPlaces.Add(_spawnPlace);

        _ai.position = _spawnPlace.position;
        _ai.SetParent(transform);

        Enemy _enemy = _ai.GetComponent<Enemy>();
        _enemy.InitData(player, _spawnPlace);
    }

    //called when some is is dying and place become free
    public void RestoreSpawnPlace(Transform _place)
    {
        spawnPlaces.Add(_place);
        usedSpawnPlaces.Remove(_place);
    }
}
