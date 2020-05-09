using UnityEngine;
using System.Collections;

public class ScreenFitCamera : MonoBehaviour
{

    private float devHeight = 12.80f;
    private float devWidth = 7.20f;

    // Use this for initialization
    void Start()
    {

        float screenHeight = Screen.height;

        //Debug.Log("screenHeight = " + screenHeight);


        float orthographicSize = this.GetComponent<Camera>().orthographicSize;

        float aspectRatio = Screen.width * 1.0f / Screen.height;

        float cameraWidth = orthographicSize * 2 * aspectRatio;

        //Debug.Log("cameraWidth = " + cameraWidth);

        if (cameraWidth < devWidth)
        {
            orthographicSize = devWidth / (2 * aspectRatio);
            //Debug.Log("new orthographicSize = " + orthographicSize);
            this.GetComponent<Camera>().orthographicSize = orthographicSize;
        }

    }
}