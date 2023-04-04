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
		var sword = collision.gameObject;
		if (sword.tag != "Sword")
			return;

		int swordDmg = sword.GetComponent<HitBoxAttack>()._attackPower; 
		hp = hp - swordDmg;
		Animator.SetTrigger(hp > 0 ? "hit" : "destroyed");
	}

	void DestroyObject()
	{
		Destroy(gameObject);
	}
}
