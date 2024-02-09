using UnityEngine;

public class HouseDoor : MonoBehaviour, Door
{
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Sprite openedDoorSprite;
    [SerializeField]
    private Sprite closedDoorSprite;
    [SerializeField]
    private bool isOpened;

    [SerializeField]
    private BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        openDoor(isOpened);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void openDoor(bool hasOpen)
    {
        isOpened = hasOpen;
        if (isOpened)
        {
            sprite.sprite = openedDoorSprite;
            collider.isTrigger = true;
        }
        else
        {
            sprite.sprite = closedDoorSprite;
            collider.isTrigger = false;
        }
    }

    public void openDoor(Player player)
    {
        openDoor(!isOpened);
    }
}
