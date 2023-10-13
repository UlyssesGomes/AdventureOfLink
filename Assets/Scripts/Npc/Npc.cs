using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{

    public float speed;

    private Animator animator;
    private int index;

    public List<Transform> paths = new List<Transform>();

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!DialogueControl.instance.IsShowing)
        {
            transform.position = Vector2.MoveTowards(transform.position, paths[index].position, speed * Time.deltaTime);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Vector2.Distance(transform.position, paths[index].position) < 0.1f)
        {
            if(index < paths.Count - 1)
            {
                index++;
            } else
            {
                index = 0;
            }
        }

        /*
         * If the npc is moving to the right, the subtraction will result in positive,
         * if it is moving to the left, the result will be negative.
         */
        Vector2 direction = paths[index].position - transform.position;
        if(direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if(direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
}
