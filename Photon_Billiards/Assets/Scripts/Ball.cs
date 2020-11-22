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
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            StartCoroutine("DestroyBall");
            //PhotonNetwork.Destroy(this.gameObject);
        }
       
    }

    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(.2f);
        PhotonNetwork.Destroy(this.gameObject);
    }
}
