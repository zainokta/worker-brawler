using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Login : MonoBehaviour
{
    [SerializeField] GameObject UserNameMenu;
    [SerializeField] GameObject HelloMenu;
    [SerializeField] private InputField UsernameInput;
    [SerializeField] Text OpenHello;

    // Start is called before the first frame update
    void Start()
    {
        UserNameMenu.SetActive(true);
    }

    public void SetUsername()
    {
        UserNameMenu.SetActive(false);
        PhotonNetwork.NickName = UsernameInput.text;
        OpenFuncHello();
    }
    void OpenFuncHello()
    {
        HelloMenu.SetActive(true);
        OpenHello.text = "Welcome : " + PhotonNetwork.NickName;
    }
}
