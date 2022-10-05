using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMove : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    public int speed = 20; //change speed here
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().center.y / 2.0f;

    }

    // Update is called once per frame  
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        if (transform.position.z < -repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
