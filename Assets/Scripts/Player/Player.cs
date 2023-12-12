using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{


    AudioSource sliceAudioSource;

    Rigidbody2D _playerRb;
    Animator _playerAnimator;
    Animator _swordAnimator;
    TrailRenderer playerTr;

    Vector2 mov;
    Vector2 lastMov;

    [Header("MOVIMENTO")]
    [SerializeField] float velocidade = 5f;

    //Chama o script do Pivo da Espada
    public SwordPivot swordPivot;

    [Header("DASH")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] float dashCooldown;


    bool isDashing = false;
    bool canDash = true;

    public static Player instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _swordAnimator = GetComponent<Animator>();
        playerTr = GetComponent<TrailRenderer>();

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

    IEnumerator PlayerDash()
    {
        canDash = false;
        isDashing = true;
        _playerRb.velocity = new Vector2(mov.x * dashSpeed, mov.y * dashSpeed);
        playerTr.emitting = true;
        Physics2D.IgnoreLayerCollision(6, 8, true);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        playerTr.emitting = false;
        Physics2D.IgnoreLayerCollision(6, 8, false);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    void OnDash(InputValue inputValue)
    {
        if (inputValue.isPressed && canDash)
        {
            StartCoroutine(PlayerDash());
        }
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

    IEnumerator PlayerDeath(){
        yield return new WaitForSecondsRealtime(0.3f);
        // isDead = true;
        GetComponent<PlayerInput>().enabled = false;
        _playerRb.velocity = Vector2.zero;
        Physics2D.IgnoreLayerCollision(7,9,true);
        SceneManager.LoadScene("GameOver");
    }

}
