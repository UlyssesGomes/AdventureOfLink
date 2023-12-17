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
        //enablePlace(true);
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
        float distance = Vector3.Distance(player.transform.position, rigid.position + movingObject.direction * movingObject.currentSpeed * Time.fixedDeltaTime);
        if (distance < 4.12)
        {
            rigid.MovePosition(rigid.position + movingObject.direction * movingObject.currentSpeed * Time.fixedDeltaTime);

        } 
        else if(distance > 4.12)
        {
            float distance2DX = (distance - 4.12f) / (movingObject.direction.x * Time.fixedDeltaTime);
            float distance2DY = (distance - 4.12f) / (movingObject.direction.y * Time.fixedDeltaTime);
            movingObject.direction.Set(distance2DX, distance2DY);
            rigid.MovePosition(rigid.position + movingObject.direction);
        }
        Debug.Log("Distance p x c: " + Vector3.Distance(player.transform.position, transform.position));
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
