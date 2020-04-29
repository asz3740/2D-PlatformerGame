using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float timeOffset;

    [SerializeField] private Vector2 posOffset;

    private Vector3 velocity;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // 카메라 현재 위치
        Vector3 startPos = transform.position;
        // 플레이어 현재 위치
        Vector3 endPos = player.transform.position;

        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;

        transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);
    }
}
