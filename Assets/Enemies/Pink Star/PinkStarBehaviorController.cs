using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkStarBehaviorController : MonoBehaviour
{
    //mask to determine ground
    [SerializeField] private int health = 150;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsObstacle;
    private BoxCollider2D boxCollider;

    public Rigidbody2D rb;

    private int xDirection = -1;
    Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        moveDirection = new Vector2(xDirection, rb.velocity.y);
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
        }
        moveDirection.x = xDirection;
        rb.velocity = moveDirection * speed;
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
}
