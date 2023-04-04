using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacleScript : MonoBehaviour
{

    public Animator Animator { get; set; }

    public BoxCollider2D boxCollider2D { get; set; }

    public int hp = 10;

    void Awake()
    {
        Animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        print($"Animator set: {Animator}");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var objTag = collision.gameObject.tag;
        if(objTag != "Player")
            return;

        var player = collision.gameObject.GetComponentInParent<Animator>(); //Player animator
        var isAttacking = player.GetBool("IsAttacking");

        if(!isAttacking)
            return;

        hp--;
        Animator.SetTrigger(hp > 0 ? "hit" : "destroyed");

        if(hp == 0)
            Destroy(this);
    }
}
