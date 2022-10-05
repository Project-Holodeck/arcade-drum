using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    // 1.1, 3.4
    private Rigidbody rb;
    private float speed = 10.0f;
    private float line = -26.3f;
    private PlayerController playerControllerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0, 0, -speed);
        if (this.transform.position.z < line) // TODO: Fix hard coded line and speed
        {
            Destroy(this.gameObject);
            playerControllerScript.comboCount = 0;
        }
    }
}
