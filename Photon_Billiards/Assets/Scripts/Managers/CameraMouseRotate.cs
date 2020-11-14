using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseRotate : MonoBehaviour
{
    [HideInInspector]
    Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = 5 * Input.GetAxis("Mouse X");
        float y = 5 * -Input.GetAxis("Mouse Y");
        Camera.current.transform.Rotate(x, y, 0);
    }
}
