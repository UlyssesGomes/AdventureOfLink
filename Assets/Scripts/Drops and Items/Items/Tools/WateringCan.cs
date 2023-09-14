using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : DrawableItem
{
    public int waterCapacity;           // amount of water this object can keep

    // Update is called once per frame
    void Update()
    {
        
    }

    void Start()
    {
        putIntoStore();
        makeMonoItem();
        id = (int)ItemsEnum.WATERING_CAN;
        name = "Watering Can";
    }
}
