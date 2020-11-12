using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRink : MonoBehaviour
{

    public float rink = 6;

    void Start()
    {

        float orthoSize = rink * Screen.height / Screen.width * 0.5f;

        Camera.main.orthographicSize = orthoSize;

    }

   
}
