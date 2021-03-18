using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayManager : MonoBehaviour
{
    public GameObject PlayerPref;
    //public GameObject GameCanvas;
    public GameObject SceneCamera;
    // Start is called before the first frame update
    private void Awake()
    {
        //GameCanvas.SetActive(true);
        SpawnPlayer();
    }
    public void SpawnPlayer()
    {
        float randomValue = Random.Range(-1f, 1f);
        PhotonNetwork.Instantiate(PlayerPref.name, new Vector2(this.transform.position.x * randomValue, this.transform.position.y * randomValue), Quaternion.identity, 0);

        //GameCanvas.SetActive(false);
        SceneCamera.SetActive(false);
    }
}
