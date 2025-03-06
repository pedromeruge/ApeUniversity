using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonkeyControl : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;

    public float speed;

    private bool dead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isRunning",true);
    }

    // Update is called once per frame
    void Update()
    {

        if (dead) return;

        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform){
            rb.linearVelocity = new Vector2(speed,0);
        }else{
            rb.linearVelocity = new Vector2(-speed,0);
        }

        if(Vector2.Distance(transform.position,currentPoint.position) < 0.5f && currentPoint == pointB.transform){
            flip();
            currentPoint = pointA.transform;
        }

        if(Vector2.Distance(transform.position,currentPoint.position) < 0.5f && currentPoint == pointA.transform){
            flip();
            currentPoint = pointB.transform;
            
        }
    }

    private void flip(){
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos(){
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    public void monkeyCaught(){
        rb.linearVelocity = Vector2.zero;
        //anim.SetBool("isDead",true);
        anim.SetBool("isRunning",false);
        dead = true;
        
    }
}
