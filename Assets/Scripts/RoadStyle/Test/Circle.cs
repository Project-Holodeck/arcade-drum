using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Circle : HitObjectVisual
{
    // 1.1, 3.4
    private float speed = 10.0f;
    private float line = -4f;
    private PlayerController playerControllerScript;
    private AudioSource[] soundEffects;
    
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        
    }

    public void Setup(float speed){
        this.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.forward * speed * Time.deltaTime;
        
        if (transform.position.z < line) // TODO: Fix hard coded line and speed
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Invoke("destruction", 2.0f);
            playerControllerScript.comboCount = 0;
        }
    }

    public override void Hit(){
        //soundEffects = GetComponentsInChildren<AudioSource>();
        //Debug.Log(soundEffects.Length);
        //soundEffects[0].Play();
        //gameObject.transform.GetChild(0).gameObject.SetActive(false);
        destruction();
    }

    void destruction()
    {
        Destroy(gameObject);
    }
}
