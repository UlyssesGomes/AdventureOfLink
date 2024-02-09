using UnityEngine;

public class RoofOpaqueAgent : Agent
{
    private SpriteRenderer sprite;                  // roof house sprite to transform color
    private Color currentColor;                     // sprite current color

    private float speedTransparentEffect = 2f;      // transformation speed for opaque color
    private float currentAlpha;                     // current alpha component color

    public RoofOpaqueAgent(SpriteRenderer sprite)
    {
        this.sprite = sprite;
    }

    public override int agentType()
    {
        return (int)HouseAgentEnum.ROOF_OPAQUE_AGENT;
    }

    public override void restart()
    {
        currentAlpha = 0f;
    }

    public override void start()
    {
        currentColor = sprite.color;
        currentAlpha = currentColor.a;
    }

    public override void update()
    {
        if(currentAlpha < 1.00000f)
        {
            currentAlpha += speedTransparentEffect * Time.deltaTime;
            currentColor.a = currentAlpha;
            sprite.color = currentColor;
        }
        else
        {
            stop();
        }
    }
}
