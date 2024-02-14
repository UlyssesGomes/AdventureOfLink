using UnityEngine;

public class HouseBuildBarAgent : Agent
{
    private GameObject buildBar;            // blue bar object to be controlled by this agent
    private float currentTime;              // current time in seconds
    private float MAX_TIME = 5f;            // max time in seconds

    public HouseBuildBarAgent(GameObject buildBar)
    {
        this.buildBar = buildBar;
    }

    public override int agentType()
    {
        return (int)AgentsEnum.CHEST_BAR_AGENT;
    }

    public override void restart()
    {
        currentTime = 0f;
    }

    public override void start()
    {
        currentTime = 0f;
        buildBar.SetActive(true);
    }

    public override void update()
    {
        if (currentTime < MAX_TIME)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            buildBar.SetActive(false);
            stop();
        }
    }
}
