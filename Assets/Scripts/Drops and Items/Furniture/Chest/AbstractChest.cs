using UnityEngine;

public abstract class AbstractChest : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;              // chest sprite renderer to be shown in scenery

    private void Awake()
    {
        sprite = GetComponentInParent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite.sprite = getChestData().sprite;
    }

    /// <summary>
    /// Open and close sprite chest
    /// </summary>
    /// <param name="open">flag to control chest opening</param>
    public void openChest(bool open)
    {
        if (open)
            sprite.sprite = getChestData().openedChesterSprite;
        else
            sprite.sprite = getChestData().sprite;
    }

    /// <summary>
    /// Chest start implemented in child to be called in this Start().
    /// </summary>
    public abstract void chestStart();

    /// <summary>
    /// Return chest instance for this object.
    /// </summary>
    /// <returns></returns>
    public abstract ChestItem getChestData();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            openChest(true);
        else
            Debug.Log("Outro objeto detectado.");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            openChest(false);
    }
}
