using UnityEngine;

using System.Collections.Generic;

public class AssetFactory : MonoBehaviour
{
    [SerializeField]
    private GameItem[] assetsCollections;                       // items availables in memory
    private Dictionary<int, GameItem> assetsDictionary;         // dictionary to find these items quickly

    [SerializeField]
    private GameObject[] furniturePrefabs;                      // furniture available in memory
    private Dictionary<int, GameObject> furnitureDictionary;    // dictionary to find these furnitures quickly

    [SerializeField]
    private GameObject[] housesPrefabs;                         // houses available in memory
    private Dictionary<int, GameObject> housesDictionary;       // dictionary to find these houses quickly

    [SerializeField]
    private GameObject[] fxPrefabs;                             // fx available in memory
    private Dictionary<int, GameObject> fxDictionary;           // dictionary to find these fx quickly

    private static AssetFactory assetFactory;                   // singleton instance

    private void Awake()
    {
        assetFactory = this;
    }

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
            Furniture furniture = gameObject.GetComponentInChildren<Furniture>();
            int itemId = (int)furniture.itemId;
            furnitureDictionary.Add(itemId, gameObject);
        }

        housesDictionary = new Dictionary<int, GameObject>();
        foreach (GameObject gameObject in housesPrefabs)
        {
            House<BuildingItemIdEnum> house = gameObject.GetComponentInChildren<House<BuildingItemIdEnum>>();
            int itemId = (int)house.itemId;
            housesDictionary.Add(itemId, gameObject);
        }

        fxDictionary = new Dictionary<int, GameObject>();
        foreach (GameObject gameObject in fxPrefabs)
        {
            Fx fx = gameObject.GetComponentInChildren<Fx>();
            int fxType = (int)fx.getFxType();
            fxDictionary.Add(fxType, gameObject);
        }
    }

    /// <summary>
    /// Return instance of this object. Remember to add this
    /// script/class in only one GameObject.
    /// </summary>
    /// <returns>Instance of this class</returns>
    public static AssetFactory getInstance()
    {
        return assetFactory;
    }

    /// <summary>
    /// Instantiate a SerializableObject by its itemId value.
    /// </summary>
    /// <param name="itemId">id of desired item</param>
    /// <returns>Desired GameItem instance</returns>
    public GameItem instanceGameItemByItemId(int itemId)
    {
        return Instantiate(assetsDictionary[itemId]);
    }

    /// <summary>
    /// Return an item by its id to check infos only. Dont modify this values.
    /// </summary>
    /// <param name="itemId">int item id</param>
    /// <returns>GameItem associated with past id</returns>
    public GameItem checkItemInfo(int itemId)
    {
        return assetsDictionary[itemId];
    }

    /// <summary>
    /// Instantiate a GameObject by its itemId value.
    /// </summary>
    /// <param name="itemId">id of disired item</param>
    /// <returns>Desired GameObject</returns>
    public GameObject instanceFurnitureGameObjectByItemId(int itemId)
    {
        return Instantiate(furnitureDictionary[itemId]);
    }

    /// <summary>
    /// Instantiate a House by its itemId value.
    /// </summary>
    /// <param name="itemId">id of disired house</param>
    /// <returns>Desired GameObject</returns>
    public GameObject instanceHouseGameObjectByItemId(int itemId)
    {
        return Instantiate(housesDictionary[itemId]);
    }

    /// <summary>
    /// Instantiate a GameObject by its type value
    /// </summary>
    /// <param name="type">type of desired fx</param>
    /// <returns>desired GameObject</returns>
    public GameObject instanceFxGameObjectByType(int type)
    {
        return Instantiate(fxDictionary[type]);
    }
}
