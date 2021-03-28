using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Weapon : MonoBehaviourPunCallbacks
{
    public bool MoveDir = false;

    public float ProjectSpeed;

    public float DestroyTime;

    public float Damage;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine("DestroyIntime");
    }

    // Update is called once per frame
    IEnumerator DestroyIntime()
    {
        yield return new WaitForSeconds(DestroyTime);
        this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void ChangeDir()
    {
        MoveDir = true;
    }

    [PunRPC]
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (!MoveDir)
        {
            transform.Translate(Vector2.right * ProjectSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * ProjectSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        if(target != null && (!target.IsMine || target.IsRoomView))
        {
            if(target.tag == "Enemy")
            {
                target.RPC("ReduceHealth", RpcTarget.AllBuffered, Damage);
            }
            this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
        }
    }
}
