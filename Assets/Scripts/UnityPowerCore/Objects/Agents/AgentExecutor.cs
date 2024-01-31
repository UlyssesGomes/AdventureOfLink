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
                agentDictionary.Remove(agent.agentId());
            }

            removeAgentList.Clear();
        }
    }

    /// <summary>
    /// Add agent o removeAgentList and this agent will be removed at the end of
    /// next update() execution, to prevend agent.update() concurrently its removing.
    /// </summary>
    /// <param name="agent">Agent to be removed.</param>
    public void removeAgent(Agent agent)
    {
        removeAgentList.Add(agent);
    }

    /// <summary>
    /// Add agent to agents list to be runing in update and tells to him
    /// that this class is your executor.
    /// </summary>
    /// <param name="agent">Agent to runing</param>
    public void addAgent(Agent agent)
    {
        if(!agentDictionary.ContainsKey(agent.agentId()))
        {
            agent.setExecutor(this);
            agents.Add(agent);
            agentDictionary.Add(agent.agentId(), agent);
            agent.awake();
            agent.start();
        }
        else
        {
            Debug.Log("Não adicionou o agente.");
        }
    }
}
