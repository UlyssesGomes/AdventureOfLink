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
        switch (controller.objectUnitStateId)
        {
            case (int) PlayerStatesEnum.IDDLE:
                animator.SetInteger("transition", 0);
                break;
            case (int) PlayerStatesEnum.WALKING:
                animator.SetInteger("transition", 1);
                break;
            case (int) PlayerStatesEnum.RUNNING:
                animator.SetInteger("transition", 2);
                break;
            case (int) PlayerStatesEnum.ROLLING:
                animator.SetInteger("transition", 3);
                break;
            case (int)PlayerStatesEnum.CUTTING:
                animator.SetInteger("transition", 4);
                break;
            case (int)PlayerStatesEnum.DIGGING:
                animator.SetInteger("transition", 5);
                break;            
            case (int)PlayerStatesEnum.WATERING:
                animator.SetInteger("transition", 6);
                break;
            default:
                animator.SetInteger("transition", 0);
                break;
        }
    }
}
