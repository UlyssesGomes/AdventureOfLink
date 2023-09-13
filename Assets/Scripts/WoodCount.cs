using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO - script adicionado na hud para observar quantidade de wood obtidos.
//        Deletar esse script e o game object dele na scene após adicionar
//        a hud oficial.
public class WoodCount : MonoBehaviour
{
    [SerializeField]
    Player player;
    PlayerInventory inventory;

    private int count;

    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        inventory = player.GetComponent("PlayerInventory") as PlayerInventory;
        text = GetComponent<Text>();
        //text.text = "Woods (" + getWoodsCount() + "x)";
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Woods (" + getWoodsCount() + "x) Size (" + inventory.size() + ")";
    }

    private int getWoodsCount()
    {
        GameItem g = inventory.getSetItem((int)ItemsEnum.WOOD);
        if (g is null)
        {
            return 0;
        }

        return g.amount;
    }
}
