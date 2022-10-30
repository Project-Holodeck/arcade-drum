using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMove : MonoBehaviour
{
    public float repeatWidth;
    private float speed = 0.01f; //change speed here
    private float offset = 0f;
    private TrackMove oneBefore;

    private Vector3 startPos;
    // Start is called before the first frame update
    void Awake()
    {
        repeatWidth = GetComponent<BoxCollider>().size.z;
    }

    public void Setup(float speed, Vector3 startPos, TrackMove oneBefore, float offset)
    {
        this.speed = speed;
        this.offset = offset;
        this.startPos = startPos;
        this.oneBefore = oneBefore;
    }

    // Update is called once per frame  
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);
        if (transform.position.z < -4 + offset)
        {
            transform.position = oneBefore.transform.position + transform.forward*repeatWidth;
        }
    }
}
