using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamagable
{
    public float Speed;
    public float Distance;
    private bool moveRight = true;
    public Transform GroundDetection;

    void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);

        RaycastHit2D groundhit = Physics2D.Raycast(GroundDetection.position, Vector2.down, Distance);
        if (groundhit.collider == false)
        {
            if (moveRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                moveRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moveRight = true;
            }
        }
    }

    public void TakeDamage()
    {
        PlayerController player = GetComponent<PlayerController>();
        player.Health -= 1;
        if (player.Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable PlayerHealth = collision.GetComponent<IDamagable>();
        PlayerController player = GetComponent<PlayerController>();
        if (PlayerHealth != null)
        {
            PlayerHealth.TakeDamage();
        }
    }

}
