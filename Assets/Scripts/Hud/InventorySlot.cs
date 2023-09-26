using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, Observer<int>
{
    [SerializeField]
    private GameObject gameImage;
    private GameItem gameItem;

    [SerializeField]
    private GameObject amountBar;
    private Image filledBar;

    private Image itemImage;

    [SerializeField]
    private GameObject selectionObject;

    [SerializeField]
    private static int currentIndex = 0;
    [SerializeField]
    private int slotIndex;

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
