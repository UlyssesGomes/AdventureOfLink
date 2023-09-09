using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField]
    private float treeHealth;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject woodPrefab;

    [SerializeField]
    private ParticleSystem leafs;

    private float currentRespownTime;
    private const float RESPOWN_TIME = 15.0f;

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
                treeHealth = 10;
                animator.SetTrigger("restore");
            }
        }
    }

    public void onHit(Collider2D collision)
    {
        if(treeHealth > 0)
        {
            treeHealth--;
            animator.SetTrigger("isHit");
            leafs.Play();

            if(treeHealth <= 0)
            {
                //cria o toco e instancia os drops
                animator.SetTrigger("cut");
                currentRespownTime = 0.0f;
                transform.eulerAngles = normalVector;
                GameObject obj = Instantiate(woodPrefab, transform.position, transform.rotation);
                obj.GetComponent<Wood>().dropDirection(getMovimentDirection(collision));
            }
        }
    }

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
