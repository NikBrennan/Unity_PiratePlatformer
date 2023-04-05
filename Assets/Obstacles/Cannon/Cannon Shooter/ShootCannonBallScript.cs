
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCannonBallScript : MonoBehaviour
{
    public float shotsPerSecond = 1;
    public float speed = 1;
    public int damage = 100;
    public GameObject cannonBallPrefab;
    public Animator Animator;
    public AudioSource cannonFire;
    SpriteRenderer SpriteRenderer { get; set; }
    private float ElapsedTime = 0;

    private int _count = 0;
    public bool dying = false;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        if(ElapsedTime >= 1 / shotsPerSecond)
        {
            Shoot();
            ElapsedTime = 0;
        }
        else
            ElapsedTime += Time.deltaTime;
    }

    void Shoot()
    {
        if (!dying)
        {
			cannonFire.Play();
			Animator.SetTrigger("fire");
		}
    }

    void ShootCannonBall()
    {
        Animator.Play("Idle");
		var cannonball = Instantiate(cannonBallPrefab, transform.position - new Vector3(0.45f, .15f), Quaternion.identity);
		var ballScript = cannonball.GetComponent<CannonBallScript>();
		ballScript.speed = speed;
		ballScript.direction = SpriteRenderer.flipX ? Vector2.right : Vector2.left;
		ballScript.damage = damage;
		cannonball.name = $"Cannon Ball - {_count++}";
	}

    void setDying()
    {
        dying = true;
    }
}
