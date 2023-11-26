using UnityEngine;
using UnityEngine.UI;

public class SlotBuildingMaterial : SlotBuilding
{
    [SerializeField]
    public Text text;

    /// <summary>
    /// Set the text in the right format to indicate the current quantity of items 
    /// and show the total quantity needed.
    /// </summary>
    /// <param name="amount">current amount of that item in inventory</param>
    /// <param name="total">total amount required of that item</param>
    public void setText(int amount, int total)
    {
        text.text = total + "/" + amount;
    }
}
