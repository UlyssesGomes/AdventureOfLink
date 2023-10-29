using UnityEngine;

using System.Collections.Generic;

public class DropAssetManager : MonoBehaviour
{
    [SerializeField]
    private GameItem[] assetsCollections;                   // items availables in memory
    private Dictionary<int, GameItem> assetsDictionary;     // dictionary to find these items quickly

    private void Start()
    {
        assetsDictionary = new Dictionary<int, GameItem>();

        foreach(GameItem g in assetsCollections)
        {
            assetsDictionary.Add((int)g.itemId, g);
        }
    }

    /// <summary>
    /// Intantiate a SerializableObject by its itemId value.
    /// </summary>
    /// <param name="itemId">id of desired item</param>
    /// <returns>Desired GameItem instance</returns>
    public GameItem intanceGameItemByItemId(int itemId)
    {
        return Instantiate(assetsDictionary[itemId]);
    }
}
