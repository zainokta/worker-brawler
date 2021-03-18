using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Health : MonoBehaviourPunCallbacks
{
    public Image FillImage;
    // Start is called before the first frame update
    [PunRPC]
    public void ReduceHealth(float amount)
    {
        ModifyHealth(amount);
    }

    // Update is called once per frame
    private void ModifyHealth(float amount)
    {
        if (photonView.IsMine)
        {
            FillImage.fillAmount -= amount;
        }
        else
        {
            FillImage.fillAmount -= amount;
        }
    }
}
