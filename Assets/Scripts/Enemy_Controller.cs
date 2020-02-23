using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public float speed;
    public int   health = 40;
    private Animator enemy_anim;
    private Rigidbody2D enemyRigidbody;
    private Vector3 velocity = Vector3.zero;
    public Collider2D RightPlayerCheck;
    public Collider2D LeftPlayerCheck;
    public Vector2 target;
    [SerializeField] private float jumpForce = 400f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;


    void Start()
    {
        enemy_anim     = GetComponent<Animator>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Check_Orientation_of_Player();
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
        if (1 > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (2 < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void Check_Orientation_of_Player()
    {

    }
}
