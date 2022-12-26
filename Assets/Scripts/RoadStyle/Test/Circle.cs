using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Circle : HitObjectVisual
{
    // 1.1, 3.4
    private float speed = 10.0f;
    private float line = -4f;
    private float dissolveRate = 0.125f;
    private float refreshRate = 0.05f;
    private PlayerController playerControllerScript;
    private AudioSource[] soundEffects;

    public MeshRenderer skinnedMesh;

    private Material[] skinnedMaterials;
    
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        if(skinnedMesh != null)
            skinnedMaterials = skinnedMesh.materials;
        
       
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
         StartCoroutine(Dissolve());
    }

    IEnumerator Dissolve() {
        if(skinnedMaterials.Length > 0) {
            float counter = 0;
            
            while(skinnedMaterials[0].GetFloat("DissolveAmount") < 1) {
                counter += dissolveRate;
                for(int i = 0; i < skinnedMaterials.Length; i++) 
                    skinnedMaterials[i].SetFloat("DissolveAmount", counter);
                
                yield return new WaitForSeconds(refreshRate);
            }

            if(skinnedMaterials[0].GetFloat("DissolveAmount") >= 1) Destroy(gameObject);
            
        }
    }
}
