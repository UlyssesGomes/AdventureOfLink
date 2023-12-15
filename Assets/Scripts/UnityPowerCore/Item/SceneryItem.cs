using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Diagnostics;

public abstract class SceneryItem<T> : MonoBehaviour where T : GameItem
{
    [SerializeField]
    protected T itemAsset;              // item that this class intance will generate after collistion
        
    protected int createAmount = 1;     // amount of the itemAsset

    /// <summary>
    /// Check if this item is in collision with the player. If true, call
    /// createItem() method.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            createItem(collision);
        }
        else
        {
            UnityEngine.Debug.Log("-Collision: " + collision.gameObject.name);
        }
    }

    /// <summary>
    /// Try to add this item to storedItem in PlayerInventory. If all items
    /// are added, then remove them from the scenery, otherwise, only decrease
    /// amount.
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void createItem(Collider2D collision)
    {
        PlayerInventory inventory = collision.transform.GetComponent("PlayerInventory") as PlayerInventory;
        
        GameItem gameItem = Instantiate(itemAsset);
        int startAmount = gameItem.amount = createAmount;
        int addedAmount = inventory.addStoreItem(gameItem);

        if(startAmount - addedAmount == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            gameItem.amount = startAmount - addedAmount;
        }
    }
}
