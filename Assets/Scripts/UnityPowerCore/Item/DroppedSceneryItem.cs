using UnityEngine;

public class DroppedSceneryItem : SceneryItem<GameItem>
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float timeMove;
    [SerializeField]
    private Vector2 direction;

    private float timeCount;

    // Start is called before the first frame update
    void Start()
    {
        createAmount = 1;
        float angle = Random.Range(0f, 359f);
        direction = VectorUtils.createVector3(1, angle);
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount < timeMove)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
