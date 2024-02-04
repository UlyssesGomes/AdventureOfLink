using UnityEngine;

public class PuffSmoke : Fx
{
    [SerializeField]
    private Animator animator;        // Animator component that belongs to puff smoke

    public override int getFxType()
    {
        return (int)FxEnum.PUFF_SMOKE;
    }
    void Start()
    {
        animator.SetInteger("transition", (int)PuffSmokeEnum.PUFF_STARTED);
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (animator.GetInteger("transition") == (int) PuffSmokeEnum.PUFF_STARTED)
        {
            animator.SetInteger("transition", (int)PuffSmokeEnum.PUFF_RUNNING);
        }
        else if(stateInfo.IsName("puff_ended"))
        {
            Destroy(gameObject);
        }
    }
}
