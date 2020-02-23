using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Player_Controller controller;

    public float speed = 20f;

    public float horizontalMove = 0f;
    public bool jump = false;
   
    public Animator animator;

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            this.animator.SetBool("Jump", true);
        }        
    }

    public void OnLand()
    {
        animator.SetBool("Jump", false);
        Debug.Log("Landed");
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime, jump);
        jump = false;
    }
}