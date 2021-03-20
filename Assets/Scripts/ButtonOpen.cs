using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpen : MonoBehaviour
{
    public void OpenButton (string name)
    {
        Application.OpenURL(name);
    }

    public void CloseApp()
    {
        OpenButton("https://saweria.co/ategonstd");
        Application.Quit();
    }
}
