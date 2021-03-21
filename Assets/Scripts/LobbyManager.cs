using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text[] texts;
    [SerializeField]
    private Text roomName;
    private bool started;

    [SerializeField] Text timer;

    // Start is called before the first frame update
    void Start()
    {
        texts[0].text = PhotonNetwork.LocalPlayer.NickName;
        roomName.text = "Server : " + PhotonNetwork.CloudRegion;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        //timer.text = GameManager.instance.Timer.ToString();
        //StartCoroutine(Timeout());
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            texts[1].text = PhotonNetwork.PlayerListOthers[0].NickName;
            StartCoroutine(Timeout());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    IEnumerator Timeout()
    {
        if (!started && PhotonNetwork.IsMasterClient)
        {
            started = true;
            //StartCoroutine(GameManager.instance.Countdown());
            yield return new WaitForSeconds(5f);
            PhotonNetwork.LoadLevel("Gameplay");
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        texts[1].text = newPlayer.NickName;
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        texts[1].text = "";
    }
}
