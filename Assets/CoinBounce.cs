using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBounce : MonoBehaviour
{
    float maxHeight;
    float minHeight;
    const float speed = 0.2f;
    float direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        maxHeight = transform.position.y + 0.1f;
        minHeight = transform.position.y - 0.1f;
        // pick random start direction
        if (Random.Range(0, 2) == 0)
        {
            direction = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // make the coin bounce up and down in 2d space looped
        if (transform.position.y >= maxHeight)
        {
            direction = -1;
        }
        else if (transform.position.y <= minHeight)
        {
            direction = 1;
        }

        transform.position += new Vector3(0, direction * speed * Time.deltaTime, 0);
    }
}
