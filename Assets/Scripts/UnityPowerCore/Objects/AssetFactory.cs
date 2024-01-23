using UnityEngine;

using System.Collections.Generic;

public class AssetFactory : MonoBehaviour
{
    [SerializeField]
    private GameItem[] assetsCollections;                   // items availables in memory
    private Dictionary<int, GameItem> assetsDictionary;     // dictionary to find these items quickly

    [SerializeField]
    private GameObject[] furniturePrefabs;
    private Dictionary<int, GameObject> furnitureDictionary;

    private void Start()
    {
        assetsDictionary = new Dictionary<int, GameItem>();

        foreach(GameItem g in assetsCollections)
        {
            assetsDictionary.Add((int)g.itemId, g);
        }


        furnitureDictionary = new Dictionary<int, GameObject>();
        foreach (GameObject gameObject in furniturePrefabs)
        {
            Furniture furniture = gameObject.GetComponent<Furniture>();
            int itemId = furniture.getItemId();
            furnitureDictionary.Add(itemId, gameObject);
        }
    }

    /// <summary>
    /// Intantiate a SerializableObject by its itemId value.
    /// </summary>
    /// <param name="itemId">id of desired item</param>
    /// <returns>Desired GameItem instance</returns>
    public GameItem instanceGameItemByItemId(int itemId)
    {
        return Instantiate(assetsDictionary[itemId]);
    }

    /// <summary>
    /// Return and item by its id to check infos only. Dont modify this values.
    /// </summary>
    /// <param name="itemId">int item id</param>
    /// <returns>GameItem associated with past id</returns>
    public GameItem checkItemInfo(int itemId)
    {
        return assetsDictionary[itemId];
    }
}
