using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayManager : MonoBehaviour
{
    public GameObject[] PlayerPref;
    //public GameObject GameCanvas;
    public GameObject SceneCamera;
    public Text PingText;
    // Start is called before the first frame update
    private void Awake()
    {
        //GameCanvas.SetActive(true);
        GameManager Gm = FindObjectOfType<GameManager>();
        SpawnPlayer(Gm.PlayerChoosen);
    }

    private void Update()
    {
        PingText.text = "Ping : " + PhotonNetwork.GetPing();
    }
    public void SpawnPlayer(int plaChoose)
    {
        float randomValue = Random.Range(-1f, 1f);
        PhotonNetwork.Instantiate(PlayerPref[plaChoose].name, new Vector2(this.transform.position.x * randomValue, this.transform.position.y * randomValue), Quaternion.identity, 0);

        //GameCanvas.SetActive(false);
        SceneCamera.SetActive(false);
    }
}
