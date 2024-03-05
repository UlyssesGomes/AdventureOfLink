using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureBarAgent : Agent
{
    private Furniture furniture;            // object to be controlled by this agent
    private float currentTime;              // current time in seconds
    private float MAX_TIME = 5f;            // max time in seconds

    public FurnitureBarAgent(Furniture furniture)
    {
        this.furniture = furniture;
    }

    public override void start()
    {
        currentTime = 0f;
        furniture.showBuildingBar(true);
    }

    public override void restart()
    {
        currentTime = 0f;
    }

    public override int agentType()
    {
        return (int) AgentsEnum.CHEST_BAR_AGENT;
    }

    public override void update()
    {
        if(currentTime < MAX_TIME)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            furniture.showBuildingBar(false);
            stop();
        }
    }
}
