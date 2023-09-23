using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodItem : DrawableItem
{
    private void Awake()
    {
        makeStackable();
        putIntoStore();
        id = getNextUniqueId();
        itemName = "Wood";
        type = (int)ItemsEnum.WOOD;
        total = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
