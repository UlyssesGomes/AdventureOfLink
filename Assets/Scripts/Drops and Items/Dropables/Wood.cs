using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : AbstractSceneryItem<GameItem>
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float timeMove;

    private float timeCount;

    // Start is called before the first frame update
    void Start()
    {
        createAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if(timeCount < timeMove)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Set direction of wood rolling after its respawn.
    /// </summary>
    /// <param name="value"></param>
    public void dropDirection(int value)
    {
        speed *= value;
    }
}
