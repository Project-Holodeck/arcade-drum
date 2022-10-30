using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMove : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    public float speed = 0.01f; //change speed here
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.y;
        //Debug.Log(repeatWidth);
    }

    // Update is called once per frame  
    void Update()
    {
        transform.Translate(Vector3.up* Time.deltaTime * speed);
        if (transform.position.z < repeatWidth * 0.8f)
        {
            transform.position = startPos;
        }
    }
}
