using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPersist : MonoBehaviour
{
    public static PlayerPersist Instance;
	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Update()
	{
		if (Input.GetAxisRaw("Horizontal") != 0)
		{
			print("asd");
		} 
	}
}
