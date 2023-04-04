
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCannonBallScript : MonoBehaviour
{
    public int shotsPerSecond = 1;
    public float speed = 1;
    public GameObject cannonBallPrefab;
    public int hp = 10;
    Animator Animator { get; set; }
    SpriteRenderer SpriteRenderer { get;  set; }

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();

       var end = StartCoroutine(Shoot());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Shoot()
    {
        while(hp > 0)
        {
            Animator.SetTrigger("fire");
            var cannonball = Instantiate(cannonBallPrefab,transform.position,Quaternion.identity);
            var ballScript = cannonball.GetComponent<CannonBallScript>();
            ballScript.speed = speed;
            ballScript.direction = SpriteRenderer.flipX ? Vector2.right : Vector2.left;
            print("shot");
            yield return new WaitForSeconds(shotsPerSecond);
        }
    }
}
