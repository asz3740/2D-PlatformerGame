using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Button : MonoBehaviour
{
    public void ClickStart()
    {
        LoadingSceneManager.LoadScene("Stage1");
    }    
    
    public void ClickExit()
    {
        Application.Quit();
    }    
}