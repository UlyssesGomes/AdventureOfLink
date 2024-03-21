using UnityEngine;

public class DroppedSceneryItem : AbstractSceneryItem<DrawableItem>
{
    [SerializeField]
    private SpriteRenderer sprite;              // sprite to show game item image in scenery

    [SerializeField]
    private float speed;                        // speed that item will move when dropped
    private float currentSpeed;
    [SerializeField]
    private float timeMove;                     // time in which the object will move
    [SerializeField]
    private Vector2 direction;                  // moviment direction

    private float timeCount;                    // count of elapsed time

    private bool isStarted;                     // flag to control start movement

    // Start is called before the first frame update
    void Start()
    {
        createAmount = 1;
        float angle = Random.Range(0f, 359f);
        direction = VectorUtils.createVector3(1, angle);
        speed -= Random.Range(0, 3);
        currentSpeed = speed;
    }

    void Update()
    {
        if(isStarted)
            timeCount += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (timeCount < timeMove)
        {
            currentSpeed = speed * ((timeMove - timeCount) / timeMove);
            transform.Translate(direction * currentSpeed * Time.deltaTime);
        }
        else
        {
            isStarted = false;
        }
    }

    /// <summary>
    /// Prepare item to be shown in scenery by receiving a drawable item
    /// and set sprite and itemAsset.
    /// </summary>
    /// <param name="gameItem">Drawable item to be place in scenery</param>
    public void setItem(DrawableItem drawableItem)
    {
        sprite.sprite = drawableItem.sprite;
        itemAsset = drawableItem;
        isStarted = true;
    }
}
