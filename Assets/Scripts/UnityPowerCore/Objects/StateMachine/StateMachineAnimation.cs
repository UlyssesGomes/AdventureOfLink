using UnityEngine;

public abstract class StateMachineAnimation<T> : MonoBehaviour where T : StateMachineController<T>
{
    [SerializeField]
    protected T controller;             // StateMachineController to be animated
    protected Animator animator;        // Animator component that belongs to contoller
    protected int currentAnim;          // current animation key

    // Start is called before the first frame update
    void Start()
    {
        startAnimation();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        updateAnimation();
        setAnimationTransition();
    }

    /// <summary>
    /// Check if new animation key is different, if true then call setInteger, otherwise dont call.
    /// Call this method instead of animator.SetInteger() if you want the transition to only exist 
    /// in a new state.
    /// </summary>
    /// <param name="nextAnim">Animation key</param>
    protected void setAnimator(string propertyName, int nextAnim)
    {
        if (nextAnim != currentAnim)
        {
            currentAnim = nextAnim;
            animator.SetInteger(propertyName, currentAnim);
        }
    }

    /// <summary>
    /// Set initial configurations to animation. Call this instead Start();
    /// </summary>
    protected abstract void startAnimation();

    /// <summary>
    /// Update animation. Call this instead Update();
    /// </summary>
    protected abstract void updateAnimation();

    /// <summary>
    /// Implement transition activation according to current animation
    /// </summary>
    protected abstract void setAnimationTransition();
}
