using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPref;
    //public GameObject GameCanvas;
    public GameObject SceneCamera;
    public Text PingText;

    public static float GameTime = 180f;
    public Text TimeInGame;

    public Text LoseWinState;

    // Start is called before the first frame update
    private void Awake()
    {
        TimeInGame.gameObject.SetActive(false);
        //GameCanvas.SetActive(true);
        SpawnPlayer();
    }

    private void Update()
    {
        PingText.text = "Ping : " + PhotonNetwork.GetPing() + " fps";

        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            TimeInGame.gameObject.SetActive(true);
            GameTime -= 0.01f;
            TimeInGame.text = GameTime.ToString("0");
        }
        else
        {
            TimeInGame.gameObject.SetActive(false);
        }

        if (Health.YouLose == true)
        {
            LoseWinState.gameObject.SetActive(true);
            LoseWinState.text = "You Lose"; 
        }
        else if(Health.YouWin == true)
        {
            LoseWinState.gameObject.SetActive(true);
            LoseWinState.text = "You Win";
        }

        Debug.Log(Health.YouLose);
        Debug.Log(Health.YouWin);
        Debug.Log(GameTime);
    }
    public void SpawnPlayer()
    {
        float randomValue = Random.Range(-1f, 1f);
        PhotonNetwork.Instantiate(PlayerPref.name, new Vector2(this.transform.position.x * randomValue, this.transform.position.y * randomValue), Quaternion.identity, 0);

        //GameCanvas.SetActive(false);
        SceneCamera.SetActive(false);
    }
}
