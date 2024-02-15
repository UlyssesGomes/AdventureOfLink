using System;

public abstract class Agent
{
    protected AgentExecutor executor;           // Executor of this agent
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
    /// This method will be called when agent is already in
    /// executor's list and must be tried to add again. Its not
    /// possible add to executor's list 2 agents of the same
    /// type.
    /// </summary>
    public abstract void restart();

    /// <summary>
    /// Update object atributes. 
    /// </summary>
    public abstract void update();

    /// <summary>
    /// Return the id of this agent implementation.
    /// </summary>
    /// <returns>Id of this agent implementation.</returns>
    public abstract int agentType();

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
}
