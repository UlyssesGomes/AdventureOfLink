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

    private AgentExecutor executor;

    public override void furnitureStart()
    {
        chest = Instantiate(chest);
        buildingColor = new Color(1f, 1f, 1f, 0.2549f);
        defaultColor = sprite.color;

        buildingAmount = 0.0f;

        if(buildingAmount < 100f)
        {
            //buildingBar.SetActive(true);
            filledBar.fillAmount = buildingAmount / 100f;
            sprite.color = buildingColor;
        }

        executor = new AgentExecutor();
    }

    public override void furnitureUpdate()
    {
        executor.update();
    }

    protected override void buildingImpact(float value)
    {
        executor.addAgent(new ChestBarAgent(this));
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

    /// <summary>
    /// Enable or disable building bar by param isShow.
    /// </summary>
    /// <param name="isShow">boolean to show or hide building bar.</param>
    public void showBuildingBar(bool isShow)
    {
        buildingBar.SetActive(isShow);
    }
}
