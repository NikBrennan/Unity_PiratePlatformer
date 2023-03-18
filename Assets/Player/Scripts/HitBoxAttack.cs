using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxAttack : MonoBehaviour
{
    private bool _canAttack;
	public int _attackPower = 50;

	private void OnEnable()
	{
		_canAttack = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
