using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rigid;
    private Vector2 _direction;
    private PlayerStatesEnum _playerState;

    private float currentSpeed;
    public float walkingSpeed;
    public float runningSpeed;
    public float rollingSpeed;
    private bool isRunning;
    private bool isRolling;
    private float rollingTime;
    private const float ROLLING_TIME = 2.0f;

    #region getters and setters
    public Vector2 direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public PlayerStatesEnum playerState
    {
        get { return _playerState; }
        set { _playerState = value; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        isRunning = isRolling = false;
    }

    // Update is called once per frame
    void Update()
    {
        OnInput();
        OnRun();
        OnRoll();
    }

    private void FixedUpdate()
    {
        OnMove();
        SetState();
    }

    #region Moviment
    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void OnMove()
    {
        rigid.MovePosition(rigid.position + _direction * currentSpeed * Time.fixedDeltaTime);
    }

    void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }
    }

    void OnRoll()
    {
        if (isRolling)
        {
            rollingTime += Time.fixedDeltaTime;

            if (rollingTime >= ROLLING_TIME)
            {
                isRolling = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            isRolling = true;
            rollingTime = 0;
        }
    }

    void SetState()
    {
        if (direction.sqrMagnitude > 0 && isRolling)
        {
            _playerState = PlayerStatesEnum.ROLLING;
            currentSpeed = rollingSpeed;
        }
        else if (direction.sqrMagnitude > 0 && isRunning)
        {
            _playerState = PlayerStatesEnum.RUNNING;
            currentSpeed = runningSpeed;
        }
        else if(direction.sqrMagnitude > 0)
        {
            _playerState = PlayerStatesEnum.WALKING;
            currentSpeed = walkingSpeed;
        }
        else
        {
            _playerState = PlayerStatesEnum.IDDLE;
        }
    }
    #endregion
}
