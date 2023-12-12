using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float velocidade = 3f;
    Rigidbody2D rb;
    Transform target;
    SpriteRenderer sprite;
    Vector2 movDir;

    PointsUI ponto;

    public int damage = 1;

    bool isTakingDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ponto = FindObjectOfType<PointsUI>();
        sprite = GetComponent<SpriteRenderer>();
    }


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;    
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (target && !isTakingDamage)
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
            StartCoroutine(Knockback());
            gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }

    IEnumerator Knockback()
    {
        isTakingDamage = true;
        rb.velocity = new Vector2(-transform.localScale.x * 6f, -transform.localScale.y * 8f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
        isTakingDamage = false;
    }

    public void Pontuacao(){
        ponto.AtualizaPontuacao();
    }


}
