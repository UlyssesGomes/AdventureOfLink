using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HotkeyInventorySlot : InventorySlot
{
    [SerializeField]
    private Text buttonLabel;

    /// <summary>
    /// Set hotkey inventory button label value.
    /// </summary>
    public void setLabel()
    {
        buttonLabel.text = $"{indexId + 1}";
    }
}
