using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBarAgent : Agent
{
    private Chest chest;                    // object to be controlled by this agent
    private float currentTime;              // current time in seconds
    private float MAX_TIME = 5f;            // max time in seconds

    public ChestBarAgent(Chest chest)
    {
        this.chest = chest;
        id = getNextId();
    }

    public override void start()
    {
        currentTime = 0f;
        chest.showBuildingBar(true);
    }

    public override int agentId()
    {
        return id;
    }

    public override void update()
    {
        if(currentTime < MAX_TIME)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            Debug.Log("Finalizou o agente.");
            chest.showBuildingBar(false);
            stop();
        }
    }
}
