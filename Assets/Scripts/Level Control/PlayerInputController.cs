using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public bool[] lanePressedArray;

    // Private component references
    public static PlayerInputController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // Can't have two level controllers active at once
        }
        else
        {
            instance = this;
            lanePressedArray = new bool[4];
        }
    }

    // Update is called once per frame
    public void UpdateInputs()
    {
        lanePressedArray[0] = Input.GetKeyDown(KeyCode.A);
        lanePressedArray[1] = Input.GetKeyDown(KeyCode.S);
        lanePressedArray[2] = Input.GetKeyDown(KeyCode.D);
        lanePressedArray[3] = Input.GetKeyDown(KeyCode.F);
    }
}
