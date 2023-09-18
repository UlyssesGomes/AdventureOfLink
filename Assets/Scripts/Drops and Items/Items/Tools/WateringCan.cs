using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : DrawableItem
{
    private float _waterCapacity = -1;             // amount of water this object can keep
    public float waterMaxCapacity;                 // max amount of water in watering can

    public float waterOutterFlow;                  // how much water flow from water can when it's in use

    public float waterCapacity
    {
        get { return _waterCapacity; }
        set
        {
            if (value > waterMaxCapacity)
            {
                _waterCapacity = waterMaxCapacity;
            }
            else if (value < 0.000f)
            {
                _waterCapacity = 0;
            }
            else
            {
                _waterCapacity = value;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Start()
    {
        putIntoStore();
        makeMonoItem();
        id = getNextUniqueId();
        type = (int)ItemsEnum.WATERING_CAN;
        name = "Watering Can";
        waterOutterFlow = 1.5f;
    }

    public void toWater()
    {
        waterCapacity -= 1.5f * Time.deltaTime;
    }

    public override float getTotalPercent()
    {
        return _waterCapacity / waterMaxCapacity;
    }
}
