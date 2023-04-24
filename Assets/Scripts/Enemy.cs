using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : KillPlayer
{
    [Tooltip("The points between which the platform moves")]
    [SerializeField] Transform startPoint = null, endPoint = null;

    [SerializeField] float speed = 1f;
    [SerializeField] float distEPS = 0.1f;
    [SerializeField] float threshold = 1f;
    
    private bool moveFromStartToEnd = true;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (moveFromStartToEnd)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, endPoint.position, speed * Time.fixedDeltaTime));
        }
        else
        {  // move from end to start
            rb.MovePosition(Vector2.MoveTowards(transform.position, startPoint.position, speed * Time.fixedDeltaTime));
        }

        if (Mathf.Abs(transform.position.x - startPoint.position.x) < distEPS)
        {
            moveFromStartToEnd = true;
        }
        else if (Mathf.Abs(transform.position.x - endPoint.position.x) < distEPS)
        {
            moveFromStartToEnd = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.tag == triggeringTag && other.relativeVelocity.y < -1*threshold)
            Destroy(this.gameObject);
        else
            base.OnTriggerEnter2D(other.gameObject.GetComponent<Collider2D>());
    }
}
