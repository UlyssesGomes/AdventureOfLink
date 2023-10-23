using UnityEngine;

public class SlotFarmAnimation : StateMachineAnimation<SlotFarm>
{
    // Start is called before the first frame update
    protected override void startAnimation()
    {
        currentAnim = -1;
    }

    // Update is called once per frame
    protected override void updateAnimation()
    { }

    #region Moviment
    protected override void setAnimationTransition()
    {
        switch (controller.objectUnitStateId)
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
