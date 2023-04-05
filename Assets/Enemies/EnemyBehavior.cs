using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //mask to determine ground
    [SerializeField] private int attackPower = 35;
    [SerializeField] private int health = 150;
    [SerializeField] private float maxSpeed;
    private float currentSpeed;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsObstacle;
    private BoxCollider2D boxCollider;

    public Rigidbody2D rb;

    private int xDirection = 1;
    Vector2 moveDirection;
    private Animator animator;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        moveDirection = new Vector2(xDirection, rb.velocity.y);
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isRunning", true);
        currentSpeed = maxSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if enemy has no ground in front of them, or obstacle, they will change movement direction
        if (!hasGround() || seeObstacle())
        {
            //moveDirection.x *= -1;
            transform.Rotate(0, 180, 0);
        }

        rb.velocity = moveDirection * currentSpeed;
    }

    //function to track if enemy has ground to walk
    private bool hasGround()
    {
        RaycastHit2D hasGround = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, 1f, whatIsGround);
        return hasGround.collider != null;
    }
    //function to track if enemy is near obstacle
    private bool seeObstacle()
    {
        RaycastHit2D path = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, whatIsObstacle);
        return path.collider != null;
    }

    //Here enemy receives a hit from player's sword
    public void getHit(int damage)
    {
        health -= damage;
        if (health > 0)
        {
            animator.Play("hit");
        }
        else
        {
            StartCoroutine(Die());
        }

        //more logic, work in progress

    }

    // in this function enemy will attack player if they collide
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.Play("attack");
            GameObject player = collision.gameObject;
            player.GetComponent<CharacterController2D>().getHit(attackPower);
        }
    }


    // process to destroy enemy on death
    IEnumerator Die()
    {
        attackPower = 0; //make shure dead one can't hit player
        animator.Play("die");
        currentSpeed = 0;
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }

    IEnumerator Turn()
    {
        currentSpeed = 0.5f;
        yield return new WaitForSeconds(1f);
        currentSpeed = maxSpeed;
    }
}
