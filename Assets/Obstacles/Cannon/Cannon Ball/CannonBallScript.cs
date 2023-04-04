using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.zero;
    public CircleCollider2D CircleCollider { get; set; }
    public Animator Animator { get; set; }

    private void Awake()
    {
        CircleCollider = GetComponent<CircleCollider2D>();
        Animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //var player = collision.gameObject.GetComponent
            //kill/hit player
            Animator.SetTrigger("hit");
            Destroy(gameObject);
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
