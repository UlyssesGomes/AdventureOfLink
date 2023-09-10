using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private IDictionary<int, GameItem> storedItems;

    [SerializeField]
    private List<GameItem> playerSet;

    [SerializeField]
    private GameItem prefabGameItem;
    // Start is called before the first frame update
    void Start()
    {
        storedItems = new Dictionary<int, GameItem>();
        playerSet = new List<GameItem>(7);

        GameItem item = Instantiate(prefabGameItem);
        item.itemName = "Watering can";
        item.id = (int)ItemsEnum.WATERING_CAN;

        playerSet.Add(item);
    }

    /*
     * Add a GameItem to storedItems, if already have an item of that type
     * add amount of that item.
     */
    public void addStoreItem(GameItem item)
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
    public bool removeStoreItem(GameItem item)
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

    /*
     * Get item in player set by index.
     */
    public GameItem getSetItem(int index)
    {
        return playerSet[index];
    }
}
