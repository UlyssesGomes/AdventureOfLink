using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (isStackable)
            {
                _amount = value;
            }
            else if(_amount == 0 && value < 1 && value >= 0)
            {
                _amount = 1;
            } 
            else if(value > 1)
            {
                _amount = 1;
            }
            else if(value < 0)
            {
                _amount = 0;
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
        amount = 1;
        total = 1;
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
