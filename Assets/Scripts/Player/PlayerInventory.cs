using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private IDictionary<int, GameItem> storedItems;     // items stored in backpack

    [SerializeField]
    private List<GameItem> playerListItem;              // switable action items, items that is ready to go to hand by press key 1~5

    [SerializeField]
    private GameItem prefabGameItem;
    // Start is called before the first frame update
    void Start()
    {
        storedItems = new Dictionary<int, GameItem>();
        playerListItem = new List<GameItem>();

        GameItem item = Instantiate(prefabGameItem);
        item.itemName = "Watering can";
        item.id = (int)ItemsEnum.SIMPLE_AXE;

        playerListItem.Add(item);
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
     * Get item in player list by index.
     */
    public GameItem getListItem(int index)
    {
        return playerListItem[index];
    }

    /*
    * Get item in player set by index.
    */
    public GameItem getSetItem(int index)
    {
        if(storedItems.ContainsKey(index))
        {
            if(storedItems[index] == null)
            {
                //Debug.Log("Vai retornar null");
            }
            return storedItems[index];
        }
        return null;
    }

    public int size()
    {
        return storedItems.Count;
    }
}
