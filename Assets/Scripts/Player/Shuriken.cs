using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Shuriken : MonoBehaviour
{
    [SerializeField] 
    private float speed;
    private Rigidbody2D myRigid;
    private Vector2 direction;
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward,200*Time.deltaTime);
    }

    void FixedUpdate()
    {
        myRigid.velocity = direction * speed;
        
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
