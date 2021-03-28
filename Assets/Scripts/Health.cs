using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Health : MonoBehaviourPunCallbacks
{
    public float Amount;

    public Image FillImage;
    private GameObject Player;
    private GameObject Enemy;

    //public Health EnemyStats;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;
    public Image Bar;
    public Text nameChar;
    PlayManager pm;

    public static bool YouWin;
    public static bool YouLose;
    private void Start()
    {
        Debug.Log(PhotonNetwork.PlayerListOthers[0].UserId);
        pm = FindObjectOfType<PlayManager>();
        //Player = GameObject.FindGameObjectWithTag("Player");
        //Enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void Update()
    {
        if (pm.gameEnd)
        {
            sendHealth(Amount);
        }
    }
    [PunRPC]
    public void sendHealth(float amount)
    {
        if (photonView.IsMine)
        {
            pm.playerHealth = amount;
        }
        if (!photonView.IsMine)
        {
            pm.enemyHealth = amount;
        }
        pm.checkHealth();
    }


    [PunRPC]
    public void ReduceHealth(float amount)
    {
        ModifyHealth(amount);
    }

    private void CheckHealth()
    {
        FillImage.fillAmount = Amount / 100f;
        if(photonView.IsMine && Amount <= 0)
        {
            this.GetComponent<PhotonView>().RPC("Death", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void Death()
    {
        rb.gravityScale = 0;
        boxCollider2D.enabled = false;
        spriteRenderer.enabled = false;
        nameChar.enabled = false;
        Bar.enabled = false;
        //Player.SetActive(false);
        WinLoseState();
    }

    [PunRPC]
    public void WinLoseState()
    {
        if (photonView.IsMine)
        {
            if(this.gameObject.tag == "Player" && Amount <= 0)
            {
                YouLose = true;
            }
            else if(this.gameObject.tag == "Enemy" && Amount <= 0)
            {
                YouWin = true;
            }
        }
        else if (!photonView.IsMine)
        {
            if (this.gameObject.tag == "Player" && Amount <= 0)
            {
                YouLose = true;
            }
            else if (this.gameObject.tag == "Enemy" && Amount <= 0)
            {
                YouWin = true;
            }
        }
    }

    // Update is called once per frame
    private void ModifyHealth(float amount)
    {
        if (photonView.IsMine)
        {
            Amount -= amount;
            FillImage.fillAmount -= amount;
        }
        else
        {
            Amount -= amount;
            FillImage.fillAmount -= amount;
        }

        CheckHealth();
    }
}
