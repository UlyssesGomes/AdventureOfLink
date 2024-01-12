using UnityEngine;

public class Chest : StateMachineController<Chest> //Furniture<ChestItem>
{
    [SerializeField]
    protected ChestItem furnitureAsset;         // item that this class intance will generate after collistion
    [SerializeField]
    protected SpriteRenderer sprite;            // sprite to render on screen

    public Rigidbody2D rigid;                   // collision component
    public MovingObject movingObject;           // object speed controller

    [HideInInspector]
    public bool canPlace;                       // enable when chest must be placed in the ground (its not in collision)

    [SerializeField]
    private Color colorTransparent;             // color used to make the chest transparent when placing is blocked
    private Color colorDefault;                 // default color to be used on the chest when placing is allowed

    [SerializeField]
    private Player player;                      // player who create this box

    // TODO - delete after chest adjust position system in placing state were implemented.
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private float distance;
    [SerializeField]
    private Vector2 dir;

    private bool isAngleMoving = false;         // flag to check if player is pressing direction after reach max distance

    [SerializeField]
    private float maxDistance;                  // Max distance allowed from the player
    [SerializeField]
    private float angleSpeed;                   // angle speed which chest move while in limit distance

    protected override void stateMachineAwake()
    {
        movingObject = new MovingObject();
    }

    protected override void stateMachineStart()
    {
        furnitureAsset = Instantiate(furnitureAsset);
        sprite.sprite = furnitureAsset.sprite;
        furnitureAsset.itemName = "Baú alterado.";

        movingObject.baseSpeed = movingObject.currentSpeed = 3;

        colorDefault = sprite.color;
    }

    protected override void stateMachineUpdate()
    { }

    protected override void stateMachineFixedUpdate()
    {
        onMove();
    }

    protected override UnitState<Chest> getFirstState()
    {
        return getNextState((int)ChestStateEnum.CHEST_POSITIONING);
    }

    protected override Chest getStateMachineObject()
    {
        return this;
    }

    protected override void instantiateAllUnitStates()
    {
        addUnitStateInstance(new ChestPositioning());
    }

    /// <summary>
    /// Method to call interact() when player in collision with this object's interaction area.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interact();
        }
    }

    /// <summary>
    /// Method to call getAway() when player is no more in collision with this object's interaction area.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            getAway();
        }
    }
    
    /// <summary>
    /// Close chest to interaction.
    /// </summary>
    protected void getAway()
    {
        sprite.sprite = furnitureAsset.sprite;
    }

    /// <summary>
    /// Open chest to interact.
    /// </summary>
    protected void interact()
    {
        sprite.sprite = furnitureAsset.openedChesterSprite;
    }

    /// <summary>
    /// Chest move method to place chest on the ground.
    /// </summary>
    private void onMove()
    {
        distance = Vector3.Distance(player.transform.position, rigid.position + movingObject.direction * movingObject.currentSpeed * Time.fixedDeltaTime);
        
        // direction = destination - source
        direction = transform.position - player.transform.position;
        // TODO - a partir do direction, calcular o angulo, em seguida encontrar o X e Y sabendo o angulo e a distância (4.12)
        // TODO - implementar o movimento em círculo, se tiver a distância de 4.12 a um angulo de 45º, e o player tiver apertando
        //        para cima, então manter a distância e continuar aumentando o angulo.
        if (distance < maxDistance)
        {
            rigid.MovePosition(rigid.position + movingObject.direction * movingObject.currentSpeed * Time.fixedDeltaTime);
        }
        else if (distance > maxDistance)
        {
            //VectorUtils.angleBetweenPoints2(Vector3.zero, Vector3.one);
            float angle = VectorUtils.angleInVector3(direction);

            Vector3 maxPosition = VectorUtils.createVector3(maxDistance, angle);
            rigid.MovePosition(player.transform.position + maxPosition);

            if(isActiveAndEnabled)
            {
                positionCircularMoviment();
            }

            if (movingObject.direction != Vector2.zero)
                isAngleMoving = true;
        }
        else if (distance == maxDistance || movingObject.direction == Vector2.zero)
        {
            isAngleMoving = false;
        }
    }

    /// <summary>
    /// Calculate chest position and if its over distance limits, the chest starts circulating 
    /// around the player, respecting the maximum distance.
    /// </summary>
    private void positionCircularMoviment()
    {
        if (isAngleMoving)
        {
            float angle = VectorUtils.angleInVector3(direction);

            dir = movingObject.direction;
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
            sprite.color = colorDefault;
        else
            sprite.color = colorTransparent;
        canPlace = isEnable;
    }
}
