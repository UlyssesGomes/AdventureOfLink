using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodItem : DrawableItem
{
    // Start is called before the first frame update
    void Start()
    {
        makeStackable();
        putIntoStore();
        id = getNextUniqueId();
        name = "Wood";
        type = (int)ItemsEnum.WOOD;
        total = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
