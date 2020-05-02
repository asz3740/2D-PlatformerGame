using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField] 
    private Transform castPoint;
    [SerializeField] 
    private float agroRange;
    [SerializeField] 
    private float moveSpeed;
    private Rigidbody2D myRigid;
    private Animator anim;
    bool isFacingLeft;
    private bool isAgro = false;
    private bool isSearching = false;
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer(agroRange))
        {
            isAgro = true;
        }
        else
        {
            if (isAgro)
            {
                if (!isSearching)
                {
                    isSearching = true;
                    Invoke("StopChasingPlayer",5);
                }
            }
        }

        if (isAgro)
        {
            ChasePlayer();
        }
    }

    private bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;

        if (isFacingLeft)
        {
            castDist = -distance;
        }

        Vector2 endPos = castPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }
        return val;
    }
    

    private void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            myRigid.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
            isFacingLeft = false;
        }
        else
        {
            myRigid.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
            isFacingLeft = true;
        }
        anim.Play("Monster_Walk");
    }
    
    private void StopChasingPlayer()
    {
        isAgro = false;
        isSearching = false;
        myRigid.velocity = new Vector2(0, 0);
        anim.Play("Monster_Idle");
    }

  
}
