using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class Ball : MonoBehaviourPunCallbacks
{
    public int ballNum;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PocketCol")
        {
            GameManager.Instance.CountBall(ballNum);
            StartCoroutine("DestroyBall");
        }
       
    }

    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(.2f);
        PhotonNetwork.Destroy(this.gameObject);
    }
}
