using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D _playerRb;
    Animator _playerAnimator;
    Animator _swordAnimator;

    Vector2 mov;
    Vector2 lastMov;

    [SerializeField] float velocidade = 5f;


    //[SerializeField] GameObject swordPivot;

    //Chama o script do Pivo da Espada
    public SwordPivot swordPivot;



    //Sword Start
    // public Vector2 PointerInput => pointerInput;

    // [SerializeField] 
    // private InputActionReference pointerPosition;

    //Sword End


    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _swordAnimator = GetComponent<Animator>();

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
            swordPivot.Attack();
            //gameObject.GetComponent<SwordPivot>().Attack();
            print("ATACOU");

        }
    }

    // private Vector2 GetPointerInput(){
    //     Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
    //     mousePos.z = Camera.main.nearClipPlane;
    //     return Camera.main.ScreenToWorldPoint(mousePos);
    // }


/*    void OnMove(InputValue input)
    {
        mov = input.Get<Vector2>();
    }*/
}
