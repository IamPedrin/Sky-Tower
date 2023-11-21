using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D _playerRb;
    Animator _playerAnimator;

    Vector2 mov;

    [SerializeField] float velocidade = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //_playerRb.velocity = new Vector2(mov.x * velocidade, mov.y * velocidade);
        _playerRb.MovePosition(_playerRb.position + mov * velocidade * Time.fixedDeltaTime);
        _playerAnimator.SetFloat("Horizontal", mov.x);
        _playerAnimator.SetFloat("Vertical", mov.y);
        _playerAnimator.SetFloat("Speed", mov.sqrMagnitude);
    }
    
    void OnMove(InputValue input)
    {
        mov = input.Get<Vector2>();
    }
}
