using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private GameObject gameImage;
    private GameItem gameItem;

    [SerializeField]
    private GameObject amountBar;
    private Image filledBar;

    private Image itemImage;

    private Vector3 pictureScale;

    private void Awake()
    {
        filledBar = amountBar.transform.GetChild(0).gameObject.GetComponent<Image>();
        itemImage = gameImage.GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        filledBar.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if ((!(gameItem is null) && !gameItem.isStackable) || gameItem is null)
        {
            amountBar.SetActive(false);
            gameImage.SetActive(false);
        }

        if (!(gameItem is null) && gameItem.isStackable)
        {
            amountBar.SetActive(true);
            filledBar.fillAmount = gameItem.getTotalPercent();
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
}
