using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, Observer<int>
{
    [SerializeField]
    private GameObject gameImage;               // GameObject that has Image component
    private GameItem gameItem;                  // GameItem instance

    [SerializeField]
    private GameObject amountBar;               // GameObject of bar to show item amount
    private Image filledBar;                    // Image component of amountBar

    private Image itemImage;                    // Item Image to be shown in inventory

    [SerializeField]
    private GameObject selectionObject;         // Selection to showing currently selected item

    [SerializeField]
    private static int currentIndex = 0;        // Unique index for created instance of InventorySlot
    [SerializeField]
    private int slotIndex;                      // Index for this InventorySlot instance

    private void Awake()
    {
        filledBar = amountBar.transform.GetChild(0).gameObject.GetComponent<Image>();
        itemImage = gameImage.GetComponent<Image>();
        slotIndex = currentIndex++;

        update(-1);
    }
    // Start is called before the first frame update
    void Start()
    {
        filledBar.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameItem is null)
        {
            amountBar.SetActive(false);
            gameImage.SetActive(false);
        }
        else if (gameItem.isAmountVisisble())
        {
            amountBar.SetActive(true);
            filledBar.fillAmount = gameItem.getTotalPercent();
        }
        else if (!gameItem.isAmountVisisble())
        {
            amountBar.SetActive(false);
            gameImage.SetActive(true);
        }
    }

    /*
     * Set current GameItem in this slot
     */
    public void setItem(GameItem item)
    {
        gameItem = item;
        if(item is DrawableItem)
        {
            DrawableItem i = item as DrawableItem;
            itemImage.sprite = i.sprite;
            gameImage.SetActive(true);
        }
        else
        {
            // TODO - make the same comparison to AnimatedItem;
        }
    }

    /*
     * Get this gameItem instance.
     */
    public GameItem getItem()
    {
        return gameItem;
    }

    /*
     * Receive subject index selected and show up selection
     * if is the same index that this slot.
     */
    public void update(int subjectEvent)
    {
        if(slotIndex == subjectEvent)
        {
            selectionObject.SetActive(true);
        }
        else
        {
            selectionObject.SetActive(false);
        }
    }
}
