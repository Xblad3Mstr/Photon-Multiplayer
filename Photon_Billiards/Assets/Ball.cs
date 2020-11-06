using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int ballNum;
    [SerializeField]
    private GameObject ball;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PocketCol")
        {
            GameManager.Instance.CountBall(ballNum);
            Destroy(ball);
        }
       
    }
}
