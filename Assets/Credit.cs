using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour
{
    public GameObject movingObj;
    public Vector2 batas;
    public Vector2 awal;
    public float spedd = 1f;
    // Start is called before the first frame update
    void Start()
    {
        awal = movingObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movingObj.transform.Translate(0, spedd * Time.deltaTime, 0);

        if(movingObj.transform.position.y >= batas.y)
        {
            movingObj.transform.position = awal;
        }

        if (Input.GetMouseButton(0))
        {
            spedd = 2.5f;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            spedd = 1f;
        }
    }
}
