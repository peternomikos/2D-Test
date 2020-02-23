using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public bool attack = false;
    public float fireRate = 1.0f;
    public Collider2D AttackTriggerLeft;
    public Collider2D AttackTriggerRight;
    public Player_Controller controller;
    private Animator animator;

    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        AttackTriggerLeft.enabled = false;
        AttackTriggerRight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            animator.SetBool("Attack", true);
            if (controller.isFacedRight)
            {
                AttackTriggerRight.enabled = true;
            }
            else
            {
                AttackTriggerLeft.enabled = true;
            }
        }
    }

    private void AttackEnd()
    {
        animator.SetBool("Attack", false);
        if (controller.isFacedRight)
        {
            AttackTriggerRight.enabled = false;
        }
        else
        {
            AttackTriggerLeft.enabled = false;
        }
    }
}
