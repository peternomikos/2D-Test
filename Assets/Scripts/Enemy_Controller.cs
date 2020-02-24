using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public float speed;
    
    private Animator enemy_anim;
    private Rigidbody2D enemyRigidbody;
    private bool isFacedRight;
    private Vector3 targetVelocity;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private int health = 40;
    [SerializeField] private float jumpForce = 400f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;


    void Start()
    {
        enemy_anim     = GetComponent<Animator>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        isFacedRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        DeathCheck();
    }

    public void Damage(int damage)
    {
        health -= damage;
    }

    public void OnEnemyDeath()
    {
        Destroy(gameObject);
    }

    public void IsDead()
    {
        enemyRigidbody.AddForce(new Vector2(400f, jumpForce));
    }

    private void DeathCheck()
    {
        if (health <= 0)
        {
            enemy_anim.SetBool("Death", true);
        }
    }

    private void Move()
    {
        if (isFacedRight)
        {
            targetVelocity = new Vector2(speed * 10f, enemyRigidbody.velocity.y);
        }
        else
        {
            targetVelocity = new Vector2(-speed * 10f, enemyRigidbody.velocity.y);
        }

        enemyRigidbody.velocity = Vector3.SmoothDamp(enemyRigidbody.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

        enemy_anim.SetBool("Moving", true);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Edges") || col.CompareTag("Enemy"))
        {
            isFacedRight = !isFacedRight;
            GetComponent<SpriteRenderer>().flipX = isFacedRight;
        }
    }
}
