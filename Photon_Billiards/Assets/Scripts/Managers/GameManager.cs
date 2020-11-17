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
    public Camera playCam;

    private void Start()
    {
        Instance = this;


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
        

        //if (playerPrefab == null)
        //{ // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.

        //    Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        //}
        //else
        //{


        //    if (PlayerManager.LocalPlayerInstance == null)
        //    {
        //        Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

        //        // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
        //        PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        //    }
        //    else
        //    {

        //        Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
        //    }


        //}
    }

    void Update()
    {

    }


    public void CountBall(int num)
    {
        Debug.Log("Ball number: " + num + " in pocket");
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
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        Player newPlayer;
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            PhotonView view = cue.GetPhotonView();
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[0])
            {
                newPlayer = PhotonNetwork.PlayerList[1];
                view.TransferOwnership(newPlayer);
            }
            else
            {
                newPlayer = PhotonNetwork.PlayerList[0];
                view.TransferOwnership(newPlayer);
            }
        }
        
        cue.transform.rotation = Quaternion.Euler(Vector3.zero);
        cue.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cue.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        cue.transform.rotation = Quaternion.Euler(Vector3.zero);
        cue.GetComponent<PlayerManager>().isHit = false;
    }
}
