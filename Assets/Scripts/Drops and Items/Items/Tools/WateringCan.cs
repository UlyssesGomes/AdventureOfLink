using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : DrawableItem
{
    private float _waterCapacity = -1;             // amount of water this object can keep
    public float waterMaxCapacity;                 // max amount of water in watering can

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
        id = (int)ItemsEnum.WATERING_CAN;
        name = "Watering Can";
    }
}
