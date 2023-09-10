using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float timeMove;

    private float timeCount;

    public DrawableItem woodItem;
    // Start is called before the first frame update
    void Start()
    {
        woodItem.id = (int)ItemsEnum.WOOD;
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
            woodItem.amount = 1;
            inventory.addStoreItem(woodItem);
            Destroy(gameObject);
        }
    }

    public void dropDirection(int value)
    {
        speed *= value;
    }
}
