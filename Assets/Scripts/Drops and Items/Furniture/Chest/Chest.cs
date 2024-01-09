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

    private bool isAngleMoving = false;

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
        if (distance < 4.12f)
        {
            rigid.MovePosition(rigid.position + movingObject.direction * movingObject.currentSpeed * Time.fixedDeltaTime);
        }
        else if (distance > 4.12)
        {
            //VectorUtils.angleBetweenPoints2(Vector3.zero, Vector3.one);
            float angle = VectorUtils.angleInVector3(direction);

            Vector3 maxPosition = VectorUtils.createVector3(4.12f, angle);
            rigid.MovePosition(player.transform.position + maxPosition);

            if(isActiveAndEnabled)
            {
                positionCircularMoviment();
            }

            if (movingObject.direction != Vector2.zero)
                isAngleMoving = true;
        }
        else if (distance == 4.12f || movingObject.direction == Vector2.zero)
        {
            isAngleMoving = false;
        }
    }

    private void positionCircularMoviment()
    {
        if (isAngleMoving)
        {
            if (movingObject.direction == VectorUtils.UP)
            {
                float angle = VectorUtils.angleInVector3(direction);
                angle += angle * 1 * Time.fixedDeltaTime;

                Vector3 maxPosition = VectorUtils.createVector3(4.12f, angle);
                rigid.MovePosition(player.transform.position + maxPosition);
            }
            else if (movingObject.direction == VectorUtils.UP_RIGHT)
            {

            }
            else if (movingObject.direction == VectorUtils.RIGHT)
            {

            }
            else if (movingObject.direction == VectorUtils.DOWN_RIGHT)
            {

            }
            else if (movingObject.direction == VectorUtils.DOWN)
            {

            }
            else if (movingObject.direction == VectorUtils.DOWN_LEFT)
            {

            }
            else if (movingObject.direction == VectorUtils.LEFT)
            {

            }
            else if (movingObject.direction == VectorUtils.UP_LEFT)
            {

            }
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
