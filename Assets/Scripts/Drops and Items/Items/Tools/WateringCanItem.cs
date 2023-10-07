using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WateringCanItem", menuName ="Items/new WateringCan")]
public class WateringCanItem : DrawableItem
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

    protected override void defineItem()
    {
        itemId = (int) ItemIdEnum.WATERING_CAN;
        type = (int) ItemTypeEnum.WATERING;
    }

    public void toWater()
    {
        waterCapacity -= waterOutterFlow * Time.deltaTime;
    }

    public override float getTotalPercent()
    {
        return _waterCapacity / waterMaxCapacity;
    }

    public override bool isAmountVisisble()
    {
        return true;
    }
}
