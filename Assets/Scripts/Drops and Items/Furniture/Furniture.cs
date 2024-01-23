using UnityEngine;

public class Furniture : MonoBehaviour
{
    [SerializeField]
    private GameItem gameItem;
    
    private void Start()
    {
        loadChestData();
    }

    /// <summary>
    /// Return itemId from GameItem scriptable object of this prefab.
    /// </summary>
    /// <returns>ItemId of this prefab.</returns>
    public int getItemId()
    {
        loadChestData();
        return (int)gameItem.itemId;
    }
    
    /// <summary>
    /// Check if gameItem is already loaded, and then load it case the answer its not.
    /// </summary>
    private void loadChestData()
    {
        if(gameItem == null)
        {
            Chest chest = GetComponentInChildren<Chest>();
            gameItem = chest.getChestData();
        }
    }
}
