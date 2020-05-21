using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
  
    [SerializeField] 
    private Transform ShurikenPos;
    
    [SerializeField]
    private GameObject ShurikenPrefab;
    
    [SerializeField] 
    protected float movementSpeed;
    protected bool facingRight;
    //protected bool monsFacingRight;
    public bool Attack { get; set; }
    
    public Animator MyAnim { get; private set; }
    public virtual void Start()
    {
        facingRight = true;
        //monsFacingRight = false;
        MyAnim = GetComponent<Animator>();
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

    public virtual void ThrowObject()
    {
        if (facingRight)
        {
            GameObject temp = (GameObject)Instantiate(ShurikenPrefab, ShurikenPos.position, Quaternion.identity);
            temp.GetComponent<Shuriken>().Initialize(Vector2.right);
         
        }
        else
        {
            GameObject temp = (GameObject) Instantiate(ShurikenPrefab, ShurikenPos.position, Quaternion.identity);
            temp.GetComponent<Shuriken>().Initialize(Vector2.left);
        }
    }
    
    public virtual void ThrowObject2()
    {
        if (!facingRight)
        {
            GameObject temp = (GameObject)Instantiate(ShurikenPrefab, ShurikenPos.position, Quaternion.identity);
            temp.GetComponent<Shuriken>().Initialize(Vector2.right);
         
        }
        else
        {
            GameObject temp = (GameObject) Instantiate(ShurikenPrefab, ShurikenPos.position, Quaternion.identity);
            temp.GetComponent<Shuriken>().Initialize(Vector2.left);
        }
    }
    

    
    
}
