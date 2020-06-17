using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Area : MonoBehaviour
{
    [SerializeField] private Text ScriptTxt;
    
    void Start()
    {
        ScriptTxt.text = SceneManager.GetActiveScene().name;
    }
}
