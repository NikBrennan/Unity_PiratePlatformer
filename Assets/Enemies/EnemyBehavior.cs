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
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        moveDirection = new Vector2(xDirection, rb.velocity.y);
        animator = GetComponent<Animator>();
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

        if (!hasGround() || seeObstacle())
        {
            xDirection *= -1;
            //transform.Rotate(0, 180, 0);
            //currentSpeed = 0;            
            //animator.SetBool("isRunning", false);
            //StartCoroutine(Turn());

        }
        moveDirection.x = xDirection;
        rb.velocity = moveDirection * currentSpeed;
    }

    private bool hasGround()
    {
        RaycastHit2D hasGround = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, 1f, whatIsGround);
        return hasGround.collider != null;
    }
    private bool seeObstacle()
    {
        RaycastHit2D path = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, moveDirection, 0.1f, whatIsObstacle);
        return path.collider != null;
    }

    public void getHit(int damage)
    {
        health -= damage;
        if (health > 0)
        {
            animator.Play("hit");
        }
        else
        {
            animator.Play("die");
            Destroy(gameObject);
        }

        //more logic, work in progress

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.Play("attack");
            GameObject player = collision.gameObject;
            player.GetComponent<CharacterController2D>().getHit(attackPower);


            //attack sound can ho here
        }
    }

    IEnumerator Turn()
    {
        // animator.SetBool("isRunning", false);
        // currentSpeed = 0;

        Debug.Log("coroutine started");
        yield return new WaitForSeconds(1);

        // currentSpeed = maxSpeed;
        // animator.SetBool("isRunning", true);
        //currentSpeed = maxSpeed;
        // Debug.Log(currentSpeed + " current");
        // Debug.Log(maxSpeed + " max");
        // animator.SetBool("isRunning", true);
    }
}
