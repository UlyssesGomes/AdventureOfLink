using UnityEngine;

public class InputManager<T>: Input where T : struct
{
    public static T allowedControllingAgent;        // agent allowed to use input
    public T currentAgent;                          // current agent using input

    public bool isLocked;                           // only allowed agent can take input control

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
}
