using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Player player;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        GetPlayerState();
    }

    #region Moviment
    void OnMove()
    {
        if (player.movingObject.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (player.movingObject.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    void GetPlayerState()
    {
        switch (player.objectStateId)
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
    #endregion
}
