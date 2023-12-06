using UnityEngine;

public class InputManager<T>: Input where T : struct
{
    public static T allowedControllingAgent;        // agent allowed to use input
    public T currentAgent;                          // current agent using input

    public bool isLocked;                           // only allowed agent can take input control

    public InputManager(T agent)
    {
        currentAgent = agent;
    }

    public float GetAxis(string axisName)
    {
        float magnitude = Input.GetAxis(axisName);
        if ((isLocked && currentAgent.Equals(allowedControllingAgent)) || !isLocked)
            return magnitude;

        return 0.0f;
    }

    public float GetAxisRaw(string axisName)
    {
        float magniture = Input.GetAxisRaw(axisName);
        if ((isLocked && currentAgent.Equals(allowedControllingAgent)) || !isLocked)
            return magniture;

        return 0.0f;
    }

    public bool GetButton(string buttonName)
    {

        if (Input.GetButton(buttonName) && (isLocked && currentAgent.Equals(allowedControllingAgent)) || !isLocked)
            return true;

        return false;
    }

    public bool GetButtonDown(string buttonName)
    {
        if (Input.GetButtonDown(buttonName) && (isLocked && currentAgent.Equals(allowedControllingAgent)) || !isLocked)
            return true;

        return false;
    }

    public bool GetButtonUp(string buttonName)
    {
        if (Input.GetButtonUp(buttonName) && (isLocked && currentAgent.Equals(allowedControllingAgent)) || !isLocked)
            return true;

        return false;
    }

    public bool GetKey(string name)
    {
        if (Input.GetKey(name) && (isLocked && currentAgent.Equals(allowedControllingAgent)) || !isLocked)
            return true;

        return false;
    }

    public bool GetKey(KeyCode key)
    {
        if (Input.GetKey(key) && (isLocked && currentAgent.Equals(allowedControllingAgent)) || !isLocked)
            return true;

        return false;
    }

    public bool GetKeyDown(string name)
    {
        if (Input.GetKeyDown(name) && (isLocked && currentAgent.Equals(allowedControllingAgent)) || !isLocked)
            return true;

        return false;
    }

    public bool GetKeyDown(KeyCode key)
    {
        if (Input.GetKeyDown(key) && (isLocked && currentAgent.Equals(allowedControllingAgent)) || !isLocked)
            return true;

        return false;
    }

    public bool GetKeyUp(string name)
    {
        if (Input.GetKeyUp(name) && (isLocked && currentAgent.Equals(allowedControllingAgent)) || !isLocked)
            return true;

        return false;
    }

    public bool GetKeyUp(KeyCode key)
    {
        if (Input.GetKeyUp(key) && (isLocked && currentAgent.Equals(allowedControllingAgent)) || !isLocked)
            return true;

        return false;
    }

    public bool GetMouseButton(int button)
    {
        if (Input.GetMouseButton(button) && (isLocked && currentAgent.Equals(allowedControllingAgent)) || !isLocked)
            return true;

        return false;
    }

    public bool GetMouseButtonDown(int button)
    {
        if (Input.GetMouseButtonDown(button) && (isLocked && currentAgent.Equals(allowedControllingAgent)) || !isLocked)
            return true;

        return false;
    }

    public bool GetMouseButtonUp(int button)
    {
        if (Input.GetMouseButtonUp(button) && (isLocked && currentAgent.Equals(allowedControllingAgent)) || !isLocked)
            return true;

        return false;
    }
}
