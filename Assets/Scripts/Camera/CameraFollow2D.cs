using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    // 싱글톤 변수
    static private CameraFollow2D instance; 
    
    public float m_DampTime = 10f;
    public Transform m_Target;
    public float m_XOffset = 0;
    public float m_YOffset = 0;

    private float margin = 0.1f;

    // 카메라 범위 (맵 밖은 나오지 않도록 한다)
    [SerializeField]
    private BoxCollider2D bound;

    private Vector3 minBound;
    private Vector3 maxBound;

    private float halfWidth;
    private float halfHeight;

    private Camera theCamera;

    // 싱글톤
    void Awake () {
        if (instance != null)
        {
            DestroyObject(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        
        if (m_Target==null){
            m_Target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        DontDestroyOnLoad(this.gameObject); 
    }
    
    private void Start()
    {
        // 카메라 범위
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    void Update() {
        if(m_Target) {
            float targetX = m_Target.position.x + m_XOffset;
            float targetY = m_Target.position.y + m_YOffset;

            if (Mathf.Abs(transform.position.x - targetX) > margin)
                targetX = Mathf.Lerp(transform.position.x, targetX, 1/m_DampTime * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - targetY) > margin)
                targetY = Mathf.Lerp(transform.position.y, targetY, m_DampTime * Time.deltaTime);
            
            transform.position = new Vector3(targetX, targetY, transform.position.z);
        }

        // 맵 범위설정 (카메라 제어)
        float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
        float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);
        
        this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
    }
}
