using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

public class PlayerInputController : MonoBehaviour
{

    public bool[] laneShortPressedArray, laneLongPressedArray;

    // Private component references
    public static PlayerInputController instance;

    SerialPort arduino;
    char[] arduinoInput;
    string ArduinoInputString;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // Can't have two player input controllers active at once
        }
        else
        {
            instance = this;
            laneShortPressedArray = new bool[4];
            laneLongPressedArray = new bool[4];
        }
    }

    // Update is called once per frame
    /*
    public void UpdateInputs()
    {
        laneShortPressedArray[0] = Input.GetKeyDown(KeyCode.A);
        laneShortPressedArray[1] = Input.GetKeyDown(KeyCode.S);
        laneShortPressedArray[2] = Input.GetKeyDown(KeyCode.D);
        laneShortPressedArray[3] = Input.GetKeyDown(KeyCode.F);

        laneLongPressedArray[0] = Input.GetKey(KeyCode.A);
        laneLongPressedArray[1] = Input.GetKey(KeyCode.S);
        laneLongPressedArray[2] = Input.GetKey(KeyCode.D);
        laneLongPressedArray[3] = Input.GetKey(KeyCode.F);

    }*/
    

    void Start()
    {
        arduinoInput = new char[4];
        
        arduino = new SerialPort("COM3", 9600);
        arduino.DtrEnable = true;
        arduino.RtsEnable = true;
        arduino.Open();
        Thread sampleThread = new Thread(new ThreadStart(serialCallback));
        sampleThread.IsBackground = true;
        sampleThread.Start();
    }


    public void serialCallback()
    {
        while(true) {
            Debug.Log(arduino.IsOpen);
            try
            {
                ArduinoInputString = arduino.ReadLine();
                Debug.Log(ArduinoInputString);

                //Getting input from Serial Monitor in the format "[0][1][2][3]\n"
                for(int i = 0; i < 4; i++) 
                    arduinoInput[i] = ArduinoInputString[i];
            }
            catch (System.Exception)
            {
                Debug.Log("Aw");
            }
        }   
          
    }

    public void UpdateInputs()
    {
        if (arduino.IsOpen)
        {
            try
            {
                for(int i = 0; i < 4; i++) {

                    //GetKey
                    laneLongPressedArray[i] = (arduinoInput[i] == i*2 + '1');

                    //GetKeyDown
                    if(laneShortPressedArray[i])
                        laneShortPressedArray[i] = false;
                    else if(arduinoInput[i] == i*2 + '1')
                        laneShortPressedArray[i] = true;

                    //Checking output
                    if(laneLongPressedArray[i])
                        Debug.Log("Yes " + i);
                }

            }
            catch (System.Exception)
            {
                Debug.Log("I hate my life");
            }

        }
    }
}