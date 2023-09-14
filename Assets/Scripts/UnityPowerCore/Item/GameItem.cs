using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    public int id;                  // unique id
    public string itemName;         // name of the item
    public bool isStackable;        // if true, this object can have amount > 1
    public bool isOnFloor;          // 
    private int _amount;            // amount of the same item

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
    }
}
