using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Main items spawning logic
 */

public class ItemsSpawner : MonoBehaviour
{
    #region 
    public static ItemsSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Item> items = new List<Item>();

    //called when some enemy dies
    public void SpawnNewItem(Vector3 _pos)
    {
        int _randomId = Random.Range(0, items.Count);
        Transform _newItem = Instantiate(items[_randomId].gameObject).transform;
        _newItem.position = _pos;
        _newItem.SetParent(transform);
    }
}
