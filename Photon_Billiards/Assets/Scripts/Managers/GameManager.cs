using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    [HideInInspector]
    public static GameManager Instance;
    [SerializeField]
    private GameObject cue;
    [SerializeField]
    private Text gameOver;
    private bool gameIsOver;
    public Camera playCam;

    [Header("Set Dynamically")]
    public Text player1Score;
    public Text player2Score;

    private void Start()
    {
        Instance = this;
        gameIsOver = false;

        // in case we started this demo with the wrong scene being active, simply load the menu scene
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene(0);

            return;
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[1])
            {
                playCam.enabled = false;
            }
        }

        GameObject scorePlayer1 = GameObject.Find("PlayerOneScore");
        player1Score = scorePlayer1.GetComponent<Text>();
        player1Score.text = "0";

        GameObject scorePlayer2 = GameObject.Find("PlayerTwoScore");
        player2Score = scorePlayer2.GetComponent<Text>();
        player2Score.text = "0";
    }

    public void GameOver(string winner)
    {
        gameOver.text = winner;
        gameIsOver = true;
    }

    void Update()
    {

    }


    public void CountBall(int num, GameObject ball)
    {
        Debug.Log("Ball number: " + num + " in pocket");
        if (cue.GetComponent<PhotonView>().IsMine)
        {
            int score = int.Parse(player1Score.text);
            score += 100;
            player1Score.text = score.ToString();
            if (int.Parse(player1Score.text) >= 800)
            {
                string winner = "Player 1 wins";
                GameManager.Instance.GameOver(winner);
            }
            //Destroy(ball);
        }
        if (!cue.GetComponent<PhotonView>().IsMine)
        {
            int score = int.Parse(player2Score.text);
            score += 100;
            player2Score.text = score.ToString();
            if (int.Parse(player2Score.text) >= 800)
            {
                string winner = "Player 2 wins";
                GameManager.Instance.GameOver(winner);
            }
            //Destroy(ball);
        }
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.Log("OnPlayerEnteredRoom() " + other.NickName); // not seen if you're the player connecting

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

            //LoadArena();
        }
    }

    public void SwitchTurn()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        Player newPlayer;
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            //PhotonView view = cue.GetPhotonView();
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[0])
            {
                newPlayer = PhotonNetwork.PlayerList[1];
                foreach (GameObject ball in balls)
                {
                    PhotonView view = ball.GetPhotonView();
                    view.TransferOwnership(newPlayer);

                    ball.transform.rotation = Quaternion.Euler(Vector3.zero);
                    ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    ball.transform.rotation = Quaternion.Euler(Vector3.zero);
                }
            }
            else
            {
                newPlayer = PhotonNetwork.PlayerList[0];
                foreach (GameObject ball in balls)
                {
                    PhotonView view = ball.GetPhotonView();
                    view.TransferOwnership(newPlayer);

                    ball.transform.rotation = Quaternion.Euler(Vector3.zero);
                    ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    ball.transform.rotation = Quaternion.Euler(Vector3.zero);
                }
            }
        }
        else
        {
            foreach (GameObject ball in balls)
            {
                ball.transform.rotation = Quaternion.Euler(Vector3.zero);
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                ball.transform.rotation = Quaternion.Euler(Vector3.zero);
            }
        }

        if (!gameIsOver)
        {
            cue.GetComponent<PlayerManager>().isHit = false;
        }

    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }
}
