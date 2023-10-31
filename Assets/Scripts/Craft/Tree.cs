using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField]
    private float treeHealth;                       // tree hit points
    [SerializeField]
    private float treeMaxHealth;                    // tree max hit points
    [SerializeField]
    private Animator animator;                      // animation to show tree on hit and cutted

    [SerializeField]
    private GameObject woodPrefab;                  // scenety item wood droped 

    [SerializeField]
    private ParticleSystem leafs;                   // particle of leaves falling when tree get a hit

    private float currentRespownTime;               // respawn time left to this tree grow again
    private const float RESPOWN_TIME = 2.0f;        // total respawn time

    Vector2 normalVector = new Vector2(0, 0);
    Vector2 mirrorVector = new Vector2(0, 180);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(treeHealth <= 0)
        {
            currentRespownTime += Time.deltaTime;
            if(currentRespownTime >= RESPOWN_TIME)
            {
                treeHealth = treeMaxHealth;
                animator.SetTrigger("restore");
            }
        }
    }

    /// <summary>
    /// Method called after tree get hitted. If the tree still have health, then its 
    /// can be hitted.
    /// </summary>
    /// <param name="collision"></param>
    public void onHit(Collider2D collision)
    {
        if(treeHealth > 0)
        {
            treeHealth--;
            animator.SetTrigger("isHit");
            leafs.Play();

            if(treeHealth <= 0)
            {
                // create wood and instantiate drop
                animator.SetTrigger("cut");
                currentRespownTime = 0.0f;
                transform.eulerAngles = normalVector;
                GameObject obj = Instantiate(woodPrefab, transform.position, transform.rotation);
                obj.GetComponent<Wood>().dropDirection(getMovimentDirection(collision));
            }
        }
    }

    /// <summary>
    /// If in collidin with an axe, then call "onHit" an its health will be decreased.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe"))
        {
            onHit(collision);

            if (getMovimentDirection(collision) > 0)
            {
                animator.transform.eulerAngles = mirrorVector;
            } 
            else
            {
                animator.transform.eulerAngles = normalVector;
            }
        }
    }

    /// <summary>
    /// When the tree is hit by an axe, the tree shakes to the opposite side of the axe.
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    private int getMovimentDirection(Collider2D collision)
    {
        int directionValue = 1;
        Vector2 playerPosition = collision.GetComponentInParent<Transform>().position;
        if (playerPosition.x > transform.position.x)
        {
            directionValue *= -1;
        }
        return directionValue;
    }
}
