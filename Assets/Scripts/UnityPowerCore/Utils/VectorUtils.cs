﻿using UnityEngine;

public static class VectorUtils 
{

    /// <summary>
    /// Calculate angle between two points that represents a Vector.
    /// </summary>
    /// <param name="origin">Origin point as Vector3</param>
    /// <param name="dest">Destination point as Vector3</param>
    /// <returns>Angle in degrees</returns>
    public static float angleBetweenPoints3(Vector3 origin, Vector3 dest)
    {
        Vector3 vector = dest - origin;

        float theta = Mathf.Atan2(vector.y, vector.x);
        theta *= Mathf.Rad2Deg;

        // Cartesian Q3 and Q4
        if ((vector.x < 0 && vector.y < 0) || (vector.x >= 0 && vector.y < 0))
        {
            theta += 360.0f;
        }

        return theta;
    }

    /// <summary>
    /// Calculate a angle in a Vector3 which origin is in (0, 0, 0)
    /// </summary>
    /// <param name="vector">Vector3</param>
    /// <returns>Angle in degrees</returns>
    public static float angleInVector3(Vector3 vector)
    {
        float theta = Mathf.Atan2(vector.y, vector.x);
        theta *= Mathf.Rad2Deg;

        // Cartesian Q3 and Q4
        if ((vector.x < 0 && vector.y < 0) || (vector.x >= 0 && vector.y < 0))
        {
            theta += 360.0f;
        }

        return theta;
    }

    /// <summary>
    /// Transform angle from radians to degrees
    /// </summary>
    /// <param name="radians">Value in radians</param>
    /// <returns>Values in degrees</returns>
    public static float radiansToDegrees(float radians)
    {
        return Mathf.Rad2Deg * radians;
    }

    /// <summary>
    /// Transform angle from degrees to radians
    /// </summary>
    /// <param name="degrees">Value in degrees</param>
    /// <returns>Value in radians</returns>
    public static float degreesToRadians(float degrees)
    {
        return Mathf.Deg2Rad * degrees;
    }
}