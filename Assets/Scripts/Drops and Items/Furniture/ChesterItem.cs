using UnityEngine;

[CreateAssetMenu(fileName = "ChesterItem", menuName = "LocalGame/Items/new ChesterItem")]
public class ChesterItem : DrawableItem
{
    [SerializeField]
    private Sprite openedChesterSprite;
    [SerializeField]
    private GameItem[] slots;
    [SerializeField]
    private bool isOpened;
}
