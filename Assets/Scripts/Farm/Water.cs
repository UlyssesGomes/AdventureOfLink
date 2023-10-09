using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Water : MonoBehaviour
{
    [SerializeField]
    private bool detectingPlayer;

    private Player player;
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
                    wateringCan.waterCapacity++;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = true;
            player = collision.GetComponent<Player>() as Player;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = false;
        }
    }
}
