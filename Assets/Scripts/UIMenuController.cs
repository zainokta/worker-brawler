using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuController : MonoBehaviour
{
    [SerializeField] Text buildVersion;
    GameManager gameManager;
    SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
        gameManager.FadeScreen("FadeIn");
        soundManager.Play("MainTheme");
        VersionBuild();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VersionBuild()
    {
        Debug.Log("Application Version : " + Application.version);
        buildVersion.text = "Application Version : " + Application.version;
    }

    public void SetPanel(GameObject obj, bool a)
    {
        soundManager.Play("Click");
        if (a)
        {
            obj.SetActive(a);
            StartCoroutine(SetAnim(obj,a, "PopUp"));
        }
        else if(!a)
        {
            StartCoroutine(SetAnim(obj, a, "PopDown"));
        }
    }

    IEnumerator SetAnim(GameObject obj, bool a, string action)
    {
        Animator animator = obj.GetComponent<Animator>();
        animator.Play(action);
        yield return new WaitForSeconds(.5f);
        obj.SetActive(a);
    }

}
