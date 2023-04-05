using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    public float speed;
    public Vector2 direction = Vector2.zero;
    public CircleCollider2D CircleCollider { get; set; }
    public Animator Animator { get; set; }
    public  int damage;
    public AudioSource cannonFire;
    private void Awake()
    {
        CircleCollider = GetComponent<CircleCollider2D>();
        Animator = GetComponent<Animator>();
        Animator.ResetTrigger("hit");
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
        GetComponent<CircleCollider2D>().enabled = false;
            var player = collision.gameObject.GetComponent<CharacterController2D>();
            player.PlayerHealth -= damage;
           
            StartCoroutine(Explode());
          
        }
     
    }

    IEnumerator Explode()
    {
        cannonFire.Play();
        Animator.SetTrigger("hit");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * 5f * Time.deltaTime);
    }
}
