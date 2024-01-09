using UnityEngine;

public static class VectorUtils 
{
    private static Vector2 _up = new Vector2(0, 1);
    private static Vector2 _up_right = new Vector2(1, 1);
    private static Vector2 _right = new Vector2(1, 0);
    private static Vector2 _down_right = new Vector2(1, -1);
    private static Vector2 _down = new Vector2(0, -1);
    private static Vector2 _down_left = new Vector2(-1, -1);
    private static Vector2 _left = new Vector2(-1, 0);
    private static Vector2 _up_left = new Vector2(-1, 1);

    public static Vector2 UP { get => _up; }
    public static Vector2 UP_RIGHT { get => _up_right; }
    public static Vector2 RIGHT { get => _right; }
    public static Vector2 DOWN_RIGHT { get => _down_right; }
    public static Vector2 DOWN { get => _down; }
    public static Vector2 DOWN_LEFT { get => _down_left; }
    public static Vector2 LEFT { get => _left; }
    public static Vector2 UP_LEFT { get => _up_left; }

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
    /// Caculates a vector as Vector3 through magnitude and angle.
    /// </summary>
    /// <param name="magnitude">Vector magnitude</param>
    /// <param name="angle">angle in degrees</param>
    /// <returns>Vector3 with x and y components</returns>
    public static Vector3 createVector3(float magnitude, float angle)
    {
        Vector3 vector = Vector3.zero;

        float angleRadians = angle * Mathf.Deg2Rad;

        vector.x = Mathf.Cos(angleRadians) * magnitude;
        vector.y = Mathf.Sin(angleRadians) * magnitude;

        return vector;
    }

    /// <summary>
    /// Calculates magnitude through x and y components.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>magnitudes</returns>
    public static float calculateMagnitude(float x, float y)
    {
        float magnitude = Mathf.Sqrt(((Mathf.Pow(x, 2)) + (Mathf.Pow(y, 2))));

        return magnitude;
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
