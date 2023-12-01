using UnityEngine;

[CreateAssetMenu(fileName = "ChestItem", menuName = "LocalGame/Items/new ChestItem")]
public class ChestItem : DrawableItem
{
    [SerializeField]
    public Sprite openedChesterSprite;
    [SerializeField]
    public GameItem[] slots;
    [SerializeField]
    public bool isOpened;
}
