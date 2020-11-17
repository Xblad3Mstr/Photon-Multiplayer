﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class Ball : MonoBehaviourPunCallbacks
{
    public int ballNum;
    [SerializeField]
    private GameObject ball;
    [Header ("Set Dynamically")]
    public Text player1Score;
    public Text player2Score;

    private void Start()
    {
        GameObject scorePlayer1 = GameObject.Find("PlayerOneScore");
        player1Score = scorePlayer1.GetComponent<Text>();
        player1Score.text = "0";

        GameObject scorePlayer2 = GameObject.Find("PlayerTwoScore");
        player2Score = scorePlayer2.GetComponent<Text>();
        player2Score.text = "0";
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PocketCol")
        {
            GameManager.Instance.CountBall(ballNum);
            Destroy(ball);
            if (photonView.IsMine)
            {
            int score = int.Parse(player1Score.text);
            score += 100;
            player1Score.text = score.ToString();
            }
            if (!photonView.IsMine)
            {
                int score = int.Parse(player2Score.text);
                score += 100;
                player2Score.text = score.ToString();
            }
        }
       
    }
}
