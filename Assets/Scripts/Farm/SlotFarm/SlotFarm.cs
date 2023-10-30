using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : StateMachineController<SlotFarm>
{
    [Header("Components")]
    [SerializeField]
    public bool isReadyToHarvest;                   // when the plant is ready to harvest, this attribute must be set to true
    public int digAmount;                           // amount of hits a players need to dig util the hole appears
    [SerializeField]
    public int maxDigAmount;                        // amount of hit the slot farm have when it is full
    public bool detectWater;                        // when true, player is watering this hole.

    public float currentRespownTime;                // if slotfarm have no digAmount, start timer by add elapsedTime each frame
    public readonly float RESPOWN_TIME = 10.0f;     // when currentRespownTime reach this amount, the hole must be close

    public float waterAmount;                       // amount of water in hole
    public float maxWaterAmount;                    // max amount of water in hole, when is max, the hole is watered

    [SerializeField]
    private GameObject carrotPrefab;                // scenety item carrot droped

    public void onHit()
    {
        if(digAmount > 0)
        {
            digAmount--;

            if(digAmount <= 0)
            {
                currentRespownTime = 0.0f;
            }
            // TODO - implement grow system to harvest fuits and vegetables.
        }
    }

    /// <summary>
    /// call harvest to create a new havest item.
    /// </summary>
    public void doHarvest()
    {
        if (isReadyToHarvest)
        {
            isReadyToHarvest = false;
            Instantiate(carrotPrefab, transform.position, transform.rotation);
        }
    }

    /// <summary>
    /// Check collision with shovel and water to call watering and digging actions.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shovel"))
        {
            onHit();
        }

        if (collision.CompareTag("WaterCan"))
        {
            detectWater = true;
        }
    }


    /// <summary>
    /// When player its no more watering, stop watering this hole.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
            detectWater = false;
    }

    protected override void stateMachineAwake()
    { }

    protected override void stateMachineStart()
    {
        digAmount = maxDigAmount;
    }

    protected override void stateMachineUpdate()
    { }

    protected override void stateMachineFixedUpdate()
    { }

    protected override UnitState<SlotFarm> getFirstState()
    {
        return getNextState((int)SlotFarmEnum.START);
    }

    protected override SlotFarm getStateMachineObject()
    {
        return this;
    }

    protected override void instantiateAllUnitStates()
    {
        addUnitStateInstance(new SlotFarmStartState());
        addUnitStateInstance(new SlotFarmHoleState());
        addUnitStateInstance(new SlotFarmPlantedState());
    }
}
