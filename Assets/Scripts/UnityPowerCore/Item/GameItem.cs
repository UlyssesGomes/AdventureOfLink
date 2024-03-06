using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class GameItem : ScriptableObject
{
    [HideInInspector]
    public int id;                  // unique id that identifies this instance
    public ObjectIdEnum itemId;       // id that identifies that type of item
    public ItemTypeEnum type;       // item type, must be cutting, watering, battle...
    public string itemName;         // name of the item
    public bool isStackable;        // if true, this object can have amount > 1
    public bool isViewPercent;      // if true, this item can present a percent bar
    private int _amount;            // amount of the same item
    public int total;               // total of this item in the same slot
    public AnimTypeEnum animType;   // presentation type of this object, it can be sprite or animation

    private static int nextId;      // stores the next unique and valid id for the next object

    private void Awake()
    {
        id = getNextUniqueId();
    }

    public int amount
    {
        get { return _amount; }
        set
        {
            if (total > 0)
            {
                if (value <= total)
                {
                    _amount = value;
                }
                else
                {
                    _amount = total;
                }
            }
            else
            {
                throw new Exception("Item '" + itemName + "' has no 'total' defined.");
            }
        }
    }


    /// <summary>
    /// If true, then a amount bar must be displayed in inventary slot.
    /// </summary>
    /// <returns>Bool flag that indicates if amount is visible</returns>
    public virtual bool isAmountVisisble()
    {
        return isViewPercent;
    }

    /// <summary>
    /// Returns the total percentage of how much of this item is in the slot.
    /// With 0 being none and 1 being the maximum quantity.
    /// </summary>
    /// <returns>Percent (float between 0 and 1)</returns>
    public virtual float getTotalPercent()
    {
        return (float)_amount / total;
    }

    /// <summary>
    /// Generate unique item id.
    /// </summary>
    /// <returns>Unique int id</returns>
    public int getNextUniqueId()
    {
        return nextId++;
    }

    /// <summary>
    /// Add amount to stackable items respecting total amount.
    /// </summary>
    /// <param name="amount"></param>
    public void addAmountToStackableItems(int amount)
    {
        if (this.amount + amount <= total)
        {
            this.amount += amount;
        }
        else
        {
            // TODO - make a "tandan" sound;
        }
    }
}
