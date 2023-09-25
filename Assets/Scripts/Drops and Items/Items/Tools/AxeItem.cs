using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeItem : DrawableItem
{

    // Update is called once per frame
    void Update()
    {
    }

    void Awake()
    {
        id = getNextUniqueId();
        name = "Axe";
        type = (int)ItemsEnum.SIMPLE_AXE;
        putIntoStore();
        makeMonoItem();
    }
}
