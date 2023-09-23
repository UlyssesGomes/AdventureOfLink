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

    [SerializeField]
    private GameObject woodItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
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
            // cria aqui o obj
            GameObject obj = Instantiate(woodItemPrefab);
            WoodItem woodItem = obj.GetComponent<WoodItem>();
            int amountGenerated = 1;
            woodItem.amount = amountGenerated;
            PlayerInventory  inventory = collision.GetComponent<Player>().GetComponent("PlayerInventory") as PlayerInventory;
            int amountAdded = inventory.addStoreItem(woodItem);
            if(amountAdded == amountGenerated)
            {
                Destroy(obj);
                Destroy(gameObject);
            }
            else
            {
                woodItem.amount = amountGenerated - amountAdded;
            }
        }
    }

    public void dropDirection(int value)
    {
        speed *= value;
    }
}
