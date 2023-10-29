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
        setAnimator("transition", controller.objectUnitStateId);
    }
    #endregion
}
