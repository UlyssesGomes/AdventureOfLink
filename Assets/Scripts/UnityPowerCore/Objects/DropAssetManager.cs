﻿using UnityEngine;

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
    public GameItem instanceGameItemByItemId(int itemId)
    {
        return Instantiate(assetsDictionary[itemId]);
    }

    /// <summary>
    /// Return and item by its id to check infos only.
    /// </summary>
    /// <param name="itemId">int item id</param>
    /// <returns>GameItem associated with past id</returns>
    public GameItem checkItemInfo(int itemId)
    {
        return assetsDictionary[itemId];
    }
}
