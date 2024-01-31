using System;

public abstract class Agent
{
    private static int nextId;                  // stores the next valid id to be used in child Agent
    protected int id;                           // id to be used in each child agent
    private AgentExecutor executor;             // Executor of this agent
    public bool isRunning;                      // tell if this agent is ative

    public void awake()
    {
        isRunning = true;
    }

    /// <summary>
    /// Start agent attributes
    /// </summary>
    public abstract void start();

    /// <summary>
    /// Update object atributes. 
    /// </summary>
    public abstract void update();

    /// <summary>
    /// Return the id of this agent implementation.
    /// </summary>
    /// <returns>Id of this agent implementation.</returns>
    public abstract int agentId();

    /// <summary>
    /// Set AgentExecutor instance
    /// </summary>
    /// <param name="executor">AgentExecutor instance</param>
    public void setExecutor(AgentExecutor executor)
    {
        this.executor = executor;
    }

    /// <summary>
    /// Tells to executor that this agent its not running anymore
    /// and can be removed from execution list.
    /// </summary>
    protected void stop()
    {
        isRunning = false;
    }

    protected int getNextId()
    {
        return nextId++;
    }
}
