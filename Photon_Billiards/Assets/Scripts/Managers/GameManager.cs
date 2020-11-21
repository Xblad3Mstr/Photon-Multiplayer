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
    [SerializeField]
    private GameObject[] ballIcons;

    public Camera playCam;

    private bool gameIsOver;
    private int p1Score = 0;
    private int p2Score = 0;
    private Text player1Name;
    private Text player2Name;


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

        player1Name = GameObject.Find("PlayerOneScore Title").GetComponent<Text>();
        player2Name = GameObject.Find("PlayerTwoScore Title").GetComponent<Text>();
    }

    public void GameOver(string winner)
    {
        gameOver.text = winner;
        gameIsOver = true;
        PhotonNetwork.Destroy(cue);
    }

    void Update()
    {
        player1Score.text = p1Score.ToString();
        player2Score.text = p2Score.ToString();
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            SetPlayerNames();
        }
    }


    public void CountBall(int num)
    {
        Debug.Log("Ball number: " + num + " in pocket");
        if (cue.GetPhotonView().Owner == PhotonNetwork.PlayerList[0])
        {

            Debug.Log("Player 1 scored");
            p1Score += 100;
            
            if (p1Score >= 800)
            {
                string winner = "Player 1 wins";
                GameOver(winner);
            }
            foreach (GameObject ballIcon in ballIcons)
            {
                if (ballIcon.name == num.ToString())
                {
                    ballIcon.SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("Player 2 scored");
            p2Score += 100;
            
            if (p2Score >= 800)
            {
                string winner = "Player 2 wins";
                GameOver(winner);
            }
            foreach (GameObject ballIcon in ballIcons)
            {
                if (ballIcon.name == num.ToString())
                {
                    ballIcon.SetActive(true);
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.Log("OnPlayerEnteredRoom() " + other.NickName); // not seen if you're the player connecting

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(p1Score);
            stream.SendNext(p2Score);
        }
        else
        {
            // Network player, receive data
            p1Score = (int)stream.ReceiveNext();
            p2Score = (int)stream.ReceiveNext();
        }
    }

    private void SetPlayerNames()
    {
        player1Name.text = PhotonNetwork.PlayerList[0].NickName;
        player2Name.text = PhotonNetwork.PlayerList[1].NickName;
        
        if (!cue.GetPhotonView().IsMine)
        {
            player1Name.text = PhotonNetwork.PlayerList[0].NickName;
            player2Name.text = PhotonNetwork.PlayerList[1].NickName;
        }
    }
}
