using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotkeySlot : MonoBehaviour
{
    [SerializeField]
    private GameObject gameImage;           // GameObject with an Image component in slot
    private Image itemImage;                // Image from gameImage

    private GameItem gameItem;              // Instance of an item of GameItem

    public int itemStoredIndex;             // Index that this hotkey referer to in storedItems in playerInventory

    // Start is called before the first frame update
    void Awake()
    {
        itemImage = gameImage.GetComponent<Image>();
    }

    private void Start()
    {
        itemStoredIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * Set a selected gameItem.
     */
    public void setItem(GameItem item)
    {
        gameItem = item;
        if(item is null)
        {
            gameItem = null;
            gameImage.SetActive(false);
        }
        if (item is DrawableItem)
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
