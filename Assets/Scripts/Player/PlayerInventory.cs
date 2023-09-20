using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private IDictionary<int, GameItem> storedItems;     // items stored in backpack

    [SerializeField]
    private List<GameItem> playerListItem;              // switable action items, items that is ready to use by press key 1~5

    [SerializeField]
    private GameItem prefabGameItem;
    private GameObject prefabGameObject;

    // Start is called before the first frame update
    void Start()
    {
        storedItems = new Dictionary<int, GameItem>();
        playerListItem = new List<GameItem>();

#if DEBUG
        playerListItem.Add(loadItem(ItemsEnum.SIMPLE_AXE));
#endif
    }

    /*
     * Add a GameItem to storedItems, if already have an item of that type
     * add amount of that item.
     */
    public void addStoreItem(GameItem item)
    {
        Debug.Log("quantidade: " + item.amount);
        if (!storedItems.ContainsKey(item.type))
        {
            storedItems.Add(item.type, item);
        }
        else
        {
            storedItems[item.type].addAmountToStackableItems(item.amount);
        }
    }

    /*
     * Remove item.amount units from storeItems. If the correct
     * amount is removed, then return true, otherwise return false.
     */
    public bool removeStoreItem(GameItem item)
    {
        if (storedItems.ContainsKey(item.type))
        {
            if(storedItems[item.type].amount >= item.amount)
            {
                storedItems[item.type].amount -= item.amount;

                if(storedItems[item.type].amount == 0)
                {
                    storedItems.Remove(item.type);
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
     * Get storeItems reference to iterate.
     */
    public IDictionary<int, GameItem> getStoreItems()
    {
        return storedItems;
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

#if DEBUG
    /*
     * Method used to load initial weapons for test in develop mode.
     */
    private GameItem loadItem(ItemsEnum item)
    {
        GameItem gameitem = null;
        UnityEngine.Object loadedResource;

        string pathPrefix = "Prefabs/Items/";
        
        switch (item)
        {
            case ItemsEnum.WATERING_CAN:
                loadedResource = Resources.Load(pathPrefix + "WateringCan");
                if (loadedResource == null)
                {
                    throw new Exception("...no file found - please check the configuration");
                }
                prefabGameObject = (GameObject)Instantiate(loadedResource);
                gameitem = prefabGameObject.GetComponent<WateringCan>();
                gameitem.itemName = "WateringCan";
                gameitem.type = (int)ItemsEnum.WATERING_CAN;
                break;

            case ItemsEnum.SIMPLE_AXE:
                loadedResource = Resources.Load(pathPrefix + "Axe");
                if (loadedResource == null)
                {
                    throw new Exception("...no file found - please check the configuration");
                }
                prefabGameObject = (GameObject)Instantiate(loadedResource);
                gameitem = prefabGameObject.GetComponent<Axe>();
                gameitem.itemName = "Axe";
                gameitem.type = (int)ItemsEnum.SIMPLE_AXE;
                break;
        }

        return gameitem;
    }
#endif
}
