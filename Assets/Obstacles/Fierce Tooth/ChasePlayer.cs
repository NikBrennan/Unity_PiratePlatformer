using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    /// <summary>
    /// Modifies the circle collider
    /// </summary>
    public int speed = 10;
    public int jump = 15;
    public int damage = int.MaxValue;
    // A mask determining what is ground to the character; thank you Nik :)
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform Feet;
    [SerializeField] private BoxCollider2D Head;


    CircleCollider2D circleCollider;
    Animator Animator { get; set; }
    SpriteRenderer SpriteRenderer { get; set; }
    Rigidbody2D Rigidbody { get; set; }
    private bool isGrounded = true;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();

        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody = GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Animator.SetTrigger("anticipate");
        if(collision.tag == "Player")
        {
            var player = collision.gameObject.GetComponent<CharacterController2D>();
            player.PlayerHealth -= damage;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag != "Player")
            return;

        Animator.SetInteger("speed", speed);

        //move towards the player
        var player = collision.gameObject.transform.position;


        if(player.x > transform.position.x)
        {
            transform.Translate(Vector2.ClampMagnitude(speed * Time.deltaTime * Vector2.right, speed));
            SpriteRenderer.flipX = true;
        }
        else
        {
            transform.Translate(Vector2.ClampMagnitude(speed * Time.deltaTime * Vector2.left, speed));
            SpriteRenderer.flipX = false;
        }
        if(player.y > transform.position.y + jump && isGrounded)
        {
            isGrounded = false;
            Animator.SetTrigger("jump");
            Rigidbody.AddForce(new(0, jump));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Animator.SetInteger("speed", 0);
        Animator.SetBool("isFalling", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Feet.position, .2f, m_WhatIsGround);
        for(int i = 0; i < colliders.Length; i++)
            if(colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                Animator.SetBool("isFalling", false);
            }

        //check if player jumped on enemy head
        GameObject collisionObj = collision.gameObject;
        if(collisionObj.CompareTag("Player") && Head.IsTouching(collisionObj.GetComponent<CircleCollider2D>()))
            //kill the enemy
            StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        Destroy(GetComponent<CircleCollider2D>());

        Animator.SetTrigger("hit");
        Animator.SetBool("isDead", true);
        //reset other animations
        Animator.ResetTrigger("jump");
        Animator.ResetTrigger("anticipate");
        Animator.SetInteger("speed", 0);
        Animator.SetBool("isFalling", false);

        yield return new WaitForSeconds(1);

        Destroy(Rigidbody);
        Destroy(Head);
        Destroy(Feet.gameObject);
        Destroy(GetComponent<BoxCollider2D>());

    }

    // Update is called once per frame
    void Update()
    {
        if(!isGrounded)
            Animator.SetBool("isFalling", true);
        else
            Animator.SetBool("isFalling", false);

    }
}
