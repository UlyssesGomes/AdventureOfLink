using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Water : MonoBehaviour
{
    [SerializeField]
    private bool detectingPlayer;           // if true, the player is in collision with a lake or reservoir border 

    private Player player;                  // player instance
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            PlayerInventory inventory = player.GetComponent<PlayerInventory>() as PlayerInventory;
            GameItem item = inventory.getCurrentSwitableItem();
            if (!(item is null) && item.type == ItemTypeEnum.WATERING)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    WateringCanItem wateringCan = item as WateringCanItem;
                    inventory.notifyStoredItemsObservers(inventory.switableItemIndex);
                    wateringCan.waterCapacity++;
                }
            }
        }
    }

    /// <summary>
    /// When in collision with a player, get its intance to do water refilling.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = true;
            player = collision.GetComponent<Player>() as Player;
        }
    }

    /// <summary>
    /// When its no more in collision, stop refilling item with water.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = false;
        }
    }
}
