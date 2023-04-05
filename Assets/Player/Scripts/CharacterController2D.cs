using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
    public AudioSource walkingSound;
    public AudioSource JumpingSound;

    [SerializeField] public int PlayerHealth = 100;
    [SerializeField] public float PlayerSpeed = 8f;
    [SerializeField] private float JumpForce = 600f;
    private float MovementSmoothing = .05f;
    // Air strafing
    [SerializeField] private bool AirControl = true;


    // The layer the ground/floor is a part of
    [SerializeField] private LayerMask WhatIsGround;
    // The layer the ship is a part of
    [SerializeField] private LayerMask WhatIsShip;
    // At players feet
    [SerializeField] private Transform GroundCheck;
    // Radius of the overlap circle to determine if grounded
    const float GroundedRadius = .2f;
    [SerializeField] private bool Grounded;

    private bool IsFalling;
    public bool OnShip = false;

    public Rigidbody2D Rigidbody2D;
    private bool FacingRight = true;
    private Vector3 Velocity = Vector3.zero;

    public bool _isAttacking = false;

    public UnityEvent OnLandEvent;

    private void Update()
    {
        ChangeScene();
    }

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }

    private void FixedUpdate()
    {
        bool wasGrounded = Grounded;
        Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Debug.Log("Grounded");
                Grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }

        // Reuses same code from above to do a collision check for the ship
        Collider2D[] shipCollider = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsShip);
        for (int i = 0; i < shipCollider.Length; i++)
        {
            if (shipCollider[i].gameObject != gameObject)
            {
                Debug.Log("Grounded");
                Grounded = true;
                OnShip = true;

                // If win condition is met, freeze player on ship
                Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;

                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }


    }

    // Returns boolean state of whether player is attacking or not
    public bool isAttacking()
    {
        return _isAttacking;
    }

    // Updates the attackstate
    public void updateAttacking(bool state)
    {
        _isAttacking = state;
    }

    public void Move(float move, bool jump)
    {
        // Move player when grounded or if air strafing is enabled
        if (Grounded || AirControl)
        {
            Vector3 targetVelocity = new Vector2(move * PlayerSpeed, Rigidbody2D.velocity.y);
            Rigidbody2D.velocity = Vector3.SmoothDamp(Rigidbody2D.velocity, targetVelocity, ref Velocity, MovementSmoothing);

            if (move > 0 && !FacingRight)
            {
                Flip();
            }
            else if (move < 0 && FacingRight)
            {
                Flip();
            }
        }

        // If the player is jumping
        if (Grounded && jump)
        {
            // Player is still grounded at this point before jumpforce is added
            Grounded = true;
            Rigidbody2D.AddForce(new Vector2(0f, JumpForce));
        }
    }

    // Determines if the player is in a state of falling
    public bool isFalling()
    {
        if (Rigidbody2D.velocity.y < -2.0 && Grounded == false)
        {
            IsFalling = true;
            return true;
        }
        else
        {
            IsFalling = false;
            return false;
        }
    }

    private void Flip()
    {
        FacingRight = !FacingRight;

        // Invert the x value of the scale so player flips around
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void ChangeScene()
    {

        if (transform.position.x >= 9.3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            gameObject.transform.position = new Vector3((gameObject.transform.position.x * -1) + 1,
                gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (transform.position.x <= -9.3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            gameObject.transform.position = new Vector3((gameObject.transform.position.x * -1) - 1,
                gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }

    public void getHit(int damage)
    {
        PlayerHealth -= damage;
        //play animation, sound, and other effects
        //example can be found in enemies/EnemyBehavior.cs
        // remove this comment when done
    }
}