using UnityEngine;

public class MovingObject
{
    private float _baseSpeed;                       // original speed of the object, this value cant be changed during the game
    public float currentSpeed;                      // current speed of the object

    public Vector2 direction;                      // direction that player is moving

    private bool isFirtsBaseSpeedChange = true;     // allow only one change of _baseSpeed


    #region getters and setters
    public float baseSpeed
    {
        get { return _baseSpeed; }
        set {
            if (isFirtsBaseSpeedChange)
            {
                _baseSpeed = value;
                isFirtsBaseSpeedChange = false;
            }
        }
    }
    #endregion
}
