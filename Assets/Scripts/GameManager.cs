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
    public static GameManager instance;
    public playerData pl;
    [SerializeField] Animator animFade;
    //private float timer;

    //public float Timer { get => (int)timer; set => timer = value; }

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
        //Timer = 3f;
    }

    //call me for fade the screen
    public void FadeScreen(string state)
    {
        animFade.Play(state);
        StartCoroutine(WaitFade());
    }

    //public IEnumerator Countdown()
    //{
    //    while(Timer > 0)
    //    {
    //        yield return new WaitForSeconds(1);
    //        Timer--;
    //    }
    //}

    IEnumerator WaitFade()
    {
        yield return new WaitForSeconds(1);
    }
}
