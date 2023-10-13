using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    private float _baseSpeed;                       // original speed of the object, this value cant be changed during the game
    public float currentSpeed;                      // current speed of the object

    private Vector2 _direction;                     // direction that player is moving

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

    public Vector2 direction
    {
        get { return _direction; }
        set { _direction = value; }
    }
    #endregion
}
