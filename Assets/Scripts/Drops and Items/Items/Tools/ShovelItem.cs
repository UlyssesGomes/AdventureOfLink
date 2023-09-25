using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelItem : DrawableItem
{
    private void Awake()
    {
        makeMonoItem();
        putIntoStore();
        id = getNextUniqueId();
        itemName = "Shovel";
        type = (int)ItemsEnum.SIMPLE_SHOVEL;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
