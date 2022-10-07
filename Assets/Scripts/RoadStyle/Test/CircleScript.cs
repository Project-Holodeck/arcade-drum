using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will probably be deprecated to switch to something that stays stuck on the minecart tracks.
/// </summary>
public class CircleScript : MonoBehaviour
{
    // 1.1, 3.4
    private float speed = 10.0f;
    private float line = -26.3f;
    private PlayerController playerControllerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.forward * speed * Time.deltaTime;
        
        if (this.transform.position.z < line) // TODO: Fix hard coded line and speed
        {
            Destroy(this.gameObject);
            playerControllerScript.comboCount = 0;
        }
    }
}
