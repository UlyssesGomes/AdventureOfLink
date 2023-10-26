using UnityEngine;

using System;

enum InventoryNotifyAction { ALL = int.MaxValue, }

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private GameItem [] storedItems;                                            // items stored in backpack
    public int switableItemIndex;                                               // item index selected by hotkey

    public Observable<GenericSubject<int, GameItem[]>> storedItemsObservable;   // store items observable to notify changes in storedItems array.

    private GenericSubject<int, GameItem[]> subjectEvent;

    public readonly int hotkeyInventorySize = 5;                                // total slot size to hotkey inventory (hotkey slot is the first five slots of storedItems
    public int inventoryCurrentSize = 15;                                       // current available inventory size to store itens.
    public readonly int TOTAL_INVENTORY_SIZE = 15;// next value must be 25 when backpack system is ready to use  
    // total amount available to slots interface including hotkeysSlots

    private void Awake()
    {
        storedItemsObservable = new Observable<GenericSubject<int, GameItem[]>>();
        subjectEvent = new GenericSubject<int, GameItem[]>();
        storedItems = new GameItem[TOTAL_INVENTORY_SIZE];
    }

    // Start is called before the first frame update
    void Start()
    {
        subjectEvent.subject = storedItems;
        switableItemIndex = -1;

#if DEBUG
        //GameItem gi = loadItem(ItemsEnum.SIMPLE_AXE);
        //playerSwitableListItem.Add(gi);
        //addStoreItem(gi);
#endif
    }

    /// <summary>
    /// Add a GameItem to storedItems array, if already have an item of that type
    /// add amount of that item and return how many was added.If thereIf there is no slot 
    /// available dont store and return 0.
    /// </summary>
    /// <param name="item"></param>
    /// <returns>How amount of that item was added</returns>
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
            notifyStoredItemsObservers(index);
        }

        index = getFreeSlotIndex();

        if(index >= 0 && amountLeft > 0)
        {
            storedItems[index] = item;
            item.amount = amountLeft;
            amountLeft = 0;
            notifyStoredItemsObservers(index);
        }        
       

        if(amountLeft == 0 || (startAmount - amountLeft) < startAmount)
        {
            return startAmount - amountLeft;
        }

        return 0;
    }

    /// <summary>
    /// Remove item.amount units from storeItems and then return how many in amount
    /// was removed. If amount become to zero. Remove item from array too. If there is
    /// no amount to be removed, return 0.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
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
                    notifyStoredItemsObservers(index);
                    return amount;
                }
                else if (storedItems[index].amount == amountLeft)
                {
                    storedItems[index].amount = 0;
                    removeStoredItemById(storedItems[index].id);
                    
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
            return amount - amountLeft;
        }

        return 0;
    }

    /// <summary>
    /// Remove gameItem by id from storedItems array.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GameItem removeStoredItemById(int id)
    {
        for(int u = 0; u < storedItems.Length; u++)
        {
            if(storedItems[u].id == id)
            {
                GameItem gi = storedItems[u];
                storedItems[u] = null;
                notifyStoredItemsObservers(u);
                return gi;
            }
        }

        return null;
    }

    /// <summary>
    /// Get storeItems reference to iterate.
    /// </summary>
    /// <returns></returns>
    public GameItem [] getStoreItems()
    {
        return storedItems;
    }

    /// <summary>
    /// Get current storedItems array size.
    /// </summary>
    /// <returns></returns>
    public int size()
    {
        return storedItems.Length;
    }

    /// <summary>
    /// Add observer to observe changes in storedItems array.
    /// </summary>
    /// <param name="observer"></param>
    public void addStoredItemsObservers(Observer<GenericSubject<int, GameItem[]>> observer)
    {
        storedItemsObservable.addObservers(observer);
    }

    /// <summary>
    /// Notify all observers that item with index "index" has changed.
    /// </summary>
    /// <param name="index"></param>
    public void notifyStoredItemsObservers(int index)
    {
        subjectEvent.id = -1;
        subjectEvent.type = index;
        storedItemsObservable.notify(subjectEvent);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public void notifyStoredItemsObserversById(int id)
    {
        subjectEvent.id = id;
        subjectEvent.type = -1;
        storedItemsObservable.notify(subjectEvent);
    }

    /// <summary>
    /// Get an index for an item with same type and amount free capacity.
    /// If there is no item with same type or free amount, return -1. 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private int getStoredItemIndexWithCapacity(GameItem item)
    {
        for (int u = 0; u < storedItems.Length; u++)
        {
            if (!(storedItems[u] is null) && storedItems[u].itemId == item.itemId && storedItems[u].amount < storedItems[u].total)
            {
                return u;
            }
        }

        return -1;
    }

    /// <summary>
    /// Return the first occurent of that item with same type. Return -1 otherwise.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private int getFirstStoredItemIndex(GameItem item)
    {
        for(int u = 0; u < storedItems.Length; u++)
        {
            if(item.itemId == storedItems[u].itemId)
            {
                return u;
            }
        }
        return -1;
    }

    /// <summary>
    /// Return the index of the first empty slot. Return -1 otherwise.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Get current item in hand ready to use.
    /// </summary>
    /// <returns>GameItem</returns>
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
    private GameItem loadItem(ItemIdEnum item)
    {
        UnityEngine.Object loadedResource;

        string pathPrefix = "Prefabs/Items/";
        GameItem gameItem = null;
        
        switch (item)
        {
            case ItemIdEnum.WATERING_CAN:
                // TODO NOW - tirar esse comentário.
                //loadedResource = Resources.Load(pathPrefix + "WateringCan");
                //if (loadedResource == null)
                //{
                    //throw new Exception("...no file found - please check the configuration");
                //}
                //prefabGameObject = (GameObject)Instantiate(loadedResource);
                //WateringCanItem wateringCan = prefabGameObject.GetComponent<WateringCanItem>();
                //wateringCan.itemName = "WateringCan";
                //wateringCan.type = (int)ItemsEnum.WATERING_CAN;
                //gameItem = wateringCan;
                break;

            case ItemIdEnum.SIMPLE_AXE:
                // TODO NOW - tirar esse comentário.
                //loadedResource = Resources.Load(pathPrefix + "AxeItem");
                //if (loadedResource == null)
                //{
                //    throw new Exception("...no file found - please check the configuration");
                //}
                //prefabGameObject = (GameObject)Instantiate(loadedResource);
                //AxeItem axe = prefabGameObject.GetComponent<AxeItem>();
                //axe.itemName = "Axe";
                //axe.type = (int)ItemsEnum.SIMPLE_AXE;
                //gameItem = axe;
                break;
        }

        return gameItem;
    }
#endif
}
