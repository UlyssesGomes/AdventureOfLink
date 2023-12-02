using UnityEngine;

public class InputManager<T>: Input where T : struct
{
    public static T allowedControllingAgent;

    public static float GetAxis(string axisName, T currentAgent)
    {
        float magniture = Input.GetAxis(axisName);
        if (magniture != 0.0f && currentAgent.Equals(allowedControllingAgent))
        {
            return magniture;
        }

        return 0.0f;
    }

    public static float GetAxisRaw(string axisName, T currentAgent)
    {
        float magniture = Input.GetAxisRaw(axisName);
        if (magniture != 0.0f && currentAgent.Equals(allowedControllingAgent))
        {
            return magniture;
        }

        return 0.0f;
    }

    public static bool GetButton(string buttonName, T currentAgent)
    {

        if(Input.GetButton(buttonName) && currentAgent.Equals(allowedControllingAgent))
        {
            return true;
        }

        return false;
    }

    public static bool GetButtonDown(string buttonName, T currentAgent)
    {
        if (Input.GetButtonDown(buttonName) && currentAgent.Equals(allowedControllingAgent))
        {
            return true;
        }

        return false;
    }

    public static bool GetButtonUp(string buttonName, T currentAgent)
    {
        if (Input.GetButtonUp(buttonName) && currentAgent.Equals(allowedControllingAgent))
        {
            return true;
        }

        return false;
    }

}
