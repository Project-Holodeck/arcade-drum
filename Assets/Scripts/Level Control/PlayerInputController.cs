using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.IO.Ports;
using System.Threading;

public class PlayerInputController : MonoBehaviour
{
    public bool[] lanePressedArray;

    // Private component references
    public static PlayerInputController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // Can't have two player input controllers active at once
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
    /*public string portName;
    SerialPort arduino;
    byte arduinoInput1;
    byte arduinoInput2;
    byte arduinoInput3;
    byte arduinoInput4;

    public GameObject Cube1;
    public GameObject Cube2;
    public GameObject Cube3;
    public GameObject Cube4;

    void Start()
    {
        arduino = new SerialPort("COM3", 9600);
        arduino.Open();
        Thread sampleThread = new Thread(new ThreadStart(serialCallback));
        sampleThread.IsBackground = true;
        sampleThread.Start();
    }


    public void serialCallback()
    {
        while (true)
        {

            if (arduino.IsOpen)
            {    // Als uw serialpoort open is
                try
                {
                    arduinoInput1 = (byte)arduino.ReadByte();
                    arduinoInput2 = (byte)arduino.ReadByte();
                    arduinoInput3 = (byte)arduino.ReadByte();
                    arduinoInput4 = (byte)arduino.ReadByte();

                }
                catch (System.Exception)
                {
                }
            }
        }
    }

    public void UpdateInputs()
    {
        if (arduino.IsOpen)
        {
            try
            {
                if (arduinoInput1 == '1')
                {
                    lanePressedArray[0] = false;
                }
                else if (arduinoInput1 == '2')
                {
                    lanePressedArray[0] = true;
                }

                if (arduinoInput2 == '3')
                {
                    lanePressedArray[1] = false;

                }
                else if (arduinoInput2 == '4')
                {
                    lanePressedArray[1] = true;

                }
                if (arduinoInput3 == '5')
                {
                    lanePressedArray[2] = false;

                }
                else if (arduinoInput3 == '6')
                {
                    lanePressedArray[2] = true;

                }
                if (arduinoInput4 == '7')
                {
                    lanePressedArray[3] = false;

                }
                else if (arduinoInput4 == '8')
                {
                    lanePressedArray[3] = true;

                }
            }
            catch (System.Exception)
            {
                Debug.Log("I hate my life");
            }

        }
    }*/
}