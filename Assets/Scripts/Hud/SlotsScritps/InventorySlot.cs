using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, Observer<GenericSubject<int, GameItem[]>>
{
    [SerializeField]
    public int indexId;                         // index from playerInventory.storedItems that this Obserser is watching
    [SerializeField]
    protected Image itemImage;                  // image of the item
    [SerializeField]
    protected Text quantityLabel;               // label to show amount of item per slot
    [HideInInspector]
    public GameItem gameItem;                   // instance with all atributes of the item.

    public Observable<GenericSubject<int, GameItem[]>> observableParent;

    // Start is called before the first frame update
    void Start()
    {
        itemImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void update(GenericSubject<int, GameItem[]> subjectEvent)
    {
        if(subjectEvent.type == indexId)
        {
            gameItem = subjectEvent.subject[indexId];

            if(gameItem && gameItem.animType == AnimTypeEnum.DRAWABLE)
            {
                DrawableItem i = gameItem as DrawableItem;
                itemImage.sprite = i.sprite;
                changeQuantityLabelVisibility();
                itemImage.gameObject.SetActive(true);
            }
            else
            {
                changeQuantityLabelVisibility();
                itemImage.gameObject.SetActive(false);
            }
        }
    }

    public void changeQuantityLabelVisibility()
    {
        if(gameItem.total > 1)
        {
            quantityLabel.text = gameItem.amount.ToString();
            quantityLabel.gameObject.SetActive(true);
        }
        else
        {
            quantityLabel.text = "0";
            quantityLabel.gameObject.SetActive(false);
        }
    }

    public void setObservable(Observable<GenericSubject<int, GameItem[]>> observable)
    {
        observableParent = observable;
    }
}
