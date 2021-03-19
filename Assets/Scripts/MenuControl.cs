using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;

public class MenuControl : MonoBehaviourPunCallbacks,IMatchmakingCallbacks
{
    private const byte MAX_PLAYERS = 2;
    private RoomOptions roomOptions;
   
    // Start is called before the first frame update
    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    private void Start()
    {
        roomOptions = new RoomOptions() { MaxPlayers = MAX_PLAYERS };
        //PhotonNetwork.NickName = "Rakei";

        Debug.Log(PhotonNetwork.NickName);
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    public void CreateLobby()
    {
        string roomName = GenerateRandomRoomName();
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.CreateRoom(roomName, roomOptions, null);
            Debug.Log(roomName);
        }
    }

    public void QuickMatch()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            //loadBalancingClient.OpJoinRandomRoom();
            PhotonNetwork.JoinRandomRoom();
            //PhotonNetwork.JoinRoom("roomjen");

        }
    }

    private string GenerateRandomRoomName()
    {
        string roomName = "";
        const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";
        int charAmount = Random.Range(4, 7); 

        for (int i = 0; i < charAmount; i++)
        {
            roomName += glyphs[Random.Range(0, glyphs.Length)];
        }
        return "lobby-" + roomName;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("CharacterSelection");
    }
}
