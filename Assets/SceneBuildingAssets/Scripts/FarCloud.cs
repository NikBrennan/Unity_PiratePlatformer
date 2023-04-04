using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarCloud : MonoBehaviour
{
    public float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Scroll the cloud from right to left
		transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Destroy the cloud once out of view
        if (transform.position.x <= -13)
        {
            Destroy(gameObject);
        }
	}
}
