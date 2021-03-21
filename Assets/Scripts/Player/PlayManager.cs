﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayManager : MonoBehaviourPunCallbacks
{
    public GameObject[] PlayerPref;
    //public GameObject GameCanvas;
    public GameObject SceneCamera;
    public Text PingText;

    public static float GameTime = 5f;
    public Text TimeInGame;
    public Text LoseWinState;

    public bool YouWin;

    public float playerHealth;
    public float enemyHealth;

    public bool gameEnd = false;

    // Start is called before the first frame update
    private void Awake()
    {
        TimeInGame.gameObject.SetActive(false);
        //GameCanvas.SetActive(true);
        GameManager Gm = FindObjectOfType<GameManager>();
        SpawnPlayer(Gm.pl.PlayerChoosen);
    }

    private void Update()
    {
        PingText.text = "Ping : " + PhotonNetwork.GetPing() + " ms";

        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            TimeInGame.gameObject.SetActive(true);
            GameTime -= 0.01f;
            TimeInGame.text = GameTime.ToString("0");
            if (GameTime <= 0)
            {
                if (!gameEnd)
                {
                    checkHealth();
                    gameEnd = true;
                }
            }
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
        //Debug.Log(GameTime);
    }

    public void checkHealth()
    {
        if (playerHealth > enemyHealth)
        {
            LoseWinState.text = "You Win";
        }
        if (playerHealth < enemyHealth)
        {

            LoseWinState.text = "You Lose";
        }
        if(playerHealth == enemyHealth)
        {
            LoseWinState.text = "Draw";
        }
    }

    public void SpawnPlayer(int plaChoose)
    {
        float randomValue = Random.Range(-1f, 1f);
        PhotonNetwork.Instantiate(PlayerPref[plaChoose].name, new Vector2(this.transform.position.x * randomValue, this.transform.position.y * randomValue), Quaternion.identity, 0);
        //GameCanvas.SetActive(false);
        SceneCamera.SetActive(false);
    }
}
