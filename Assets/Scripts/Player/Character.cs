using System;
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
    private Transform MeleeAttackPos;
    [SerializeField]
    private GameObject MeleeAttackPrefab;

    [SerializeField]
    protected int health;
    [SerializeField]
    protected float movementSpeed;
    protected bool facingRight;
    //[SerializeField]
    //private CapsuleCollider2D swordCollider;
    [SerializeField] 
    private List<string> damageSources;
    
    public abstract bool IsDead { get; }
    public bool Attack { get; set; }
    public bool TakingDamage { get; set; }
    
    public Animator MyAnim { get; private set; }
    // public CapsuleCollider2D SwordCollider
    // {
    //     get { return swordCollider; }
    // }

    
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
    
    public abstract IEnumerator TakeDamage();

    public abstract void Death();
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

    public void MeleeAttack()
    {
        Instantiate(MeleeAttackPrefab, MeleeAttackPos.position, Quaternion.identity);
        //SwordCollider.enabled = true;
    }

    //private int count;
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //count++;
        if (damageSources.Contains(other.tag))
        {
            print("호출");
            //print("in" + count);
            StartCoroutine(TakeDamage());
        }
    }
    
}
