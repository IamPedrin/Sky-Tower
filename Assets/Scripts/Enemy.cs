using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float velocidade = 3f;
    Rigidbody2D rb;
    Transform target;
    SpriteRenderer spriteEnemy;
    Vector2 movDir;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteEnemy = GetComponent<SpriteRenderer>();
    }


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;    
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            movDir = dir;
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
