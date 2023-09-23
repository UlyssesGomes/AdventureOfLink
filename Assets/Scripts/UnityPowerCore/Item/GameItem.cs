using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class GameItem : MonoBehaviour
{
    public int id;                  // unique id
    public int type;                // item type
    public string itemName;         // name of the item
    public bool isStackable;        // if true, this object can have amount > 1
    public bool isOnFloor;          // if true, this item will be redered in the world map
    private int _amount;            // amount of the same item
    public int total;               // total of this item in the same slot

    private static int nextId;      // stores the next unique and valid id for the next object

    public int amount {
        get { return _amount; }
        set {
            if(total > 0)
            {
                if(_amount + value <= total)
                {
                    _amount += value;
                }
                else
                {
                    _amount = total;
                }
                Debug.Log("_amount: " + _amount);
            }
            else
            {
                throw new Exception("Item '" + itemName + "' has no 'total' defined.");
            }
        }
    }

    /*
     * Turn object visible in world
     */
    public void putOnFloor()
    {
        gameObject.SetActive(true);
        isOnFloor = true;
    }

    /*
     * Turn object invisible on the floor and dont make collision.
     */
    public void putIntoStore()
    {
        gameObject.SetActive(false);
        isOnFloor = false;
    }

    /*
     * Make object agroupable with other amount of the same ID
     */
    public void makeStackable()
    {
        isStackable = true;
        amount = 1;
    }

    /*
     * Make object non agroupable, only one item is allowed per slot.
     */
    public void makeMonoItem()
    {
        isStackable = false;
        total = 1;
        amount = 1;
    }

    /*
     * Returns the total percentage of how much of this item is in the slot. 
     * With 0 being none and 1 being the maximum quantity.
     */
    public virtual float getTotalPercent()
    {
        return (float) _amount / total;
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
        if(this.amount + amount <= total)
        {
            this.amount += amount;
        }
        else
        {
            // TODO - make a "tandan" sound;
        }
    }
}
