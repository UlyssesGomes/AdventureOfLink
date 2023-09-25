﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodItem : DrawableItem
{
    private void Awake()
    {
        total = 2;
        makeStackable();
        putIntoStore();
        id = getNextUniqueId();
        itemName = "Wood";
        type = (int)ItemsEnum.WOOD;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
