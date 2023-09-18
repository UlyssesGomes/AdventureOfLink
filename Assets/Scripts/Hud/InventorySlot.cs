using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    private GameItem gameItem;

    [SerializeField]
    private GameObject amountBar;
    private Image filledBar;

    private void Awake()
    {
        filledBar = amountBar.transform.GetChild(0).gameObject.GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        filledBar.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if ((!(gameItem is null) && !gameItem.isStackable) || gameItem is null)
        {
            amountBar.SetActive(false);
        }

        if (!(gameItem is null) && gameItem.isStackable)
        {
            amountBar.SetActive(true);
            Debug.Log("amount: " + gameItem.amount);
            Debug.Log("total: " + gameItem.total);
            Debug.Log("percent: " + gameItem.getTotalPercent());
            filledBar.fillAmount = gameItem.getTotalPercent();
        }
    }

    public void setItem(GameItem item)
    {
        gameItem = item;
    }
}
