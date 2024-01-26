using UnityEngine;
using System.Collections.Generic;

public class FurniturePlacement : MonoBehaviour
{
    protected DrawableItem furnitureAsset;      // scritable object to get its properties, DONT CHANGE ITS VALUES
    [SerializeField]
    protected SpriteRenderer sprite;            // sprite to render on screen

    private Player player;                      // player who create this box
    public PlayerGizmosGuide gizmosGuide;       // Gizmos to show chest position placement

    public Rigidbody2D rigid;                   // collision component
    public PolygonCollider2D collider;          // collider of object to be placed
    public MovingObject movingObject;           // object speed controller

    [HideInInspector]
    public bool canPlace;                       // enable when chest must be placed in the ground (its not in collision)

    [SerializeField]
    private Color chestColorBlocked;            // color used in the chest to indicate that this chest cant be placed in its current possition
    [SerializeField]
    private Color chestColorDefault;            // default color to be used on the chest when placing is allowed

    private bool isAngleMoving = false;         // flag to check if player is pressing direction after reach max distance

    [SerializeField]
    private float angle;                        // current angle between player and chest
    [SerializeField]
    private float distance;                     // Distance from player
    [SerializeField]
    private float maxDistance;                  // Max distance allowed from the player
    [SerializeField]
    private float distanceTolerance = 0.01f;    // tolerance to round value
    [SerializeField]
    private float angleSpeed;                   // angle speed which chest move while in limit distance

    protected InputManager<InputAgentsEnum> input = new InputManager<InputAgentsEnum>(InputAgentsEnum.CHEST);

    private void Awake()
    {
        movingObject = new MovingObject();
    }

    private void Start()
    {
        movingObject.baseSpeed = movingObject.currentSpeed = 3;

        distance = Vector3.Distance(player.transform.position, rigid.position);
        if (distance >= maxDistance)
        {
            canPlace = false;
            gizmosGuide.changeToRed();
        }
        else
        {
            canPlace = true;
            gizmosGuide.changeToWhite();
        }
    }

    private void Update()
    {
        getInputMovementNormalized();

        if (input.GetKey(KeyCode.F) && canPlace)
        {
            disableFurniturePlacement();
            GameObject gameObject = player.assetfactory.instanceGameObjectByItemId((int)furnitureAsset.itemId);
            gameObject.transform.position = rigid.position;
        }
        else if(input.GetKey(KeyCode.Escape))
        {
            disableFurniturePlacement();

        }
        else if (input.GetKey(KeyCode.F) && !canPlace)
        {
            // TODO - emit "tandan" sound, because chest cant be placed.
            Debug.LogWarning("TANDAN - the chest cant be placed here.");
        }
    }

    private void FixedUpdate()
    {
        onMove();
    }

    /// <summary>
    /// Unlock player.
    /// </summary>
    private void disableFurniturePlacement()
    {
        InputManager<InputAgentsEnum>.isLocked = false;
        enablePlacement(false);
    }

    private void onMove()
    {
        distance = Vector3.Distance(player.transform.position, rigid.position + movingObject.direction * movingObject.currentSpeed * Time.fixedDeltaTime);
        if (movingObject.direction != Vector2.zero || distance > maxDistance)
        {
            // direction = destination - source
            Vector2 direction = transform.position - player.transform.position;
            if (distance <= maxDistance)
            {
                rigid.MovePosition(rigid.position + movingObject.direction * movingObject.currentSpeed * Time.fixedDeltaTime);

                gizmosGuide.changeToWhite();
            }
            else if (distance > maxDistance)
            {
                float angle = VectorUtils.angleInVector3(direction);

                Vector3 maxPosition = VectorUtils.createVector3(maxDistance, angle);
                rigid.MovePosition(player.transform.position + maxPosition);

                if(isActiveAndEnabled)
                {
                    positionCircularMoviment(direction);
                }

                if (movingObject.direction != Vector2.zero)
                    isAngleMoving = true;

            }
            else if (distance == maxDistance || movingObject.direction == Vector2.zero)
            {
                isAngleMoving = false;
            }

            if(distance + distanceTolerance >= maxDistance)
                gizmosGuide.changeToRed();
        }
    }

    /// <summary>
    /// Calculate chest position and if its over distance limits, the chest starts circulating 
    /// around the player, respecting the maximum distance.
    /// </summary>
    private void positionCircularMoviment(Vector2 direction)
    {
        if (isAngleMoving)
        {
            angle = VectorUtils.angleInVector3(direction);

            if (movingObject.direction == VectorUtils.UP.normalized && angle != 90f)
            {
                if (angle >= 0f && angle < 90f)
                {
                    angle += angleSpeed * Time.fixedDeltaTime;

                    if (angle >= 90f)
                        angle = 90f;
                }
                else if(angle > 90f && angle <= 180f)
                {
                    angle -= angleSpeed * Time.fixedDeltaTime;

                    if (angle <= 90f)
                        angle = 90f;
                }
            }
            else if (movingObject.direction == VectorUtils.UP_RIGHT.normalized && angle != 45f)
            {
                if ((angle < 45f && angle >= 0f) || (angle > 315f && angle < 360f))
                {
                    angle += angleSpeed * Time.fixedDeltaTime;

                    if (angle >= 45f && angle  <= 90f)
                        angle = 45f;
                    else if (angle >= 360f)
                        angle = 0f;
                }
                else if (angle > 45f && angle <= 135f)
                {
                    angle -= angleSpeed * Time.fixedDeltaTime;

                    if (angle <= 45f)
                        angle = 45f;
                }
            }
            else if (movingObject.direction == VectorUtils.RIGHT.normalized && angle != 0f)
            {
                if (angle > 0 && angle <= 90f)
                {
                    angle -= angleSpeed * Time.fixedDeltaTime;

                    if (angle <= 0f)
                        angle = 0f;
                }
                else if (angle >= 270f && angle < 360)
                {
                    angle += angleSpeed * Time.fixedDeltaTime;

                    if (angle >= 360f)
                        angle = 0f;
                }
            }
            else if (movingObject.direction == VectorUtils.DOWN_RIGHT.normalized && angle != 315f)
            {
                if ((angle > 315f && angle <= 360f) || (angle < 45f && angle >= -1f))
                {
                    angle -= angleSpeed * Time.fixedDeltaTime;

                    if (angle <= 315f && angle > 270f)
                    {
                        angle = 315f;
                    }
                    else if (angle <= 0.0f)
                    {
                        angle = 360f - Mathf.Abs(angle);
                    }
                }
                else if (angle >= 225f && angle < 315f)
                {
                    angle += angleSpeed * Time.fixedDeltaTime;

                    if (angle >= 315f)
                        angle = 315f;
                }
            }
            else if (movingObject.direction == VectorUtils.DOWN.normalized && angle != 270f)
            {
                if (angle == 0f)
                    angle = 360f;

                if (angle > 270f && angle <= 360f)
                {
                    angle -= angleSpeed * Time.fixedDeltaTime;

                    if (angle <= 270f && angle >= 180f)
                    {
                        angle = 270f;
                    }
                }
                else if (angle >= 180f && angle < 270f)
                {
                    angle += angleSpeed * Time.fixedDeltaTime;

                    if (angle >= 270f)
                        angle = 270f;
                }
            }
            else if (movingObject.direction == VectorUtils.DOWN_LEFT.normalized && angle != 225f)
            {
                if (angle >= 135f && angle < 225f)
                {
                    angle += angleSpeed * Time.fixedDeltaTime;

                    if (angle >= 225f)
                    {
                        angle = 225f;
                    }
                }
                else if (angle > 225f && angle <= 315f)
                {
                    angle -= angleSpeed * Time.fixedDeltaTime;

                    if (angle <= 225f)
                        angle = 225f;
                }
            }
            else if (movingObject.direction == VectorUtils.LEFT.normalized)
            {
                if (angle >= 90f && angle < 180f)
                {
                    angle += angleSpeed * Time.fixedDeltaTime;

                    if (angle >= 180f)
                    {
                        angle = 180f;
                    }
                }
                else if (angle > 180f && angle <= 270f)
                {
                    angle -= angleSpeed * Time.fixedDeltaTime;

                    if (angle <= 180f)
                        angle = 180f;
                }
            }
            else if (movingObject.direction == VectorUtils.UP_LEFT.normalized && angle != 135f)
            {
                if (angle >= 45f && angle < 135f)
                {
                    angle += angleSpeed * Time.fixedDeltaTime;

                    if (angle >= 135f)
                    {
                        angle = 135f;
                    }
                }
                else if (angle > 135f && angle <= 225f)
                {
                    angle -= angleSpeed * Time.fixedDeltaTime;

                    if (angle <= 135f)
                        angle = 135f;
                }
            }

            Vector3 maxPosition = VectorUtils.createVector3(maxDistance, angle);
            rigid.MovePosition(player.transform.position + maxPosition);
        }
    }

    /// <summary>
    /// Enable chest to set in a place or not.
    /// </summary>
    /// <param name="isEnable">Enable param</param>
    public void enablePlace(bool isEnable)
    {
        if (isEnable)
            sprite.color = chestColorDefault;
        else
            sprite.color = chestColorBlocked;
        canPlace = isEnable;
    }

    /// <summary>
    /// Update guizmo UI with a player and new furniture to be shown.
    /// </summary>
    /// <param name="player">player that will shown the guizmo ui</param>
    /// <param name="furnitureAsset">furniture to get sprite of the object to be placed</param>
    public void setDataPlacement(Player player, DrawableItem furnitureAsset)
    {
        this.player = player;
        this.furnitureAsset = furnitureAsset;

        gizmosGuide = player.gizmosGuide;

        sprite.sprite = furnitureAsset.sprite;
        sprite.color = chestColorDefault;
        updateSpritePhysicsShape();


        Vector3 v = new Vector3(1.5f, 0f);
        transform.position = player.transform.position + v;
    }

    /// <summary>
    /// Enable guizmo UI on screen.
    /// </summary>
    /// <param name="isEnable"></param>
    public void enablePlacement(bool isEnable)
    {
        gameObject.SetActive(isEnable);
        if(gizmosGuide)
            gizmosGuide.setEnable(isEnable);

        if(player)
        {
            if (isEnable)
                player.setLockPlayer(true);
            else
                player.setLockPlayer(false);
        }

        if(isEnable)
            InputManager<InputAgentsEnum>.allowedControllingAgent = InputAgentsEnum.CHEST;
        InputManager<InputAgentsEnum>.isLocked = isEnable;

    }

    /// <summary>
    /// Update PolygonCollider2D with custom shape of the sprite in use.
    /// </summary>
    private void updateSpritePhysicsShape()
    {
        collider.pathCount = sprite.sprite.GetPhysicsShapeCount();

        List<Vector2> path = new List<Vector2>();

        for (int i = 0; i < collider.pathCount; i++)
        {
            path.Clear();
            sprite.sprite.GetPhysicsShape(i, path);
            collider.SetPath(i, path.ToArray());
        }
    }

    private void getInputMovementNormalized()
    {
        movingObject.direction = new Vector2(input.GetAxisRaw("Horizontal"), input.GetAxisRaw("Vertical"));
        if (movingObject.direction.x != 0.0f && movingObject.direction.y != 0.0f)
            movingObject.direction.Normalize();
    }
}
