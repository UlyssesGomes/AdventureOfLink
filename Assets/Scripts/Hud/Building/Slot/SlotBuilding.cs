using UnityEngine;
using UnityEngine.UI;

public class SlotBuilding : MonoBehaviour
{
    [SerializeField]
    private GameObject slotObject;
    [SerializeField]
    private Image contentSprite;

    /// <summary>
    /// Show slot only if has a sprite to him, otherwise, SetActive to false.
    /// </summary>
    /// <param name="sprite"></param>
    public void setContentSprite(Sprite sprite)
    {
        if(sprite != null)
        {
            contentSprite.sprite = sprite;
            slotObject.SetActive(true);
        }
        else
        {
            slotObject.SetActive(false);
        }
    }
}
