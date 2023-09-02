using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    private float speed = 0.03f;

    public string objectName;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script Movement starting for " + objectName + "...");
        //cam = GameObject.Find(objectName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("Cima");
            //cam.transform.Translate(0, -speed, 0);
            Camera.main.transform.Translate(0, speed, 0);
        } else if(Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Direita");
            //cam.transform.Translate(speed, 0, 0);
            Camera.main.transform.Translate(speed, 0, 0);
        } else if(Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("Baixo");
            //cam.transform.Translate(0, speed, 0);
            Camera.main.transform.Translate(0, -speed, 0);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Esquerda");
            //cam.transform.Translate(-speed, 0, 0);
            Camera.main.transform.Translate(-speed, 0, 0);
        }
    }
}
