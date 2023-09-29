using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private GameItem [] storedItems;                    // items stored in backpack

    public int switableItemIndex;                       // item index selected by hotkey

    [SerializeField]
    private GameItem prefabGameItem;
    private GameObject prefabGameObject;

    private List<Observer<InventorySubjectEnum>> storeItemsObservers;         // List of observers of sotredItems events
    private List<Observer<InventorySubjectEnum>> playerSetItemObservers;      // List of observers of playerSetItems events

    private void Awake()
    {
        storeItemsObservers = new List<Observer<InventorySubjectEnum>>();
        playerSetItemObservers = new List<Observer<InventorySubjectEnum>>();
    }

    // Start is called before the first frame update
    void Start()
    {
        storedItems = new GameItem[10];
        switableItemIndex = -1;

#if DEBUG
        //GameItem gi = loadItem(ItemsEnum.SIMPLE_AXE);
        //playerSwitableListItem.Add(gi);
        //addStoreItem(gi);
#endif
    }

    /*
     * Add a GameItem to storedItems array, if already have an item of that type
     * add amount of that item and return how many was added. If thereIf there is no slot 
     * available dont store and return 0.
     */
    public int addStoreItem(GameItem item)
    {
        int amountLeft = item.amount;
        int startAmount = item.amount;
        int index;

        index = getStoredItemIndexWithCapacity(item);
        if (index >= 0)
        {
            int freeCapacity = storedItems[index].total - storedItems[index].amount;

            if (freeCapacity >= amountLeft)
            {
                storedItems[index].amount += item.amount;
                amountLeft = 0;
            }
            else
            {
                storedItems[index].amount = storedItems[index].total;
                amountLeft -= freeCapacity;
            }
        }

        index = getFreeSlotIndex();

        if(index >= 0 && amountLeft > 0)
        {
            storedItems[index] = item;
            item.amount = amountLeft;
            amountLeft = 0;
        }        
       

        if(amountLeft == 0 || (startAmount - amountLeft) < startAmount)
        {
            notifyStoredItemsObservers(InventorySubjectEnum.ADD_STORE_ITEMS_EVENT);
            return startAmount - amountLeft;
        }

        return 0;
    }

    /*
     * Remove item.amount units from storeItems and then return how many in amount
     * was removed. If amount become to zero. Remove item from array too. If there is 
     * no amount to be removed, return 0.
     */
    public int removeStoreItemAmount(GameItem item, int amount)
    {
        int amountLeft = amount;
        int index;
        do
        {
            index = getFirstStoredItemIndex(item);
            if (index >= 0)
            {
                if (storedItems[index].amount > amountLeft)
                {
                    storedItems[index].amount = -amountLeft;
                    notifyStoredItemsObservers(InventorySubjectEnum.REMOVE_STORE_ITEMS_EVENT);
                    return amount;
                }
                else if (storedItems[index].amount == amountLeft)
                {
                    storedItems[index].amount = 0;
                    removeStoredItemById(storedItems[index].id);
                    notifyStoredItemsObservers(InventorySubjectEnum.REMOVE_STORE_ITEMS_EVENT);
                    return amount;
                }
                else
                {
                    amountLeft -= storedItems[index].amount;
                    storedItems[index].amount = 0;
                    removeStoredItemById(storedItems[index].id);
                    break;
                }
            }
        } while (index > -1 && amountLeft > 1);

        if(amountLeft < amount)
        {
            notifyStoredItemsObservers(InventorySubjectEnum.REMOVE_STORE_ITEMS_EVENT);
            return amount - amountLeft;
        }

        return 0;
    }

    public GameItem removeStoredItemById(int id)
    {
        for(int u = 0; u < storedItems.Length; u++)
        {
            if(storedItems[u].id == id)
            {
                GameItem gi = storedItems[u];
                storedItems[u] = null;
                return gi;
            }
        }

        return null;
    }

    /*
     * Get storeItems reference to iterate.
     */
    public GameItem [] getStoreItems()
    {
        return storedItems;
    }


    public int size()
    {
        return storedItems.Length;
    }

    public void addStoredItemsObservers(Observer<InventorySubjectEnum> observer)
    {
        storeItemsObservers.Add(observer);
    }

    private void notifyStoredItemsObservers(InventorySubjectEnum subjectEvent)
    {
        foreach(Observer<InventorySubjectEnum> o in storeItemsObservers)
        {
            o.update(subjectEvent);
        }
    }

    public void addPlayerSetItemsObservers(Observer<InventorySubjectEnum> observer)
    {
        playerSetItemObservers.Add(observer);
    }

    private void noitfyPlayerSetItemsObservers(InventorySubjectEnum subjectEvent)
    {
        foreach(Observer<InventorySubjectEnum> o in playerSetItemObservers)
        {
            o.update(subjectEvent);
        }
    }

    /*
     *  Get an index for an item with same type and amount free capacity.
     *  If there is no item with same type or free amount, return -1. 
     */
    private int getStoredItemIndexWithCapacity(GameItem item)
    {
        for (int u = 0; u < storedItems.Length; u++)
        {

            if (!(storedItems[u] is null) && storedItems[u].type == item.type && storedItems[u].amount < storedItems[u].total)
            {
                return u;
            }
        }

        return -1;
    }

    /*
     * Return the first occurent of that item with same type. Return -1 otherwise.
     */
    private int getFirstStoredItemIndex(GameItem item)
    {
        for(int u = 0; u < storedItems.Length; u++)
        {
            if(item.type == storedItems[u].type)
            {
                return u;
            }
        }
        return -1;
    }

    /*
     * Return the index of the first empty slot. Return -1 otherwise.
     */
    private int getFreeSlotIndex()
    {
        for(int u = 0; u < storedItems.Length; u++)
        {
            if(storedItems[u] is null)
            {
                return u;
            }
        }

        return -1;
    }

    public GameItem getCurrentSwitableItem()
    {
        if(switableItemIndex >= 0)
        {
            return storedItems[switableItemIndex];
        }

        return null;
    }

#if DEBUG
    /*
     * Method used to load initial weapons for test in develop mode.
     */
    private GameItem loadItem(ItemsEnum item)
    {
        UnityEngine.Object loadedResource;

        string pathPrefix = "Prefabs/Items/";
        GameItem gameItem = null;
        
        switch (item)
        {
            case ItemsEnum.WATERING_CAN:
                loadedResource = Resources.Load(pathPrefix + "WateringCan");
                if (loadedResource == null)
                {
                    throw new Exception("...no file found - please check the configuration");
                }
                prefabGameObject = (GameObject)Instantiate(loadedResource);
                WateringCanItem wateringCan = prefabGameObject.GetComponent<WateringCanItem>();
                wateringCan.itemName = "WateringCan";
                wateringCan.type = (int)ItemsEnum.WATERING_CAN;
                gameItem = wateringCan;
                break;

            case ItemsEnum.SIMPLE_AXE:
                loadedResource = Resources.Load(pathPrefix + "AxeItem");
                if (loadedResource == null)
                {
                    throw new Exception("...no file found - please check the configuration");
                }
                prefabGameObject = (GameObject)Instantiate(loadedResource);
                AxeItem axe = prefabGameObject.GetComponent<AxeItem>();
                axe.itemName = "Axe";
                axe.type = (int)ItemsEnum.SIMPLE_AXE;
                gameItem = axe;
                break;
        }

        return gameItem;
    }
#endif
}
