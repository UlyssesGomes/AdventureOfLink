using UnityEngine;

public abstract class House<U>: MonoBehaviour
{
    [SerializeField]
    protected Sprite sprite;                // sprite to render on screen
    public U itemId;                        // item id that represents this item
}
