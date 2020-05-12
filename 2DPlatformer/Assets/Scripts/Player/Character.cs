using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Animator myAnim;
    [SerializeField] 
    protected float movementSpeed;
    protected bool facingRight;
    public bool Attack { get; set; }
    public virtual void Start()
    {
        facingRight = true;
        
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }
}
