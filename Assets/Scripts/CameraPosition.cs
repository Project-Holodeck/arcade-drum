using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{

    public float[] songCameraPos = {0.0f, 4.85f, -8.29f};
    public float[] menuCameraPos = {0.0f, 4.3f, -6.2f};
    public float originalRotation = -0.21f;
    public float newRotation = 9.48f;

    public static CameraPosition instance;

    // Start is called before the first frame update
    void Start()
    {

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        setMenuPosition();
    }

    // Update is called once per frame
    public void setMenuPosition() {
        transform.position = new Vector3(menuCameraPos[0], menuCameraPos[1], menuCameraPos[2]);
        transform.rotation = Quaternion.Euler(new Vector3(originalRotation, 0, 0));
    }

    public void setBeatmapPosition() {
        transform.position = new Vector3(songCameraPos[0], songCameraPos[1], songCameraPos[2]);
        transform.rotation = Quaternion.Euler(new Vector3(newRotation, 0, 0));
    }
}
