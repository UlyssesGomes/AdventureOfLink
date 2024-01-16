using UnityEngine;
using System.Collections.Generic;

public class FurniturePlacement : StateMachineController<FurniturePlacement>
{
    //[SerializeField]
    protected DrawableItem furnitureAsset;      // scritable object to get its properties, DONT CHANGE ITS VALUES
    [SerializeField]
    protected SpriteRenderer sprite;            // sprite to render on screen

    //[SerializeField]
    private Player player;                      // player who create this box
    public PlayerGizmosGuide gizmosGuide;       // Gizmos to show chest position placement

    public Rigidbody2D rigid;                   // collision component
    public PolygonCollider2D collider;          // collider of object to be placed
    public MovingObject movingObject;           // object speed controller

    [HideInInspector]
    public bool canPlace;                       // enable when chest must be placed in the ground (its not in collision)
    [SerializeField]
    private bool isPlaced;                      // flag to control if chest is placed or not on the ground.

    [SerializeField]
    private Color colorTransparent;             // color used to make the chest transparent when placing is blocked
    private Color colorDefault;                 // default color to be used on the chest when placing is allowed

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

    protected override void stateMachineAwake()
    {
        movingObject = new MovingObject();
    }

    protected override void stateMachineStart()
    {
        Debug.Log("FurniturePlacement -> start()");
        setPlacedChest(false);

        movingObject.baseSpeed = movingObject.currentSpeed = 3;

        distance = Vector3.Distance(player.transform.position, rigid.position);
        if(distance >= maxDistance)
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

    protected override void stateMachineUpdate()
    { }

    protected override void stateMachineFixedUpdate()
    {
        onMove();
    }

    protected override UnitState<FurniturePlacement> getFirstState()
    {
        return getNextState((int)ChestStateEnum.FURNITURE_POSITIONING);
    }

    protected override FurniturePlacement getStateMachineObject()
    {
        return this;
    }

    protected override void instantiateAllUnitStates()
    {
        addUnitStateInstance(new FurniturePositioning());
        addUnitStateInstance(new FurniturePlaced());
    }

    /// <summary>
    /// Method to call interact() when player in collision with this object's interaction area.
    /// </summary>
    /// <param name="collision"></param>
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        interact();
    //    }
    //}

    /// <summary>
    /// Method to call getAway() when player is no more in collision with this object's interaction area.
    /// </summary>
    /// <param name="collision"></param>
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        getAway();
    //    }
    //}
    
    /// <summary>
    /// Close chest to interaction.
    /// </summary>
    //protected void getAway()
    //{
    //    sprite.sprite = furnitureAsset.sprite;
    //}

    /// <summary>
    /// Open chest to interact.
    /// </summary>
    //protected void interact()
    //{
    //    sprite.sprite = furnitureAsset.openedChesterSprite;
    //}

    /// <summary>
    /// Chest move method to place chest on the ground.
    /// </summary>
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
                else if (angle > 270f && angle < 360)
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
        Debug.Log("FurniturePlacement -> enablePlace()");
        if (isEnable)
            sprite.color = colorDefault;
        else if(!isEnable && !isPlaced)
            sprite.color = colorTransparent;
        canPlace = isEnable;
    }

    /// <summary>
    /// Change the body type of the chest depending on whether it is fixed to the 
    /// ground or not. When fixed, change from dynamic to static.
    /// </summary>
    /// <param name="isPlaced"></param>
    public void setPlacedChest(bool isPlaced)
    {
        Debug.Log("FurniturePlacement -> setPlacedChest()");
        if (isPlaced)
        {
            rigid.bodyType = RigidbodyType2D.Static;
            player.setLockPlayer(false);
        }
        else
        {
            rigid.bodyType = RigidbodyType2D.Dynamic;
            player.setLockPlayer(true);
        }

        this.isPlaced = isPlaced;
    }

    public void setDataPlacement(Player player, DrawableItem furnitureAsset)
    {
        this.player = player;
        this.furnitureAsset = furnitureAsset;

        gizmosGuide = player.gizmosGuide;

        sprite.sprite = furnitureAsset.sprite;
        updateSpritePhysicsShape();

        colorDefault = sprite.color;
    }

    public void enablePlacement(bool isEnable)
    {
        gameObject.SetActive(isEnable);
    }

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
}
