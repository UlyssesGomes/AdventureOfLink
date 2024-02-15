using System.Collections.Generic;
using UnityEngine;

public class AgentExecutor 
{
    private List<Agent> agents;                             // list of agents to be running.
    private IDictionary<int, Agent> agentDictionary;        // dictionary to find easily the agents
    private List<Agent> removeAgentList;                    // list of agents to be removed after update

    // Start is called before the first frame update
    public AgentExecutor()
    {
        agents = new List<Agent>();
        agentDictionary = new Dictionary<int, Agent>();
        removeAgentList = new List<Agent>();
    }

    // Update is called once per frame
    public void update()
    {
        foreach(Agent agent in agents)
        {
            if(agent.isRunning)
                agent.update();
            else
            {
                removeAgentList.Add(agent);
            }
        }

        if(removeAgentList.Count > 0)
        {
            foreach(Agent agent in removeAgentList)
            {
                agents.Remove(agent);
                agentDictionary.Remove(agent.agentType());
            }

            removeAgentList.Clear();
        }
    }

    /// <summary>
    /// Add agent to removeAgentList and this agent will be removed at the end of
    /// next update() execution, to prevend agent.update() concurrently its removing.
    /// </summary>
    /// <param name="agent">Agent to be removed.</param>
    public void removeAgent(Agent agent)
    {
        removeAgentList.Add(agent);
    }

    /// <summary>
    /// If you are running excluding agent in same executor (agent that when running in the 
    /// executor, does not allow the opposite to run).
    /// </summary>
    /// <param name="type">type of the opposite agent to deativate</param>
    public void deativatingAgentByType(int type)
    {
        if(agentDictionary.ContainsKey(type))
            agentDictionary[type].isRunning = false;
    }

    /// <summary>
    /// Add agent to agents list to be runing in update and tells to him
    /// that this class is your executor.
    /// </summary>
    /// <param name="agent">Agent to runing</param>
    public void addAgent(Agent agent)
    {
        if(!agentDictionary.ContainsKey(agent.agentType()))
        {
            agent.setExecutor(this);
            agents.Add(agent);
            agentDictionary.Add(agent.agentType(), agent);
            agent.awake();
            agent.start();
        }
        else
        {
            agentDictionary[agent.agentType()].restart();
        }
    }
}
