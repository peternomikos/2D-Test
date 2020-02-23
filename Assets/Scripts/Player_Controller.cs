using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private bool m_Grounded;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private LayerMask ground;
    public bool isFacedRight = true;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    private Rigidbody2D my_rigidbody;
    private Vector3 velocity = Vector3.zero;

    const float k_GroundedRadius = .2f;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        my_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, ground);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;

                if(!wasGrounded)
                {
                    OnLandEvent.Invoke(); 
                }
            }
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
            my_rigidbody.AddForce(new Vector2(0, jumpForce));
        }
    }
}