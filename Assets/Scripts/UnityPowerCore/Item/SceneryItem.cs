using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneryItem<T> : MonoBehaviour
{
    [SerializeField]
    protected GameObject itemPrefab;

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
        GameObject obj = Instantiate(itemPrefab);
        T genericItem = obj.GetComponent<T>();
        PlayerInventory inventory = collision.GetComponent<Player>().GetComponent("PlayerInventory") as PlayerInventory;
        GameItem gameItem = genericItem as GameItem;
        int startAmount = gameItem.amount;
        int addedAmount = inventory.addStoreItem(gameItem);

        if(startAmount - addedAmount == 0)
        {
            Destroy(obj);
            Destroy(gameObject);
        }
        else
        {
            gameItem.amount = startAmount - addedAmount;
        }
    }
}
