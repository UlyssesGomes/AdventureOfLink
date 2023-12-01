using UnityEngine;

public class Chest : Furniture<ChestItem>
{

    protected override void getAway()
    {
        Debug.Log("Saiu da área de colisão de interação do baú.");
        sprite.sprite = furnitureAsset.sprite;
    }

    protected override void interact()
    {
        Debug.Log("Está em colisão com a área de interação do baú.");
        sprite.sprite = furnitureAsset.openedChesterSprite;
    }
}
