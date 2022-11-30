using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit!"); 
        Destroy(other.gameObject);
    }
}
