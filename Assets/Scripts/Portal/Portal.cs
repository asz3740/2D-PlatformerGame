using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Scene 매니저 라이브러리 추가

public class Portal : MonoBehaviour
{
    [SerializeField]
    private string transferMapName;
    
    // 박스 콜라이더에 닿는 순간 이벤트 발생
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("Move");
            Debug.Log("박건도 성공");
        }
    }    
}