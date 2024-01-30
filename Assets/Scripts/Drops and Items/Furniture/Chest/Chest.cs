using UnityEngine;
using UnityEngine.UI;

public class Chest : AbstractChest
{
    private Color defaultColor;
    private Color buildingColor;

    [SerializeField]
    private GameObject buildingBar;
    [SerializeField]
    private Image filledBar;

    public override void furnitureStart()
    {
        chest = Instantiate(chest);
        buildingColor = new Color(1f, 1f, 1f, 0.2549f);
        defaultColor = sprite.color;

        buildingAmount = 0.0f;

        if(buildingAmount < 100f)
        {
            buildingBar.SetActive(true);
            filledBar.fillAmount = buildingAmount / 100f;
            sprite.color = buildingColor;
        }    
    }

    public override void furnitureUpdate()
    { }

    protected override void buildingImpact(float value)
    {
        buildingAmount += value;

        if (buildingAmount < 100f)
        {
            filledBar.fillAmount = buildingAmount / 100f;
        }
        else
        {
            buildingAmount = 100f;
            buildingBar.SetActive(false);
            sprite.color = defaultColor;
        }
    }

}
