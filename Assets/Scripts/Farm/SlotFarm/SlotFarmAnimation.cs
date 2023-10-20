using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarmAnimation : MonoBehaviour
{
    [SerializeField]
    private SlotFarm slotFarm;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSlotFarmState();
    }

    #region Moviment
    void GetSlotFarmState()
    {
        switch (slotFarm.objectUnitStateId)
        {
            case (int)SlotFarmEnum.START:
                animator.SetInteger("transition", 0);
                break;
            case (int)SlotFarmEnum.HOLE:
                animator.SetInteger("transition", 1);
                break;
            case (int)SlotFarmEnum.PLANTED:
                animator.SetInteger("transition", 2);
                break;
            default:
                animator.SetInteger("transition", 0);
                break;
        }
    }
    #endregion
}
