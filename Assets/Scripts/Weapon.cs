using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Weapon : MonoBehaviourPunCallbacks
{
    private bool Attacked;
    public float Damage;
    // Start is called before the first frame update
    void Awake()
    {
        Attacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Attacked = true;
        }
        /*else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Attacked = false;
        }*/

        Debug.Log("Attacked : " + Attacked);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        if (target != null && (!target.IsMine || target.IsRoomView))
        {
            if(target.tag == "Player")
            {
                target.RPC("ReduceHealth", RpcTarget.AllBuffered, Damage);
            }
        }
    }
}
