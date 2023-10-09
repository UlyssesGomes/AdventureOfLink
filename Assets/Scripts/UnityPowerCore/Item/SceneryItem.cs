using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneryItem<T> : MonoBehaviour
{
    [SerializeField]
    protected T itemAsset;

    /*
     * Check if this item is in collision with the player. If true, call 
     * createItem() method.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            createItem(collision);
        }
    }

    /*
     * Try to add this item to storedItem in PlayerInventory. If all items
     * are added, then remove them from the scenery, otherwise, only decrease 
     * amount.
     */
    protected virtual void createItem(Collider2D collision)
    {
        PlayerInventory inventory = collision.transform.GetComponent("PlayerInventory") as PlayerInventory;
        GameItem gameItem = itemAsset as GameItem;
        if (inventory.getStoreItems()[0] != null)
        {
            Debug.Log("Before: " + inventory.getStoreItems()[0].amount);
        }
        int startAmount = gameItem.amount = 2;
        if (inventory.getStoreItems()[0] != null)
        {
            Debug.Log("After: " + inventory.getStoreItems()[0].amount);
        }
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
