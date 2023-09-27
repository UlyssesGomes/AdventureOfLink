using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotkeySlot : MonoBehaviour
{
    [SerializeField]
    private GameObject gameImage;
    private Image itemImage;

    private GameItem gameItem;

    // Start is called before the first frame update
    void Awake()
    {
        itemImage = gameImage.GetComponent<Image>();
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
