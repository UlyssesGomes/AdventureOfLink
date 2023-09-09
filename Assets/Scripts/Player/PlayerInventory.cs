using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private IDictionary<int, GameItem> storedItems;

    // Start is called before the first frame update
    void Start()
    {
        storedItems = new Dictionary<int, GameItem>();
    }

    /*
     * Add a GameItem to storedItems, if already have an item of that type
     * add amount of that item.
     */
    public void addItem(GameItem item)
    {
        if (!storedItems.ContainsKey(item.id))
        {
            storedItems.Add(item.id, item);
        }
        else
        {
            storedItems[item.id].amount += item.amount;
        }
    }

    /*
     * Remove item.amount units from storeItems. If the correct
     * amount is removed, then return true, otherwise return false.
     */
    public bool removeItem(GameItem item)
    {
        if (storedItems.ContainsKey(item.id))
        {
            if(storedItems[item.id].amount >= item.amount)
            {
                storedItems[item.id].amount -= item.amount;

                if(storedItems[item.id].amount == 0)
                {
                    storedItems.Remove(item.id);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }
}
