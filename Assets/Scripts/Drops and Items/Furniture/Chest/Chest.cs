﻿using UnityEngine;
using UnityEngine.UI;

public class Chest : AbstractChest
{
    private Color defaultColor;             // chest default color to be desplayed when chest was set on the ground
    private Color buildingColor;            // color to be shown when chest is under building

    private AgentExecutor executor;         // executor to run chest bar agent

    private AssetFactory assetfactory;      // Manager of assets available in memory.

    private Vector3 underConstructionPos;
    private Vector3 afterConstructionPos;

    public override void furnitureStart()
    {
        afterConstructionPos = transform.position;
        underConstructionPos = new Vector3(0f, -0.65f, 0f);

        assetfactory = AssetFactory.getInstance();
        chest = Instantiate(chest);
        buildingColor = new Color(1f, 1f, 1f, 0.2549f);
        defaultColor = sprite.color;

        buildingAmount = 0.0f;

        if(buildingAmount < 100f)
        {
            filledBar.fillAmount = buildingAmount / 100f;
            sprite.color = buildingColor;
            gameObject.transform.position -= underConstructionPos;
        }

        executor = new AgentExecutor();
    }

    public override void furnitureUpdate()
    {
        executor.update();
    }

    protected override void buildingImpact(float value)
    {
        executor.addAgent(new FurnitureBarAgent(this));
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
            transform.position = afterConstructionPos;
        }
    }
}
