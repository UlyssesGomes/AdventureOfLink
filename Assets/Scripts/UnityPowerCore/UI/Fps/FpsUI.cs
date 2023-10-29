using UnityEngine;
using UnityEngine.UI;

public class FpsUI : MonoBehaviour
{
    [SerializeField]
    private Text text;
	private float deltaTime;
	private float []lastFps;
    private int arrayIndex;

    private void Start()
    {
        lastFps = new float[35];
        arrayIndex = 0;
    }

    void Update()
	{
		float fps = 1.0f / fpsAverage();
		text.text = "FPS: " + Mathf.Ceil(fps).ToString();
	}

    private float fpsAverage()
    {
        if (arrayIndex >= lastFps.Length)
            arrayIndex = 0;

        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        lastFps[arrayIndex++] = deltaTime;

        float average = 0f;
        foreach(float t in lastFps)
        {
            average += t;
        }

        return average / (float)lastFps.Length;
    }
}
