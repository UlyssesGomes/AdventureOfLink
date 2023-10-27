using UnityEngine;

public class PlayerAnimation : StateMachineAnimation<Player>
{
    protected override void startAnimation()
    { }

    protected override void updateAnimation()
    {
        OnMove();
    }

    #region Moviment
    void OnMove()
    {
        if (controller.movingObject.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (controller.movingObject.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
    #endregion

    protected override void setAnimationTransition()
    {
        animator.SetInteger("transition", controller.objectUnitStateId);
    }
}
