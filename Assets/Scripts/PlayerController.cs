using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamagable
{
    //CamelCase public variable adlari method adlari
    //pascalCase methodun icindeki variable adlari 
    //private field yazanda _pascalCase olur

    [SerializeField]
    float _speed;
    [SerializeField]
    float _jumpforce;
    [SerializeField]
    bool _isGrounded = false;

    Rigidbody2D _rb2d;

    [SerializeField]
    int _jumpCounter = 2;

    public int Health;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        PlayerJump();
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector2(direction, 0) * _speed * Time.deltaTime);
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded || Input.GetKeyDown(KeyCode.Space) && _jumpCounter > 0)
        {
            _rb2d.velocity = Vector2.up * _jumpforce;
            _jumpCounter -= 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            _jumpCounter = 2;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }


    public void TakeDamage()
    {
        Health -= 1;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
