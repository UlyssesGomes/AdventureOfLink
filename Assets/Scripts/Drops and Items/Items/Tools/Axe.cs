using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : DrawableItem
{

    // Update is called once per frame
    void Update()
    {
        
    }

    void Start()
    {
        name = "Axe";
        id = (int)ItemsEnum.SIMPLE_AXE;
        putIntoStore();
        makeMonoItem();
    }
}
