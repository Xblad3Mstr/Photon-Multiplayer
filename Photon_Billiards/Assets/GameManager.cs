using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Rigidbody cue;

    private float power = 1f;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            cue.AddForce(transform.forward); 
        }
    }
}
