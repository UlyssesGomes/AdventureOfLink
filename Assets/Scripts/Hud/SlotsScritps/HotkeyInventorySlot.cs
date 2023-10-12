using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HotkeyInventorySlot : InventorySlot
{
    [SerializeField]
    private Text buttonLabel;

    private 

    void Awake()
    {
        buttonLabel.text = $"{indexId + 1}";   
    }
}
