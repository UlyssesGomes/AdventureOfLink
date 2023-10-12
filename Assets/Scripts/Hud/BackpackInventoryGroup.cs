using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackInventoryGroup : MonoBehaviour
{
    private bool isVisible;

    [SerializeField]
    private GridLayoutGroup grid;
    private InventorySlot [] backpackInventorySlotList;

    private void Awake()
    {
        backpackInventorySlotList = new InventorySlot[20];

        for(int u = 0; u < grid.transform.childCount; u++)
        {
            backpackInventorySlotList[u] = grid.transform.GetChild(u).GetComponent<InventorySlot>();
        }
    }

    private void Start()
    {
        isVisible = false;
        gameObject.SetActive(isVisible);
    }

    /*
     * Call this method to switch between active and inactive and vice versa.
     */
    public void changeVisibility()
    {
        isVisible = !isVisible;
        gameObject.SetActive(isVisible);
    }
}
