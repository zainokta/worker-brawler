using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Login : MonoBehaviour
{
    [Header("UI Panel")]
    [SerializeField] GameObject UserNameMenu;
    [SerializeField] GameObject HelloMenu;
    [SerializeField] GameObject FalseMenu;

    [Header("Username")]
    [SerializeField] private InputField UsernameInput;

    [Header("Text")]
    [SerializeField] Text OpenHello;

    // Start is called before the first frame update
    void Start()
    {
        UserNameMenu.SetActive(true);
    }

    public void SetUsername()
    {
        if(UsernameInput.text.Length > 3)
        {
            UserNameMenu.SetActive(false);
            PhotonNetwork.NickName = UsernameInput.text;
            OpenFuncHello();
        }
        if(UsernameInput.text.Length <= 3)
        {
            FalseMenu.SetActive(true);
            StartCoroutine(FalseTextUI());
        }
    }
    void OpenFuncHello()
    {
        FalseMenu.SetActive(false);
        HelloMenu.SetActive(true);
        OpenHello.text = "Welcome : " + PhotonNetwork.NickName;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator FalseTextUI()
    {
        yield return new WaitForSeconds(5);
        FalseMenu.SetActive(false);
    }
}
