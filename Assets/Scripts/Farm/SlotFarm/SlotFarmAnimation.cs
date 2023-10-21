using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarmAnimation : MonoBehaviour
{
    [SerializeField]
    private SlotFarm slotFarm;
    Animator animator;
    private int currentAnim;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentAnim = -1;
    }

    // Update is called once per frame
    void Update()
    {
        GetSlotFarmState();
    }

    private void setAnimator(int nextAnim)
    {
        if(nextAnim != currentAnim)
        {
            currentAnim = nextAnim;
            animator.SetInteger("transition", currentAnim);
        }
    }

    #region Moviment
    void GetSlotFarmState()
    {
        switch (slotFarm.objectUnitStateId)
        {
            case (int)SlotFarmEnum.START:
                setAnimator(0);
                break;
            case (int)SlotFarmEnum.HOLE:
                setAnimator(1);
                break;
            case (int)SlotFarmEnum.PLANTED:
                setAnimator(2);
                break;
            default:
                setAnimator(0);
                break;
        }
    }
    #endregion
}
