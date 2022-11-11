using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class helloDrum : MonoBehaviour
{
    public string portName;
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
        arduino = new SerialPort("COM4", 9600);
        arduino.Open();
        Thread sampleThread = new Thread(new ThreadStart(sampleFunction));
        sampleThread.IsBackground = true;
        sampleThread.Start();
    }


    public void sampleFunction()
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

    void Update()
    {
        if (arduino.IsOpen)
        {
            try
            {
                if (arduinoInput1 == '1')
                {
                    Cube1.transform.position += new Vector3(1 * Time.deltaTime, 0, 0);

                }
                else if (arduinoInput1 == '2')
                {
                    Cube1.transform.position -= new Vector3(1 * Time.deltaTime, 0, 0);
                }

                if (arduinoInput2 == '3')
                {
                    Cube2.transform.position += new Vector3(1 * Time.deltaTime, 0, 0);

                }
                else if (arduinoInput2 == '4')
                {
                    Cube2.transform.position -= new Vector3(1 * Time.deltaTime, 0, 0);

                }
                if (arduinoInput3 == '5')
                {
                    Cube3.transform.position += new Vector3(1 * Time.deltaTime, 0, 0);

                }
                else if (arduinoInput3 == '6')
                {
                    Cube3.transform.position -= new Vector3(1 * Time.deltaTime, 0, 0);

                }
                if (arduinoInput4 == '7')
                {
                    Cube4.transform.position += new Vector3(1 * Time.deltaTime, 0, 0);

                }
                else if (arduinoInput4 == '8')
                {
                    Cube4.transform.position -= new Vector3(1 * Time.deltaTime, 0, 0);

                }
            }
            catch (System.Exception)
            {
                Debug.Log("I hate my life");
            }

        }
    }
}