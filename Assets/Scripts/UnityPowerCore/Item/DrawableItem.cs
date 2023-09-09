using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawableItem : GameItem
{
    public SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent("WoodSprite") as SpriteRenderer;
    }
}
