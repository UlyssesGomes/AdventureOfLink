using UnityEngine;

public static class VectorUtils 
{
    public static float angleBetweenPoints2(Vector2 origin, Vector2 target)
    {
        float theta = 0;
        return theta;
    }

    public static float angleBetweenPoints3(Vector3 origin, Vector3 target)
    {
        Vector3 vector = target - origin;

        float theta = 0;

        theta = Mathf.Atan2(vector.y, vector.x);

        return theta * Mathf.Rad2Deg;
    }

    public static float radiansToDegrees(float radians)
    {
        return Mathf.Rad2Deg * radians;
    }

    public static float degreesToRadians(float degrees)
    {
        return Mathf.Deg2Rad * degrees;
    }
}
