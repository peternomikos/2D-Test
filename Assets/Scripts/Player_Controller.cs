using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Controller : MonoBehaviour
{
    public bool m_Grounded;
    public bool isFacedRight;
    public Animator animator;

    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private float hitForce = -300f;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_EnemyCheck;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask enemy;
    [SerializeField] private int health;

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    private Rigidbody2D my_rigidbody;
    private Vector3 velocity = Vector3.zero;
    private bool alive;
    const float k_GroundedRadius = .1f;
    const float k_EnemyRadius = .1f;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        my_rigidbody = GetComponent<Rigidbody2D>();
        isFacedRight = true;
        alive = true;
        health = 10;
        if (OnLandEvent == null) OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        if (IsAlive())
        {
            CheckGround();

            CheckHit();
        }
        else
        {
            animator.SetBool("Hit", false);
        }
    }

    public void Move(float move, bool jump)
    {
        if (m_Grounded)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, my_rigidbody.velocity.y);
            my_rigidbody.velocity = Vector3.SmoothDamp(my_rigidbody.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

            if (move > 0)
            {
                isFacedRight = true;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (move < 0)
            {
                isFacedRight = false;
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if (m_Grounded && jump)
        {
            m_Grounded = false;
            Debug.Log("jump");
            my_rigidbody.AddForce(new Vector2(0, jumpForce));
        }
    }

    private bool IsAlive()
    {
        if (health > 0)
        {
            return true;
        }
        else
        {
            animator.SetBool("IsAlive", false);
            return false;
        }
    }
    
    private void CheckGround()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, ground);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;

                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }

    private void CheckHit()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(m_EnemyCheck.position, k_EnemyRadius, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject != gameObject)
            {
                my_rigidbody.AddForce(new Vector2(hitForce, 0));
                animator.SetBool("Hit", true);
                health--;
            }
        }
    }

    private void KillPlayer()
    {
        animator.SetBool("IsAlive", true);
    }
}