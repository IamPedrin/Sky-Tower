using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //AudioSource damageTaken;
    AudioSource death;



    [SerializeField] float velocidade = 3f;
    Rigidbody2D rb;
    Transform target;
    Vector2 movDir;

    PointsUI ponto;

    public int damage = 1;

    bool isTakingDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ponto = FindObjectOfType<PointsUI>();
    }


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;    
        //damageTaken = GetComponent<AudioSource>();
        death = GetComponent<AudioSource>();
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
            //AudioSource.PlayClipAtPoint(damageTaken.clip, transform.position);
            gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }

    public void Pontuacao(){
        AudioSource.PlayClipAtPoint(death.clip, transform.position);
        ponto.AtualizaPontuacao();
    }

}
