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

    Vector2 vector2; 



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

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            //Inicia o processo de dar dano no player
            //collision.gameObject.GetComponent<Player>().TomarDano();
            print("TOMOU DANO");
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Sword")){
            print("ARROBA PONTO COM");
            Destroy();
        }
    }

    public void Destroy(){
        //GameObject poofClone = Instantiate(poofVFXPrefab,transform.position,Quaternion.identity);
        //Destroy(poofClone,1.5f);
        Destroy(gameObject);
    }
}
