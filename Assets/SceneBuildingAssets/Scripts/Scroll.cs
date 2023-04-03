using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float speed = 10f;
    public GameObject cloud;
    [SerializeField] float resetPos;
	// Start is called before the first frame update
	void Start()
    {
        
	}

    // Update is called once per frame
    void Update()
    {
      
        cloud.transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (cloud.transform.position.x <= resetPos)
        {
            Vector3 originalPos = new Vector3(0, cloud.transform.position.y, cloud.transform.position.z);
            cloud.transform.position = originalPos;
        }
    }
}
