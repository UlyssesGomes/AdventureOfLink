using UnityEngine;

public class HouseDoor : MonoBehaviour, Door
{
    [SerializeField]
    private SpriteRenderer spriteComponentOpened;
    [SerializeField]
    private GameObject openedDoor;
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
            spriteComponentOpened.enabled = false;
            openedDoor.SetActive(true);
            collider.isTrigger = true;
        }
        else
        {
            spriteComponentOpened.enabled = true;
            openedDoor.SetActive(false);
            collider.isTrigger = false;
        }
    }

    public void openDoor(Player player)
    {
        openDoor(!isOpened);
    }
}
