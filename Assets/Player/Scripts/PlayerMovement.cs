using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D characterController;
    public Animator animator;
    float horizontalMove = 0;
    float runSpeed = 40;
    bool isJumping = false;
    public AudioSource walkingSound;
    public AudioSource JumpingSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Math.Abs(horizontalMove));

        if (horizontalMove > 0 && horizontalMove != null)
            walkingSound.Play();

        if (Input.GetButtonDown("Jump"))
        {
            JumpingSound.Play();
            Debug.Log("Jump");
			isJumping = true;
            animator.SetBool("IsJumping", true);
		}
        if (characterController.isFalling() && !characterController.isAttacking())
        {
            Debug.Log("Falling");
            animator.SetBool("IsFalling", true);
			animator.SetBool("IsJumping", false);
		}
    }
    public void onLanding()
    {
		Debug.Log("Landed");
		animator.SetBool("IsJumping", false);
		animator.SetBool("IsFalling", false);
	}

	private void FixedUpdate()
	{
        characterController.Move(horizontalMove * Time.fixedDeltaTime, isJumping);	
        isJumping = false;
	}
}
