﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private GameItem [] storedItems;                    // items stored in backpack

    [SerializeField]
    private List<GameItem> playerListItem;              // switable action items, items that is ready to use by press key 1~5

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
        playerListItem = new List<GameItem>();

#if DEBUG
        playerListItem.Add(loadItem(ItemsEnum.SIMPLE_AXE));
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
        int index;

        do
        {
            index = getStoredItemIndexWithCapacity(item);
            if (index >= 0)
            {
                int freeCapacity = storedItems[index].total - storedItems[index].amount;

                if (freeCapacity >= amountLeft)
                {
                    storedItems[index].amount += item.amount;
                    amountLeft -= 0;
                }
                else
                {
                    storedItems[index].amount = storedItems[index].total;
                    amountLeft -= freeCapacity;
                }
            }

            index = getFreeSlotIndex();

            if(index >= 0)
            {
                storedItems[index] = item;
                return item.amount;
            }        
        } while (index >= 0 && amountLeft > 0);

        if(amountLeft == 0 || (item.amount - amountLeft) < item.amount)
        {
            notifyStoredItemsObservers(InventorySubjectEnum.ADD_STORE_ITEMS_EVENT);
            return item.amount - amountLeft;
        }

        Testear cenário em que se tem 1 item com capacidade 1 de um total de 2, e esteja adicionando uma quantidade de 2.

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

    /*
     * Get item in player list by index.
     */
    public GameItem getListItem(int index)
    {
        return playerListItem[index];
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
