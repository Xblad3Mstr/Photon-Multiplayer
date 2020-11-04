using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Rigidbody cue;
    [SerializeField]
    private Image powerBar;
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
            cue.AddForce(transform.forward * power * powerMultiplier);
            powerBar.fillAmount = 0f;
        }
    }
}
