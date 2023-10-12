using UnityEngine;
using UnityEngine.UI;

public abstract class InventoryGroupAbstract<T> : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup grid;               // grid with all slots
    [HideInInspector]
    public T[] inventorySlotList;               // slot list

    [SerializeField]
    protected PlayerInventory playerInventory;  // player inventory controller

    void Awake()
    {
        inventorySlotList = new T[getInventoryGroupSize()];
        inventorySlotList = grid.transform.GetComponentsInChildren<T>();
    }

    /// <summary>
    /// Get total size of inventorySlotList.
    /// </summary>
    /// <returns></returns>
    protected abstract int getInventoryGroupSize();
}
