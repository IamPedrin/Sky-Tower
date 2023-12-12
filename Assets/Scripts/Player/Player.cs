using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{


    AudioSource sliceAudioSource;

    Rigidbody2D _playerRb;
    Animator _playerAnimator;
    Animator _swordAnimator;

    Vector2 mov;
    Vector2 lastMov;

    [Header("MOVIMENTO")]
    [SerializeField] float velocidade = 5f;

    //Chama o script do Pivo da Espada
    public SwordPivot swordPivot;

    public static Player instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _swordAnimator = GetComponent<Animator>();
        sliceAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        Movement();

    }


    void Movement()
    {
        _playerRb.velocity = new Vector2(mov.x * velocidade, mov.y * velocidade);

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


        if ((moveX == 0 && moveY == 0) && (mov.x != 0 || mov.y != 0))
        {
            lastMov = mov;
        }

        mov.x = Input.GetAxisRaw("Horizontal");
        mov.y = Input.GetAxisRaw("Vertical");

        mov.Normalize();

        _playerAnimator.SetFloat("Horizontal", mov.x);
        _playerAnimator.SetFloat("Vertical", mov.y);
        _playerAnimator.SetFloat("LastHorizontal", lastMov.x);
        _playerAnimator.SetFloat("LastVertical", lastMov.y);
        _playerAnimator.SetFloat("Speed", mov.magnitude);

    }

    void OnFire(InputValue inputValue){
        if(inputValue.isPressed){
            // _swordAnimator.SetTrigger("swordAttack");
            AudioSource.PlayClipAtPoint(sliceAudioSource.clip, transform.position);
            swordPivot.Attack();
            //gameObject.GetComponent<SwordPivot>().Attack();
            print("ATACOU");

        }
    }

    // public void GameOver(){
    //     GameOver.Setup();
    // }

    public void InitPlayerDeath(){
        StartCoroutine(PlayerDeath());
    }

    IEnumerator PlayerDeath()
    {
        GetComponent<PlayerInput>().enabled = false;
        _playerRb.velocity = Vector2.zero;
        yield return new WaitForSecondsRealtime(0.3f);
        GetComponent<PlayerInput>().enabled = true;
        _playerRb.velocity = Vector2.zero;
        Physics2D.IgnoreLayerCollision(6, 8, false);
        SceneManager.LoadScene("GameOver");
    }

}
