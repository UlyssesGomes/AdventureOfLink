﻿using UnityEngine;

public class RoofTransparentAgent : Agent
{
    private SpriteRenderer sprite;                  // roof house sprite to transform color
    private SpriteRenderer chimneySprite;           // chimney sprite to transform color
    private SpriteRenderer smokeSprite;             // smoke chimney sprite to transform color
    private Color currentColor;                     // sprite current color
    private Color defaultColor;                     // sprite original color

    private float speedTransparentEffect = 2f;      // transformation speed for opaque color
    private float currentAlpha;                     // current alpha component color

    public RoofTransparentAgent(SpriteRenderer sprite, SpriteRenderer chimneySprite, SpriteRenderer smokeSprite)
    {
        this.sprite = sprite;
        this.chimneySprite = chimneySprite;
        this.smokeSprite = smokeSprite;
    }

    public override int agentType()
    {
        return (int)HouseAgentEnum.ROOF_TRANSPARENT_AGENT;
    }

    public override void restart()
    {
        currentAlpha = 1f;
        sprite.color = defaultColor;
        chimneySprite.color = defaultColor;
        smokeSprite.color = defaultColor;
    }

    public override void start()
    {
        currentColor = defaultColor = sprite.color;
        currentAlpha = sprite.color.a;
    }

    public override void update()
    {
        if(currentAlpha > 0.0000f)
        {
            currentAlpha -= speedTransparentEffect * Time.deltaTime;
            currentColor.a = currentAlpha;
            sprite.color = currentColor;
            chimneySprite.color = currentColor;
            smokeSprite.color = currentColor;
        }
        else
        {
            stop();
        }
    }
}
