using UnityEngine;

public abstract class Furniture<T> : MonoBehaviour where T : DrawableItem
{
    [SerializeField]
    protected T furnitureAsset;                 // item that this class intance will generate after collistion
    [SerializeField]
    protected SpriteRenderer sprite;            // sprite to render on screen

    // Start is called before the first frame update
    void Start()
    {
        furnitureAsset = Instantiate(furnitureAsset);
        sprite.sprite = furnitureAsset.sprite;
        furnitureAsset.itemName = "Baú alterado.";
    }

    // Update is called once per frame
    void Update()
    {
        
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
    /// Do something in child instance when in interaction.
    /// </summary>
    protected abstract void interact();

    /// <summary>
    /// Do something when player is no more in interaction.
    /// </summary>
    protected abstract void getAway();
}
