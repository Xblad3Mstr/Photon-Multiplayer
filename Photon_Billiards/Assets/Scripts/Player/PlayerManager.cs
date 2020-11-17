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
    public bool isHit;



    private void Start()
    {
        powerBar.fillAmount = 0f;
        isHit = false;
        cue.transform.rotation = Quaternion.Euler(Vector3.zero);
        cue.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cue.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        cue.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            cue.transform.rotation = Quaternion.Euler(Vector3.zero);
            cue.GetComponent<Rigidbody>().velocity = Vector3.zero;
            cue.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            cue.transform.rotation = Quaternion.Euler(Vector3.zero);
            GameManager.Instance.playCam.enabled = false;
            return;
        }

        CheckHit();

        if (!isHit)
        {
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
                isHit = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            cue.transform.rotation = Quaternion.Euler(Vector3.zero);
            cue.GetComponent<Rigidbody>().velocity = Vector3.zero;
            cue.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            cue.transform.rotation = Quaternion.identity;
            GameManager.Instance.SwitchTurn();
        }
        if (cue.transform.position.y < -3) {

            ResetCuePosition();

        }
    }

    private void CheckHit()
    {
        if (!isHit)
        {
            playCam.enabled = true;
        }
        else
        {
            playCam.enabled = false;
        }
    }

    private void ResetCuePosition()
    {
        

            cue.transform.rotation = Quaternion.Euler(Vector3.zero);
            cue.transform.position = new Vector3(0, 0.09f, -1.65f);
            cue.GetComponent<Rigidbody>().velocity = Vector3.zero;
            cue.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }
}
