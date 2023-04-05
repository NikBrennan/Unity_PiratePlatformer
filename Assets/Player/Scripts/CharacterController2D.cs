using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] public int PlayerHealth = 100;
    [SerializeField] public float PlayerSpeed = 8f;
    // Amount of force added when the player jumps.
    [SerializeField] private float m_JumpForce = 400f;
    // How much to smooth out the movement
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;
    // Whether or not a player can steer while jumping;
    [SerializeField] private bool m_AirControl = false;
    // A mask determining what is ground to the character
    [SerializeField] private LayerMask m_WhatIsGround;
    // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_GroundCheck;

    // A position marking where to check for ceilings
    [SerializeField] private Transform m_CeilingCheck;
    // A mask determing what is a ship to the character
    [SerializeField] private LayerMask WhatIsShip;

    // Radius of the overlap circle to determine if grounded
    const float k_GroundedRadius = .2f;
    // Whether or not the player is grounded.
    [SerializeField] private bool m_Grounded;

    [SerializeField] private bool m_IsFalling;
    public bool OnShip = false;

    // Radius of the overlap circle to determine if the player can stand up
    const float k_CeilingRadius = .2f;
    public Rigidbody2D m_Rigidbody2D;
    // For determining which way the player is currently facing.
    private bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;

    public bool _isAttacking = false;

    private PlayerPersist playerPersist;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Update()
    {
        ChangeScene();
    }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        playerPersist = GetComponent<PlayerPersist>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                //Debug.Log("Grounded");
                m_Grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }

        // Reuses same code from above to do a collision check for the ship
        Collider2D[] shipCollider = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, WhatIsShip);
        for (int i = 0; i < shipCollider.Length; i++)
        {
            if (shipCollider[i].gameObject != gameObject)
            {
                Debug.Log("Grounded");
                m_Grounded = true;
                OnShip = true;

                // If win condition is met, freeze player on ship
                m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;

                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }


    }

    public bool isAttacking()
    {
        return _isAttacking;
    }

    public void updateAttacking(bool state)
    {
        _isAttacking = state;
    }

    public void Move(float move, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * PlayerSpeed, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            Debug.Log("Jump");
            // Add a vertical force to the player.
            m_Grounded = true;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    public bool isFalling()
    {
        if (m_Rigidbody2D.velocity.y < -2.0 && m_Grounded == false)
        {
            m_IsFalling = true;
            return true;
        }
        else
        {
            m_IsFalling = false;
            return false;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void ChangeScene()
    {

        if (transform.position.x >= 9.3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            gameObject.transform.position = new Vector3((gameObject.transform.position.x * -1) + 1, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (transform.position.x <= -9.3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            gameObject.transform.position = new Vector3((gameObject.transform.position.x * -1) - 1, gameObject.transform.position.y, gameObject.transform.position.z);
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