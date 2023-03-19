using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloud1;
    public GameObject cloud2;
    public GameObject cloud3;
	public float spawnRate = 3;
	private float timer = 0;
	public float speed = 0.5f;
	// Start is called before the first frame update
	void Start()
    {
		spawnCloud();
    }

    // Update is called once per frame
    void Update()
    {
		// Spawn a cloud every spawnRate seconds
		if (timer < spawnRate)
		{
			timer = timer + Time.deltaTime;
		}
		else
		{
			spawnCloud();
			timer = 0;
		}
	}

	void spawnCloud()
	{
		// Choose a random cloud to spawn
		int cloud = Random.Range(1, 3);
		switch(cloud)
		{
			case 1:
				InstantiateCloud(cloud1);
				break;
			case 2:
				InstantiateCloud(cloud2);
				break;
			case 3:
				InstantiateCloud(cloud3);
				break;
		}
	}

	void InstantiateCloud(GameObject cloud)
	{
		// Spawn a cloud between a y range
		Instantiate(cloud, new Vector3(13, Random.Range(0.5f, 3), transform.position.z), transform.rotation);
	}
}
