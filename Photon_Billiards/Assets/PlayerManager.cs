using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    private GameObject cue;
    [SerializeField]
    private Image powerBar;
    [SerializeField]
    private Camera playCam;
    [SerializeField]
    private float power = 500f;
    private Vector3 startingMouse;
    private float powerMultiplier;

    private void Start()
    {
        powerBar.fillAmount = 0f;
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetMouseButton(1))
        {
            cue.transform.RotateAround(cue.transform.position, transform.up, Input.GetAxis("Mouse X") * 10);

        }
        if (Input.GetMouseButtonDown(0))
        {
            startingMouse = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            //float distance = Vector3.Distance(startingMouse, Input.mousePosition);
            float distance = (startingMouse - Input.mousePosition).y;
            powerBar.fillAmount = distance / 200;
        }
        if (Input.GetMouseButtonUp(0))
        {
            powerMultiplier = powerBar.fillAmount;
            cue.GetComponent<Rigidbody>().AddForce(cue.transform.forward * power * powerMultiplier);
            powerBar.fillAmount = 0f;
            playCam.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetCue();
        }
    }

    private void ResetCue()
    {
        //if (!photonView.IsMine)
        //{
        //    Debug.Log("am I here?");
        //    base.photonView.RequestOwnership();
        //}
        //Vector3 p = cue.transform.position;
        // cue.transform.position = p;
        cue.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cue.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        cue.transform.rotation = Quaternion.identity;
        playCam.enabled = true;
    }
}
