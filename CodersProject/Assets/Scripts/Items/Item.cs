using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Main parent class for all dropped items
 * By default all items are collidable
 */

public abstract class Item : MonoBehaviour
{
    protected void DestroyItem()
    {
        Destroy(gameObject);
    } 
}
