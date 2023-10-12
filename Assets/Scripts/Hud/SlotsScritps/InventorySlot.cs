using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, Observer<GenericSubject<int, GameItem[]>>
{
    [SerializeField]
    protected int indexId;
    [SerializeField]
    protected Image itemImage;
    [HideInInspector]
    public GameItem gameItem;

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
            }
            else
            {
                itemImage.gameObject.SetActive(false);
            }
        }
    }

    public void setObservable(Observable<GenericSubject<int, GameItem[]>> observable)
    {
        observableParent = observable;
    }
}
