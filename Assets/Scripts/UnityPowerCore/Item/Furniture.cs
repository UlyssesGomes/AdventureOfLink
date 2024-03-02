using UnityEngine;
using UnityEngine.UI;

public abstract class Furniture : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer sprite;            // sprite to render on screen
    public ItemIdEnum itemId;                   // item id that represents this item

    protected float buildingAmount;             // how much percent this furniture is complete

    public GameObject buildingBar;              // building progress bar to show to player how much left work is needed
    public Image filledBar;                     // amoung of work done in progress bar

    private void Awake()
    {
        sprite = GetComponentInParent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite.sprite = getFurnitureData().sprite;
        furnitureStart();
    }

    // Update is called once per frame
    void Update()
    {
        furnitureUpdate();
    }

    /// <summary>
    /// Enable or disable building bar by param isShow.
    /// </summary>
    /// <param name="isShow">boolean to show or hide building bar.</param>
    public void showBuildingBar(bool isShow)
    {
        buildingBar.SetActive(isShow);
    }

    /// <summary>
    /// Chest start implemented in child to be called in this Start().
    /// </summary>
    public abstract void furnitureStart();

    /// <summary>
    /// Chest start implemented in child to be called in this Start().
    /// </summary>
    public abstract void furnitureUpdate();

    /// <summary>
    /// Return chest instance for this object.
    /// </summary>
    /// <returns></returns>
    public abstract DrawableItem getFurnitureData();

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
        else if(collision.CompareTag("BuilderHammer"))
        {
            if(buildingAmount < 100f)
                buildingImpact(30f);
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
    /// Do something in child instance when in interaction.
    /// </summary>
    protected abstract void interact();

    /// <summary>
    /// Do something when player is no more in interaction.
    /// </summary>
    protected abstract void getAway();

    /// <summary>
    /// Update building progress of this furniture to add amount of progress.
    /// </summary>
    /// <param name="value">amount of progress to advance</param>
    protected abstract void buildingImpact(float value);
}
