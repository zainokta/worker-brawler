using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct playerData
{
    public int PlayerChoosen;
    public string playerName;
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public playerData pl;
    [SerializeField] Animator animFade;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        FadeScreen("FadeIn");
    }

    //call me for fade the screen
    public void FadeScreen(string state)
    {
        animFade.Play(state);
        StartCoroutine(WaitFade());
    }

    IEnumerator WaitFade()
    {
        yield return new WaitForSeconds(1);
    }
}
