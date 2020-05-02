using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamaged : MonoBehaviour
{
    private bool isShaking = false;
    private float shakeAmount = .03f;
    private Vector2 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            transform.position = startPos + UnityEngine.Random.insideUnitCircle * shakeAmount;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "AttackHitBox")
        {
            isShaking = true;
            Invoke("StopShaking", .1f);
        }
    }

    void StopShaking()
    {
        isShaking = false;
        transform.position = startPos;
    }
}
