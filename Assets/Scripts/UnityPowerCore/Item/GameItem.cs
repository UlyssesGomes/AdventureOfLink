using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class GameItem : ScriptableObject
{
    [HideInInspector]
    public int id;                  // unique id that identifies this instance
    public ItemIdEnum itemId;       // id that identifies that type of item
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

    /*
     * If true, then a amount bar must be displayed in 
     * inventary slot.
     */
    public virtual bool isAmountVisisble()
    {
        return isViewPercent;
    }

    /*
     * Returns the total percentage of how much of this item is in the slot. 
     * With 0 being none and 1 being the maximum quantity.
     */
    public virtual float getTotalPercent()
    {
        return (float)_amount / total;
    }

    /*
     * Return the next unique and valid for new objects.
     */
    public int getNextUniqueId()
    {
        return nextId++;
    }

    /*
     * Add amount to stackable items respecting total amount.
     */
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
