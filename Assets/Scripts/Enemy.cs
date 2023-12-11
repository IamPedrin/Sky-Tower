using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float velocidade = 3f;
    Rigidbody2D rb;
    Transform target;
    Vector2 movDir;

    public int damage = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;    
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (target)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            movDir = dir;
        }

        if (isRunning)
        {
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x), 1, 1);
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector3(movDir.x, movDir.y) * velocidade;
            
        }
    }
}
