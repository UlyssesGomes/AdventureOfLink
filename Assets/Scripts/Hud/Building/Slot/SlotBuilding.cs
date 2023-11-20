using UnityEngine;
using UnityEngine.UI;

public class SlotBuilding : MonoBehaviour
{
    [SerializeField]
    private GameObject slotObject;
    [SerializeField]
    private Image contentSprite;

    [SerializeField]
    private GameObject selectionObject;

    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        contentSprite.gameObject.SetActive(true);
        animator.SetBool("isSelected", false);
    }

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

    /// <summary>
    /// Activate and deactivate slot selection animation.
    /// </summary>
    /// <param name="isSelected">selection value</param>
    public void setSelection(bool isSelected)
    {
        animator.SetBool("isSelected", isSelected);
        //selectionObject.SetActive(isSelected);
    }
}
