using UnityEngine;
using UnityEngine.UI;

public class Chest : AbstractChest
{
    private Color defaultColor;             // chest default color to be desplayed when chest was set on the grond
    private Color buildingColor;            // color to be shown when chest is under building

    [SerializeField]
    private GameObject buildingBar;         // building progress bar to show to player how much left work is needed
    [SerializeField]
    private Image filledBar;                // amoung of work done in progress bar

    private AgentExecutor executor;         // executor to run chest bar agent

    private AssetFactory assetfactory;      // Manager of assets available in memory.

    public override void furnitureStart()
    {
        assetfactory = GameObject.Find("DropAssetManager").GetComponent<AssetFactory>(); ;
        chest = Instantiate(chest);
        buildingColor = new Color(1f, 1f, 1f, 0.2549f);
        defaultColor = sprite.color;

        buildingAmount = 0.0f;

        if(buildingAmount < 100f)
        {
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
            GameObject puff = assetfactory.instanceFxGameObjectByType((int)FxEnum.PUFF_SMOKE);
            puff.transform.position = transform.parent.position;
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
