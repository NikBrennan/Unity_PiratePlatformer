using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public CharacterController2D characterController;
    public int attackState;

    public AudioSource swordSlash;
    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            swordSlash.Play();
            animator.SetBool("IsJumping", false);
			animator.SetBool("IsFalling", false);
			animator.SetBool("IsAttacking", true);
            characterController.updateAttacking(true);
		}
    }

    public void AnimationFinished()
    {
		Debug.Log("Animation done");
        attackState = attackState++ >= 3 ? attackState = 1 : attackState++;
        animator.SetInteger("AttackStyle", attackState);
		animator.SetBool("IsAttacking", false);
		characterController.updateAttacking(false);
	}
}
