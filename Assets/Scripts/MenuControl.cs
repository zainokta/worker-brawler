using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;

public class MenuControl : MonoBehaviourPunCallbacks
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
        PhotonNetwork.NickName = "Rakei";

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
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.CreateRoom("roomjen", roomOptions, null);
        }
    }

    public void QuickMatch()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            //PhotonNetwork.JoinRandomRoom();
            PhotonNetwork.JoinRoom("roomjen");
        }
    }

    private string GenerateRandomRoomName()
    {
        int room = Random.Range(0, 999999);
        return "lobby-" + room;
}

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("CharacterSelection");
    }
}
