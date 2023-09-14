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
        //Parei no Minuto 10:07 da aula 7. Plantar
    }

    // Update is called once per frame
    void Update()
    {
        if(detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            PlayerInventory inventory = player.GetComponent<PlayerInventory>() as PlayerInventory;
            GameItem item = inventory.getListItem(0);
            if (!(item is null) && inventory.getListItem(0).id == (int)ItemsEnum.WATERING_CAN)
            {
                WateringCan wateringCan = item as WateringCan;
                wateringCan.waterCapacity++;

                TODO - parei na aula 7.4 em 8:32.
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
