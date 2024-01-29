using UnityEngine;

public abstract class AbstractChest : Furniture
{
    [SerializeField]
    protected ChestItem chest;        // chest defined data

    public override DrawableItem getFurnitureData()
    {
        return chest;
    }

    /// <summary>
    /// Open and close sprite chest
    /// </summary>
    /// <param name="open">flag to control chest opening</param>
    public void openChest(bool open)
    {
        if (open)
            sprite.sprite = chest.openedChesterSprite;
        else
            sprite.sprite = chest.sprite;
    }

    protected override void interact()
    {
        openChest(true);
    }

    protected override void getAway()
    {
        openChest(false);
    }
}
