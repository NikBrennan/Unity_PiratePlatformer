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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var collider = collision.gameObject;
		if (collider.tag != "Sword")
			return;

		hp--;
		Animator.SetTrigger(hp > 0 ? "hit" : "destroyed");

		if (hp <= 0)
			Destroy(gameObject);
	}

}
