using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : DrawableItem
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float timeMove;

    private float timeCount;

    // Start is called before the first frame update
    void Start()
    {
        id = (int)ItemsEnum.WOOD;
        isStackable = true;
        sprite = GetComponent("WoodSprite") as SpriteRenderer;

        TODO - parai no video 7.4 no minuto 10;
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if(timeCount < timeMove)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // TODO - add 1 wood to player.

            PlayerInventory  inventory = collision.GetComponent<Player>().GetComponent("PlayerInventory") as PlayerInventory;
            amount = 1;
            inventory.addStoreItem(this);
            Destroy(gameObject);
        }
    }

    public void dropDirection(int value)
    {
        speed *= value;
    }
}
